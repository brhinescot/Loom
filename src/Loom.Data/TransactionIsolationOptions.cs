#region Using Directives

using System;
using System.Transactions;

#endregion

namespace Loom.Data
{
    public struct TransactionIsolationOption
    {
        public static TransactionOptions ReadUncommitted
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.ReadUncommitted;
                return options;
            }
        }

        public static TransactionOptions ReadCommitted
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.ReadCommitted;
                return options;
            }
        }

        public static TransactionOptions Chaos
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.Chaos;
                return options;
            }
        }

        public static TransactionOptions RepeatableRead
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.RepeatableRead;
                return options;
            }
        }

        public static TransactionOptions Serializable
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.Serializable;
                return options;
            }
        }

        public static TransactionOptions Snapshot
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.Snapshot;
                return options;
            }
        }

        public static TransactionOptions Unspecified
        {
            get
            {
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = IsolationLevel.Unspecified;
                return options;
            }
        }

        public static TransactionOptions FromMilliseconds(TransactionOptions isolationOption, int timeout)
        {
            isolationOption.Timeout = TimeSpan.FromMilliseconds(timeout);
            return isolationOption;
        }

        public static TransactionOptions FromIsolationLevel(IsolationLevel il)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = il;
            return options;
        }
    }
}