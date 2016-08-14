using System.IO;
using Giacomelli.Unity.Metadata.Domain;
using UnityEditor;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Repositories
{
	/// <summary>
	/// An IAssetRepository implementation that use AssetDatabase.
	/// </summary>
	public class AssetDatabaseAssetRepository : IAssetRepository
    {
		/// <summary>
		/// Gets a game object in the specified path.
		/// </summary>
		/// <returns>The game object.</returns>
		/// <param name="path">The asset path.</param>
		public IGameObject GetGameObject(string path)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine("Assets", path)).Adapt();
        }
    }
}
