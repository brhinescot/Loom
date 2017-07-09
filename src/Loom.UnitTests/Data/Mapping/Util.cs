#region Using Directives

using System;
using System.Data;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping
{
    /// <summary>
    ///     Summary description for Utility.
    /// </summary>
    public static class Util
    {
        internal static void WL(string line)
        {
            Console.WriteLine(line);
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