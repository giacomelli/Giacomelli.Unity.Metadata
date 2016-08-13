using System;
using System.Linq;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace Giacomelli.Unity.Metadata.Domain.UnitTests
{
    [TestFixture]
    public class PrefabMetadataServiceTest
    {
        #region Fields
        private IScriptMetadataService m_scriptMetadataService;
        private IPrefabMetadataReader m_prefabMetadataReader;
        private IPrefabMetadataWriter m_prefabMetadataWriter;
        private IFileSystem m_fileSystem;
        private ITypeService m_typeService;
        private PrefabMetadataService m_target;
        #endregion

        [SetUp]
        public void Initialize()
        {
            m_scriptMetadataService = MockRepository.GenerateMock<IScriptMetadataService>();
            m_prefabMetadataReader = MockRepository.GenerateMock<IPrefabMetadataReader>();
            m_prefabMetadataWriter = MockRepository.GenerateMock<IPrefabMetadataWriter>();
            m_fileSystem = MockRepository.GenerateMock<IFileSystem>();
            m_typeService = MockRepository.GenerateMock<ITypeService>();

            m_target = new PrefabMetadataService(m_prefabMetadataReader, m_prefabMetadataWriter, m_fileSystem, m_typeService);
        }

        [Test]
        public void GetPrefabs_NoArgs_AllPrefabs()
        {
            m_fileSystem.Expect(f => f.GetFiles(String.Empty, "*.prefab", true)).Return(new string[]
            {
                "Assets/Prefabs/Ships/Crusader.prefab",
                "Assets/Prefabs/Bonus/Ghost.prefab",
            });
            m_fileSystem.Expect(f => f.GetFileNameWithoutExtension("Assets/Prefabs/Ships/Crusader.prefab")).Return("Crusader");
            m_fileSystem.Expect(f => f.GetFileNameWithoutExtension("Assets/Prefabs/Bonus/Ghost.prefab")).Return("Ghost");
            m_prefabMetadataReader.Expect(r => r.Read("Assets/Prefabs/Ships/Crusader.prefab")).Return(new PrefabMetadata());
            m_prefabMetadataReader.Expect(r => r.Read("Assets/Prefabs/Bonus/Ghost.prefab")).Return(new PrefabMetadata());

            var actual = m_target.GetPrefabs().ToArray();
            Assert.AreEqual(2, actual.Length);
            var actualPrefab = actual[0];
            Assert.AreEqual("Crusader", actualPrefab.Name);
            Assert.AreEqual("Assets/Prefabs/Ships/Crusader.prefab", actualPrefab.Path);

            actualPrefab = actual[1];
            Assert.AreEqual("Ghost", actualPrefab.Name);
            Assert.AreEqual("Assets/Prefabs/Bonus/Ghost.prefab", actualPrefab.Path);

            m_fileSystem.VerifyAllExpectations();
            m_prefabMetadataReader.VerifyAllExpectations();
        }

        [Test]
        public void FixMissingMonobehaviours_Prefab_PrefabMetadataUpdated()
        {
            var prefab = new PrefabMetadata();
            prefab.Path = "Prefab1.prefab";

            var missingMonoBehaviours = new MonoBehaviourMetadata[]
            {
                new MonoBehaviourMetadata { Script = new ScriptMetadata { FileId = 1, FullName = "System.String" } },
                new MonoBehaviourMetadata { Script = new ScriptMetadata { FileId = 3, FullName = "System.DateTime" } },
            };

            m_prefabMetadataWriter.Expect(w => w.ReplaceGuid(missingMonoBehaviours.First().Script, "1.2", "Prefab1.prefab"));
            m_prefabMetadataWriter.Expect(w => w.ReplaceGuid(missingMonoBehaviours.Last().Script, "3.2", "Prefab1.prefab"));

            m_typeService.Expect(t => t.GetTypeByName("System.String")).Return(typeof(string));
            m_typeService.Expect(t => t.GetTypeByName("System.DateTime")).Return(typeof(DateTime));
            m_typeService.Expect(t => t.GetGuid(typeof(string))).Return("1.2");
            m_typeService.Expect(t => t.GetGuid(typeof(DateTime))).Return("3.2");

            m_target.FixMissingMonobehaviours(prefab, missingMonoBehaviours);

            m_prefabMetadataWriter.VerifyAllExpectations();
        }
    }
}
