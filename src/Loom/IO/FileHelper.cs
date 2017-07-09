#region Using Directives

using System;
using System.IO;
using System.Runtime.InteropServices;

#endregion

namespace Loom.IO
{
    /// <summary>
    ///     <para>Helper class to do all the file work. It assumes that all files are rooted.</para>
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        ///     <para>Reset the file attributes of a file so it can be overwritten.</para>
        /// </summary>
        /// <param name="filePath">
        ///     <para>The fully qualified path to the file.</para>
        /// </param>
        public static void ChangeFileAttributesToWritable(string filePath)
        {
            Argument.Assert.IsNotNull(filePath, "filePath");
            Argument.Assert.FileExists(filePath);

            FileAttributes attributes = File.GetAttributes(filePath);
            if ((attributes | FileAttributes.ReadOnly) == attributes)
            {
                attributes ^= FileAttributes.ReadOnly;
                File.SetAttributes(filePath, attributes);
            }
        }

        /// <summary>
        ///     <para>Determies if the file is read-only.</para>
        /// </summary>
        /// <param name="filePath">
        ///     <para>The fully qualified path to the file.</para>
        /// </param>
        /// <returns>
        ///     <para>
        ///         <see langword="true" /> if the file is read-only; otherwise <see langword="false" />.
        ///     </para>
        /// </returns>
        public static bool IsFileReadOnly(string filePath)
        {
            Argument.Assert.IsNotNull(filePath, "filePath");
            Argument.Assert.FileExists(filePath);

            FileAttributes attributes = File.GetAttributes(filePath);
            return (attributes | FileAttributes.ReadOnly) == attributes;
        }

        /// <summary>
        ///     <para>Creat a zero byte length file in the specified path.</para>
        /// </summary>
        /// <param name="filePath">
        ///     <para>The absolute path to the file to create.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         <paramref name="filePath" /> is a <see langword="null" /> reference (Nothing in Visual Basic).
        ///     </para>
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <para>The caller does not have the required permission.</para>
        ///     para>
        ///     <para>-or-</para>
        ///     <para>
        ///         <paramref name="filePath" /> specified a file that is read-only.
        ///     </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         <paramref name="filePath" /> is a zero-length string, contains only white space, or contains one or more
        ///         invalid characters as defined by
        ///         <see
        ///             cref="Path.GetInvalidPathChars()" />
        ///         .
        ///     </para>
        /// </exception>
        /// <exception cref="System.IO.PathTooLongException">
        ///     <para>
        ///         The specified path, file name, or both exceed the system-defined maximum length. For example, on
        ///         Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260
        ///         characters.
        ///     </para>
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        ///     <para>The specified path is invalid, such as being on an unmapped drive.</para>
        /// </exception>
        /// <exception cref="System.IO.IOException">
        ///     <para>An I/O error occurred while creating the file.</para>
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         <paramref name="filePath" /> is in an invalid format.
        ///     </para>
        /// </exception>
        /// <seealso cref="System.IO.File.Create(string)" />
        public static void CreateZeroByteFile(string filePath)
        {
            Argument.Assert.IsNotNull(filePath, "filePath");

            // Do nothing, just dispose the stream.
            using (File.Create(filePath)) { }
        }

        public static void Test()
        {
            Recycle(@"G:\Desktop\Test.txt");
        }

        public static void Recycle(string path)
        {
            NativeMethods.SHFILEOPSTRUCT shf = new NativeMethods.SHFILEOPSTRUCT
            {
                hwnd = IntPtr.Zero,
                wFunc = NativeMethods.FO_Func.FO_DELETE,
                fFlags = NativeMethods.FOF_ALLOWUNDO | NativeMethods.FOF_NOCONFIRMATION,
                pFrom = Marshal.StringToHGlobalUni(path + '\0')
            };
            NativeMethods.SHFileOperation(ref shf);
        }
    }
}