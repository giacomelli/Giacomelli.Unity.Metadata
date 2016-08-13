using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace Giacomelli.Unity.Metadata.Domain.UnitTests
{
    [TestFixture]
    public class ScriptMetadataServiceTest
    {
        #region Fields
        private ITypeService m_typeService;
        private ScriptMetadataService m_target;
        #endregion

        [SetUp]
        public void Initialize()
        {
            m_typeService = MockRepository.GenerateMock<ITypeService>();
            m_typeService.Expect(t => t.GetTypes()).Return(new Type[]
            {
                typeof(StringExtensions),
                typeof(ScriptMetadataService)
            });

            m_target = new ScriptMetadataService(m_typeService);
        }

        [Test]
        public void GetAllScripts_NoArgs_AllScriptsFromTypes()
        {
            var actual = m_target.GetAllScripts();
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count);
        }

        [Test]
        public void GetFullNameByFileId_InvalidFileId_Null()
        {
            var actual = m_target.GetFullNameByFileId(0);
            Assert.IsNull(actual);
        }

        [Test]
        public void GetFullNameByFileId_ValidFileId_FullName()
        {
            var actual = m_target.GetFullNameByFileId(-650288904);
            Assert.AreEqual("StringExtensions", actual);

            actual = m_target.GetFullNameByFileId(455244132);
            Assert.AreEqual("Giacomelli.Unity.Metadata.Domain.ScriptMetadataService", actual);
        }
    }
}
