using Giacomelli.Unity.Metadata.Domain;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Unity
{
	/// <summary>
	/// Material adaptet that adapts a Unity Material to IMaterial interface.
	/// </summary>
	public class MaterialAdapter : IMaterial
    {
        private Material m_material;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Giacomelli.Unity.Metadata.Infrastructure.Unity.MaterialAdapter"/> class.
		/// </summary>
		/// <param name="material">Material.</param>
        public MaterialAdapter(Material material)
        {
            m_material = material;
        }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
        public string Name
        {
            get
            {
                return m_material.name;
            }
        }
    }
}
