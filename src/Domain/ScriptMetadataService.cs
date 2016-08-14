using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.Serialization;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Script metadata service.
	/// </summary>
    public class ScriptMetadataService : IScriptMetadataService
    {
        private readonly ITypeService m_typeService;
        private List<ScriptMetadata> s_scripts;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Giacomelli.Unity.Metadata.Domain.ScriptMetadataService"/> class.
		/// </summary>
		/// <param name="typeService">Type service.</param>
        public ScriptMetadataService(ITypeService typeService)
        {
            m_typeService = typeService;
        }

		/// <summary>
		/// Gets the scripts.
		/// </summary>
		/// <returns>The scripts.</returns>
        public IList<ScriptMetadata> GetScripts()
        {
            if (s_scripts == null)
            {
                s_scripts = new List<ScriptMetadata>();
                var types = m_typeService.GetTypes();

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

		/// <summary>
		/// Gets the full name by file identifier.
		/// </summary>
		/// <returns>The full name by file identifier.</returns>
		/// <param name="fileId">File identifier.</param>
        public string GetFullNameByFileId(int fileId)
        {
            var script = GetScripts().FirstOrDefault(s => s.FileId == fileId);

            return script == null ? null : script.FullName;
        }
    }
}