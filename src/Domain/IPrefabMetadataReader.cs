namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for a prefab metadata reader.
	/// </summary>
    public interface IPrefabMetadataReader
    {
		/// <summary>
		/// Reads the prefab metadata form the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
        PrefabMetadata Read(string fileName);
    }
}
