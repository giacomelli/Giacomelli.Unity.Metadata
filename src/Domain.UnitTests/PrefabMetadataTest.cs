using System;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;

namespace Giacomelli.Unity.Metadata.Domain.UnitTests
{
    [TestFixture]
    public class PrefabMetadataTest
    {
        [Test]
        public void GetScriptByFileId_NoScriptWithFileId_Null()
        {
            var target = new PrefabMetadata();
            target.MonoBehaviours.Add(new MonoBehaviourMetadata { Script = new ScriptMetadata { FileId = 1 } });
            var actual = target.GetScriptByFileId(2);
            Assert.IsNull(actual);
        }

        [Test]
        public void GetScriptByFileId_ScriptWithFileId_Script()
        {
            var target = new PrefabMetadata();
            target.MonoBehaviours.Add(new MonoBehaviourMetadata { Script = new ScriptMetadata { FileId = 1 } });
            var actual = target.GetScriptByFileId(1);
            Assert.AreEqual(1, actual.FileId);
        }

        [Test]
        public void GetMissingMonoBehaviours_PrefabNotFindOnAssetRepository_Exception()
        {
            var target = new PrefabMetadata
            {
                Path = "Test.prefab"
            };
            var typeService = MockRepository.GenerateMock<ITypeService>();
            var assetRepository = MockRepository.GenerateMock<IAssetRepository>();

            Assert.Catch<InvalidOperationException>(delegate
            {
                target.GetMissingMonoBehaviours(assetRepository, typeService);
            });
        }

        [Test]
        public void GetMissingMonoBehaviours_MissingMonoBehaviours_MonoBehaviours()
        {
            var target = new PrefabMetadata
            {
                Path = "Test.prefab"
            };

            target.MonoBehaviours.Add(new MonoBehaviourMetadata { Script = new ScriptMetadata { FileId = 1, FullName = "System.String" } });
            target.MonoBehaviours.Add(new MonoBehaviourMetadata { Script = new ScriptMetadata { FileId = 2, FullName = "System.Int32" } });

            var typeService = MockRepository.GenerateMock<ITypeService>();
            typeService.Expect(t => t.GetTypeByName("System.String")).Return(typeof(string));
            typeService.Expect(t => t.GetTypeByName("System.Int32")).Return(typeof(int));

            var assetRepository = MockRepository.GenerateMock<IAssetRepository>();

            var gameObject = MockRepository.GenerateMock<IGameObject>();
            gameObject.Expect(g => g.HasComponent(typeof(string))).Return(true);
            gameObject.Expect(g => g.HasComponent(typeof(int))).Return(false);
            assetRepository.Expect(a => a.GetGameObject("Test.prefab")).Return(gameObject);

            var actual = target.GetMissingMonoBehaviours(assetRepository, typeService);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("System.Int32", actual.First().Script.FullName);

            typeService.VerifyAllExpectations();
            assetRepository.VerifyAllExpectations();
            gameObject.VerifyAllExpectations();
        }

        [Test]
        public void GetMissingMaterials_refabNotFindOnAssetRepository_Exception()
        {
            var target = new PrefabMetadata
            {
                Path = "Test.prefab"
            };
            var assetRepository = MockRepository.GenerateMock<IAssetRepository>();

            Assert.Catch<InvalidOperationException>(delegate
            {
                target.GetMissingMaterials(assetRepository);
            });
        }

        [Test]
        public void GetMissingMaterials_MissingMaterials_Materials()
        {
            var target = new PrefabMetadata
            {
                Path = "Test.prefab"
            };

            target.Materials.Add(new MaterialMetadata { FullName = "Test.Material1" });
            target.Materials.Add(new MaterialMetadata { FullName = "Test.Material2" });

            var assetRepository = MockRepository.GenerateMock<IAssetRepository>();

            var gameObject = MockRepository.GenerateMock<IGameObject>();
            var material1 = MockRepository.GenerateMock<IMaterial>();
            material1.Expect(m => m.Name).Return("Material1");

            gameObject.Expect(g => g.GetMaterials()).Return(new IMaterial[] { material1 });
            assetRepository.Expect(a => a.GetGameObject("Test.prefab")).Return(gameObject);

            var actual = target.GetMissingMaterials(assetRepository);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("Test.Material2", actual.First().FullName);

            assetRepository.VerifyAllExpectations();
            gameObject.VerifyAllExpectations();
        }
    }
}
