#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Cryptography
{
    [TestFixture]
    public class CryptographyUtilityTests
    {
        [Test]
        public void CompareBytesPass()
        {
            byte[] first = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            byte[] second = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.AreEqual(first, second);
            Assert.IsTrue(CryptographyUtility.CompareBytes(first, second));
        }

        [Test]
        public void CompareBytesFail()
        {
            byte[] first = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            byte[] second = {1, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.AreNotEqual(first, second);
            Assert.IsFalse(CryptographyUtility.CompareBytes(first, second));
        }

        [Test]
        public void CompareBytesFailWithNull()
        {
            byte[] bytes = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.IsFalse(CryptographyUtility.CompareBytes(bytes, null));
            Assert.IsFalse(CryptographyUtility.CompareBytes(null, bytes));
            Assert.IsFalse(CryptographyUtility.CompareBytes(null, null));
        }

        [Test]
        public void CombineBytes()
        {
            byte[] first = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            byte[] second = {9, 8, 7, 6, 5, 4, 3, 2, 1, 0};

            byte[] expected = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0};
            byte[] actual = CryptographyUtility.CombineBytes(first, second);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRandomBytes()
        {
            int expected = 24;
            byte[] random = CryptographyUtility.GetRandomBytes(expected);
            Assert.AreEqual(expected, random.Length);
        }

        [Test]
        public void ZeroOutBytes()
        {
            byte[] bytes = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            byte[] expected = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            CryptographyUtility.ZeroOutBytes(bytes);
            Assert.AreEqual(expected, bytes);
        }
    }
}