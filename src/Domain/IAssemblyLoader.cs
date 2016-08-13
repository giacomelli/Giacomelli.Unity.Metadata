using System.Reflection;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IAssemblyLoader
    {
        Assembly LoadFrom(string path);
    }
}
