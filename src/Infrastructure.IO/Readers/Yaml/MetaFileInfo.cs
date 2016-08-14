namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml
{
	/// <summary>
	/// Meta file info.
	/// </summary>
    public class MetaFileInfo
    {
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
        public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the GUID.
		/// </summary>
		/// <value>The GUID.</value>
        public string Guid { get; set; }
    }
}