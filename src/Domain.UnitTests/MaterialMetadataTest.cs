using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Giacomelli.Unity.Metadata.Domain.UnitTests
{
    [TestFixture]
    public class MaterialMetadataTest
    {
        [Test]
        public void Name_NullFullName_Null()
        {
            var target = new MaterialMetadata
            {
                FullName = null
            };

            Assert.IsNull(target.Name);
        }

        [Test]
        public void Name_NoDotsFullName_FullName()
        {
            var target = new MaterialMetadata
            {
                FullName = "Test"
            };

            Assert.AreEqual("Test", target.Name);
        }

        [Test]
        public void Name_WithDotsFullName_LastPart()
        {
            var target = new MaterialMetadata
            {
                FullName = "Test.Name"
            };

            Assert.AreEqual("Name", target.Name);
        }
    }
}
