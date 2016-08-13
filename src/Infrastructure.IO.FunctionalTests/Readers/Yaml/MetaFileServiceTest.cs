using System;
using System.IO;
using Giacomelli.Unity.Metadata.Infrastructure.IO.FileSystems;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml;
using NUnit.Framework;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.FunctionalTests.Readers.Yaml
{
    [TestFixture]
    public class MetaFileServiceTest
    {
        [Test]
        public void GetFileNameByGuid_Guids_FileNames()
        {
            var fs = new IsolatedFolderFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"));
            MetaFileService.Initialize(fs);
            var actual = MetaFileService.GetFileNameByGuid("a3646ea8c53c6ec448c7c9d27568d982");
            StringAssert.EndsWith("Buildron.ClassicMods.BuildMod.dll", actual);

            actual = MetaFileService.GetFileNameByGuid("3049dffabc5225d40b27675901977fdd");
            StringAssert.EndsWith("FireballA.mat", actual);

            fs = new IsolatedFolderFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Materials"));
            MetaFileService.Initialize(fs);
            actual = MetaFileService.GetFileNameByGuid("a3646ea8c53c6ec448c7c9d27568d982");
            Assert.IsNull(actual);

            actual = MetaFileService.GetFileNameByGuid("3049dffabc5225d40b27675901977fdd");
            StringAssert.EndsWith("FireballA.mat", actual);
        }
    }
}

