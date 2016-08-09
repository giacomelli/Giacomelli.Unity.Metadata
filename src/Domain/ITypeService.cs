using System;
using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface ITypeService
    {
        IEnumerable<Type> GetAllTypes();

        Type GetType(string typeName);
    }
}
