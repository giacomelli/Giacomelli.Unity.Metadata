using System;
using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using UnityEngine;

namespace Giacomelli.Unity.Metadata.Infrastructure.Unity
{
    public class GameObjectAdapter : IGameObject
    {
        private GameObject m_go;

        public GameObjectAdapter(GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException("gameObject");
            }

            m_go = gameObject;
        }

        public IEnumerable<IMaterial> GetMaterials()
        {
            return m_go.GetComponents<Renderer>()
                .SelectMany(r => r.sharedMaterials)
                .Adapt();
        }

        public bool HasComponent(Type type)
        {
            return m_go.GetComponentInChildren(type) != null;
        }
    }
}
