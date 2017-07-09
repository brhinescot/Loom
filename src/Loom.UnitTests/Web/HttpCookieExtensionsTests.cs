#region Using Directives

using System;
using System.Web;
using NUnit.Framework;

#endregion

namespace Loom.Web
{
    [TestFixture]
    public class HttpCookieExtensionsTests
    {
        private const string ExpectedName = "TestCookie";
        private const string ExpectedValue = "Today is a sort of OK day.";
        private const string ExpectedValue1 = "Today is a bad day.";
        private const string ExpectedValue2 = "Today is a awesome day.";
        private const string ExpectedValue3 = "Today is a Saturday.";
        private const string ExpectedDomain = "www.testdomain.com";
        private const bool ExpectedHttpOnly = true;
        private static readonly DateTime ExpectedExpires = DateTime.Parse("12/21/2012");

        [Test]
        public void EncryptAndDecrypt()
        {
            HttpCookie cookie = new HttpCookie(ExpectedName, ExpectedValue);
            cookie.Domain = ExpectedDomain;
            cookie.HttpOnly = ExpectedHttpOnly;
            cookie.Expires = ExpectedExpires;

            HttpCookie encrypted = cookie.Encrypt();
            Assert.AreNotEqual(ExpectedValue, encrypted.Value);
            Assert.AreEqual(ExpectedName, encrypted.Name);
            Assert.AreEqual(ExpectedDomain, encrypted.Domain);
            Assert.AreEqual(ExpectedExpires, encrypted.Expires);
            Assert.AreEqual(ExpectedHttpOnly, encrypted.HttpOnly);

            HttpCookie decrypted = encrypted.Decrypt();
            Assert.AreEqual(ExpectedValue, decrypted.Value);
            Assert.AreEqual(ExpectedName, decrypted.Name);
            Assert.AreEqual(ExpectedDomain, decrypted.Domain);
            Assert.AreEqual(ExpectedExpires, decrypted.Expires);
            Assert.AreEqual(ExpectedHttpOnly, decrypted.HttpOnly);
        }

        [Test]
        public void EncryptAndDecryptSubValues()
        {
            HttpCookie cookie = new HttpCookie(ExpectedName, ExpectedValue);
            cookie.Domain = ExpectedDomain;
            cookie.HttpOnly = ExpectedHttpOnly;
            cookie.Expires = ExpectedExpires;
            cookie.Values.Add("value1", ExpectedValue1);
            cookie.Values.Add("value2", ExpectedValue2);
            cookie.Values.Add("value3", ExpectedValue3);

            HttpCookie encrypted = cookie.Encrypt();
            Assert.AreNotEqual(ExpectedValue, encrypted.Value);
            Assert.AreEqual(ExpectedName, encrypted.Name);
            Assert.AreEqual(ExpectedDomain, encrypted.Domain);
            Assert.AreEqual(ExpectedExpires, encrypted.Expires);
            Assert.AreEqual(ExpectedHttpOnly, encrypted.HttpOnly);

            HttpCookie decrypted = encrypted.Decrypt();
            Assert.AreEqual(cookie.Value, decrypted.Value);
            Assert.AreEqual(ExpectedValue, decrypted.Values[0]);
            Assert.AreEqual(ExpectedValue1, decrypted.Values[1]);
            Assert.AreEqual(ExpectedValue2, decrypted.Values[2]);
            Assert.AreEqual(ExpectedValue3, decrypted.Values[3]);
            Assert.AreEqual(ExpectedName, decrypted.Name);
            Assert.AreEqual(ExpectedDomain, decrypted.Domain);
            Assert.AreEqual(ExpectedExpires, decrypted.Expires);
            Assert.AreEqual(ExpectedHttpOnly, decrypted.HttpOnly);
        }

        [Test]
        public void DifferentValuesPerEncryption()
        {
            HttpCookie cookie = new HttpCookie(ExpectedName, ExpectedValue);
            cookie.Domain = ExpectedDomain;
            cookie.HttpOnly = ExpectedHttpOnly;
            cookie.Expires = ExpectedExpires;

            HttpCookie encrypted1 = cookie.Encrypt();
            HttpCookie encrypted2 = cookie.Encrypt();
            Assert.AreNotEqual(encrypted1.Value, encrypted2.Value);

            HttpCookie decrypted1 = encrypted1.Decrypt();
            HttpCookie decrypted2 = encrypted2.Decrypt();
            Assert.AreEqual(decrypted1.Value, decrypted2.Value);
        }

        [Test]
        public void ExpireCookie()
        {
            HttpCookie cookie = new HttpCookie(ExpectedName, ExpectedValue);
            cookie.Domain = ExpectedDomain;
            cookie.HttpOnly = ExpectedHttpOnly;
            cookie.Expires = ExpectedExpires;

            cookie.Expire();

            Assert.LessOrEqual(DateTime.Now.AddDays(-1), cookie.Expires);
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void EncryptModifyAndDecrypt()
        {
            HttpCookie cookie = new HttpCookie(ExpectedName, ExpectedValue);
            cookie.Domain = ExpectedDomain;
            cookie.HttpOnly = ExpectedHttpOnly;
            cookie.Expires = ExpectedExpires;

            HttpCookie encrypted = cookie.Encrypt();
            encrypted.Value += "x";
            encrypted.Decrypt();
        }
    }
}