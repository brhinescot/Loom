#if DEBUG

#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Threading
{
    /// <summary>
    ///     Represents a class that uses a finalizer in debug mode
    ///     in order to detect when a watched object is not freed.
    /// </summary>
    /// <remarks>
    ///     This class is not available in Release builds. All usages should be
    ///     surrounded with the #if DEBUG preprocessor directive.
    /// </remarks>
    [DebuggerStepThrough]
    internal sealed class Sentinel
    {
        private readonly string watchedClassName;

        public Sentinel(string watchedClassName)
        {
            this.watchedClassName = watchedClassName;
        }

        ~Sentinel()
        {
            // If this finalizer runs, someone somewhere failed to
            // call Dispose, which means we've failed to leave a critical section!
            Debug.Fail(string.Format("An undisposed instance of the {0} class has been detected. Verify all" +
                                     " usages of {1} and dispose the object for all execution paths. Consider wrapping usages with " +
                                     "the using() statement.",
                watchedClassName, watchedClassName));
        }
    }
}

#endif