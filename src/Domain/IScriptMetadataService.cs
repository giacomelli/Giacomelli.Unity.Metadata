using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IScriptMetadataService
    {
        IList<ScriptMetadata> GetAllScripts();
        string GetFullNameByFileId(int fileId);
    }
}