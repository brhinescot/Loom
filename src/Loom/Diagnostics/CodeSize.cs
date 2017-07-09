#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Diagnostics
{
    /// <summary>
    /// </summary>
    public static class CodeSize
    {
        /// <summary>
        ///     Starts this instance.
        /// </summary>
        /// <returns></returns>
        public static long Mark()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64;
        }

        /// <summary>
        ///     Stops the specified timer.
        /// </summary>
        /// <returns></returns>
        [Conditional("DEBUG")]
        public static void DebugWrite(string message, long sizeBefore)
        {
            long sizeAfter = Process.GetCurrentProcess().PrivateMemorySize64;
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("****************************************************************");
            Debug.WriteLine(message);
            Debug.WriteLine("Size Difference = " + ((sizeAfter - sizeBefore) / 1024).ToString("0 KB"));
            Debug.WriteLine("Process Memory Before = " + (sizeBefore / 1024).ToString("0 KB"));
            Debug.WriteLine("Process Memory After = " + (sizeAfter / 1024).ToString("0 KB"));
            Debug.WriteLine("****************************************************************");
            Debug.WriteLine(string.Empty);
        }

        /// <summary>
        ///     Stops the specified timer.
        /// </summary>
        /// <returns></returns>
        [Conditional("DEBUG")]
        public static void DebugWrite(string message)
        {
            long sizeAfter = Process.GetCurrentProcess().PrivateMemorySize64;
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("****************************************************************");
            Debug.WriteLine(message);
            Debug.WriteLine("Process Memory = " + (sizeAfter / 1024).ToString("0 KB"));
            Debug.WriteLine("****************************************************************");
            Debug.WriteLine(string.Empty);
        }
    }
}