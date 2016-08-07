using System;
using NUnit.Framework;
using System.IO;
using Giacomelli.Unity.Metadata;

namespace Readers.Yaml
{
	[TestFixture]
	public class MetaFileServiceTest
	{
		[Test]
		public void GetFileNameByGuid_Guids_FileNames ()
		{
			MetaFileService.Initialize("Readers/Yaml/Resources/");
			var actual = MetaFileService.GetFileNameByGuid("a3646ea8c53c6ec448c7c9d27568d982");
			StringAssert.EndsWith("Buildron.ClassicMods.BuildMod.dll", actual);

			actual = MetaFileService.GetFileNameByGuid("3049dffabc5225d40b27675901977fdd");
			StringAssert.EndsWith("FireballA.mat", actual);

			MetaFileService.Initialize("Readers/Yaml/Resources/Materials");
			actual = MetaFileService.GetFileNameByGuid("a3646ea8c53c6ec448c7c9d27568d982");
			Assert.IsNull(actual);

			actual = MetaFileService.GetFileNameByGuid("3049dffabc5225d40b27675901977fdd");
			StringAssert.EndsWith("FireballA.mat", actual);
		}
	}
}

