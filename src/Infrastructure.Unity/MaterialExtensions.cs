using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Unity;

namespace UnityEngine
{
	/// <summary>
	/// Material extensions.
	/// </summary>
    public static class MaterialExtensions
    {
		/// <summary>
		/// Adapt the Unity Material to IMaterial interface.
		/// </summary>
		/// <param name="material">Material.</param>
		public static IMaterial Adapt(this Material material)
        {
            return material == null ? null : new MaterialAdapter(material);
        }

		/// <summary>
		/// Adapt the Unity Materials to IMaterial interface.
		/// </summary>
		/// <param name="materials">Materials.</param>
		public static IEnumerable<IMaterial> Adapt(this IEnumerable<Material> materials)
        {
            return materials.Select(m => m.Adapt());
        }
    }
}
