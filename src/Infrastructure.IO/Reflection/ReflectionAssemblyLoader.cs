using System.Reflection;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Infrastructure.IO.Reflection
{
	/// <summary>
	/// Reflection assembly loader.
	/// </summary>
    public class ReflectionAssemblyLoader : IAssemblyLoader
    {
        private IFileSystem m_fs;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Giacomelli.Unity.Metadata.Infrastructure.IO.Reflection.ReflectionAssemblyLoader"/> class.
		/// </summary>
		/// <param name="fileSystem">File system.</param>
        public ReflectionAssemblyLoader(IFileSystem fileSystem)
        {
            m_fs = fileSystem;
        }

		/// <summary>
		/// Loads the assembly from the specified path.
		/// </summary>
		/// <returns>The assembly.</returns>
		/// <param name="path">The assembly path.</param>
		public Assembly LoadFrom(string path)
        {
            return Assembly.LoadFrom(m_fs.GetFullPath(path));
        }
    }
}
