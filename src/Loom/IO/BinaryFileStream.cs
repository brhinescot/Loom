#region Using Directives

using System.IO;
using System.Runtime.InteropServices;

#endregion

namespace Loom.IO
{
    internal unsafe class BinaryFileStream<T> : FileStream where T : struct
    {
        public BinaryFileStream(string path, FileMode mode)
            : base(path, mode) { }

        public BinaryFileStream(string path, FileMode mode, FileAccess access)
            : base(path, mode, access) { }

        public BinaryFileStream(string path)
            : base(path, FileMode.Open) { }

        internal int Read(void* buffer)
        {
            return Read(buffer, Marshal.SizeOf(typeof(T)));
        }

        internal void Write(void* buffer)
        {
            Write(buffer, Marshal.SizeOf(typeof(T)));
        }

        private int Read(void* buffer, int count)
        {
            int n;

            if (!NativeMethods.ReadFile(SafeFileHandle, buffer, count, out n, 0))
                throw new IOException(string.Format("Error {0} reading from file!", Marshal.GetLastWin32Error()));

            return n;
        }

        private void Write(void* buffer, int count)
        {
            int n;

            if (!NativeMethods.WriteFile(SafeFileHandle, buffer, count, out n, 0))
                throw new IOException(string.Format("Error {0} writing to file!", Marshal.GetLastWin32Error()));
        }
    }
}