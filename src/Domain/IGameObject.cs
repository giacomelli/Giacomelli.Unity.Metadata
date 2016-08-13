using System;
using System.Collections.Generic;

namespace Giacomelli.Unity.Metadata.Domain
{
    public interface IGameObject
    {
        bool HasComponent(Type type);

        IEnumerable<IMaterial> GetMaterials();
    }
}
