#region Using Directives

using System;
using System.Security;
using Microsoft.Win32;

#endregion

namespace Loom.IO
{
    // TEST: Write tests for RegistryManager class.
    /// <summary>
    ///     A class for managing an application's registry settings.
    /// </summary>
    public sealed class RegistryManager : IDisposable
    {
        private const string RootKey = @"Software\";

        private readonly string applicationKey;
        private readonly string companyKey;
        private readonly RegistryKey currentUser;
        private bool isDisposed;

        /// <summary>
        ///     Creates a new <see cref="RegistryManager" /> instance.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="companyName">Name of the company.</param>
        private RegistryManager(string applicationName, string companyName)
        {
            currentUser = Registry.CurrentUser;
            applicationKey = applicationName;
            companyKey = companyName;
        }

        #region IDisposable Members

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
            currentUser.Close();
        }

        #endregion

        /// <summary>
        ///     Creates a new <see cref="RegistryManager" /> class.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        public static RegistryManager Create(string applicationName, string companyName)
        {
            return new RegistryManager(applicationName, companyName);
        }

        /// <summary>
        ///     Creates the application key.
        /// </summary>
        public void CreateApplicationKey()
        {
            CheckForDisposed();
            currentUser.CreateSubKey(RootKey + companyKey + applicationKey);
        }

        /// <summary>
        ///     Adds or updates the key and value in the applications Registry key.
        /// </summary>
        /// <remarks>
        ///     If the key already exists, it is overwritten with the new value.
        /// </remarks>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="value">Key value.</param>
        public void AddValue(object value, string keyName = null)
        {
            Argument.Assert.IsNotNull(value, "value");
            CheckForDisposed();

            RegistryKey appKey = null;
            try
            {
                appKey = currentUser.OpenSubKey(RootKey + companyKey + applicationKey, true);
                appKey?.SetValue(keyName, value);
            }
            finally
            {
                appKey?.Close();
            }
        }

        /// <summary>
        ///     Retrieves the value of the specified Registry key..
        /// </summary>
        /// <exception cref="SecurityException">
        ///     The user does not have RegistryPermission.SetInclude(delete, currentKey) access.
        /// </exception>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        public object RetrieveValue(string keyName)
        {
            CheckForDisposed();
            RegistryKey appKey = null;
            try
            {
                appKey = currentUser.OpenSubKey(RootKey + companyKey + applicationKey, true);
                object keyValue = appKey?.GetValue(keyName);
                return keyValue;
            }
            finally
            {
                appKey?.Close();
            }
        }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        private void CheckForDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException(typeof(RegistryManager).FullName, SR.ExceptionObjectDisposedCanNotRenew);
        }
    }
}