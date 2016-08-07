using System;
using UnityEngine;

namespace Giacomelli.Unity.Metadata
{
	public class MetadataConfig
	{
		public static void Initialize(string assetsRootFolder)
		{
			TypesHelper.Initialize(assetsRootFolder);
			MetaFileService.Initialize(assetsRootFolder);
		}

		public static void Initialize()
		{
			Initialize(Application.dataPath);
		}
	}
}

