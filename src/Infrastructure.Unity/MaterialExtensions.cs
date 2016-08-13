using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Unity;

namespace UnityEngine
{
    public static class MaterialExtensions
    {
        public static IMaterial Adapt(this Material material)
        {
            return material == null ? null : new MaterialAdapter(material);
        }

        public static IEnumerable<IMaterial> Adapt(this IEnumerable<Material> materials)
        {
            return materials.Select(m => m.Adapt());
        }
    }
}
