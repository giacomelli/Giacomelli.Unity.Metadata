using System;
using Giacomelli.Unity.Metadata.Infrastructure.Framework.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.UnitTests.IO
{
    [TestFixture]
    public class FileSystemExtensionsTest
    {
        [Test]
        public void GetFiles_SearchPatternAndRecursive_RootPath()
        {
            var fs = MockRepository.GenerateMock<IFileSystem>();
            fs.Expect(f => f.GetFiles(String.Empty, "*.test", true)).Return(new string[] { "test" });

            var actual = FileSystemExtensions.GetFiles(fs, "*.test", true);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("test", actual[0]);

            fs.VerifyAllExpectations();
        }

        [Test]
        public void GetFiles_SearchPattern_RootPathAndRecursive()
        {
            var fs = MockRepository.GenerateMock<IFileSystem>();
            fs.Expect(f => f.GetFiles(String.Empty, "*.test", true)).Return(new string[] { "test" });

            var actual = FileSystemExtensions.GetFiles(fs, "*.test");
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("test", actual[0]);

            fs.VerifyAllExpectations();
        }

        [Test]
        public void GetFiles_PathAndSearchPattern_Recursive()
        {
            var fs = MockRepository.GenerateMock<IFileSystem>();
            fs.Expect(f => f.GetFiles("path1", "*.test", true)).Return(new string[] { "test" });

            var actual = FileSystemExtensions.GetFiles(fs, "path1", "*.test");
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("test", actual[0]);

            fs.VerifyAllExpectations();
        }

        [Test]
        public void CopyFile_SourceAndDest_Overwrite()
        {
            var fs = MockRepository.GenerateMock<IFileSystem>();
            fs.Expect(f => f.CopyFile("path1", "path2", true));

            FileSystemExtensions.CopyFile(fs, "path1", "path2");

            fs.VerifyAllExpectations();
        }
    }
}
