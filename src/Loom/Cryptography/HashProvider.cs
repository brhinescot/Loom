#region Using Directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace Loom.Cryptography
{
    /// <summary>
    ///     Represents a class for hashing data using one-way algorithms.
    /// </summary>
    /// <remarks>
    ///     <see cref="Clear" /> should be called when the object is no longer needed in order
    ///     to free up resources held by this class. The class also explicitly implements
    ///     <see cref="System.IDisposable" /> which allows the developer to wrap class access
    ///     in a <see langword="using" /> statement.
    /// </remarks>
    public sealed class HashProvider : IEquatable<HashProvider>, IDisposable
    {
        private bool disposed;

        private HashProvider(string name) : this((HashAlgorithm) CryptoConfig.CreateFromName(name)) { }

        private HashProvider(HashAlgorithm algorithm)
        {
            BaseAlgorithm = algorithm;
        }

        public HashAlgorithm BaseAlgorithm { get; }

        /// <summary>
        ///     Gets the <see cref="HashProvider" /> for MD5 encryption.
        /// </summary>
        public static HashProvider MD5 => new HashProvider(new MD5CryptoServiceProvider());

        /// <summary>
        /// </summary>
        public string Name => BaseAlgorithm.ToString();

        /// <summary>
        ///     Gets the <see cref="HashProvider" /> for RIPEMD160 encryption.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member", Justification = "Using same capitalization as BCL class for consistency.")]
        public static HashProvider RIPEMD160 => new HashProvider(new RIPEMD160Managed());

        /// <summary>
        ///     Gets the <see cref="HashProvider" /> for SHA256 encryption.
        /// </summary>
        public static HashProvider SHA256 => new HashProvider(new SHA256Managed());

        /// <summary>
        ///     Gets the <see cref="HashProvider" /> for SHA384 encryption.
        /// </summary>
        public static HashProvider SHA384 => new HashProvider(new SHA384Managed());

        /// <summary>
        ///     Gets the <see cref="HashProvider" /> for SHA512 encryption.
        /// </summary>
        public static HashProvider SHA512 => new HashProvider(new SHA512Managed());

        #region IDisposable Members

        /// <summary>
        /// </summary>
        void IDisposable.Dispose()
        {
            Clear();
        }

        #endregion

        #region IEquatable<HashProvider> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(HashProvider other)
        {
            return other != null && Equals(BaseAlgorithm, other.BaseAlgorithm);
        }

        #endregion

        /// <summary>
        /// </summary>
        public event EventHandler<EventArgs> Disposed;

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HashProvider FromString(string name)
        {
            return new HashProvider(name);
        }

        public override bool Equals(object obj)
        {
            if (Equals(this, obj))
                return true;

            HashProvider hashProvider = obj as HashProvider;
            return hashProvider != null && Equals(hashProvider);
        }

        public override int GetHashCode()
        {
            return BaseAlgorithm.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GenerateString(byte[] data)
        {
            Argument.Assert.IsNotNull(data, nameof(data));

            byte[] hashBytes = GenerateBytes(data);
            StringBuilder hashString = new StringBuilder(hashBytes.Length * 2);
            for (int i = 0; i < hashBytes.Length; i++)
                hashString.AppendFormat("{0:X2}", hashBytes[i]);

            return hashString.ToString();
        }

        /// <summary>
        ///     Encrypts the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string GenerateString(string text)
        {
            Argument.Assert.IsNotNull(text, nameof(text));

            byte[] plainBytes = new ASCIIEncoding().GetBytes(text);
            return GenerateString(plainBytes);
        }

        /// <summary>
        ///     Encrypts the text with the specified salt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public string GenerateString(string text, string salt)
        {
            Argument.Assert.IsNotNull(text, nameof(text));
            Argument.Assert.IsNotNull(salt, nameof(salt));

            byte[] plainBytes = new ASCIIEncoding().GetBytes(string.Concat(text, salt));
            return string.Concat(GenerateString(plainBytes), salt);
        }

        /// <summary>
        ///     Computes the hash.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns></returns>
        public byte[] GenerateBytes(byte[] data)
        {
            Argument.Assert.IsNotNull(data, nameof(data));

            return BaseAlgorithm.ComputeHash(data);
        }

        /// <summary>
        ///     Computes the hash.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        /// <returns></returns>
        public byte[] GenerateBytes(string text)
        {
            Argument.Assert.IsNotNull(text, nameof(text));

            return GenerateBytes(new ASCIIEncoding().GetBytes(text));
        }

        /// <summary>
        ///     Compares the specified clear text.
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <returns></returns>
        public bool Compare(string clearText, string encryptedText)
        {
            Argument.Assert.IsNotNull(clearText, "text");
            Argument.Assert.IsNotNull(encryptedText, "encryptedText");

            return encryptedText == GenerateString(clearText);
        }

        /// <summary>
        ///     Compares the specified clear text.
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="encryptedSaltedText">The encrypted text with salt.</param>
        /// <param name="saltLength">Length of the salt.</param>
        /// <returns></returns>
        public bool Compare(string clearText, string encryptedSaltedText, int saltLength)
        {
            Argument.Assert.IsNotNull(clearText, "text");
            Argument.Assert.IsNotNullOrEmpty(encryptedSaltedText, "encryptedTextWithSalt");

            string salt = encryptedSaltedText.Substring(encryptedSaltedText.Length - saltLength);
            return encryptedSaltedText == GenerateString(clearText, salt);
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            if (disposed)
                return;

            disposed = true;
            OnDisposed(EventArgs.Empty);

            if (BaseAlgorithm != null)
                BaseAlgorithm.Clear();
        }

        /// <summary>
        /// </summary>
        /// <param name="provider1"></param>
        /// <param name="provider2"></param>
        /// <returns></returns>
        public static bool operator ==(HashProvider provider1, HashProvider provider2)
        {
            if (Argument.IsAnyNull(provider1, provider2))
                return false;

            return provider1.BaseAlgorithm == provider2.BaseAlgorithm;
        }

        /// <summary>
        /// </summary>
        /// <param name="provider1"></param>
        /// <param name="provider2"></param>
        /// <returns></returns>
        public static bool operator !=(HashProvider provider1, HashProvider provider2)
        {
            if (Argument.IsAnyNull(provider1, provider2))
                return false;

            return provider1.BaseAlgorithm != provider2.BaseAlgorithm;
        }

        /// <summary>
        /// </summary>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void OnDisposed(EventArgs e)
        {
            EventHandler<EventArgs> handler = Disposed;
            if (handler != null)
                handler(this, e);
        }
    }
}