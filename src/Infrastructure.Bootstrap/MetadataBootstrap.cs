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
	/// <summary>
	/// Giacomelly.Unity.Metadata bootstrap.
	/// </summary>
    public static class MetadataBootstrap
    {
		/// <summary>
		/// Gets the log.
		/// </summary>
		/// <value>The log.</value>
        public static ILog Log { get; private set; }

		/// <summary>
		/// Gets the type service.
		/// </summary>
		/// <value>The type service.</value>
        public static ITypeService TypeService { get; private set; }

		/// <summary>
		/// Gets the file system.
		/// </summary>
		/// <value>The file system.</value>
        public static IFileSystem FileSystem { get; private set; }

		/// <summary>
		/// Gets the script metadata service.
		/// </summary>
		/// <value>The script metadata service.</value>
        public static IScriptMetadataService ScriptMetadataService { get; private set; }

		/// <summary>
		/// Gets the prefab metadata service.
		/// </summary>
		/// <value>The prefab metadata service.</value>
        public static IPrefabMetadataService PrefabMetadataService { get; private set; }

		/// <summary>
		/// Gets the prefab metadata reader.
		/// </summary>
		/// <value>The prefab metadata reader.</value>
        public static IPrefabMetadataReader PrefabMetadataReader { get; private set; }

		/// <summary>
		/// Gets the prefab metadata writer.
		/// </summary>
		/// <value>The prefab metadata writer.</value>
        public static IPrefabMetadataWriter PrefabMetadataWriter { get; private set; }

		/// <summary>
		/// Gets the asset repository.
		/// </summary>
		/// <value>The asset repository.</value>
        public static IAssetRepository AssetRepository { get; private set; }

		/// <summary>
		/// Gets the assembly loader.
		/// </summary>
		/// <value>The assembly loader.</value>
        public static IAssemblyLoader AssemblyLoader { get; private set; }

		/// <summary>
		/// Performs the setup.
		/// </summary>
		/// <param name="assetsRootFolder">Assets root folder.</param>
		/// <param name="log">Log.</param>
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

		/// <summary>
		/// Performs the setup.
		/// </summary>
		public static void Setup()
        {
            Setup(Application.dataPath, new ConsoleLog());
        }
    }
}
