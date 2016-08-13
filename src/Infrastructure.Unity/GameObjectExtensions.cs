using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Unity;

namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        public static IGameObject Adapt(this GameObject gameObject)
        {
            return gameObject == null ? null : new GameObjectAdapter(gameObject);
        }

        public static IEnumerable<IGameObject> Adapt(this IEnumerable<GameObject> gameObjects)
        {
            return gameObjects.Select(m => m.Adapt());
        }
    }
}
