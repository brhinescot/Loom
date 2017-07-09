#region Using Directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace Loom.Cryptography
{
    /// <summary>
    ///     <para>
    ///         Represents basic cryptography services for a <see cref="SymmetricAlgorithm" />.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Because the IV (Initialization Vector) has the same distribution as the resulting
    ///         cipher text, the IV is randomly generated and prepended to the cipher text.
    ///     </para>
    /// </remarks>
    public sealed class SymmetricCryptographyProvider : IDisposable
    {
        private readonly SymmetricAlgorithm algorithm;
        private readonly byte[] key;
        private bool disposed;

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="SymmetricCryptographyProvider" /> class with a
        ///         <see
        ///             cref="SymmetricAlgorithm" />
        ///         and key.
        ///     </para>
        /// </summary>
        /// <param name="algorithm">
        ///     <para>The algorithm in which to perform cryptographic functions.</para>
        /// </param>
        /// <param name="key">
        ///     <para>The key for the algorithm.</para>
        /// </param>
        public SymmetricCryptographyProvider(SymmetricAlgorithm algorithm, byte[] key)
        {
            this.algorithm = algorithm;
            this.key = key;
        }

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="SymmetricCryptographyProvider" /> class with an algorithm type and
        ///         a key.
        ///     </para>
        /// </summary>
        /// <param name="algorithmType">
        ///     <para>
        ///         The qualified assembly name of a <see cref="SymmetricAlgorithm" />.
        ///     </para>
        /// </param>
        /// <param name="key">
        ///     <para>The key for the algorithm.</para>
        /// </param>
        public SymmetricCryptographyProvider(string algorithmType, byte[] key) : this(GetSymmetricAlgorithm(algorithmType), key) { }

        private int IVLength => algorithm.IV.Length;

        #region IDisposable Members

        /// <summary>
        /// </summary>
        void IDisposable.Dispose()
        {
            if (disposed)
                return;

            disposed = true;
            OnDisposed(this, EventArgs.Empty);

            if (algorithm != null)
                algorithm.Clear();
        }

        #endregion

        /// <summary>
        /// </summary>
        public event EventHandler<EventArgs> Disposed;

        /// <summary>
        ///     Creates a <see cref="RijndaelManaged" /> <see cref="SymmetricCryptographyProvider" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static SymmetricCryptographyProvider CreateRijndael(byte[] key)
        {
            return new SymmetricCryptographyProvider(new RijndaelManaged(), key);
        }

        /// <summary>
        ///     Creates a <see cref="DESCryptoServiceProvider" /> <see cref="SymmetricCryptographyProvider" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")]
        public static SymmetricCryptographyProvider CreateDES(byte[] key)
        {
            return new SymmetricCryptographyProvider(new DESCryptoServiceProvider(), key);
        }

        /// <summary>
        ///     Creates a <see cref="RC2CryptoServiceProvider" /> <see cref="SymmetricCryptographyProvider" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static SymmetricCryptographyProvider CreateRC2(byte[] key)
        {
            return new SymmetricCryptographyProvider(new RC2CryptoServiceProvider(), key);
        }

        /// <summary>
        ///     Creates a <see cref="TripleDESCryptoServiceProvider" /> <see cref="SymmetricCryptographyProvider" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")]
        public static SymmetricCryptographyProvider TripleDES(byte[] key)
        {
            return new SymmetricCryptographyProvider(new TripleDESCryptoServiceProvider(), key);
        }

        /// <summary>
        ///     Encrypts the specified plain text.
        /// </summary>
        /// <param name="text">The plain text.</param>
        /// <returns></returns>
        public byte[] Encrypt(string text)
        {
            byte[] plainBytes = new ASCIIEncoding().GetBytes(text);
            return Encrypt(plainBytes);
        }

        /// <summary>
        ///     <para>Encrypts bytes with the initialized algorithm and key.</para>
        /// </summary>
        /// <param name="data">
        ///     <para>The data which is to be encrypted.</para>
        /// </param>
        /// <returns>
        ///     <para>The resulting encrypted data.</para>
        /// </returns>
        public byte[] Encrypt(byte[] data)
        {
            byte[] cipherText;
            algorithm.Key = key;

            using (ICryptoTransform transform = algorithm.CreateEncryptor())
            {
                cipherText = Transform(transform, data);
            }

            byte[] output = new byte[IVLength + cipherText.Length];
            Buffer.BlockCopy(algorithm.IV, 0, output, 0, IVLength);
            Buffer.BlockCopy(cipherText, 0, output, IVLength, cipherText.Length);
            return output;
        }

        /// <summary>
        ///     <para>Decrypts the specified string with the initialized algorithm and key.</para>
        /// </summary>
        /// <param name="encryptedText">
        ///     <para>The text which you wish to decrypt.</para>
        /// </param>
        /// <returns>
        ///     <para>The resulting plain text.</para>
        /// </returns>
        public byte[] Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = new ASCIIEncoding().GetBytes(encryptedText);
            return Decrypt(encryptedBytes);
        }

        /// <summary>
        ///     <para>Decrypts the specified encrypted data with the initialized algorithm and key.</para>
        /// </summary>
        /// <param name="encryptedData">
        ///     <para>The data which is to be decrypted.</para>
        /// </param>
        /// <returns>
        ///     <para>The resulting decrypted data.</para>
        /// </returns>
        public byte[] Decrypt(byte[] encryptedData)
        {
            byte[] output;
            byte[] data = ExtractIV(encryptedData);

            using (ICryptoTransform transform = algorithm.CreateDecryptor())
            {
                output = Transform(transform, data);
            }

            return output;
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            ((IDisposable) this).Dispose();
        }

        private static SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmType)
        {
            SymmetricAlgorithm algorithm;
            try
            {
                algorithm = SymmetricAlgorithm.Create(algorithmType);
            }
            catch (Exception)
            {
                // We want to suppress any type of exception here for security reasons.
                throw new CryptographicException(SR.ExceptionCreatingSymmetricAlgorithmInstance);
            }

            if (algorithm == null)
                throw new CryptographicException(SR.ExceptionCastingSymmetricAlgorithmInstance);

            return algorithm;
        }

        private static byte[] Transform(ICryptoTransform transform, byte[] buffer)
        {
            byte[] transformBuffer;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                CryptoStream cryptoStream = null;
                try
                {
                    cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                    cryptoStream.Write(buffer, 0, buffer.Length);
                    cryptoStream.FlushFinalBlock();
                    transformBuffer = memoryStream.ToArray();
                }
                finally
                {
                    if (cryptoStream != null)
                        cryptoStream.Close();
                }
            }

            return transformBuffer;
        }

        private byte[] ExtractIV(byte[] encryptedText)
        {
            byte[] initVector = new byte[IVLength];

            if (encryptedText.Length < IVLength + 1)
                throw new CryptographicException(SR.ExceptionDecrypting);

            byte[] data = new byte[encryptedText.Length - IVLength];

            Buffer.BlockCopy(encryptedText, 0, initVector, 0, IVLength);
            Buffer.BlockCopy(encryptedText, IVLength, data, 0, data.Length);

            algorithm.IV = initVector;
            algorithm.Key = key;

            return data;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = Disposed;
            if (handler != null)
                handler(sender, e);
        }
    }
}