using System.Security;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging;
using Giacomelli.Unity.Metadata.Infrastructure.IO.FileSystems;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Readers.Yaml;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Reflection;
using Giacomelli.Unity.Metadata.Infrastructure.IO.Writers.Yaml;
using Giacomelli.Unity.Metadata.Infrastructure.Repositories;
using Giacomelli.Unity.Metadata.Infrastructure.Unity;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Bootstrap
{
    public static class MetadataBootstrap
    {
        public static ILog Log { get; private set; }

        public static ITypeService TypeService { get; private set; }

        public static IFileSystem FileSystem { get; private set; }

        public static IScriptMetadataService ScriptMetadataService { get; private set; }

        public static IPrefabMetadataService PrefabMetadataService { get; private set; }

        public static IPrefabMetadataReader PrefabMetadataReader { get; private set; }

        public static IPrefabMetadataWriter PrefabMetadataWriter { get; private set; }

        public static IAssetRepository AssetRepository { get; private set; }

        public static IAssemblyLoader AssemblyLoader { get; private set; }

        public static void Setup(string assetsRootFolder, ILog log)
        {
            Log = log;            
            Log.Debug("MetadataBootstrap.Setup: {0}", assetsRootFolder);

            FileSystem = new IsolatedFolderFileSystem(assetsRootFolder);
            AssemblyLoader = new ReflectionAssemblyLoader(FileSystem);
            TypeService = new TypeService(FileSystem, AssemblyLoader);
            MetaFileService.Initialize(FileSystem);
            ScriptMetadataService = new ScriptMetadataService(TypeService);
            PrefabMetadataReader = new YamlPrefabMetadataReader(ScriptMetadataService, FileSystem);
            PrefabMetadataWriter = new YamlPrefabMetadataWriter(FileSystem, Log);
            PrefabMetadataService = new PrefabMetadataService(PrefabMetadataReader, PrefabMetadataWriter, FileSystem, TypeService);
            AssetRepository = new AssetDatabaseAssetRepository();
        }

        public static void Setup()
        {
            Setup(Application.dataPath, new ConsoleLog());
        }
    }
}
