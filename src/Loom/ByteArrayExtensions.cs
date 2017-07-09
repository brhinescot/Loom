#region Using Directives

using Loom.Cryptography;

#endregion

namespace Loom
{
    public static class ByteArrayExtensions
    {
        public static byte[] Append(this byte[] b, byte[] bytes)
        {
            return CryptographyUtility.CombineBytes(b, bytes);
        }

        public static string ToHexString(this byte[] b)
        {
            return CryptographyUtility.GetHexStringFromBytes(b);
        }

        public static void ZeroOut(this byte[] b)
        {
            CryptographyUtility.ZeroOutBytes(b);
        }
    }
}