#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Loom
{
    public class JsonFormatter
    {
        private const int DefaultIndent = 0;
        private const string Space = " ";
        private const string Tab = Space + Space + Space + Space;

        private static readonly string NewLine = Environment.NewLine;
        private readonly Stack<Context> context = new Stack<Context>();
        private bool inDoubleString;
        private bool inSingleString;
        private bool inVariableAssignment;
        private char prev = '\0';

        private bool InString => inDoubleString || inSingleString;

        public string Format(string json)
        {
            return PrettyPrint(json);
        }

        private static void HandleEqual(char c, StringBuilder buffer)
        {
            buffer.Append(c);
        }

        private static void Indent(int tabCount, StringBuilder buffer)
        {
            tabCount += DefaultIndent;
            while (tabCount-- > 0)
                buffer.Append(Tab);
        }

        private void HandleCloseBrace(char c, StringBuilder buffer)
        {
            if (!InString)
            {
                buffer.Append(NewLine);
                context.Pop();
                Indent(context.Count, buffer);
                buffer.Append(c);
            }
            else
            {
                buffer.Append(c);
            }
        }

        private void HandleCloseBracket(char c, StringBuilder buffer)
        {
            if (!InString)
            {
                buffer.Append(NewLine);
                context.Pop();
                Indent(context.Count, buffer);
                buffer.Append(c);
            }
            else
            {
                buffer.Append(c);
            }
        }

        private void HandleColon(char c, StringBuilder buffer)
        {
            if (!InString)
            {
                inVariableAssignment = true;
                buffer.Append(Space);
                buffer.Append(c);
                buffer.Append(Space);
            }
            else
            {
                buffer.Append(c);
            }
        }

        private void HandleComma(char c, StringBuilder buffer)
        {
            buffer.Append(c);

            if (InString || context.Peek() == Context.Array)
                return;

            Indent(context.Count, buffer);
            buffer.Append(NewLine);
            Indent(context.Count, buffer);
            inVariableAssignment = false;
        }

        private void HandleDoubleQuote(char c, StringBuilder buffer)
        {
            if (!inSingleString && prev != '\\')
                inDoubleString = !inDoubleString;
            buffer.Append(c);
        }

        private void HandleOpenBrace(char c, StringBuilder buffer)
        {
            if (!InString)
            {
                if (inVariableAssignment || context.Count > 0)
                {
                    Context ctx = context.Peek();
                    if (ctx != Context.Array)
                    {
                        buffer.Append(NewLine);
                        Indent(context.Count, buffer);
                    }
                }

                buffer.Append(c);
                buffer.Append(NewLine);
                context.Push(Context.Object);
                Indent(context.Count, buffer);
            }
            else
            {
                buffer.Append(c);
            }
        }

        private void HandleOpenBracket(char c, StringBuilder buffer)
        {
            buffer.Append(c);
            if (!InString)
                context.Push(Context.Array);
        }

        private void HandleSingleQuote(char c, StringBuilder buffer)
        {
            if (!inDoubleString && prev != '\\')
                inSingleString = !inSingleString;

            buffer.Append(c);
        }

        private void HandleSpace(char c, StringBuilder buffer)
        {
            if (InString)
                buffer.Append(c);
        }

        private string PrettyPrint(string json)
        {
            StringBuilder buffer = new StringBuilder();
            Indent(context.Count, buffer);
            for (int i = 0; i < json.Length; i++)
            {
                char c = json[i];

                switch (c)
                {
                    case '{':
                        HandleOpenBrace(c, buffer);
                        break;
                    case '}':
                        HandleCloseBrace(c, buffer);
                        break;
                    case '[':
                        HandleOpenBracket(c, buffer);
                        break;
                    case ']':
                        HandleCloseBracket(c, buffer);
                        break;
                    case '=':
                        HandleEqual(c, buffer);
                        break;
                    case ',':
                        HandleComma(c, buffer);
                        break;
                    case '\'':
                        HandleSingleQuote(c, buffer);
                        break;
                    case ':':
                        HandleColon(c, buffer);
                        break;
                    case '"':
                        HandleDoubleQuote(c, buffer);
                        break;
                    case ' ':
                        HandleSpace(c, buffer);
                        break;
                    default:
                        buffer.Append(c);
                        break;
                }
                prev = c;
            }
            return buffer.ToString();
        }

        #region Nested type: Context

        private enum Context
        {
            Object,
            Array
        }

        #endregion
    }
}