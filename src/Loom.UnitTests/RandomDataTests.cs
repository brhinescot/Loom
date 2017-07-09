#region Using Directives

using System;
using Loom.Data;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class RandomDataTests
    {
        [Test]
        public void EmailGenerationMany()
        {
            const int count = 100;
            int i = 0;
            foreach (string s in RandomData.Email.Generate(count))
            {
                Console.WriteLine(s);
                Assert.IsNotNull(s);
                Assert.AreEqual(23, s.Length);
                i++;
            }
            Assert.AreEqual(count, i);
        }

        [Test]
        public void EmailGeneration()
        {
            string s = RandomData.Email.Generate();
            Assert.IsNotNull(s);
            Assert.AreEqual(23, s.Length);
        }

        [Test]
        public void SsnGenerationMany()
        {
            int count = 100;
            int i = 0;
            foreach (string s in RandomData.SocialSecurityNumber.Generate(count))
            {
                Assert.IsNotNull(s);
                Assert.AreEqual(11, s.Length);
                i++;
            }
            Assert.AreEqual(count, i);
        }

        [Test]
        public void SsnGeneration()
        {
            string s = RandomData.SocialSecurityNumber.Generate();
            Assert.IsNotNull(s);
            Assert.AreEqual(11, s.Length);
        }

        [Test]
        public void PhoneGenerationMany()
        {
            int count = 100;
            int i = 0;
            foreach (string s in RandomData.PhoneNumber.Generate(count))
            {
                Assert.IsNotNull(s);
                Assert.AreEqual(13, s.Length);
                i++;
            }
            Assert.AreEqual(count, i);
        }

        [Test]
        public void PhoneGeneration()
        {
            string s = RandomData.PhoneNumber.Generate();
            Assert.IsNotNull(s);
            Assert.AreEqual(13, s.Length);
        }

        [Test]
        public void StreetGenerationMany()
        {
            int count = 100;
            int i = 0;
            foreach (string s in RandomData.Street.Generate(count))
            {
                Assert.IsNotNull(s);
                Assert.AreEqual(28, s.Length);
                i++;
            }
            Assert.AreEqual(count, i);
        }

        [Test]
        public void StreetGeneration()
        {
            string s = RandomData.Street.Generate();
            Assert.IsNotNull(s);
            Assert.AreEqual(28, s.Length);
        }
    }
}