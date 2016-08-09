using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Giacomelli.Unity.Metadata.Domain
{
    public class TypeService : ITypeService
    {
        private List<Type> s_types = new List<Type>();

        public TypeService(string assetsRootFolder)
        {
            s_types = new List<Type>(Assembly.GetExecutingAssembly().GetTypes());
            s_types.AddRange(typeof(UnityEngine.MonoBehaviour).Assembly.GetTypes());
            s_types.AddRange(typeof(UnityEngine.UI.Text).Assembly.GetTypes());
            var externalDlls = Directory.GetFiles(assetsRootFolder, "*.dll", SearchOption.AllDirectories);

            foreach (var dll in externalDlls)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    s_types.AddRange(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    var loaderMsg = new StringBuilder();

                    foreach (var l in ex.LoaderExceptions)
                    {
                        loaderMsg.AppendFormat("{0}:{1}", l.GetType().Name, l.Message);
                        loaderMsg.AppendLine();
                    }

                    throw new InvalidOperationException(
                        "Error trying to load assembly '{0}':{1}. LoaderExceptions: {2}".With(dll, ex.Message, loaderMsg),
                        ex);
                }
            }
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return s_types;
        }

        public Type GetType(string typeName)
        {
            var type = GetAllTypes().FirstOrDefault(t => t.FullName.Equals(typeName, StringComparison.OrdinalIgnoreCase));

            if (type == null)
            {
                throw new InvalidOperationException("Could not find a type '{0}'.".With(typeName));
            }

            return type;
        }
    }
}
