using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Giacomelli.Unity.Metadata
{
	public class YamlPrefabMetadataReader : IPrefabMetadataReader
	{
		#region Fields
		private Regex m_fileIdRegex = new Regex(@"m_Script: \{fileID: ([\-0-9]+),", RegexOptions.Compiled);
		#endregion

		#region Methods
		public PrefabMetadata Read (TextReader input)
		{
			var metadata = new PrefabMetadata();
			var content = input.ReadToEnd();
			var documents = content.Split(new string[] { "--- !u!114" }, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 1; i < documents.Length; i++)
			{
				var monoBehaviour = new MonoBehaviourMetadata();
				monoBehaviour.Script = new ScriptMetadata
				{
					FileId = m_fileIdRegex.Match(documents[i]).Groups[1].Value
				};

				metadata.MonoBehaviours.Add(monoBehaviour);
			}

			return metadata;
		}
		#endregion
	}
}

