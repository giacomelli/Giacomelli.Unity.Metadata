using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Giacomelli.Unity.Metadata.Domain
{
    [DebuggerDisplay("{Name}")]
    public class PrefabMetadata
    {
        #region Constructors
        public PrefabMetadata()
        {
            MonoBehaviours = new List<MonoBehaviourMetadata>();
            Materials = new List<MaterialMetadata>();
        }
        #endregion

        #region Properties
        public string Name { get; set; }

        public string Path { get; set; }

        public IList<MonoBehaviourMetadata> MonoBehaviours { get; private set; }

        public IList<MaterialMetadata> Materials { get; private set; }
        #endregion

        #region Methods
        public ScriptMetadata GetScriptByFileId(int fileId)
        {
            var monoBehaviour = MonoBehaviours.FirstOrDefault(m => m.Script != null && m.Script.FileId == fileId);

            return monoBehaviour == null ? null : monoBehaviour.Script;
        }

        public IEnumerable<MonoBehaviourMetadata> GetMissingMonoBehaviours(IAssetRepository assetRepository, ITypeService typeService)
        {
            var prefabInstance = LoadPrefabInstance(assetRepository);
            var result = new List<MonoBehaviourMetadata>();

            foreach (var m in MonoBehaviours)
            {
                var type = typeService.GetTypeByName(m.Script.FullName);

                if (!prefabInstance.HasComponent(type))
                {
                    result.Add(m);
                }
            }

            return result;
        }

        public IEnumerable<MaterialMetadata> GetMissingMaterials(IAssetRepository assetRepository)
        {
            var prefabInstance = LoadPrefabInstance(assetRepository);
            var result = new List<MaterialMetadata>();
            var instanceMaterials = prefabInstance.GetMaterials();

            foreach (var m in Materials)
            {
                if (!instanceMaterials.All(i => i.Name.Equals(m.Name)))
                {
                    result.Add(m);
                }
            }

            return result;
        }

        private IGameObject LoadPrefabInstance(IAssetRepository assetRepository)
        {
            var prefabInstance = assetRepository.GetGameObject(Path);

            if (prefabInstance == null)
            {
                throw new InvalidOperationException(
                    "Cannot load the prefab {0} in the path {1}.".With(Name, Path));
            }

            return prefabInstance;
        }
        #endregion
    }
}