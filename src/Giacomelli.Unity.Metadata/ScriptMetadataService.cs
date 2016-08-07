using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Giacomelli.Unity.Metadata;
using UnityEngine;
using System.Linq;

namespace Giacomelli.Unity.Metadata
{
	public static class ScriptMetadataService
	{
		private static List<ScriptMetadata> s_scripts;

		public static IList<ScriptMetadata> GetAllScripts()
		{
			if (s_scripts == null)
			{
				s_scripts = new List<ScriptMetadata>();
				var types = TypesHelper.GetAllTypes();

				foreach (var t in types)
				{
					var fileId = FileIdUtil.FromType(t);

					s_scripts.Add(new ScriptMetadata
					{
						FileId = fileId,
						FullName = t.FullName
					});
				}
			}
	
			return s_scripts;
		}

		public static string GetFullNameByFileId(int fileId)
		{
			var script = GetAllScripts().FirstOrDefault(s => s.FileId == fileId);

			return script == null ? null : script.FullName;
		}

	}
}