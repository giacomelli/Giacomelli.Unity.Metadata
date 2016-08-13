using System;
using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface ITypeService
    {
        IEnumerable<Type> GetTypes();

        Type GetTypeByName(string typeName);

        string GetGuid(Type type);
    }
}
