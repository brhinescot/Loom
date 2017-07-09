#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Loom.Annotations;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal
{
    internal sealed class AntiForgeryData
    {
        private const string TokenName = "__aft";

        private static readonly RNGCryptoServiceProvider Rng = new RNGCryptoServiceProvider();

        private AntiForgeryData() { }

        private string CookieValue { get; set; }
        private string FormToken { get; set; }

        [NotNull]
        private string Value { get; set; }

        [NotNull]
        public static string GetTokenName([CanBeNull] string appPath)
        {
            return GetTokenNamePrivate(appPath);
        }

        [NotNull]
        public static string GetDataAndSetCookie([CanBeNull] string salt, [CanBeNull] string domain, [CanBeNull] string path)
        {
            return GetDataAndSetCookiePrivate(salt, domain, path);
        }

        [NotNull]
        public static string GetDataFromCookie([NotNull] HttpCookie cookie, [CanBeNull] string salt = null, [CanBeNull] string domain = null, [CanBeNull] string path = null)
        {
            Argument.Assert.IsNotNull(cookie, nameof(cookie));

            return GetDataFromCookiePrivate(cookie, salt, domain, path);
        }

        public static void ValidateJsonRequest([NotNull] IDictionary<string, object> data, [CanBeNull] string salt)
        {
            Argument.Assert.IsNotNull(data, nameof(data));

            ValidateJsonRequestPrivate(data, salt);
        }

        private static string GetTokenNamePrivate(string appPath)
        {
            return Compare.IsNullOrEmpty(appPath) ? TokenName : TokenName + "_" + appPath;
        }

        private static string GetDataAndSetCookiePrivate(string salt, string domain, string path)
        {
            AntiForgeryData data = CreateEncryptedToken(salt);
            HttpCookie cookie = new HttpCookie(GetTokenName(HttpContext.Current.Request.ApplicationPath), data.CookieValue) {HttpOnly = true, Domain = domain};
            if (!Compare.IsNullOrEmpty(path))
                cookie.Path = path;
            HttpContext.Current.Response.Cookies.Set(cookie);

            return data.FormToken;
        }

        private static string GetDataFromCookiePrivate(HttpCookie cookie, string salt, string domain, string path)
        {
            try
            {
                AntiForgeryData data = DecryptCookie(cookie.Value, salt);
                return data.FormToken;
            }
            catch (HttpAntiForgeryException)
            {
                return GetDataAndSetCookie(salt, domain, path);
            }
        }

        private static void ValidateJsonRequestPrivate(IDictionary<string, object> data, string salt)
        {
            if (!data.ContainsKey(TokenName))
                throw new HttpAntiForgeryException();

            string formToken = data[TokenName] as string;
            if (Compare.IsNullOrEmpty(formToken))
                throw new HttpAntiForgeryException();

            HttpCookie cookie = HttpContext.Current.Request.Cookies[GetTokenName(HttpContext.Current.Request.ApplicationPath)];
            if (cookie == null)
                throw new HttpAntiForgeryException();

            AntiForgeryData cookieData = DecryptCookie(cookie.Value, salt);
            AntiForgeryData formData = DecryptForm(formToken, salt);
            if (!string.Equals(cookieData.Value, formData.Value))
                throw new HttpAntiForgeryException();
        }

        private static AntiForgeryData CreateEncryptedToken(string salt)
        {
            byte[] valueData = new byte[0x10];
            byte[] systemSaltData = new byte[0x5];
            AntiForgeryData token = new AntiForgeryData();
            Triplet triplet = new Triplet();
            ObjectStateFormatter formatter = new ObjectStateFormatter();

            try
            {
                Rng.GetBytes(valueData);
                Rng.GetBytes(systemSaltData);

                triplet.First = Convert.ToBase64String(valueData);
                triplet.Third = Convert.ToBase64String(systemSaltData);

                byte[] cookieBytes;

                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, triplet);
                    cookieBytes = stream.ToArray();
                }

                token.CookieValue = MachineKey.Protect(cookieBytes, "Authentication token").ToHexString();
//                token.CookieValue = MachineKey.Encode(cookieBytes, MachineKeyProtection.All);

                Rng.GetBytes(systemSaltData);

                triplet.Second = salt;
                triplet.Third = Convert.ToBase64String(systemSaltData);

                token.FormToken = MachineKey.Protect(Encoding.UTF8.GetBytes(formatter.Serialize(triplet)), "Authentication token").ToHexString();
//                token.FormToken = MachineKey.Encode(Encoding.UTF8.GetBytes(formatter.Serialize(triplet)), MachineKeyProtection.All);
                token.Value = (string) triplet.First;

                return token;
            }
            catch (Exception)
            {
                throw new HttpAntiForgeryException();
            }
        }

        private static AntiForgeryData DecryptCookie(string value, string salt)
        {
            AntiForgeryData token = new AntiForgeryData();

            try
            {
                ObjectStateFormatter formatter = new ObjectStateFormatter();
                Triplet triplet;

                byte[] decode = MachineKey.Unprotect(Encoding.UTF8.GetBytes(value), "Authentication token");
//                var decode = MachineKey.Decode(value, MachineKeyProtection.All);
                if (decode == null)
                    throw new ArgumentException("Unable to decrypt.");

                using (MemoryStream stream = new MemoryStream(decode))
                {
                    triplet = (Triplet) formatter.Deserialize(stream);
                }

                return Decrypt(value, formatter, triplet, salt, token);
            }
            catch (Exception)
            {
                throw new HttpAntiForgeryException();
            }
        }

        private static AntiForgeryData DecryptForm(string value, string salt)
        {
            AntiForgeryData token = new AntiForgeryData();

            try
            {
                ObjectStateFormatter formatter = new ObjectStateFormatter();

                byte[] decode = MachineKey.Unprotect(Encoding.UTF8.GetBytes(value), "Authentication token");
//                var decode = MachineKey.Decode(value, MachineKeyProtection.All);
                if (decode == null)
                    throw new ArgumentException("Unable to decrypt.");

                Triplet triplet = (Triplet) formatter.Deserialize(Encoding.UTF8.GetString(decode));

                return Decrypt(value, formatter, triplet, salt, token);
            }
            catch (Exception)
            {
                throw new HttpAntiForgeryException();
            }
        }

        private static AntiForgeryData Decrypt(string value, ObjectStateFormatter formatter, Triplet triplet, string salt, AntiForgeryData token)
        {
            byte[] systemSalt = new byte[0x5];
            Rng.GetBytes(systemSalt);

            triplet.Second = salt;
            triplet.Third = Convert.ToBase64String(systemSalt);

            token.Value = (string) triplet.First;
            token.CookieValue = value;
            token.FormToken = MachineKey.Protect(Encoding.UTF8.GetBytes(formatter.Serialize(triplet)), "Authentication token").ToHexString();
//            token.FormToken = MachineKey.Encode(Encoding.UTF8.GetBytes(formatter.Serialize(triplet)), MachineKeyProtection.All);

            return token;
        }
    }
}