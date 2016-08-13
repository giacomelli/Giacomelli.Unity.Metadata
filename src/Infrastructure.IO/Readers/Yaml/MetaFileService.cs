using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml
{
    public static class MetaFileService
    {
        private static List<MetaFileInfo> s_infos = new List<MetaFileInfo>();
        private static Regex s_guidRegex = new Regex(@"guid: (\S+)", RegexOptions.Compiled);

        public static void Initialize(IFileSystem fileSystem)
        {
            s_infos = new List<MetaFileInfo>();
            var metaFiles = fileSystem.GetFiles("*.meta");

            foreach (var metaFile in metaFiles)
            {
                var content = fileSystem.ReadAllText(metaFile);
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