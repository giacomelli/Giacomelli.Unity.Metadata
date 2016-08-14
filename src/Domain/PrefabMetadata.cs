using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Prefab metadata.
	/// </summary>
    [DebuggerDisplay("{Name}")]
    public class PrefabMetadata
    {
        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Giacomelli.Unity.Metadata.Domain.PrefabMetadata"/> class.
		/// </summary>
        public PrefabMetadata()
        {
            MonoBehaviours = new List<MonoBehaviourMetadata>();
            Materials = new List<MaterialMetadata>();
        }
        #endregion

        #region Properties
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
        public string Name { get; set; }

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
        public string Path { get; set; }

		/// <summary>
		/// Gets the mono behaviours.
		/// </summary>
		/// <value>The mono behaviours.</value>
        public IList<MonoBehaviourMetadata> MonoBehaviours { get; private set; }

		/// <summary>
		/// Gets the materials.
		/// </summary>
		/// <value>The materials.</value>
        public IList<MaterialMetadata> Materials { get; private set; }
        #endregion

        #region Methods
		/// <summary>
		/// Gets the script by file identifier.
		/// </summary>
		/// <returns>The script by file identifier.</returns>
		/// <param name="fileId">File identifier.</param>
        public ScriptMetadata GetScriptByFileId(int fileId)
        {
            var monoBehaviour = MonoBehaviours.FirstOrDefault(m => m.Script != null && m.Script.FileId == fileId);

            return monoBehaviour == null ? null : monoBehaviour.Script;
        }

		/// <summary>
		/// Gets the missing mono behaviours.
		/// </summary>
		/// <returns>The missing mono behaviours.</returns>
		/// <param name="assetRepository">Asset repository.</param>
		/// <param name="typeService">Type service.</param>
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

		/// <summary>
		/// Gets the missing materials.
		/// </summary>
		/// <returns>The missing materials.</returns>
		/// <param name="assetRepository">Asset repository.</param>
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