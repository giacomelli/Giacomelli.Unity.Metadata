using System;
using System.IO;
using System.Text.RegularExpressions;
using Giacomelli.Unity.Metadata.Domain;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml
{
    public class YamlPrefabMetadataReader : IPrefabMetadataReader
    {
        #region Fields
        private static readonly Regex s_scriptFileIdRegex = new Regex(@"\{fileID: (?<fileId>[\-0-9]+), guid: [a-z0-9]+, type: 3", RegexOptions.Compiled);
        private static readonly Regex s_materialFileIdRegex = new Regex(@"(:|\-) \{fileID: (?<fileId>[\-0-9]+), guid: [a-z0-9]+, type: 2", RegexOptions.Compiled);
        private static readonly Regex s_scriptGuidRegex = new Regex(@"\{fileID: [\-0-9]+, guid: (?<guid>[a-z0-9]+), type: 3", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex s_materialGuidRegex = new Regex(@"\{fileID: [\-0-9]+, guid: (?<guid>[a-z0-9]+), type: 2", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex s_fixMaterialRegex = new Regex(@",\s*$", RegexOptions.Compiled);

        private readonly IScriptMetadataService m_scriptMetadataService;
        #endregion

        #region Constructors
        public YamlPrefabMetadataReader(IScriptMetadataService scriptMetadataService)
        {
            m_scriptMetadataService = scriptMetadataService;
        }
        #endregion

        #region Methods
        public PrefabMetadata Read(TextReader input)
        {
            var metadata = new PrefabMetadata();
            var content = input.ReadToEnd();
            ReadMonoBehaviours(metadata, content);
            ReadMaterials(metadata, content);

            return metadata;
        }

        private void ReadMonoBehaviours(PrefabMetadata metadata, string content)
        {
            var documents = content.Split(new string[] { "--- !u!114" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < documents.Length; i++)
            {
                var document = documents[i];
                var monoBehaviour = new MonoBehaviourMetadata();
                var script = new ScriptMetadata
                {
                    FileId = ReadFileId(document, s_scriptFileIdRegex),
                    Guid = ReadString(document, s_scriptGuidRegex, "guid")
                };
                script.FullName = m_scriptMetadataService.GetFullNameByFileId(script.FileId);

                if (String.IsNullOrEmpty(script.FullName))
                {
                    script.FullName = MetaFileService.GetFileNameByGuid(script.Guid);
                }

                monoBehaviour.Script = script;


                metadata.MonoBehaviours.Add(monoBehaviour);
            }
        }

        private void ReadMaterials(PrefabMetadata metadata, string content)
        {
            var documents = content.Split(new string[] { "type: 2}" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < documents.Length - 1; i++)
            {
                var document = documents[i];
                document = s_fixMaterialRegex.Replace(document, ", type: 2}");
                var material = new MaterialMetadata
                {
                    FileId = ReadFileId(document, s_materialFileIdRegex),
                    Guid = ReadString(document, s_materialGuidRegex, "guid")
                };
                material.FullName = Path.GetFileNameWithoutExtension(MetaFileService.GetFileNameByGuid(material.Guid));

                metadata.Materials.Add(material);
            }
        }

        private int ReadFileId(string document, Regex regex)
        {
            try
            {
                return Convert.ToInt32(regex.Match(document).Groups["fileId"].Value);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Error reading file id from '{0}': {1}".With(document, ex.Message));
            }
        }

        private string ReadString(string document, Regex regex, string group)
        {
            return regex.Match(document).Groups[group].Value;
        }
        #endregion
    }
}

