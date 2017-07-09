#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

#endregion

namespace Loom.IO
{
    [TestFixture]
    public class ArrayFileTests
    {
        private string fileName;

        [SetUp]
        public void SetUpFile()
        {
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ArrayFileTest.txt");
            string[] contents = {"11111", "22222", "33333", "44444", "# Comment", "55555"};
            EnumerableFile.Write(contents, fileName);
        }

        [TearDown]
        public void RemoveFile()
        {
            File.Delete(fileName);
        }

        [Test]
        public void WriteFileFromArray()
        {
            string writeFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ArrayFileWriteTest.txt");
            string[] contents = {"1", "2", "3", "4", "5"};
            EnumerableFile.Write(contents, writeFileName);
            Assert.IsTrue(File.Exists(writeFileName));

            int count = 0;
            foreach (string line in EnumerableFile.ReadToEnd(writeFileName))
                count++;
            Assert.AreEqual(5, count);
        }

        [Test]
        public void WriteFileFromList()
        {
            string writeFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ArrayFileWriteTest.txt");
            List<string> contents = new List<string>();
            contents.Add("1");
            contents.Add("2");
            contents.Add("3");
            contents.Add("4");
            contents.Add("5");

            EnumerableFile.Write(contents, writeFileName);
            Assert.IsTrue(File.Exists(writeFileName));

            int count = 0;
            foreach (string line in EnumerableFile.ReadToEnd(writeFileName))
                count++;
            Assert.AreEqual(5, count);
        }

        [Test]
        public void ReadFile()
        {
            int count = 0;
            foreach (string line in EnumerableFile.ReadToEnd(fileName))
                count++;
            Assert.AreEqual(6, count);
        }

        [Test]
        public void ReadFileWithComment()
        {
            int count = 0;
            foreach (string line in EnumerableFile.ReadToEnd(fileName, "#"))
                count++;
            Assert.AreEqual(5, count);
        }
    }
}