#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Diagnostics;
using Loom.Threading;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping
{
    [TestFixture]
    public class TimedTestBase
    {
        protected CodeTimer Timer;

        [SetUp]
        public void ThreadedSetup()
        {
            Timer = CodeTimer.Start();
        }

        [TearDown]
        public void ThreadedTearDown()
        {
            CodeTimer.WriteMilliseconds(Timer);
            CodeTimer.Stop(Timer);
            ;
        }
    }

    public class ThreadedTestBase : TimedTestBase
    {
        protected static void ThreadedRepeat(int count, Action<int> action)
        {
            Exception exception = null;
            Forker p = new Forker();
            p.ItemComplete += delegate(object sender, ParallelEventArgs args)
            {
                if (args.Exception != null && exception == null)
                    exception = args.Exception;
            };

            for (int i = 0; i < count; i++)
            {
                Action<int> tmp = action;
                int index = i;
                p.Fork(() => tmp(index));
            }

            p.Join();

            if (exception != null)
                throw exception;
        }

        protected static void ThreadedRepeat(int count, Action<int, IThreadAsserter> action)
        {
            Forker p = new Forker();
            Exception exception = null;

            List<IThreadAsserter> asserts = new List<IThreadAsserter>(count);

            p.ItemComplete += delegate(object sender, ParallelEventArgs args)
            {
                if (args.Exception != null && exception == null)
                    exception = args.Exception;
            };

            for (int i = 0; i < count; i++)
            {
                Action<int, IThreadAsserter> tmp = action;
                ThreadAsserter asserter = new ThreadAsserter();
                asserts.Add(asserter);
                int index = i;
                p.Fork(() => tmp(index, asserter));
            }

            p.Join();

            if (exception != null)
                throw exception;

            foreach (IThreadAsserter threadAsserter in asserts)
            {
                threadAsserter.Run();
                threadAsserter.Dispose();
            }
        }

        protected static void ThreadedRepeat<TState>(int count, TState state, Action<int, IThreadAsserter, TState> action)
        {
            Forker p = new Forker();
            Exception exception = null;

            List<IThreadAsserter> asserts = new List<IThreadAsserter>(count);

            p.ItemComplete += delegate(object sender, ParallelEventArgs args)
            {
                if (args.Exception != null && exception == null)
                    exception = args.Exception;
            };

            for (int i = 0; i < count; i++)
            {
                Action<int, IThreadAsserter, TState> tmp = action;
                ThreadAsserter asserter = new ThreadAsserter();
                asserts.Add(asserter);
                int index = i;
                p.Fork(() => tmp(index, asserter, state));
            }

            p.Join();

            if (exception != null)
                throw exception;

            foreach (IThreadAsserter threadAsserter in asserts)
            {
                threadAsserter.Run();
                threadAsserter.Dispose();
            }
        }
    }

    public interface IThreadAsserter : IDisposable
    {
        void Assert(Action action);
        void Run();
        void RegisterDependency(IDisposable resource);
    }

    internal class ThreadAsserter : IThreadAsserter
    {
        private readonly List<Action> asserters = new List<Action>();
        private readonly List<IDisposable> resources = new List<IDisposable>();

        #region IThreadAsserter Members

        void IThreadAsserter.Assert(Action action)
        {
            asserters.Add(action);
        }

        void IThreadAsserter.Run()
        {
            asserters.ForEach(a => a());
        }

        void IThreadAsserter.RegisterDependency(IDisposable resource)
        {
            resources.Add(resource);
        }

        void IDisposable.Dispose()
        {
            foreach (IDisposable disposable in resources)
                disposable.Dispose();
        }

        #endregion
    }
}