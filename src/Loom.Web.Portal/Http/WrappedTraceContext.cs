#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web.Portal.Http
{
    public sealed class WrappedTraceContext : ITraceContext
    {
        private readonly TraceContext traceContext;

        public WrappedTraceContext(TraceContext traceContext)
        {
            Argument.Assert.IsNotNull(traceContext, nameof(traceContext));

            this.traceContext = traceContext;
        }

        #region ITraceContext Members

        public void Write(string message)
        {
            traceContext.Write(message);
        }

        public void Write(string category, string message)
        {
            traceContext.Write(category, message);
        }

        public void Write(string category, string message, Exception errorInfo)
        {
            traceContext.Write(category, message, errorInfo);
        }

        public void Warn(string message)
        {
            traceContext.Warn(message);
        }

        public void Warn(string category, string message)
        {
            traceContext.Warn(category, message);
        }

        public void Warn(string category, string message, Exception errorInfo)
        {
            traceContext.Warn(category, message, errorInfo);
        }

        public TraceMode TraceMode
        {
            get => traceContext.TraceMode;
            set => traceContext.TraceMode = value;
        }

        public bool IsEnabled
        {
            get => traceContext.IsEnabled;
            set => traceContext.IsEnabled = value;
        }

        public event TraceContextEventHandler TraceFinished
        {
            add => traceContext.TraceFinished += value;
            remove => traceContext.TraceFinished -= value;
        }

        #endregion
    }
}