#region Using Directives

using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web.UI
{
    public static class JsonSerializer
    {
        private const string CloseCurly = "}";
        private const string CloseParenthesis = ")";
        private const string CloseSquare = "]";
        private const char Comma = ',';
        private const string False = "false";
        private const string NewData = "new Date(";
        private const string OpenCurly = "{";
        private const string OpenSquare = "[";
        private const string SingleQuote = "'";
        private const string SingleQuoteColon = "':";
        private const int TrimLength = 1;
        private const string True = "true";

        private static readonly DateTime MinDate = new DateTime(1970, 1, 1);

        public static string Serialize(object obj)
        {
            return SerializePrivate(obj);
        }

        private static string SerializePrivate(object obj)
        {
            StringBuilder sb = new StringBuilder(OpenCurly);
            Type type = obj.GetType();

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!propertyInfo.CanRead || propertyInfo.GetIndexParameters().Length > 0)
                    continue;

                object value = propertyInfo.GetValue(obj, null);
                if (value == null)
                    continue;

                sb.Append(SingleQuote + propertyInfo.Name + SingleQuoteColon);
                AppendPropertyValue(sb, value);
                sb.Append(Comma);
            }

            if (sb[sb.Length - TrimLength] == Comma)
                sb.Length -= TrimLength;

            sb.Append(CloseCurly);

            return sb.ToString();
        }

        private static void AppendPropertyValue(StringBuilder sb, object value)
        {
            Type type = value.GetType();

            if (type == typeof(string))
                sb.Append(AntiXss.JavaScriptEncode((string) value));
            else if (type == typeof(int) || type == typeof(long) || type == typeof(decimal) || type == typeof(double) || type == typeof(short) || type == typeof(float))
                sb.Append(value);
            else if (type == typeof(bool))
                sb.Append((bool) value ? True : False);
            else if (type == typeof(DateTime))
                sb.Append(NewData + ((DateTime) value - MinDate).TotalMilliseconds + CloseParenthesis);
            else if (value is IEnumerable)
                AppendArray(sb, (IEnumerable) value);
            else
                sb.Append(AntiXss.JavaScriptEncode(value.ToString()));
        }

        private static void AppendArray(StringBuilder sb, IEnumerable items)
        {
            sb.Append(OpenSquare);
            foreach (object o in items)
            {
                AppendPropertyValue(sb, o);
                sb.Append(Comma);
            }

            if (sb[sb.Length - TrimLength] == Comma)
                sb.Length -= TrimLength;

            sb.Append(CloseSquare);
        }
    }
}