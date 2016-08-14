namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.IO
{
	/// <summary>
	/// Define an interface for a file system.
	/// </summary>
    public interface IFileSystem
    {
		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="path">Path.</param>
		/// <param name="searchPattern">Search pattern.</param>
		/// <param name="recursive">If set to <c>true</c> recursive.</param>
        string[] GetFiles(string path, string searchPattern, bool recursive);

		/// <summary>
		/// Reads all text.
		/// </summary>
		/// <returns>The all text.</returns>
		/// <param name="path">Path.</param>
        string ReadAllText(string path);

		/// <summary>
		/// Reads all bytes.
		/// </summary>
		/// <returns>The all bytes.</returns>
		/// <param name="path">Path.</param>
        byte[] ReadAllBytes(string path);

		/// <summary>
		/// Writes all text.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="content">Content.</param>
        void WriteAllText(string path, string content);

		/// <summary>
		/// Gets the file name without extension.
		/// </summary>
		/// <returns>The file name without extension.</returns>
		/// <param name="path">Path.</param>
        string GetFileNameWithoutExtension(string path);

		/// <summary>
		/// Gets the full path.
		/// </summary>
		/// <returns>The full path.</returns>
		/// <param name="relativePath">Relative path.</param>
        string GetFullPath(string relativePath);

		/// <summary>
		/// Copies the file.
		/// </summary>
		/// <param name="source">Source.</param>
		/// <param name="dest">Destination.</param>
		/// <param name="overwrite">If set to <c>true</c> overwrite.</param>
        void CopyFile(string source, string dest, bool overwrite);
    }
}
