#region Using Directives

using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

#endregion

namespace Loom.Cryptography
{
    [TestFixture]
    public class SymmetricCryptographyProviderTests
    {
        [Test]
        public void CreateDESFromASCIIEncodedBytes()
        {
            using (SymmetricCryptographyProvider provider = SymmetricCryptographyProvider.CreateDES(Util.GetRandomBytes(8)))
            {
                Encoding encoder = Encoding.ASCII;

                byte[] encrypted = provider.Encrypt(encoder.GetBytes("ThisIsATest"));
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = encoder.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateDESFromUTF8EncodedBytes()
        {
            using (SymmetricCryptographyProvider provider = SymmetricCryptographyProvider.CreateDES(Util.GetRandomBytes(8)))
            {
                Encoding encoder = Encoding.UTF8;

                byte[] encrypted = provider.Encrypt(encoder.GetBytes("ThisIsATest"));
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = encoder.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateDESFromString()
        {
            using (SymmetricCryptographyProvider provider = new SymmetricCryptographyProvider("System.Security.Cryptography.DESCryptoServiceProvider", Util.GetRandomBytes(8)))
            {
                byte[] encrypted = provider.Encrypt("ThisIsATest");
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.UTF8.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateRC2()
        {
            using (SymmetricCryptographyProvider provider = SymmetricCryptographyProvider.CreateRC2(Util.GetRandomBytes(16)))
            {
                byte[] encrypted = provider.Encrypt(Encoding.ASCII.GetBytes("ThisIsATest"));
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.ASCII.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateRC2FromString()
        {
            using (SymmetricCryptographyProvider provider = new SymmetricCryptographyProvider("System.Security.Cryptography.RC2CryptoServiceProvider", Util.GetRandomBytes(16)))
            {
                byte[] encrypted = provider.Encrypt("ThisIsATest");
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.ASCII.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateTripleDES()
        {
            using (SymmetricCryptographyProvider provider = SymmetricCryptographyProvider.TripleDES(Util.GetRandomBytes(24)))
            {
                byte[] encrypted = provider.Encrypt(Encoding.ASCII.GetBytes("ThisIsATest"));
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.ASCII.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateTripleDESFromString()
        {
            using (SymmetricCryptographyProvider provider = new SymmetricCryptographyProvider("System.Security.Cryptography.TripleDESCryptoServiceProvider", Util.GetRandomBytes(24)))
            {
                byte[] encrypted = provider.Encrypt("ThisIsATest");
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.ASCII.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateRijndael()
        {
            using (SymmetricCryptographyProvider provider = SymmetricCryptographyProvider.CreateRijndael(Util.GetRandomBytes(32)))
            {
                byte[] encrypted = provider.Encrypt(Encoding.ASCII.GetBytes("ThisIsATest"));
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.ASCII.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        public void CreateRijndaelFromString()
        {
            using (SymmetricCryptographyProvider provider = new SymmetricCryptographyProvider("System.Security.Cryptography.RijndaelManaged", Util.GetRandomBytes(32)))
            {
                byte[] encrypted = provider.Encrypt("ThisIsATest");
                byte[] decrypted = provider.Decrypt(encrypted);
                string decryptedString = Encoding.ASCII.GetString(decrypted);
                Assert.AreEqual("ThisIsATest", decryptedString);
            }
        }

        [Test]
        [ExpectedException(typeof(CryptographicException))]
        public void FailCreateFromInvalidString()
        {
            new SymmetricCryptographyProvider("System.Security.Cryptography.InvalidAlg", Util.GetRandomBytes(32));
        }
    }
}