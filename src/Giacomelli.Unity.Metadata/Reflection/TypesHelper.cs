using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace Giacomelli.Unity.Metadata
{
	public static class TypesHelper
	{
		private static List<Type> s_types = new List<Type>();

		public static void Initialize(string assetsRootFolder)
		{
			s_types = new List<Type>(Assembly.GetExecutingAssembly().GetTypes());
			s_types.AddRange(typeof(UnityEngine.MonoBehaviour).Assembly.GetTypes());
			s_types.AddRange(typeof(UnityEngine.UI.Text).Assembly.GetTypes());
			var externalDlls = Directory.GetFiles(assetsRootFolder, "*.dll", SearchOption.AllDirectories);

			foreach (var dll in externalDlls)
			{
				try
				{
					var assembly = Assembly.LoadFile(dll);
					s_types.AddRange(assembly.GetTypes());
				}
				catch (ReflectionTypeLoadException ex)
				{
					throw new InvalidOperationException(
						"Error trying to load assembly '{0}':{1}".With(dll, ex.Message),
						ex);
				}
			}
		}

		public static IList<Type> GetAllTypes()
		{
			return s_types;
		}

		public static Type GetType(string typeName)
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

