#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.PageParts
{
    public class ContentRenderer : IDisposable
    {
        private readonly Dictionary<string, string> clientFunctions = new Dictionary<string, string>();

        public ContentRenderer(HtmlTextWriter writer)
        {
            const string nullArgMessage = "The HtmlTextWriter parameter can not be null";
            const string argWriterName = "writer";

            if (writer == null)
                throw new ArgumentNullException(argWriterName, nullArgMessage);

            InnerWriter = writer;
        }

        public string ClientFunctionBlock
        {
            get
            {
                const string scriptOpening = "<script language='javascript' type='text/javascript'>";
                const string scriptCommentOpening = "<!--";
                const string scriptCommentClosing = "//-->";
                const string scriptClosing = "</script>";

                StringBuilder builder = new StringBuilder();
                builder.AppendLine();
                builder.AppendLine(scriptOpening);
                builder.AppendLine(scriptCommentOpening);
                foreach (KeyValuePair<string, string> function in clientFunctions)
                    builder.AppendLine(function.Value);
                builder.AppendLine(scriptCommentClosing);
                builder.AppendLine(scriptClosing);

                return builder.ToString();
            }
        }

        /// <summary>
        ///     Gets the <see cref="HtmlTextWriter" /> used to render this instance.
        /// </summary>
        public HtmlTextWriter InnerWriter { get; }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        public void AddAttribute(string name, string value)
        {
            InnerWriter.AddAttribute(name, value);
        }

        public void AddAttribute(HtmlTextWriterAttribute key, string value)
        {
            InnerWriter.AddAttribute(key, value);
        }

        public void AddStyleAttribute(HtmlTextWriterStyle key, string value)
        {
            InnerWriter.AddStyleAttribute(key, value);
        }

        public void AddStyleAttribute(string name, string value)
        {
            InnerWriter.AddStyleAttribute(name, value);
        }

        public void AddClientEvent(ClientEvent key, IClientScriptProvider clientScript)
        {
            if (clientScript.FunctionKey != null && !clientFunctions.ContainsKey(clientScript.FunctionKey))
                clientFunctions.Add(clientScript.FunctionKey, clientScript.Function);

            AddClientEvent(key, clientScript.InlineScript);
        }

        public void AddClientEvent(ClientEvent key, string value)
        {
            InnerWriter.AddAttribute(GetClientEventName(key), value);
        }

        public void RenderTextBox(string value, string id)
        {
            RenderTextBox(value, id, null);
        }

        public void RenderTextBox(string value, string id, string label)
        {
            RenderTextBox(value, id, label, null);
        }

        public void RenderTextBox(string value, string id, string label, string cssClass)
        {
            RenderTextBox(value, id, label, cssClass, false);
        }

        public void RenderTextBox(string value, string id, string label, string cssClass, bool password)
        {
            const string passwordType = "password";
            const string textType = "text";

            if (id != null)
                InnerWriter.AddAttribute(HtmlTextWriterAttribute.Id, id);
            if (cssClass != null)
                InnerWriter.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            if (value != null)
                InnerWriter.AddAttribute(HtmlTextWriterAttribute.Value, value);
            InnerWriter.AddAttribute(HtmlTextWriterAttribute.Type, password ? passwordType : textType);
            InnerWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            InnerWriter.RenderEndTag();
        }

        /// <summary>
        ///     Writes the specified string to the output stream, along with any pending tab spacing.
        /// </summary>
        /// <param name="s">The string to write to the output stream. </param>
        public void RenderLiteral(string s)
        {
            InnerWriter.Write(s);
        }

        public void RenderRepeater<T>(IEnumerable<T> data, IListRenderer<T> renderer)
        {
            renderer.RenderHeader(this);

            foreach (T item in data)
                renderer.RenderItem(this, item);

            renderer.RenderFooter(this);
        }

        public void RenderImage(string path)
        {
            RenderImage(path, null);
        }

        public void RenderImage(string path, string alt)
        {
            const string nullArgMessage = "The path parameter can not be null";
            const string argPathName = "path";

            if (path == null)
                throw new ArgumentNullException(argPathName, nullArgMessage);

            if (alt != null)
                InnerWriter.AddAttribute(HtmlTextWriterAttribute.Alt, alt);
            InnerWriter.AddAttribute(HtmlTextWriterAttribute.Src, path);
            InnerWriter.RenderBeginTag(HtmlTextWriterTag.Img);
            InnerWriter.RenderEndTag();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                InnerWriter.Dispose();
        }

        private static string GetClientEventName(ClientEvent clientEvent)
        {
            const string onClick = "onClick";
            const string onMouseOver = "onMouseOver";
            const string onMouseOut = "onMouseOut";
            const string onBlur = "onBlur";
            const string onFocus = "onFocus";
            const string onAbort = "onAbort";
            const string onError = "onError";
            const string onLoad = "onLoad";
            const string onChange = "onChange";
            const string onSelect = "onSelect";

            switch (clientEvent)
            {
                case ClientEvent.OnClick:
                    return onClick;
                case ClientEvent.OnMouseOver:
                    return onMouseOver;
                case ClientEvent.OnMouseOut:
                    return onMouseOut;
                case ClientEvent.OnBlur:
                    return onBlur;
                case ClientEvent.OnFocus:
                    return onFocus;
                case ClientEvent.OnAbort:
                    return onAbort;
                case ClientEvent.OnError:
                    return onError;
                case ClientEvent.OnLoad:
                    return onLoad;
                case ClientEvent.OnChange:
                    return onChange;
                case ClientEvent.OnSelect:
                    return onSelect;
                default:
                    const string argClientEventName = "clientEvent";
                    throw new ArgumentOutOfRangeException(argClientEventName);
            }
        }
    }
}