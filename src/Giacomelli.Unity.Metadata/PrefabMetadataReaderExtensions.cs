using System;
using System.IO;

namespace Giacomelli.Unity.Metadata
{
	public static class PrefabMetadataReaderExtensions
	{
		public static PrefabMetadata ReadFromFile(this IPrefabMetadataReader reader, string fileName)
		{
			using (var input = File.OpenText(fileName))
			{
				return reader.Read(input); 
			}
		}
	}
}

