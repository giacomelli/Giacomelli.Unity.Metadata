using UnityEngine;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IAssetRepository
    {
        GameObject GetGameObject(string path);
    }
}
