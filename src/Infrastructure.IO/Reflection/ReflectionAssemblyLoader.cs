using System.Reflection;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Reflection
{
    public class ReflectionAssemblyLoader : IAssemblyLoader
    {
        private IFileSystem m_fs;

        public ReflectionAssemblyLoader(IFileSystem fileSystem)
        {
            m_fs = fileSystem;
        }

        public Assembly LoadFrom(string path)
        {
            return Assembly.LoadFrom(m_fs.GetFullPath(path));
        }
    }
}
