#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Cryptography
{
    /// <summary>
    ///     Summary description for CryptoTests.
    /// </summary>
    [TestFixture]
    public class HashProviderTests
    {
        [TestCase("Hello there, How are you? I am fine.")]
        [TestCase("Hello there, How are you? I am fine!")]
        [TestCase("Hello-there,-How-are-you?-I-am-fine!")]
        [TestCase("")]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void SHA512Test(string input)
        {
            using (HashProvider oneWayCrypto = HashProvider.SHA512)
            {
                string result = oneWayCrypto.GenerateString(input);
                string result2 = oneWayCrypto.GenerateString(input);
                Assert.IsNotNull(result);
                Assert.AreEqual(128, result.Length);
                Assert.AreEqual(-1, result.IndexOf('-'));
                Assert.AreEqual(result, result2);
                Util.WL(result);
            }
        }

        [TestCase("Hello there, How are you? I am fine.")]
        [TestCase("Hello there, How are you? I am fine!")]
        [TestCase("Hello-there,-How-are-you?-I-am-fine!")]
        [TestCase("")]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void SHA384Test(string input)
        {
            using (HashProvider oneWayCrypto = HashProvider.SHA384)
            {
                string result = oneWayCrypto.GenerateString(input);
                string result2 = oneWayCrypto.GenerateString(input);
                Assert.IsNotNull(result);
                Assert.AreEqual(96, result.Length);
                Assert.AreEqual(-1, result.IndexOf('-'));
                Assert.AreEqual(result, result2);
                Util.WL(result);
            }
        }

        [TestCase("Hello there, How are you? I am fine.")]
        [TestCase("Hello there, How are you? I am fine!")]
        [TestCase("Hello-there,-How-are-you?-I-am-fine!")]
        [TestCase("")]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void SHA256Test(string input)
        {
            using (HashProvider oneWayCrypto = HashProvider.SHA256)
            {
                string result = oneWayCrypto.GenerateString(input);
                string result2 = oneWayCrypto.GenerateString(input);
                Assert.IsNotNull(result);
                Assert.AreEqual(64, result.Length);
                Assert.AreEqual(-1, result.IndexOf('-'));
                Assert.AreEqual(result, result2);
                Util.WL(result);
            }
        }

        [TestCase("Hello there, How are you? I am fine.")]
        [TestCase("Hello there, How are you? I am fine!")]
        [TestCase("Hello-there,-How-are-you?-I-am-fine!")]
        [TestCase("")]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void MD5Test(string input)
        {
            using (HashProvider oneWayCrypto = HashProvider.MD5)
            {
                string result = oneWayCrypto.GenerateString(input);
                string result2 = oneWayCrypto.GenerateString(input);
                Assert.IsNotNull(result);
                Assert.AreEqual(32, result.Length);
                Assert.AreEqual(-1, result.IndexOf('-'));
                Assert.AreEqual(result, result2);
                Util.WL(result);
            }
        }

        [TestCase("starstarstar", "33454345")]
        [TestCase("starstarstar", "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG")]
        [TestCase(null, "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("starstarstar", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, null, ExpectedException = typeof(ArgumentNullException))]
        public void MD5SaltEncryptAndCompare(string password, string salt)
        {
            using (HashProvider oneWayCrypto = HashProvider.MD5)
            {
                string encrypted = oneWayCrypto.GenerateString(password, salt);
                Assert.IsTrue(oneWayCrypto.Compare(password, encrypted, salt.Length));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length - 1));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length + 1));
                Assert.IsFalse(oneWayCrypto.Compare(password + "x", encrypted, salt.Length));
            }
        }

        [TestCase("starstarstar", "33454345")]
        [TestCase("starstarstar", "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG")]
        [TestCase(null, "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("starstarstar", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, null, ExpectedException = typeof(ArgumentNullException))]
        public void SHA512SaltEncryptAndCompare(string password, string salt)
        {
            using (HashProvider oneWayCrypto = HashProvider.SHA512)
            {
                string encrypted = oneWayCrypto.GenerateString(password, salt);
                Assert.IsTrue(oneWayCrypto.Compare(password, encrypted, salt.Length));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length - 1));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length + 1));
                Assert.IsFalse(oneWayCrypto.Compare(password + "x", encrypted, salt.Length));
            }
        }

        [TestCase("starstarstar", "33454345")]
        [TestCase("starstarstar", "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG")]
        [TestCase(null, "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("starstarstar", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, null, ExpectedException = typeof(ArgumentNullException))]
        public void SHA384SaltEncryptAndCompare(string password, string salt)
        {
            using (HashProvider oneWayCrypto = HashProvider.SHA384)
            {
                string encrypted = oneWayCrypto.GenerateString(password, salt);
                Assert.IsTrue(oneWayCrypto.Compare(password, encrypted, salt.Length));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length - 1));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length + 1));
                Assert.IsFalse(oneWayCrypto.Compare(password + "x", encrypted, salt.Length));
            }
        }

        [TestCase("starstarstar", "33454345")]
        [TestCase("starstarstar", "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG")]
        [TestCase(null, "ARSYRAUFUYUYFUFSUYFKHUHFIUGDIGIGSIUGSIUG", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("starstarstar", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(null, null, ExpectedException = typeof(ArgumentNullException))]
        public void SHA256SaltEncryptAndCompare(string password, string salt)
        {
            using (HashProvider oneWayCrypto = HashProvider.SHA256)
            {
                string encrypted = oneWayCrypto.GenerateString(password, salt);
                Assert.IsTrue(oneWayCrypto.Compare(password, encrypted, salt.Length));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length - 1));
                Assert.IsFalse(oneWayCrypto.Compare(password, encrypted, salt.Length + 1));
                Assert.IsFalse(oneWayCrypto.Compare(password + "x", encrypted, salt.Length));
            }
        }
    }
}