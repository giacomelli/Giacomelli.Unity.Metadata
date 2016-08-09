using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IPrefabMetadataService
    {
        IEnumerable<PrefabMetadata> GetAllPrefabs();
    }
}