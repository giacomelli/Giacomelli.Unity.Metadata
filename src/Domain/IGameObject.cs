using System;
using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for a game object.
	/// </summary>
    public interface IGameObject
    {
		/// <summary>
		/// Verify if the game object has a component of the specified type.
		/// </summary>
		/// <returns><c>true</c>, if component was hased, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        bool HasComponent(Type type);

		/// <summary>
		/// Gets the materials.
		/// </summary>
		/// <returns>The materials.</returns>
        IEnumerable<IMaterial> GetMaterials();
    }
}
