using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for a script metadata service.
	/// </summary>
    public interface IScriptMetadataService
    {
		/// <summary>
		/// Gets the scripts.
		/// </summary>
		/// <returns>The scripts.</returns>
        IList<ScriptMetadata> GetScripts();

		/// <summary>
		/// Gets the full name by file identifier.
		/// </summary>
		/// <returns>The full name by file identifier.</returns>
		/// <param name="fileId">File identifier.</param>
        string GetFullNameByFileId(int fileId);
    }
}