using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Giacomelli.Unity.Metadata
{
	[DebuggerDisplay("{Name}")]
	public class PrefabMetadata
	{
		#region Constructors
		public PrefabMetadata()
		{
			MonoBehaviours = new List<MonoBehaviourMetadata> ();
			Materials = new List<MaterialMetadata>();
		}
		#endregion

		#region Properties
		public string Name { get; set; }
		public string Path { get; set; }
		public IList<MonoBehaviourMetadata> MonoBehaviours { get; set; }
		public IList<MaterialMetadata> Materials { get; set; }
		#endregion
	}
}

