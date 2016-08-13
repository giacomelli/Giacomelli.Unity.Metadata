using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IPrefabMetadataService
    {
        IEnumerable<PrefabMetadata> GetPrefabs();

        void FixMissingMonobehaviours(PrefabMetadata prefab, IEnumerable<MonoBehaviourMetadata> missingMonoBehaviours);
    }
}