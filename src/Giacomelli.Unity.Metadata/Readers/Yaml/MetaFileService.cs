using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Giacomelli.Unity.Metadata
{
	public static class MetaFileService
	{
		private static List<MetaFileInfo> s_infos = new List<MetaFileInfo>();
		private static Regex s_guidRegex = new Regex(@"guid: (\S+)", RegexOptions.Compiled);

		public static void Initialize(string assetsRootFolder)
		{
			s_infos = new List<MetaFileInfo>();
			var metaFiles = Directory.GetFiles(assetsRootFolder, "*.meta", SearchOption.AllDirectories);

			foreach (var metaFile in metaFiles)
			{
				var content = File.ReadAllText(metaFile);
				s_infos.Add(new MetaFileInfo
				{
					FileName = metaFile.Replace(".meta", string.Empty),
					Guid = s_guidRegex.Match(content).Groups[1].Value
				});
			}
		}

		public static string GetFileNameByGuid(string guid)
		{
			var info = s_infos.FirstOrDefault(i => i.Guid.Equals(guid, StringComparison.Ordinal));

			return info == null ? null : info.FileName;
		}
	}
}

