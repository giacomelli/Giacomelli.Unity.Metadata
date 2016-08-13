using System.Collections.Generic;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Domain
{
    public class PrefabMetadataService : IPrefabMetadataService
    {
        private readonly IPrefabMetadataReader m_reader;
        private readonly IPrefabMetadataWriter m_writer;
        private readonly IFileSystem m_fs;
        private readonly ITypeService m_typeService;

        public PrefabMetadataService(
            IPrefabMetadataReader prefabMetadataReader,
            IPrefabMetadataWriter prefabMetadataWriter,
            IFileSystem fileSystem,
            ITypeService typeService)
        {
            m_reader = prefabMetadataReader;
            m_writer = prefabMetadataWriter;
            m_fs = fileSystem;
            m_typeService = typeService;
        }

        public IEnumerable<PrefabMetadata> GetPrefabs()
        {
            var prefabs = new List<PrefabMetadata>();
            var prefabFiles = m_fs.GetFiles("*.prefab");

            foreach (var path in prefabFiles)
            {
                var prefab = m_reader.Read(path);
                prefab.Name = m_fs.GetFileNameWithoutExtension(path);
                prefab.Path = path;
                prefabs.Add(prefab);
            }

            return prefabs;
        }

        public void FixMissingMonobehaviours(PrefabMetadata prefab, IEnumerable<MonoBehaviourMetadata> missingMonoBehaviours)
        {
            foreach (var m in missingMonoBehaviours)
            {     
                var scriptType = m_typeService.GetTypeByName(m.Script.FullName);
                var newGuid = m_typeService.GetGuid(scriptType);
                m_writer.ReplaceGuid(m.Script, newGuid, prefab.Path);
            }
        }
    }
}