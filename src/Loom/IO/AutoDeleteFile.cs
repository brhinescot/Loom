#region Using Directives

using System;
using System.Diagnostics;
using System.IO;
using System.Security;

#endregion

namespace Loom.IO
{
    /// <summary>
    ///     A file wrapper which automatically deletes the file unless Disarm()
    ///     is called.
    /// </summary>
    public sealed class AutoDeleteFile : IDisposable
    {
        private bool isArmed = true;
        private bool isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoDeleteFile" /> class.
        /// </summary>
        /// <param name="file"></param>
        public AutoDeleteFile(FileInfo file)
        {
            Debug.Assert(file != null);
            Argument.Assert.IsNotNull(file, nameof(file));

            File = file;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoDeleteFile" /> class.
        /// </summary>
        /// <param name="fileName"></param>
        public AutoDeleteFile(string fileName) : this(new FileInfo(fileName)) { }

        /// <summary>
        ///     Gets a reference to the underlying <see cref="FileInfo" /> instance.
        /// </summary>
        public FileInfo File { get; }

        #region IDisposable Members

        public void Dispose()
        {
            if (isDisposed || !isArmed)
                return;

            try
            {
                File.Delete();
            }
            catch (SecurityException) { }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }

            isDisposed = true;
        }

        #endregion

        /// <summary>
        ///     When called by client code, turns off the auto delete functionality.
        /// </summary>
        public void Disarm()
        {
            isArmed = false;
        }
    }
}