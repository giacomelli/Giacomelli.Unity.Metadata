namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.IO
{
	/// <summary>
	/// File system extensions.
	/// </summary>
    public static class FileSystemExtensions
    {
		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="fs">Fs.</param>
		/// <param name="searchPattern">Search pattern.</param>
		/// <param name="recursive">If set to <c>true</c> recursive.</param>
        public static string[] GetFiles(this IFileSystem fs, string searchPattern, bool recursive)
        {
            return fs.GetFiles(string.Empty, searchPattern, recursive);
        }

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="fs">Fs.</param>
		/// <param name="searchPattern">Search pattern.</param>
        public static string[] GetFiles(this IFileSystem fs, string searchPattern)
        {
            return fs.GetFiles(string.Empty, searchPattern, true);
        }

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="fs">Fs.</param>
		/// <param name="path">Path.</param>
		/// <param name="searchPattern">Search pattern.</param>
        public static string[] GetFiles(this IFileSystem fs, string path, string searchPattern)
        {
            return fs.GetFiles(path, searchPattern, true);
        }

		/// <summary>
		/// Copies the file.
		/// </summary>
		/// <param name="fs">Fs.</param>
		/// <param name="source">Source.</param>
		/// <param name="dest">Destination.</param>
        public static void CopyFile(this IFileSystem fs, string source, string dest)
        {
            fs.CopyFile(source, dest, true);
        }
    }
}
