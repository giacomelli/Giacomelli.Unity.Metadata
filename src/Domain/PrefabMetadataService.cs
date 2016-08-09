using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Domain
{
    // TODO: create unit tests.
    public class PrefabMetadataService : IPrefabMetadataService
    {
        private readonly IScriptMetadataService m_scriptMetadataService;
        private readonly IPrefabMetadataReader m_prefabMetadataReader;

        public PrefabMetadataService(IScriptMetadataService scriptMetadataService, IPrefabMetadataReader prefabMetadataReader)
        {
            m_scriptMetadataService = scriptMetadataService;
            m_prefabMetadataReader = prefabMetadataReader;
        }

        public IEnumerable<PrefabMetadata> GetAllPrefabs()
        {
            var prefabs = new List<PrefabMetadata>();
            var rootPath = Application.dataPath + "/";

            // TODO: create a interface to access file system.
            var prefabFiles = Directory.GetFiles(rootPath, "*.prefab", SearchOption.AllDirectories);
            var scripts = m_scriptMetadataService.GetAllScripts();

            foreach (var path in prefabFiles)
            {
                var prefab = m_prefabMetadataReader.ReadFromFile(path);
                prefab.Name = Path.GetFileNameWithoutExtension(path);
                prefab.Path = "Assets/" + path.Replace(rootPath, string.Empty);
                //prefab.FillScriptsNames(scripts);
                prefabs.Add(prefab);
            }

            return prefabs;
        }
    }
}