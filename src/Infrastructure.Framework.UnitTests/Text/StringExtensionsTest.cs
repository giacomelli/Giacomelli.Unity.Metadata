using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.UnitTests.Text
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void With_Args_Formatted()
        {
            var actual = "x: {0}, y: {1}".With(1, 2);
            Assert.AreEqual("x: 1, y: 2", actual);
        }
    }
}
