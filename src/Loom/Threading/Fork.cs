#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

namespace Loom.Threading
{
    public delegate void ForkCall();

    /// <summary>
    ///     Represents a class used to process multiple code paths simultaneously.
    /// </summary>
    /// <example>
    ///     <code>
    ///  <![CDATA[
    ///      // Declare the variables we want to assign
    ///      string name = null;
    ///      int number = 0;
    /// 
    ///      // Start a new async fork
    ///      // Assign the variables inside the fork 
    /// 
    ///      Fork.Begin()
    ///          .Call(() => name = CallSomeWebService (123,"Dean") )
    ///          .Call(() => number = ExecSomeStoredProc ("hello") )
    ///          .End(); 
    /// 
    ///      // The fork has finished 
    /// 
    ///      // We can use the variables now
    ///      Console.WriteLine("{0} {1}", name, number);
    ///  ]]>
    ///  </code>
    /// </example>
    public class Fork
    {
        private readonly List<ForkCall> calls = new List<ForkCall>();

        /// <summary>
        ///     Starts an async fork
        /// </summary>
        /// <returns>Returns a new fork instance</returns>
        public static Fork Begin()
        {
            return new Fork();
        }

        /// <summary>
        ///     Queues a call in the fork
        /// </summary>
        /// <param name="call">Delegate that should be executed async</param>
        /// <returns>Returns self</returns>
        public Fork Call(ForkCall call)
        {
            calls.Add(call);
            return this;
        }

        /// <summary>
        ///     Executes all the calls async and waits until all of them are finished.
        /// </summary>
        public void End()
        {
            // Convert all calls to running threads then wait for all threads to finish.
            calls.Select(GetThread)
                .ToList()
                .ForEach(thread => thread.Join());
        }

        private static Thread GetThread(ForkCall call)
        {
            Thread thread = new Thread(GetThreadStart(call)) {IsBackground = true};
            thread.Start();
            return thread;
        }

        private static ThreadStart GetThreadStart(ForkCall call)
        {
            ThreadStart ts = () => call();
            return ts;
        }
    }
}