#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web.Portal
{
    public interface ITraceContext
    {
        TraceMode TraceMode { get; set; }
        bool IsEnabled { get; set; }
        void Write(string message);
        void Write(string category, string message);
        void Write(string category, string message, Exception errorInfo);
        void Warn(string message);
        void Warn(string category, string message);
        void Warn(string category, string message, Exception errorInfo);
        event TraceContextEventHandler TraceFinished;
    }
}