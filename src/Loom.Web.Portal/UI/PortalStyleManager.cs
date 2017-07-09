#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.UI
{
    public sealed class PortalStyleManager
    {
        private Dictionary<ClientIncludeKey, string> embeddedBlocks;
        private List<ClientIncludeKey> embeddedKeys;
        private List<ClientIncludeKey> includeKeys;
        private Dictionary<ClientIncludeKey, CssInclude> includes;

        public void RegisterCssInclude(string key, string url, string ifCondition = null)
        {
            RegisterCssInclude(null, key, url, ifCondition);
        }

        public void RegisterCssInclude(Type type, string key, string url, string ifCondition = null)
        {
            if (type == null)
                type = typeof(PortalView);

            if (includes == null)
                includes = new Dictionary<ClientIncludeKey, CssInclude>();
            if (includeKeys == null)
                includeKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (includes.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalStyleManager", "RegisterCssInclude", "type='{0}', key='{1}', url='{2}'", type.Name, key, url);

            includes.Add(clientIncludeKey, new CssInclude(url) {Condition = ifCondition});
            includeKeys.Add(clientIncludeKey);
        }

        public void RegisterEmbeddedCss(string key, string url)
        {
            RegisterEmbeddedCss(null, key, url);
        }

        public void RegisterEmbeddedCss(Type type, string key, string script)
        {
            if (type == null)
                type = typeof(PortalView);

            if (embeddedBlocks == null)
                embeddedBlocks = new Dictionary<ClientIncludeKey, string>();
            if (embeddedKeys == null)
                embeddedKeys = new List<ClientIncludeKey>();

            ClientIncludeKey clientIncludeKey = new ClientIncludeKey(type, key, true);

            if (embeddedBlocks.ContainsKey(clientIncludeKey))
                return;

            PortalTrace.Write("PortalStyleManager", "RegisterEmbeddedCss", "type='{0}', key='{1}'", type.Name, key);
            embeddedBlocks.Add(clientIncludeKey, script);
            embeddedKeys.Add(clientIncludeKey);
        }

        internal string GetCssIncludes()
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

        internal string GetEmbeddedCss()
        {
            if (embeddedKeys == null)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Append("    <style type=\"text/css\">");

            for (int i = 0; i < embeddedKeys.Count; i++)
                sb.AppendLine(embeddedBlocks[embeddedKeys[i]]);

            sb.Append("    </style>");

            return sb.ToString();
        }
    }
}