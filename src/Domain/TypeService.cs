using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;

namespace Giacomelli.Unity.Metadata.Domain
{
    public class TypeService : ITypeService
    {
        private static Regex s_guidRegex = new Regex(@"guid: (\S+)", RegexOptions.Compiled);
        private readonly IFileSystem m_fs;
        private readonly IAssemblyLoader m_assemblyLoader;
        private List<Type> s_types;
        
        public TypeService(IFileSystem fileSystem, IAssemblyLoader assemblyLoader)
        {
            m_fs = fileSystem;
            m_assemblyLoader = assemblyLoader;
        }

        public IEnumerable<Type> GetTypes()
        {
            if (s_types == null)
            {
                s_types = new List<Type>(Assembly.GetExecutingAssembly().GetTypes());
                s_types.AddRange(typeof(UnityEngine.MonoBehaviour).Assembly.GetTypes());
                s_types.AddRange(typeof(UnityEngine.UI.Text).Assembly.GetTypes());
                var externalDlls = m_fs.GetFiles("*.dll");

                foreach (var dll in externalDlls)
                {
                    try
                    {
                        var assembly = m_assemblyLoader.LoadFrom(m_fs.GetFullPath(dll));
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

            return s_types;
        }

        public Type GetTypeByName(string typeName)
        {
            var type = GetTypes().FirstOrDefault(t => t.FullName.Equals(typeName, StringComparison.OrdinalIgnoreCase));

            if (type == null)
            {
                throw new InvalidOperationException("Could not find a type '{0}'.".With(typeName));
            }

            return type;
        }

        public string GetGuid(Type type)
        {
            var metaFile = m_fs.GetFiles("{0}.cs.meta".With(type.Name)).FirstOrDefault();

            if (metaFile == null)
            {
                var assemblyName = m_fs.GetFileNameWithoutExtension(type.Assembly.CodeBase);
                metaFile = m_fs.GetFiles("{0}.dll.meta".With(assemblyName)).FirstOrDefault();

                if (metaFile == null)
                {
                    throw new InvalidOperationException("Could not find .meta file of type '{0}'.".With(type.Name));
                }
            }

            var content = m_fs.ReadAllText(metaFile);
            return s_guidRegex.Match(content).Groups[1].Value;
        }
    }
}
