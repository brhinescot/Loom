#region Using Directives

using System.Diagnostics;
using System.Globalization;

#endregion

namespace Loom.Web.Portal
{
    [DebuggerStepThrough]
    internal static class PortalTrace
    {
        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void Write(string typeName, string methodName, string message)
        {
            if (PortalContext.Current == null)
                return;

            if (typeName != methodName)
                Trace.Write(typeName + "." + methodName + ": " + message, "Portal");
            else
                Trace.Write(typeName + ": " + message, "Portal");
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void Write(string typeName, string methodName, string format, params object[] args)
        {
            if (PortalContext.Current == null)
                return;

            if (typeName != methodName)
                Trace.Write(typeName + "." + methodName + ": " + string.Format(CultureInfo.CurrentCulture, format, args), "Portal");
            else
                Trace.Write(typeName + ": " + string.Format(CultureInfo.CurrentCulture, format, args), "Portal");
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void WriteIf(bool condidtion, string typeName, string methodName, string message)
        {
            if (PortalContext.Current == null)
                return;

            if (!condidtion)
                return;

            Write(typeName, methodName, message);
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void WriteIf(bool condidtion, string typeName, string methodName, string format, params object[] args)
        {
            if (PortalContext.Current == null)
                return;

            if (!condidtion)
                return;

            Write(typeName, methodName, format, args);
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void Warn(string typeName, string methodName, string message)
        {
            if (PortalContext.Current == null)
                return;

            if (typeName != methodName)
                Trace.TraceWarning(typeName + "." + methodName + ": " + message);
            else
                Trace.TraceWarning(typeName + ": " + message);
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void Warn(string typeName, string methodName, string format, params object[] args)
        {
            if (PortalContext.Current == null)
                return;

            if (typeName != methodName)
                Trace.TraceWarning(typeName + "." + methodName + ": " + string.Format(CultureInfo.CurrentCulture, format, args));
            else
                Trace.TraceWarning(typeName + ": " + string.Format(CultureInfo.CurrentCulture, format, args));
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void WarnIf(bool condidtion, string typeName, string methodName, string message)
        {
            if (PortalContext.Current == null)
                return;

            if (!condidtion)
                return;

            Warn(typeName, methodName, message);
        }

        [DebuggerStepThrough]
        [Conditional("TRACE")]
        public static void WarnIf(bool condidtion, string typeName, string methodName, string format, params object[] args)
        {
            if (PortalContext.Current == null)
                return;

            if (!condidtion)
                return;

            Warn(typeName, methodName, format, args);
        }
    }
}