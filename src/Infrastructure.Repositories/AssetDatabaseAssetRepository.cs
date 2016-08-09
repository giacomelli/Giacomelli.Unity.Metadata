using Giacomelli.Unity.Metadata.Domain;
using UnityEditor;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Repositories
{
    public class AssetDatabaseAssetRepository : IAssetRepository
    {
        public GameObject GetGameObject(string path)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
    }
}
