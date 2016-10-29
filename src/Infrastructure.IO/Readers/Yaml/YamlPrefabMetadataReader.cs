using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml
{
	/// <summary>
	/// Yaml prefab metadata reader.
	/// </summary>
    public class YamlPrefabMetadataReader : IPrefabMetadataReader
    {
        #region Fields
        private static readonly Regex ScriptFileIdRegex = new Regex(@"\{fileID: (?<fileId>[\-0-9]+), guid: [a-z0-9]+, type: 3", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex MaterialFileIdRegex = new Regex(@"(:|\-) \{fileID: (?<fileId>[\-0-9]+), guid: [a-z0-9]+, type: 2", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex ScriptGuidRegex = new Regex(@"\{fileID: [\-0-9]+, guid: (?<guid>[a-z0-9]+), type: 3", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex MaterialGuidRegex = new Regex(@"\{fileID: [\-0-9]+, guid: (?<guid>[a-z0-9]+), type: 2", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex FixMaterialRegex = new Regex(@",\s*$", RegexOptions.Compiled);

        private readonly IScriptMetadataService m_scriptMetadataService;
        private readonly IFileSystem m_fileSystem;
        #endregion

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml.YamlPrefabMetadataReader"/> class.
		/// </summary>
		/// <param name="scriptMetadataService">Script metadata service.</param>
		/// <param name="fileSystem">File system.</param>
        public YamlPrefabMetadataReader(IScriptMetadataService scriptMetadataService, IFileSystem fileSystem)
        {
            m_scriptMetadataService = scriptMetadataService;
            m_fileSystem = fileSystem;
        }
		#endregion

		#region Methods
		/// <summary>
		/// Reads the prefab metadata form the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public PrefabMetadata Read(string fileName)
        {
            var metadata = new PrefabMetadata();
            var content = m_fileSystem.ReadAllText(fileName);
            ReadMonoBehaviours(metadata, content);
            ReadMaterials(metadata, content);

            return metadata;
        }

        private static void ReadMaterials(PrefabMetadata metadata, string content)
        {
            var documents = content.Split(new string[] { "type: 2}" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < documents.Length - 1; i++)
            {
                var document = documents[i];
                document = FixMaterialRegex.Replace(document, ", type: 2}");
                var material = new MaterialMetadata
                {
                    FileId = ReadFileId(document, MaterialFileIdRegex),
                    Guid = ReadString(document, MaterialGuidRegex, "guid")
                };
                material.FullName = Path.GetFileNameWithoutExtension(MetaFileService.GetFileNameByGuid(material.Guid));

                metadata.Materials.Add(material);
            }
        }

        private static int ReadFileId(string document, Regex regex)
        {
            try
            {
				var id = regex.Match(document).Groups["fileId"].Value;

				if (string.IsNullOrEmpty(id))
				{
					return 0;
				}

                return Convert.ToInt32(id, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Error reading file id from '{0}': {1}".With(document, ex.Message));
            }
        }

        private static string ReadString(string document, Regex regex, string group)
        {
            return regex.Match(document).Groups[group].Value;
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
                    FileId = ReadFileId(document, ScriptFileIdRegex),
                    Guid = ReadString(document, ScriptGuidRegex, "guid")
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
        #endregion
    }
}