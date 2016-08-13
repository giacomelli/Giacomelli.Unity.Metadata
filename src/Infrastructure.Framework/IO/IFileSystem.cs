namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.IO
{
    public interface IFileSystem
    {
        string[] GetFiles(string path, string searchPattern, bool recursive);

        string ReadAllText(string path);

        byte[] ReadAllBytes(string path);

        void WriteAllText(string path, string content);

        string GetFileNameWithoutExtension(string path);

        string GetFullPath(string relativePath);

        void CopyFile(string source, string dest, bool overwrite);
    }
}
