using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml;
using Giacomelli.Unity.Metadata.Infrastructure.Repositories;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Bootstrap
{
    public static class MetadataBootstrap
    {
        public static ITypeService TypeService { get; private set; }
        public static IScriptMetadataService ScriptMetadataService { get; private set; }
        public static IPrefabMetadataService PrefabMetadataService { get; private set; }
        public static IPrefabMetadataReader PrefabMetadataReader { get; private set; }
        public static IAssetRepository AssetRepository { get; private set; }

        public static void Setup(string assetsRootFolder)
        {
            TypeService = new TypeService(assetsRootFolder);
            MetaFileService.Initialize(assetsRootFolder);
            ScriptMetadataService = new ScriptMetadataService(TypeService);
            PrefabMetadataReader = new YamlPrefabMetadataReader(ScriptMetadataService);
            PrefabMetadataService = new PrefabMetadataService(ScriptMetadataService, PrefabMetadataReader);
            AssetRepository = new AssetDatabaseAssetRepository();
        }

        public static void Setup()
        {
            Setup(Application.dataPath);
        }
    }
}
