using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;
using NUnit.Framework;
using Rhino.Mocks;
using UnityEngine;
using UnityEngine.UI;

namespace Giacomelli.Unity.Metadata.Domain.UnitTests
{
    [TestFixture]
    public class TypeServiceTest
    {
        #region Fields
        private IFileSystem m_fileSystem;
        private IAssemblyLoader m_assemblyLoader;
        private TypeService m_target;
        #endregion

        [SetUp]
        public void Initialize()
        {
            m_fileSystem = MockRepository.GenerateMock<IFileSystem>();
            m_fileSystem.Expect(f => f.GetFiles(string.Empty, "*.dll", true)).Return(new string[] { "Giacomelli.Unity.Metadata.Domain.UnitTests.dll" });
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giacomelli.Unity.Metadata.Domain.UnitTests.dll");
            m_fileSystem.Expect(f => f.GetFullPath("Giacomelli.Unity.Metadata.Domain.UnitTests.dll")).Return(fullPath);
            m_assemblyLoader = MockRepository.GenerateMock<IAssemblyLoader>();
            m_assemblyLoader.Expect(a => a.LoadFrom(null)).IgnoreArguments().WhenCalled(m =>
            {
                m.ReturnValue = Assembly.LoadFrom(m.Arguments[0] as string);
            }).Return(null);
            m_target = new TypeService(m_fileSystem, m_assemblyLoader);
        }

        [Test]
        public void GetTypes_NoArgs_CurrentAssemblyUnityAndDllAllTypes()
        {
            var actual = m_target.GetTypes();
            Assert.IsNotNull(actual);
            Assert.Less(1000, actual.Count());
        }

        [Test]
        public void GetTypes_LoadAssemblyFailed_Exception()
        {
            var fs = MockRepository.GenerateMock<IFileSystem>();
            fs.Expect(f => f.GetFiles(string.Empty, "*.dll", true)).Return(new string[] { "Giacomelli.Unity.Metadata.Domain.UnitTests.dll" });
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nunit.framework.dll");
            fs.Expect(f => f.GetFullPath("Giacomelli.Unity.Metadata.Domain.UnitTests.dll")).Return(fullPath);
            var assemblyLoader = MockRepository.GenerateMock<IAssemblyLoader>();
            assemblyLoader.Expect(a => a.LoadFrom(fullPath)).Throw(new ReflectionTypeLoadException(new Type[] { typeof(int) }, new Exception[] { new Exception("TEST") }));

            var target = new TypeService(fs, assemblyLoader);

            Assert.Catch<InvalidOperationException>(delegate
            {
                target.GetTypes();
            });
        }

        [Test]
        public void GetType_InvalidType_Excpetion()
        {
            Assert.Catch<InvalidOperationException>(delegate
            {
                m_target.GetTypeByName("xpto");
            });
        }

        [Test]
        public void GetTypeByName_TypeName_Type()
        {
            var actual = m_target.GetTypeByName("Giacomelli.Unity.Metadata.Domain.UnitTests.TypeServiceTest");
            Assert.AreEqual(typeof(TypeServiceTest), actual);

            actual = m_target.GetTypeByName("UnityEngine.GameObject");
            Assert.AreEqual(typeof(GameObject), actual);

            actual = m_target.GetTypeByName("UnityEngine.UI.InputField");
            Assert.AreEqual(typeof(InputField), actual);
        }

        [Test]
        public void GetGuid_TypeNotFound_Exception()
        {
            m_fileSystem.Expect(f => f.GetFiles(string.Empty, "String.cs.meta", true)).Return(new string[0]);
            m_fileSystem.Expect(f => f.GetFileNameWithoutExtension(null)).IgnoreArguments().Return("xpto");
            m_fileSystem.Expect(f => f.GetFiles(string.Empty, "xpto.dll.meta", true)).Return(new string[0]);

            Assert.Catch<InvalidOperationException>(delegate
            {
                m_target.GetGuid(typeof(String));
            });
        }

        [Test]
        public void GetGuid_TypeWithFile_FileGuid()
        {
            m_fileSystem.Expect(f => f.GetFiles(string.Empty, "String.cs.meta", true)).Return(new string[] { "String.cs.meta" });
            m_fileSystem.Expect(f => f.ReadAllText("String.cs.meta")).Return("guid: testguid abc");
            var actual = m_target.GetGuid(typeof(String));
            Assert.AreEqual("testguid", actual);
        }

        [Test]
        public void GetGuid_TypeInsideDll_DllGuid()
        {
            m_fileSystem.Expect(f => f.GetFiles(string.Empty, "String.cs.meta", true)).Return(new string[0]);
            m_fileSystem.Expect(f => f.GetFileNameWithoutExtension(null)).IgnoreArguments().Return("xpto");
            m_fileSystem.Expect(f => f.GetFiles(string.Empty, "xpto.dll.meta", true)).Return(new string[] { "xpto.dll.meta" });
            m_fileSystem.Expect(f => f.ReadAllText("xpto.dll.meta")).Return("guid: testguid abc");
            var actual = m_target.GetGuid(typeof(String));
            Assert.AreEqual("testguid", actual);
        }
    }
}
