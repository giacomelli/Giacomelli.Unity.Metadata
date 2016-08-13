namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.IO
{
    public static class FileSystemExtensions
    {
        public static string[] GetFiles(this IFileSystem fs, string searchPattern, bool recursive)
        {
            return fs.GetFiles(string.Empty, searchPattern, recursive);
        }

        public static string[] GetFiles(this IFileSystem fs, string searchPattern)
        {
            return fs.GetFiles(string.Empty, searchPattern, true);
        }

        public static string[] GetFiles(this IFileSystem fs, string path, string searchPattern)
        {
            return fs.GetFiles(path, searchPattern, true);
        }

        public static void CopyFile(this IFileSystem fs, string source, string dest)
        {
            fs.CopyFile(source, dest, true);
        }
    }
}
