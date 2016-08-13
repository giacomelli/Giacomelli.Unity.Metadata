using Giacomelli.Unity.Metadata.Domain;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Unity
{
    public class MaterialAdapter : IMaterial
    {
        private Material m_material;

        public MaterialAdapter(Material material)
        {
            m_material = material;
        }

        public string Name
        {
            get
            {
                return m_material.name;
            }
        }
    }
}
