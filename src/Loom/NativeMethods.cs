#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

#endregion

namespace Loom
{
    // TODO: Check that System.Threading.NativeOverlapped parameter works correctly
    internal static class NativeMethods
    {
        #region FO_Func enum

        public enum FO_Func : uint
        {
            FO_MOVE = 1,
            FO_COPY = 2,
            FO_DELETE = 3,
            FO_RENAME = 4
        }

        #endregion

        internal const int ERROR_FILE_NOT_FOUND = 2;
        internal const int FOF_ALLOWUNDO = 0x40;
        internal const int FOF_NOCONFIRMATION = 0x10; //Don't prompt the user.; 
        internal const int FO_DELETE = 3;

        [DllImport("kernel32", SetLastError = true)]
        public static extern unsafe bool ReadFile(
            SafeFileHandle hFile,
            void* pBuffer,
            int numberOfBytesToRead,
            out int numberOfBytesRead,
            int overlapped
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern unsafe bool WriteFile(
            SafeFileHandle handle,
            void* bytes,
            int numBytesToWrite,
            out int numBytesWritten,
            int overlapped
        );

//        [DllImport("kernel32", SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        internal static extern unsafe bool ReadFile(
//            SafeFileHandle hFile,
//            void* pBuffer,
//            int numberOfBytesToRead,
//            out int numberOfBytesRead,
//            NativeOverlapped* overlapped
//            );

//        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadFile")]
//        [return: MarshalAsAttribute(UnmanagedType.Bool)]
//        public static extern bool ReadFile(
//            [In] IntPtr hFile, 
//            IntPtr lpBuffer, 
//            uint nNumberOfBytesToRead, 
//            IntPtr lpNumberOfBytesRead, 
//            IntPtr lpOverlapped);

//        [DllImport("kernel32.dll", SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        internal static extern unsafe bool WriteFile(
//            SafeFileHandle handle,
//            void* bytes,
//            int numBytesToWrite,
//            out int numBytesWritten,
//            NativeOverlapped* overlapped
//            );

//        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteFile")]
//        [return: MarshalAsAttribute(UnmanagedType.Bool)]
//        public static extern bool WriteFile(
//            [In] IntPtr hFile, 
//            [In] IntPtr lpBuffer, 
//            uint nNumberOfBytesToWrite, 
//            IntPtr lpNumberOfBytesWritten, 
//            IntPtr lpOverlapped);

        /// Return Type: HANDLE->void*
        /// lpFileName: LPCWSTR->WCHAR*
        /// lpFindFileData: LPWIN32_FIND_DATAW->_WIN32_FIND_DATAW*
        [DllImport("kernel32.dll", EntryPoint = "FindFirstFileW")]
        public static extern IntPtr FindFirstFileW(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            [Out] out WIN32_FIND_DATAW lpFindFileData);

        /// Return Type: BOOL->int
        /// hFindFile: HANDLE->void*
        /// lpFindFileData: LPWIN32_FIND_DATAW->_WIN32_FIND_DATAW*
        [DllImport("kernel32.dll", EntryPoint = "FindNextFileW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextFileW(
            [In] IntPtr hFindFile,
            [Out] out WIN32_FIND_DATAW lpFindFileData);

        /// Return Type: BOOL->int
        /// hFindFile: HANDLE->void*
        [DllImport("kernel32.dll", EntryPoint = "FindClose")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(IntPtr hFindFile);

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle([In] IntPtr hObject);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHFileOperation([In] ref SHFILEOPSTRUCT lpFileOp);

        [DllImport("msvcrt.dll", SetLastError = true)]
        internal static extern int _getch();

        /// <summary>
        ///     Returns the installation directory of the common language runtime (CLR) that is loaded into the process.
        /// </summary>
        /// <remarks>
        ///     The installation directory is fully qualified, for example, "c:\windows\microsoft.net\framework\v1.0.3705".
        /// </remarks>
        /// <param name="pbuffer">
        ///     [out] A buffer in which the runtime returns a string that contains the fully
        ///     qualified name of the installation directory for the runtime that is loaded into the process. If the
        ///     runtime has not yet been loaded into the process, the function returns the appropriate directory
        ///     information for the latest version of the runtime installed on the computer.
        /// </param>
        /// <param name="cchBuffer">[in] The size, in bytes, of pbuffer.</param>
        /// <param name="dwlength">[out] The number of characters returned in pbuffer.</param>
        /// <returns>An HRESULT</returns>
        [DllImport("mscoree.dll")]
        internal static extern int GetCORSystemDirectory([MarshalAs(UnmanagedType.LPWStr)] StringBuilder pbuffer, int cchBuffer, ref int dwlength);

        #region Nested type: FILETIME

        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            /// DWORD->unsigned int
            public uint dwLowDateTime;

            /// DWORD->unsigned int
            public uint dwHighDateTime;
        }

        #endregion

        #region Nested type: SHFILEOPSTRUCT

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)] public FO_Func wFunc;
            [MarshalAs(UnmanagedType.SysInt)] public IntPtr pFrom;
            [MarshalAs(UnmanagedType.SysInt)] public IntPtr pTo;

            public ushort fFlags;

            //public bool fAnyOperationsAborted;
            // the bool size is 1 byte. in the platform SDK BOOL is just an int
            public int fAnyOperationsAborted;

            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPWStr)] public string lpszProgressTitle;
        }

        #endregion

        #region Nested type: WIN32_FIND_DATAW

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATAW
        {
            /// DWORD->unsigned int
            public uint dwFileAttributes;

            /// FILETIME->_FILETIME
            public FILETIME ftCreationTime;

            /// FILETIME->_FILETIME
            public FILETIME ftLastAccessTime;

            /// FILETIME->_FILETIME
            public FILETIME ftLastWriteTime;

            /// DWORD->unsigned int
            public uint nFileSizeHigh;

            /// DWORD->unsigned int
            public uint nFileSizeLow;

            /// DWORD->unsigned int
            public uint dwReserved0;

            /// DWORD->unsigned int
            public uint dwReserved1;

            /// WCHAR[260]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string cFileName;

            /// WCHAR[14]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)] public string cAlternateFileName;
        }

        #endregion
    }
}

// ReSharper restore InconsistentNaming