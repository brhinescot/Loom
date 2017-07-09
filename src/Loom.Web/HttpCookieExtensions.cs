#region Using Directives

using System;
using System.Text;
using System.Web;
using System.Web.Security;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Provides a way to encrypt and decrypt a <see cref="HttpCookie" /> by using the same algorithms
    ///     and key information that are used for ASP.NET forms authentication cookies.
    /// </summary>
    /// <remarks>
    ///     Encryption and decryption uses algorithms and keys defined in the machineKey section of the config file.
    /// </remarks>
    public static class HttpCookieExtensions
    {
        /// <summary>
        ///     Creates an encrypted copy of the cookie using settings specified in the machineKey configuration.
        /// </summary>
        /// <param name="plainTextCookie">The <see cref="HttpCookie" /> to encrypt.</param>
        /// <returns>An encrypted copy of the supplied <paramref name="plainTextCookie" />.</returns>
        public static HttpCookie Encrypt(this HttpCookie plainTextCookie)
        {
            return SecureClone(plainTextCookie, Encrypt);
        }

        /// <summary>
        ///     Creates a decrypted copy of the encrypted cookie using settings specified in the machineKey configuration.
        /// </summary>
        /// <param name="encryptedCookie">The <see cref="HttpCookie" /> to decrypt.</param>
        /// <returns>A decrypted copy of the supplied <paramref name="encryptedCookie" />.</returns>
        public static HttpCookie Decrypt(this HttpCookie encryptedCookie)
        {
            return SecureClone(encryptedCookie, Decrypt);
        }

        /// <summary>
        ///     Expires the cookie by setting it's expiration date to one day in the past.
        /// </summary>
        /// <param name="cookie">The <see cref="HttpCookie" /> to expire.</param>
        public static void Expire(this HttpCookie cookie)
        {
            cookie.Expires = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        ///     Creates a clone of the given cookie
        /// </summary>
        /// <param name="cookie">A cookie to clone</param>
        /// <param name="func"> </param>
        /// <returns>The cloned cookie</returns>
        private static HttpCookie SecureClone(HttpCookie cookie, Func<string, string> func)
        {
            HttpCookie clone = new HttpCookie(cookie.Name, func(cookie.Value));
            clone.Domain = cookie.Domain;
            clone.Expires = cookie.Expires;
            clone.HttpOnly = true;
            clone.Path = cookie.Path;
            clone.Secure = cookie.Secure;

            return clone;
        }

        private static string Encrypt(string value)
        {
            if (value == null)
                return null;

            return Protect(Encoding.UTF8.GetBytes(value));
        }

        private static string Decrypt(string value)
        {
            if (value == null)
                return null;

            return Encoding.UTF8.GetString(Unprotect(value));
        }

        private static string Protect(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            byte[] value = MachineKey.Protect(data, "HttpCookie");
            return Convert.ToBase64String(value);
        }

        private static byte[] Unprotect(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            byte[] bytes = Convert.FromBase64String(value);
            return MachineKey.Unprotect(bytes, "HttpCookie");
        }
    }
}