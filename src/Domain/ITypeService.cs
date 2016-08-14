using System;
using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for a type service.
	/// </summary>
    public interface ITypeService
    {
		/// <summary>
		/// Gets the types.
		/// </summary>
		/// <returns>The types.</returns>
        IEnumerable<Type> GetTypes();

		/// <summary>
		/// Gets a type by the name.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="typeName">Type name.</param>
        Type GetTypeByName(string typeName);

		/// <summary>
		/// Gets the GUID.
		/// </summary>
		/// <returns>The GUID.</returns>
		/// <param name="type">Type.</param>
        string GetGuid(Type type);
    }
}
