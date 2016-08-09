using System.IO;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IPrefabMetadataReader
    {
        PrefabMetadata Read(TextReader input);
    }
}

