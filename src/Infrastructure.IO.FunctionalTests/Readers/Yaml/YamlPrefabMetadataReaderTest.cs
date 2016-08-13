using System;
using System.IO;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Bootstrap;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml;
using NUnit.Framework;
using Rhino.Mocks;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.FunctionalTests.Readers.Yaml
{
    [TestFixture]
    public class YamlPrefabMetadataReaderTest
    {
        [SetUp]
        public void Initialize()
        {
            var assetsRootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            MetadataBootstrap.Setup(assetsRootFolder, MockRepository.GenerateMock<ILog>());
        }

        [Test]
        public void Read_PrefabSample1_Metadata()
        {
            var target = new YamlPrefabMetadataReader(
                MetadataBootstrap.ScriptMetadataService,
                MetadataBootstrap.FileSystem);

            var actual = target.Read("PrefabSample1.prefab");

            Assert.IsNotNull(actual);

            // MonoBehaviours.
            Assert.AreEqual(2, actual.MonoBehaviours.Count);
            AssertFile(actual.MonoBehaviours[0].Script, 1363015578, "a184ae646778d4bf48c881bb73f2188a", "Detonator");
            AssertFile(actual.MonoBehaviours[1].Script, -1557964980, "a184ae646778d4bf48c881bb73f2188a", "DetonatorForce");

            // Materials.
            Assert.AreEqual(9, actual.Materials.Count);
            AssertFile(actual.Materials[0], 2100000, "3049dffabc5225d40b27675901977fdd", "FireballA");
            AssertFile(actual.Materials[1], 2100000, "02a46fb295082ed488cab61782ddc01f", "FireballB");
            AssertFile(actual.Materials[2], 2100000, "2d7e594d401a7524e82695dcd66c2bc0", "SmokeA");
            AssertFile(actual.Materials[3], 2100000, "36aa334bd0865ca459c9b3dfbbaf7198", "SmokeB");
            AssertFile(actual.Materials[4], 2100000, "21588279b895eef48a0448f7f2519813", "ShockWave");
            AssertFile(actual.Materials[5], 2100000, "35c8aea4fddc3f842bd829a8fd3d7e1c", "Sparks");
            AssertFile(actual.Materials[6], 2100000, "b4d8055067ce04d4c98367d69c10b358", "Glow");
            AssertFile(actual.Materials[7], 2100000, "21588279b895eef48a0448f7f2519813", "ShockWave");
            AssertFile(actual.Materials[8], 100000, "49d7999f9fb85954c9541f9a0b0486c3", "Burninating");
        }

        [Test]
        public void Read_PrefabSample2_Metadata()
        {
            var target = new YamlPrefabMetadataReader(
              MetadataBootstrap.ScriptMetadataService,
              MetadataBootstrap.FileSystem);

            var actual = target.Read("PrefabSample2.prefab");
            Assert.IsNotNull(actual);

            // MonoBehaviours.
            Assert.AreEqual(12, actual.MonoBehaviours.Count);
            AssertFile(actual.MonoBehaviours[0].Script, -1920082196, "11ef1fb787a2147ae9af0f0c36457494", "BuildProgressBarController");
            AssertFile(actual.MonoBehaviours[1].Script, -111349563, "11ef1fb787a2147ae9af0f0c36457494", "BuildController");
            AssertFile(actual.MonoBehaviours[2].Script, 1977646725, "11ef1fb787a2147ae9af0f0c36457494", "BuildFocusedPanelController");
            AssertFile(actual.MonoBehaviours[3].Script, 708705254, "f5f67c52d1564df4a8936ccd202a3bd8", "Text");
            AssertFile(actual.MonoBehaviours[4].Script, 1980459831, "f5f67c52d1564df4a8936ccd202a3bd8", "CanvasScaler");

            AssertFile(actual.MonoBehaviours[5].Script, 1301386320, "f5f67c52d1564df4a8936ccd202a3bd8", "GraphicRaycaster");
            AssertFile(actual.MonoBehaviours[6].Script, 1301386320, "f5f67c52d1564df4a8936ccd202a3bd8", "GraphicRaycaster");
            AssertFile(actual.MonoBehaviours[7].Script, 708705254, "f5f67c52d1564df4a8936ccd202a3bd8", "Text");
            AssertFile(actual.MonoBehaviours[8].Script, 1980459831, "f5f67c52d1564df4a8936ccd202a3bd8", "CanvasScaler");
            AssertFile(actual.MonoBehaviours[9].Script, -765806418, "f5f67c52d1564df4a8936ccd202a3bd8", "Image");
            AssertFile(actual.MonoBehaviours[10].Script, -765806418, "f5f67c52d1564df4a8936ccd202a3bd8", "Image");
            AssertFile(actual.MonoBehaviours[11].Script, 708705254, "f5f67c52d1564df4a8936ccd202a3bd8", "Text");

            // Materials.
            Assert.AreEqual(8, actual.Materials.Count);
            AssertFile(actual.Materials[0], 2100000, "e0d998f08577741cf98afbb403dc7b72", null);
            AssertFile(actual.Materials[1], 2100000, "6b427feebf1464d7e9581f02828a4dcc", null);
            AssertFile(actual.Materials[2], 2100000, "6b427feebf1464d7e9581f02828a4dcc", null);
            AssertFile(actual.Materials[3], 2100000, "e0d998f08577741cf98afbb403dc7b72", null);
            AssertFile(actual.Materials[4], 2100000, "d14bfd8b29d014603bd8ff1f861d4318", "BuildMaterial");
            AssertFile(actual.Materials[5], 2100000, "dd1bddd8c2778468ab0c370d02bd9000", "BuildHidingMaterial");
            AssertFile(actual.Materials[6], 2100000, "dd1bddd8c2778468ab0c370d02bd9000", "BuildHidingMaterial");
            AssertFile(actual.Materials[7], 13400000, "10637b6ffa9aa462f8e52a60b774bced", null);
        }


        private void AssertFile(FileMetadataBase target, int expectedFileId, string expectedGuid, string expectedName)
        {
            Assert.IsNotNull(target);
            Assert.AreEqual(expectedFileId, target.FileId, "FiledId for {0} not match", expectedName);

            Assert.IsNotNull(target.Guid, "Guid for {0} should exists", expectedName);
            Assert.AreEqual(expectedGuid, target.Guid.ToString(), "Guid for {0} not match", expectedName);
            Assert.AreEqual(expectedName, target.Name);
        }
    }
}
