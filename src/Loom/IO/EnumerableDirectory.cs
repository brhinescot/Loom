#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Loom.IO
{
    public static class EnumerableDirectory
    {
        public static IEnumerable<string> FindFiles(string path)
        {
            string searchPath = !Path.HasExtension(path) ? Path.Combine(path, "*.*") : path;
            string directory = Path.GetDirectoryName(path);

            if (Compare.IsNullOrEmpty(directory))
                throw new DirectoryNotFoundException(path);

            IntPtr handle = IntPtr.Zero;
            try
            {
                NativeMethods.WIN32_FIND_DATAW data;
                handle = NativeMethods.FindFirstFileW(searchPath, out data);
                while (handle != IntPtr.Zero)
                {
                    if (data.cFileName != "." && data.cFileName != "..")
                        yield return Path.Combine(directory, data.cFileName);

                    if (NativeMethods.FindNextFileW(handle, out data))
                        continue;

                    NativeMethods.FindClose(handle);
                    handle = IntPtr.Zero;
                }
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    NativeMethods.FindClose(handle);
                    handle = IntPtr.Zero;
                }
            }
        }
    }
}