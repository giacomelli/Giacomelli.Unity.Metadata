//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using NUnit.Framework;

//namespace Giacomelli.Unity.Metadata.Domain.UnitTests
//{
//    [TestFixture]
//    public class AutoImplementedPropertiesTest
//    {
//        [Test]
//        public void Properties_AutoImplemented_GetSet()
//        {
//            var assemblies = new Assembly[] { typeof(MaterialMetadata).Assembly, typeof(GameObjectExtensions).Assembly };
//            var entityTypes = new List<Type>();

//            foreach (var assembly in assemblies)
//            {
//                entityTypes.AddRange(assembly.GetTypes().Where(t => !t.ContainsGenericParameters && t.GetConstructor(new Type[0]) != null));
//            }

//            var propertiesCount = 0;

//            foreach (var entityType in entityTypes)
//            {
//                var instance = Activator.CreateInstance(entityType);
//                var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
//                    .Where(p => p.CanWrite && p.SetMethod.IsDefined(typeof(CompilerGeneratedAttribute)));

//                foreach (var p in properties)
//                {
//                    propertiesCount++;
//                    var expected = p.GetValue(instance);
//                    p.SetValue(instance, expected);
//                    var actual = p.GetValue(instance);
//                    Assert.AreEqual(expected, actual);
//                }

//            }

//            Assert.AreNotEqual(0, propertiesCount);
//        }
//    }
//}
