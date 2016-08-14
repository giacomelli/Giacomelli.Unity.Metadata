namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for a [refab metadata writer.
	/// </summary>
    public interface IPrefabMetadataWriter
    {
		/// <summary>
		/// Replaces the GUID.
		/// </summary>
		/// <param name="oldScript">Old script.</param>
		/// <param name="newGuid">New GUID.</param>
		/// <param name="fileName">File name.</param>
        void ReplaceGuid(ScriptMetadata oldScript, string newGuid, string fileName);
    }
}
