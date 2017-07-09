#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using Loom.Web.Portal.Configuration;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.UI
{
    public sealed class PortalScriptManager
    {
        private Dictionary<ClientIncludeKey, string> body;
        private List<ClientIncludeKey> bodyKeys;

        private Dictionary<ClientIncludeKey, string> clientScriptBlocks;
        private List<ClientIncludeKey> clientScriptKeys;

        private Dictionary<ClientIncludeKey, string> documentReadyBlocks;
        private List<ClientIncludeKey> documentReadyKeys;

        private Dictionary<string, Dictionary<string, string>> expandoBlocks;
        private List<string> expandoKeys;
        private List<ClientIncludeKey> headIncludeKeys;

        private Dictionary<ClientIncludeKey, ScriptInclude> headIncludes;
        private List<ClientIncludeKey> includeKeys;

        private Dictionary<ClientIncludeKey, ScriptInclude> includes;

        // TODO: Possibly move jQuery registration to a one time application start event.
        public void RegisterJQueryInclude(JQueryElement jQuery = null)
        {
            string url = null;
            bool disableFallback = false;
            if (jQuery != null)
            {
                string extension = jQuery.Debug ? ".js" : ".min.js";
                disableFallback = jQuery.DisableFallback;

                switch (jQuery.Cdn)
                {
                    case Strings.Microsoft:
                        // Documentation: http://www.asp.net/ajaxlibrary/cdn.ashx
                        url = "//ajax.aspnetcdn.com/ajax/jQuery/jquery-" + jQuery.Version + extension;
                        break;
                    case Strings.Google:
                        // Documentation: http://code.google.com/apis/libraries/devguide.html#jquery
                        url = "//ajax.googleapis.com/ajax/libs/jquery/" + jQuery.Version + "/jquery" + extension;
                        break;
                    case Strings.JQuery:
                        url = "//code.jquery.com/jquery-" + jQuery.Version + extension;
                        break;
                    default:
                        url = jQuery.Cdn;
                        break;
                }
            }

            if (url == null)
            {
                RegisterClientScriptInclude(Strings.JQuery, Strings.JQueryResourcePath);
            }
            else
            {
                RegisterClientScriptInclude(Strings.JQuery, url);
                if (!disableFallback)
                    RegisterClientScriptBlock(Strings.JqueryFallback, "    !window.jQuery && document.write('<script src=\"" + Strings.JQueryResourcePath + "\"><\\/script>');" + Environment.NewLine);
            }
        }

        public void RegisterModernizrInclude()
        {
            RegisterClientScriptHeadInclude(Strings.Modernizr, Strings.ModernizrResourcePath);
        }

        public void RegisterExpandoAttribute(string id, string attribute, string value)
        {
            if (expandoBlocks == null)
                expandoBlocks = new Dictionary<string, Dictionary<string, string>>();
            if (expandoKeys == null)
                expandoKeys = new List<string>();

            bool containsKey = expandoBlocks.ContainsKey(id);
            if (containsKey && expandoBlocks[id].ContainsKey(attribute))
                return;

            if (containsKey)
            {
                PortalTrace.Write("PortalScriptManager", "RegisterExpandoAttribute", "id='{0}', attribute='{1}', value='{2}'", id, attribute, value);
                expandoBlocks[id][attribute] = value;
                return;
            }

            PortalTrace.Write("PortalScriptManager", "RegisterExpandoAttribute", "id='{0}', attribute='{1}', value='{2}'", id, attribute, value);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes[attribute] = value;
            expandoBlocks[id] = attributes;
            expandoKeys.Add(id);
        }

        public void RegisterClientScriptBlock(string key, string script)
        {
            RegisterClientScriptBlock(null, key, script);
        }

        public void RegisterClientScriptBlock(Type type, string key, string script)
        {
            if (type == null)
                type = typeof(PortalView);

            if (clientScriptBlocks == null)
                clientScriptBlocks = new Dictionary<ClientIncludeKey, string>();
            if (clientScriptKeys == null)
                clientScriptKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (clientScriptBlocks.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalScriptManager", "RegisterClientScriptBlock", "type='{0}', key='{1}'", type.Name, key);
            clientScriptBlocks.Add(clientIncludeKey, script);
            clientScriptKeys.Add(clientIncludeKey);
        }

        public void RegisterDocumentReadyBlock(string key, string url)
        {
            RegisterDocumentReadyBlock(null, key, url);
        }

        public void RegisterDocumentReadyBlock(Type type, string key, string script)
        {
            if (type == null)
                type = typeof(PortalView);

            if (documentReadyBlocks == null)
                documentReadyBlocks = new Dictionary<ClientIncludeKey, string>();
            if (documentReadyKeys == null)
                documentReadyKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (documentReadyBlocks.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalScriptManager", "RegisterDocumentReadyBlock", "type='{0}', key='{1}'", type.Name, key);
            documentReadyBlocks.Add(clientIncludeKey, script);
            documentReadyKeys.Add(clientIncludeKey);
        }

        public void RegisterClientScriptInclude(string key, string url)
        {
            RegisterClientScriptInclude(null, key, url);
        }

        public void RegisterClientScriptInclude(Type type, string key, string url)
        {
            if (type == null)
                type = typeof(PortalView);

            if (includes == null)
                includes = new Dictionary<ClientIncludeKey, ScriptInclude>();
            if (includeKeys == null)
                includeKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (includes.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalScriptManager", "RegisterClientScriptInclude", "type='{0}', key='{1}', url='{2}'", type.Name, key, url);
            includes.Add(clientIncludeKey, new ScriptInclude(url));
            includeKeys.Add(clientIncludeKey);
        }

        public void RegisterClientScriptHeadInclude(string key, string url)
        {
            RegisterClientScriptHeadInclude(null, key, url);
        }

        public void RegisterClientScriptHeadInclude(Type type, string key, string url)
        {
            if (type == null)
                type = typeof(PortalView);

            if (headIncludes == null)
                headIncludes = new Dictionary<ClientIncludeKey, ScriptInclude>();
            if (headIncludeKeys == null)
                headIncludeKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (headIncludes.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalScriptManager", "RegisterClientScriptInclude", "type='{0}', key='{1}', url='{2}'", type.Name, key, url);
            headIncludes.Add(clientIncludeKey, new ScriptInclude(url));
            headIncludeKeys.Add(clientIncludeKey);
        }

        public void RegisterBodyScript(Type type, string key, string script)
        {
            if (type == null)
                type = typeof(PortalView);

            if (body == null)
                body = new Dictionary<ClientIncludeKey, string>();
            if (bodyKeys == null)
                bodyKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (body.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalScriptManager", "RegisterBodyScript", "type='{0}', key='{1}'", type.Name, key);
            body.Add(clientIncludeKey, script);
            bodyKeys.Add(clientIncludeKey);
        }

        internal string GetAllScriptBlocks()
        {
            if (clientScriptKeys == null && documentReadyKeys == null && expandoKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
//            sb.AppendLine("    // <![CDATA[");

            if (expandoKeys != null)
            {
                sb.AppendLine("function registerExpandos(){");
                for (int i = 0; i < expandoKeys.Count; i++)
                {
                    sb.AppendLine("    var " + expandoKeys[i] + "Ex = document.all ? document.all['" + expandoKeys[i] + "'] : document.getElementById('" + expandoKeys[i] + "');");
                    foreach (KeyValuePair<string, string> pair in expandoBlocks[expandoKeys[i]])
                        sb.AppendLine("    " + expandoKeys[i] + "Ex." + pair.Key + " = '" + pair.Value + "';");
                }
                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendLine("registerExpandos();");
                sb.AppendLine();
            }

            if (clientScriptKeys != null)
            {
                for (int i = 0; i < clientScriptKeys.Count; i++)
                    sb.AppendLine(clientScriptBlocks[clientScriptKeys[i]]);
                sb.AppendLine();
            }

            if (documentReadyKeys != null)
            {
                sb.AppendLine("    $(function(){");
                for (int i = 0; i < documentReadyKeys.Count; i++)
                    sb.AppendLine(documentReadyBlocks[documentReadyKeys[i]]);
                sb.AppendLine("    });");
            }

//            sb.AppendLine("    // ]]>");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        internal string GetClientScriptBlock()
        {
            if (clientScriptKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("    // <![CDATA[");

            for (int i = 0; i < clientScriptKeys.Count; i++)
                sb.AppendLine(clientScriptBlocks[clientScriptKeys[i]]);

            sb.AppendLine("    // ]]>");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        internal string GetDocumentReadyBlock()
        {
            if (documentReadyKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("    // <![CDATA[");
            sb.AppendLine("    $(function(){");

            for (int i = 0; i < documentReadyKeys.Count; i++)
                sb.AppendLine(documentReadyBlocks[documentReadyKeys[i]]);

            sb.AppendLine("    });");
            sb.AppendLine("    // ]]>");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        internal string GetClientScriptIncludes()
        {
            if (includeKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture))
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                writer.Indent = 0;
                for (int i = 0; i < includeKeys.Count; i++)
                    includes[includeKeys[i]].RenderControl(writer);
            }

            return sb.ToString();
        }

        internal string GetClientScriptHeadIncludes()
        {
            if (headIncludeKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture))
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                writer.Indent = 0;
                for (int i = 0; i < headIncludeKeys.Count; i++)
                    headIncludes[headIncludeKeys[i]].RenderControl(writer);
            }

            return sb.ToString();
        }

        internal string GetBodyScript()
        {
            if (bodyKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bodyKeys.Count; i++)
                sb.Append(body[bodyKeys[i]]);

            return sb.ToString();
        }
    }
}