using System.IO;
using Giacomelli.Unity.Metadata.Domain;
using UnityEditor;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Repositories
{
    public class AssetDatabaseAssetRepository : IAssetRepository
    {
        public IGameObject GetGameObject(string path)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine("Assets", path)).Adapt();
        }
    }
}
