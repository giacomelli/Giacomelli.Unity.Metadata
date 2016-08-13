using System;
using System.IO;
using System.Linq;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.FileSystems
{
    public class IsolatedFolderFileSystem : IFileSystem
    {
        private string m_rootPath;

        public IsolatedFolderFileSystem(string rootPath)
        {
            var separator = Path.DirectorySeparatorChar.ToString();
            m_rootPath = rootPath.EndsWith(separator, StringComparison.OrdinalIgnoreCase) ? rootPath : rootPath + separator;
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string[] GetFiles(string path, string searchPattern, bool recursive)
        {
            var files = Directory.GetFiles(GetFullPath(path), searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return files.Select(f => MakeRelativePath(f)).ToArray();
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(GetFullPath(path));
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(GetFullPath(path));
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(GetFullPath(path), content);
        }

        public string GetFullPath(string relativePath)
        {
            return Path.Combine(m_rootPath, relativePath);
        }

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
