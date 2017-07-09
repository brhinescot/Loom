#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Web.IO
{
    /// <summary>
    ///     Represents errors that occur during file uploads due to the file size
    ///     being beyond the maximum limit.
    /// </summary>
    [Serializable]
    public sealed class PostedFileSizeException : Exception, ISerializable
    {
        private string fileName;

        private int fileSize;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostedFileSizeException" /> class.
        /// </summary>
        public PostedFileSizeException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostedFileSizeException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PostedFileSizeException(string message) : base(message) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostedFileSizeException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public PostedFileSizeException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostedFileSizeException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized
        ///     object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext"></see> that contains contextual
        ///     information about the source or destination.
        /// </param>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        ///     The class name is null or
        ///     <see cref="System.Exception.HResult"></see> is zero (0).
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The info parameter is null. </exception>
        private PostedFileSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            fileSize = info.GetInt32("fileSize");
            fileName = info.GetString("fileName");
        }

        /// <summary>
        ///     Gets or sets the size of the file that caused the exception.
        /// </summary>
        /// <value>The size of the file.</value>
        public int FileSize
        {
            get => fileSize;
            set => fileSize = value;
        }

        /// <summary>
        ///     Gets or sets the name of the file that caused the exception.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        #region ISerializable Members

        /// <summary>
        ///     When overridden in a derived class, sets the <see cref="System.Runtime.Serialization.SerializationInfo"></see> with
        ///     information about the exception.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized
        ///     object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext"></see> that contains contextual
        ///     information about the source or destination.
        /// </param>
        /// <exception cref="System.ArgumentNullException">The info parameter is a null reference (Nothing in Visual Basic). </exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fileSize", fileSize);
            info.AddValue("fileName", fileName);
            GetObjectData(info, context);
        }

        #endregion
    }
}