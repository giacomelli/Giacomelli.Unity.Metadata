using System.Diagnostics;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Mono behaviour metadata.
	/// </summary>
    [DebuggerDisplay("{Script.FileId}: {Script.Name}")]
    public class MonoBehaviourMetadata
    {
        #region Properties
		/// <summary>
		/// Gets or sets the script.
		/// </summary>
		/// <value>The script.</value>
        public ScriptMetadata Script { get; set; }
        #endregion
    }
}