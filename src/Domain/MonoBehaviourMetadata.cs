using System.Diagnostics;

namespace Giacomelli.Unity.Metadata.Domain
{
    [DebuggerDisplay("{Script.FileId}: {Script.Name}")]
    public class MonoBehaviourMetadata
    {
        #region Properties
        public ScriptMetadata Script { get; set; }
        #endregion
    }
}

