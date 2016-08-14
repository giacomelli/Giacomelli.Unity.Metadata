using System;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Base class for file metadata.
	/// </summary>
    public abstract class FileMetadataBase
    {
        #region Properties
		/// <summary>
		/// Gets or sets the file identifier.
		/// </summary>
		/// <value>The file identifier.</value>
        public int FileId { get; set; }

		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value>The full name.</value>
        public string FullName { get; set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(FullName))
                {
                    return FullName;
                }

                var parts = FullName.Split('.');
                return parts[parts.Length - 1];
            }
        }

		/// <summary>
		/// Gets or sets the GUID.
		/// </summary>
		/// <value>The GUID.</value>
        public string Guid { get; set; }
        #endregion
    }
}
