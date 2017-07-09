#region Using Directives

using System;
using System.Text;

#endregion

namespace Loom
{
    internal static unsafe class PointerUtils
    {
        private const char NullTerminator = '\0';

        internal static string ArrayToString(byte* buffer, int maxLength)
        {
            return (IntPtr) buffer == IntPtr.Zero ? null : ArrayToStringPrivate(buffer, maxLength);
        }

        internal static void FillArray(byte* array, string value, int maxLength)
        {
            FillArrayPrivate(value, maxLength, array);
        }

        private static string ArrayToStringPrivate(byte* buffer, int maxLength)
        {
            StringBuilder s = new StringBuilder();
            try
            {
                for (int i = 0; i < maxLength; i++)
                {
                    if (buffer[i] == NullTerminator)
                        break;

                    s.Append((char) buffer[i]);
                }
            }
            catch (AccessViolationException e)
            {
                throw new ArgumentException("Data in buffer not null terminated or bad pointer", e);
            }

            return s.ToString();
        }

        private static void FillArrayPrivate(string value, int maxLength, byte* array)
        {
            if (Compare.IsNullOrEmpty(value))
                return;

            byte[] bytes = Encoding.ASCII.GetBytes(value);
            for (int i = 0; i < maxLength; i++)
            {
                if (i < bytes.Length)
                {
                    array[i] = bytes[i];
                    continue;
                }

                array[i] = 0;
            }
        }
    }
}