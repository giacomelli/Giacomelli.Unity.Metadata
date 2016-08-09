using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Domain
{
    public static class PrefabMetadataExtensions
    {
        public static IEnumerable<MonoBehaviourMetadata> GetMissingMonoBehaviours(this PrefabMetadata prefab, IAssetRepository assetRepository, ITypeService typeService)
        {
            var prefabInstance = assetRepository.GetGameObject(prefab.Path);

            if (prefabInstance == null)
            {
                throw new InvalidOperationException(
                    "Cannot load the prefab {0} in the path {1}.".With(prefab.Name, prefab.Path));
            }

            var result = new List<MonoBehaviourMetadata>();

            foreach (var m in prefab.MonoBehaviours)
            {
                var type = typeService.GetType(m.Script.FullName);

                if (!prefabInstance.HasComponent(type))
                {
                    result.Add(m);
                }
            }

            return result;
        }

        public static IEnumerable<MaterialMetadata> GetMissingMaterials(this PrefabMetadata prefab, IAssetRepository assetRepository)
        {
            var prefabInstance = assetRepository.GetGameObject(prefab.Path);

            if (prefabInstance == null)
            {
                throw new InvalidOperationException(
                    "Cannot load the prefab {0} in the path {1}.".With(prefab.Name, prefab.Path));
            }

            var result = new List<MaterialMetadata>();
            var instanceMaterials = prefabInstance.GetComponents<Renderer>().SelectMany(r => r.sharedMaterials);

            foreach (var m in prefab.Materials)
            {
                if (!instanceMaterials.All(i => i.name.Equals(m.Name)))
                {
                    result.Add(m);
                }
            }

            return result;
        }

        public static void FillScriptsNames(this IEnumerable<PrefabMetadata> prefabs, IEnumerable<ScriptMetadata> allScripts)
        {
            foreach (var prefab in prefabs)
            {
                prefab.FillScriptsNames(allScripts);
            }
        }

        public static void FillScriptsNames(this PrefabMetadata prefab, IEnumerable<ScriptMetadata> allScripts)
        {
            foreach (var m in prefab.MonoBehaviours)
            {
                if (String.IsNullOrEmpty(m.Script.FullName))
                {
                    var availableScript = allScripts.FirstOrDefault(s => s.FileId == m.Script.FileId);

                    if (availableScript != null)
                    {
                        m.Script.FullName = availableScript.FullName;
                    }
                }
            }
        }
    }
}

