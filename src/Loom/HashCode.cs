#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom
{
    [DebuggerStepThrough]
    public static class HashCode
    {
        [DebuggerStepThrough]
        public static int Combine(int h1, int h2, int h3, int h4, int h5)
        {
            return Combine(CombinePrivate(h1, h2), CombinePrivate(h3, h4), h5);
        }

        [DebuggerStepThrough]
        public static int Combine(int h1, int h2, int h3, int h4)
        {
            return CombinePrivate(CombinePrivate(h1, h2), CombinePrivate(h3, h4));
        }

        [DebuggerStepThrough]
        public static int Combine(int h1, int h2, int h3)
        {
            return CombinePrivate(CombinePrivate(h1, h2), h3);
        }

        [DebuggerStepThrough]
        public static int Combine(int h1, int h2)
        {
            return CombinePrivate(h1, h2);
        }

        private static int CombinePrivate(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }
    }
}