using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.Serialization;

namespace Giacomelli.Unity.Metadata.Domain
{
    public class ScriptMetadataService : IScriptMetadataService
    {
        private readonly ITypeService m_typeService;
        private List<ScriptMetadata> s_scripts;

        public ScriptMetadataService(ITypeService typeService)
        {
            m_typeService = typeService;
        }

        public IList<ScriptMetadata> GetAllScripts()
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

        public string GetFullNameByFileId(int fileId)
        {
            var script = GetAllScripts().FirstOrDefault(s => s.FileId == fileId);

            return script == null ? null : script.FullName;
        }
    }
}