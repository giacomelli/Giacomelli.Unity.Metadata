using System.Reflection;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Define an interface for an assembly loader.
	/// </summary>
    public interface IAssemblyLoader
    {
		/// <summary>
		/// Loads the assembly from the specified path.
		/// </summary>
		/// <returns>The assembly.</returns>
		/// <param name="path">The assembly path.</param>
        Assembly LoadFrom(string path);
    }
}
