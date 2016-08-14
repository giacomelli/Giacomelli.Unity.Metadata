using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for a prefab metadata service.
	/// </summary>
    public interface IPrefabMetadataService
    {
		/// <summary>
		/// Gets the prefabs.
		/// </summary>
		/// <returns>The prefabs.</returns>
        IEnumerable<PrefabMetadata> GetPrefabs();

		/// <summary>
		/// Fix the missing MonoBehaviours.
		/// </summary>
		/// <param name="prefab">Prefab.</param>
		/// <param name="missingMonoBehaviours">Missing mono behaviours.</param>
        void FixMissingMonobehaviours(PrefabMetadata prefab, IEnumerable<MonoBehaviourMetadata> missingMonoBehaviours);
	}
}