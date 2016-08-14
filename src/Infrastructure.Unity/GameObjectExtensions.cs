using System.Collections.Generic;
using System.Linq;
using Giacomelli.Unity.Metadata.Domain;
using Giacomelli.Unity.Metadata.Infrastructure.Unity;

namespace UnityEngine
{
	/// <summary>
	/// Game object extensions.
	/// </summary>
    public static class GameObjectExtensions
    {
		/// <summary>
		/// Adapt the Unity GameObject to IGameObject interface.
		/// </summary>
		/// <param name="gameObject">Game object.</param>
        public static IGameObject Adapt(this GameObject gameObject)
        {
            return gameObject == null ? null : new GameObjectAdapter(gameObject);
        }

		/// <summary>
		/// Adapt the Unity GameObjects to IGameObject interface.
		/// </summary>
		/// <param name="gameObjects">Game objects.</param>
		public static IEnumerable<IGameObject> Adapt(this IEnumerable<GameObject> gameObjects)
        {
            return gameObjects.Select(m => m.Adapt());
        }
    }
}
