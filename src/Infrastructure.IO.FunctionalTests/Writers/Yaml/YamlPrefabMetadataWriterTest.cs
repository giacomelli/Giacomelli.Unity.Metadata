using System;
using System.IO;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Bootstrap;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Writers.Yaml;
using NUnit.Framework;
using Rhino.Mocks;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.FunctionalTests.Writers.Yaml
{
    [TestFixture]
    public class YamlPrefabMetadataWriterTest
    {
        [SetUp]
        public void Initialize()
        {
            var assetsRootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            MetadataBootstrap.Setup(assetsRootFolder, MockRepository.GenerateMock<ILog>());
        }

        [Test]
        public void ReplaceGuid_OldAndNew_Replaced()
        {
            var fs = MetadataBootstrap.FileSystem;
            var prefabPath = "PrefabSample1.ReplaceGuidTest.prefab";
            fs.CopyFile("PrefabSample1.prefab", prefabPath);

            var target = new YamlPrefabMetadataWriter(fs, MockRepository.GenerateMock<ILog>());

            var oldScript1 = new ScriptMetadata { FileId = 1363015578, Guid = "a184ae646778d4bf48c881bb73f2188a" };
            var oldScript2 = new ScriptMetadata { FileId = -1557964980, Guid = "a184ae646778d4bf48c881bb73f2188a" };
            target.ReplaceGuid(oldScript1, "1ReplaceGuidTest1", prefabPath);
            target.ReplaceGuid(oldScript2, "2ReplaceGuidTest2", prefabPath);

            var reader = new YamlPrefabMetadataReader(MetadataBootstrap.ScriptMetadataService, fs);
            var actual = reader.Read(prefabPath);
            Assert.AreEqual(2, actual.MonoBehaviours.Count);
            Assert.AreEqual("1ReplaceGuidTest1", actual.MonoBehaviours.First().Script.Guid);
            Assert.AreEqual("2ReplaceGuidTest2", actual.MonoBehaviours.Last().Script.Guid);
        }
    }
}
