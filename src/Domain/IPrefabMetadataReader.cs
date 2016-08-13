namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IPrefabMetadataReader
    {
        PrefabMetadata Read(string fileName);
    }
}
