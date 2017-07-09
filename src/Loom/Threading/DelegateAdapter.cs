#region Using Directives

using System;
using System.Threading;

#endregion

namespace Loom.Threading
{
    /// <summary>
    ///     Uses DynamicInvoke to allow any method to be easily mapped to the
    ///     ThreadStart, WaitCallback or TimerCallback.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to use the DelagateAdapter.
    ///     <code>
    ///  using System;
    ///  using System.Collections;
    ///  using System.Threading;
    ///  using Timing;
    ///  using DelegateAdapter;
    ///  public class Program
    ///  {
    /// 		// Create any method and a corresponding delegate:
    ///  	public delegate double WorkMethodHandler(double factor, string name);
    ///  	public static double WorkMethod(double factor, string name)
    ///  	{           
    ///  		Console.WriteLine(name);
    ///  		return 3.14159 * factor;
    ///  	}
    ///  
    ///  	public static void Main()
    ///  	{
    /// 			// Create the DelegateAdapter with the appropriate method and arguments:
    ///  		DelegateAdapter adapter = new DelegateAdapter(new WorkMethodHandler(WorkMethod), 3.123456789, "Richard");
    ///  
    ///  		// Automatically creates new ThreadStart and passes to the Thread constructor.
    ///  		// The adapter is implicitly convertible to a ThreadStart, which is why this works.
    ///  		Thread worker = new Thread(adapter);
    ///  
    ///  		// change the arguments:
    ///  		adapter.Args = new object[] {9.14159d, "Roberto"};
    ///  
    ///  		// run it:
    ///  		worker.Start();
    ///  
    ///  		// wait to exit:
    ///  		worker.Join();
    ///  
    ///  		// get result:
    ///  		Console.WriteLine(adapter.ReturnValue);          
    ///  	}
    ///  }
    ///  </code>
    /// </example>
    public sealed class DelegateAdapter
    {
        private readonly object[] argments;
        private readonly Delegate targetDelegate;

        /// <summary>
        ///     Creates an instance of a <see cref="DelegateAdapter" /> given any <see cref="Delegate" />.
        /// </summary>
        /// <param name="target">
        ///     The <see cref="Delegate" /> that will be invoked when one of the
        ///     output delegates is invoked.
        /// </param>
        public DelegateAdapter(Delegate target) : this(target, null) { }

        /// <summary>
        ///     Creates an instance of <see cref="DelegateAdapter" /> given any <see cref="Delegate" /> and it's
        ///     parameters to pass.
        /// </summary>
        /// <param name="target">
        ///     The <see cref="Delegate" /> that will be invoked when one of the
        ///     output <see cref="Delegate" />'s is invoked.
        /// </param>
        /// <param name="args">
        ///     The arguments that will be passed to the target
        ///     delegate.
        /// </param>
        public DelegateAdapter(Delegate target, params object[] args)
        {
            targetDelegate = target;
            argments = args;
        }

        /// <summary>
        ///     Gets the return value of the last execution of this <see cref="DelegateAdapter" />'s
        ///     target method.
        /// </summary>
        public object ReturnValue { get; private set; }

        /// <summary>
        ///     Executes the <see cref="Delegate" /> on a new thread.
        /// </summary>
        /// <returns>The new thread that was started.</returns>
        public static Thread StartThread(Delegate target, params object[] args)
        {
            DelegateAdapter adapter = new DelegateAdapter(target, args);
            Thread worker = new Thread(adapter);
            worker.Start();
            return worker;
        }

        /// <summary>
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static implicit operator ThreadStart(DelegateAdapter adapter)
        {
            Argument.Assert.IsNotNull(adapter, "adapter");
            return adapter.CreateThreadStart();
        }

        /// <summary>
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static implicit operator WaitCallback(DelegateAdapter adapter)
        {
            Argument.Assert.IsNotNull(adapter, "adapter");
            return adapter.CreateWaitCallback();
        }

        /// <summary>
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static implicit operator TimerCallback(DelegateAdapter adapter)
        {
            Argument.Assert.IsNotNull(adapter, "adapter");
            return adapter.CreateTimerCallback();
        }

        /// <summary>
        ///     Dynamically invokes the target <see cref="Delegate" /> with the provided arguments.
        /// </summary>
        public void Execute()
        {
            ReturnValue = targetDelegate.DynamicInvoke(argments);
        }

        /// <summary>
        ///     Dynamically invokes the target <see cref="Delegate" /> with the state object provided
        ///     by the caller.  *Note* ignores any args passed to the <see cref="DelegateAdapter" />.
        /// </summary>
        /// <param name="state"></param>
        public void Execute(object state)
        {
            object[] cachedState = state as object[];
            ReturnValue = targetDelegate.DynamicInvoke(cachedState ?? new[] {state});
        }

        /// <summary>
        ///     Creates a new, unique <see cref="ThreadStart" /> for use with the <see cref="Thread" /> class.
        /// </summary>
        /// <returns>The new ThreadStart</returns>
        public ThreadStart CreateThreadStart()
        {
            return Execute;
        }

        /// <summary>
        ///     Creates a new, unique <see cref="WaitCallback" /> for use with the <see cref="ThreadPool" />
        ///     class.
        /// </summary>
        /// <returns>The new WaitCallback</returns>
        public WaitCallback CreateWaitCallback()
        {
            return Execute;
        }

        /// <summary>
        ///     Creates a new, unique <see cref="TimerCallback" /> for use with the <see cref="Timer" />
        ///     class.
        /// </summary>
        /// <returns>The new TimerCallback</returns>
        public TimerCallback CreateTimerCallback()
        {
            return Execute;
        }
    }
}