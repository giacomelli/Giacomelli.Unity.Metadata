using System;
using System.IO;

namespace Giacomelli.Unity.Metadata
{
	public interface IPrefabMetadataReader
	{
		PrefabMetadata Read(TextReader input);
	}
}

