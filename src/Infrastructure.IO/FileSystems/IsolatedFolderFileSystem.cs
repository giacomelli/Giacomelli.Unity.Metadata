using System;
using System.IO;
using System.Linq;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.FileSystems
{
	/// <summary>
	/// Isolated folder file system.
	/// </summary>
    public class IsolatedFolderFileSystem : IFileSystem
    {
        private string m_rootPath;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Giacomelli.Unity.Metadata.Infrastructure.IO.FileSystems.IsolatedFolderFileSystem"/> class.
		/// </summary>
		/// <param name="rootPath">Root path.</param>
        public IsolatedFolderFileSystem(string rootPath)
        {
            var separator = Path.DirectorySeparatorChar.ToString();
            m_rootPath = rootPath.EndsWith(separator, StringComparison.OrdinalIgnoreCase) ? rootPath : rootPath + separator;
        }

		/// <summary>
		/// Gets the file name without extension.
		/// </summary>
		/// <returns>The file name without extension.</returns>
		/// <param name="path">Path.</param>
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="path">Path.</param>
		/// <param name="searchPattern">Search pattern.</param>
		/// <param name="recursive">If set to <c>true</c> recursive.</param>
        public string[] GetFiles(string path, string searchPattern, bool recursive)
        {
            var files = Directory.GetFiles(GetFullPath(path), searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return files.Select(f => MakeRelativePath(f)).ToArray();
        }

		/// <summary>
		/// Reads all text.
		/// </summary>
		/// <returns>The all text.</returns>
		/// <param name="path">Path.</param>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(GetFullPath(path));
        }

		/// <summary>
		/// Reads all bytes.
		/// </summary>
		/// <returns>The all bytes.</returns>
		/// <param name="path">Path.</param>
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(GetFullPath(path));
        }

		/// <summary>
		/// Writes all text.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="content">Content.</param>
        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(GetFullPath(path), content);
        }

		/// <summary>
		/// Gets the full path.
		/// </summary>
		/// <returns>The full path.</returns>
		/// <param name="relativePath">Relative path.</param>
        public string GetFullPath(string relativePath)
        {
            return Path.Combine(m_rootPath, relativePath);
        }

		/// <summary>
		/// Copies the file.
		/// </summary>
		/// <param name="source">Source.</param>
		/// <param name="dest">Destination.</param>
		/// <param name="overwrite">If set to <c>true</c> overwrite.</param>
        public void CopyFile(string source, string dest, bool overwrite)
        {
            File.Copy(GetFullPath(source), GetFullPath(dest), overwrite);
        }

        private string MakeRelativePath(string fullPath)
        {
            return fullPath.Replace(m_rootPath, String.Empty);
        }
    }
}
