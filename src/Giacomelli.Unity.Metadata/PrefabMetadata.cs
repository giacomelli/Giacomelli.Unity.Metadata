using System;
using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata
{
	public class PrefabMetadata
	{
		#region Constructors
		public PrefabMetadata()
		{
			MonoBehaviours = new List<MonoBehaviourMetadata> ();
		}
		#endregion

		#region Properties
		public IList<MonoBehaviourMetadata> MonoBehaviours { get; set; }
		#endregion
	}
}

