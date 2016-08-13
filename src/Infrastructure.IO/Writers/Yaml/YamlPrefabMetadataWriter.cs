using System.Text.RegularExpressions;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Writers.Yaml
{
    public class YamlPrefabMetadataWriter : IPrefabMetadataWriter
    {
        private readonly IFileSystem m_fs;
        private readonly ILog m_log;

        public YamlPrefabMetadataWriter(IFileSystem fileSystem, ILog log)
        {
            m_fs = fileSystem;
            m_log = log;
        }

        public void ReplaceGuid(ScriptMetadata oldScript, string newGuid, string fileName)
        {
            m_log.Debug("Replacing guid from '{0}' to '{1}' on file '{2}'...", oldScript.Guid, newGuid, fileName);
            var content = m_fs.ReadAllText(fileName);
            var regex = new Regex(@"(\{{fileID: {0}, guid: )({1})(, type: 3)".With(oldScript.FileId, oldScript.Guid), RegexOptions.Compiled | RegexOptions.IgnoreCase);

            content = regex.Replace(
                content,
                (m) =>
            {
                return "{0}{1}{2}".With(m.Groups[1].Value, newGuid, m.Groups[3].Value);
            });

            m_fs.WriteAllText(fileName, content);
        }
    }
}
