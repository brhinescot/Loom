#region Using Directives

using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

#endregion

namespace Loom
{
    /// <summary>
    ///     Summary description for Utility.
    /// </summary>
    internal static class Util
    {
        internal static void WL(string line)
        {
            Console.WriteLine(line);
        }

        internal static byte[] GetRandomBytes(int length)
        {
            byte[] ss = new byte[length];
            for (int i = 0; i < length; i++)
                ss[i] = Convert.ToByte(i); // just some dummy value
            return ss;
        }

        public static long GenerateRandomInt64()
        {
            StringBuilder result = new StringBuilder();
            char[] chars = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            byte[] data = new byte[19];
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetNonZeroBytes(data);

            for (int i = 0; i < data.Length; i++)
                result.Append(chars[data[i] % (chars.Length - 1)]);

            return Convert.ToInt64(result.ToString());
        }

        public static void AssertSqlSame(string expected, IDbCommand actual)
        {
            AssertStringsSame(expected, actual.CommandText);
        }

        public static void AssertStringsSame(string expected, string actual)
        {
            if (expected != actual)
                Assert.Fail(@"{2}EXPECTED TEXT:{2}  {0}{2}ACTUAL TEXT:{2}  {1}{2}{2}", expected, actual, Environment.NewLine);
        }
    }
}