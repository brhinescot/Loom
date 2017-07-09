#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI
{
    internal class ViewRenderer
    {
        private static PortalScriptManager portalScript;
        private static PortalStyleManager portalStyle;
        private readonly StringBuilder builder = new StringBuilder();

        private ViewRenderer() { }

        public int BodyCloseIndex { get; set; }
        public int BodyAttributeIndex { get; set; }
        public int HeadCloseIndex { get; set; }

        public static ViewRenderer Create(PortalView view, PortalMasterView master = null)
        {
            Argument.Assert.IsNotNull(view, nameof(view));

            portalScript = view.PortalScript;
            portalStyle = view.PortalStyle;

            ViewRenderer renderer = new ViewRenderer();
            renderer.InitializeIndexes(renderer.RenderBaseHtml(view));

            return renderer;
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        public void RenderAll()
        {
            if (portalScript != null)
            {
                RenderScriptBlocks();
                RenderScriptIncludes();
                RenderBodyScript();

                string clientScriptHeadIncludes = portalScript.GetClientScriptHeadIncludes();
                if (!Compare.IsAnyNullOrEmpty(clientScriptHeadIncludes))
                    if (HeadCloseIndex == -1)
                        builder.Insert(0, Environment.NewLine + clientScriptHeadIncludes);
                    else
                        builder.Insert(HeadCloseIndex, clientScriptHeadIncludes);
            }

            if (portalStyle == null)
                return;

            RenderEmbeddedCss();
            RenderCssIncludes();
        }

        public void RenderBodyScript()
        {
            PortalTrace.Write("ViewRenderer", "RenderBodyScript", "Begin RenderBodyScript");

            string bodyScript = portalScript.GetBodyScript();
            if (!Compare.IsNullOrEmpty(bodyScript))
                builder.Insert(BodyAttributeIndex, " onload=\"" + bodyScript + "\"");

            PortalTrace.Write("ViewRenderer", "RenderBodyScript", "End RenderBodyScript");
        }

        public void RenderScriptIncludes()
        {
            string clientScriptIncludes = portalScript.GetClientScriptIncludes();
            if (!Compare.IsAnyNullOrEmpty(clientScriptIncludes))
                if (BodyCloseIndex == -1)
                    builder.Append(Environment.NewLine + clientScriptIncludes);
                else
                    builder.Insert(BodyCloseIndex, Environment.NewLine + clientScriptIncludes);
        }

        public void RenderScriptBlocks()
        {
            PortalTrace.Write("ViewRenderer", "RenderScriptBlocks", "Begin RenderScriptBlocks");

            string allScriptBlocks = portalScript.GetAllScriptBlocks();
            if (!Compare.IsAnyNullOrEmpty(allScriptBlocks))
                if (BodyCloseIndex == -1)
                    builder.Append(Environment.NewLine + allScriptBlocks);
                else
                    builder.Insert(BodyCloseIndex, Environment.NewLine + allScriptBlocks);

            PortalTrace.Write("ViewRenderer", "RenderScriptBlocks", "End RenderScriptBlocks");
        }

        public void RenderCssIncludes()
        {
            PortalTrace.Write("ViewRenderer", "RenderCssIncludes", "Begin RenderCssIncludes");

            string cssIncludes = portalStyle.GetCssIncludes();
            if (!Compare.IsAnyNullOrEmpty(cssIncludes))
                builder.Insert(HeadCloseIndex == -1 ? 0 : HeadCloseIndex, Environment.NewLine + cssIncludes);

            PortalTrace.Write("ViewRenderer", "RenderCssIncludes", "End RenderCssIncludes");
        }

        public void RenderEmbeddedCss()
        {
            PortalTrace.Write("ViewRenderer", "RenderEmbeddedCss", "Begin RenderEmbeddedCss");

            string embeddedCss = portalStyle.GetEmbeddedCss();
            if (!Compare.IsAnyNullOrEmpty(embeddedCss))
                builder.Insert(HeadCloseIndex == -1 ? 0 : HeadCloseIndex, Environment.NewLine + embeddedCss);

            PortalTrace.Write("ViewRenderer", "RenderEmbeddedCss", "End RenderEmbeddedCss");
        }

        private string RenderBaseHtml(PortalView view)
        {
            using (StringWriter sw = new StringWriter(builder, CultureInfo.InvariantCulture))
            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(sw))
            {
                view.RenderView(htmlWriter);
                return builder.ToString();
            }
        }

        private void InitializeIndexes(string baseHtml)
        {
            BodyAttributeIndex = baseHtml.IndexOf("<body>", StringComparison.OrdinalIgnoreCase) + 5;
            BodyCloseIndex = baseHtml.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
            HeadCloseIndex = baseHtml.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
        }
    }
}