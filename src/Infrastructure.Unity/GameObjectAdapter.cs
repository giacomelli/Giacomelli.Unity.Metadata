using System;
using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Unity
{
	/// <summary>
	/// Game object adapter that adapts a Unity GameObject to IGameObject interface.
	/// </summary>
    public class GameObjectAdapter : IGameObject
    {
        private GameObject m_go;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Giacomelli.Unity.Metadata.Infrastructure.Unity.GameObjectAdapter"/> class.
		/// </summary>
		/// <param name="gameObject">Game object.</param>
        public GameObjectAdapter(GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException("gameObject");
            }

            m_go = gameObject;
        }

		/// <summary>
		/// Gets the materials.
		/// </summary>
		/// <returns>The materials.</returns>
        public IEnumerable<IMaterial> GetMaterials()
        {
            return m_go.GetComponents<Renderer>()
                .SelectMany(r => r.sharedMaterials)
                .Adapt();
        }

		/// <summary>
		/// Hases the component.
		/// </summary>
		/// <returns><c>true</c>, if component was hased, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public bool HasComponent(Type type)
        {
            return m_go.GetComponentInChildren(type) != null;
        }
    }
}
