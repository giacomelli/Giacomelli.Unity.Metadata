namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IPrefabMetadataWriter
    {
        void ReplaceGuid(ScriptMetadata oldScript, string newGuid, string fileName);
    }
}
