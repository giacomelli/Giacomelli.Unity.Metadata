namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IAssetRepository
    {
        IGameObject GetGameObject(string path);
    }
}
