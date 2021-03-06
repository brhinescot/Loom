#region Using Directives

using System;
using System.ComponentModel;
using System.Web.UI;
using Loom.Annotations;
using Loom.Web.Portal.UI.Controls;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.Portal.UI
{
    public class PortalView : PortalPartialView
    {
        private PortalScriptManager portalScript;
        private PortalStyleManager portalStyle;

        private PortalMasterView viewTemplate;

        public string ViewTemplate { get; [UsedImplicitly] set; }

        public PortalScriptManager PortalScript
        {
            get
            {
                if (portalScript != null)
                    return portalScript;

                portalScript = new PortalScriptManager();
                PortalTrace.Write("PortalView", "PortalScript", "PortalScriptManager created.");
                return portalScript;
            }
        }

        public PortalStyleManager PortalStyle
        {
            get
            {
                if (portalStyle != null)
                    return portalStyle;

                portalStyle = new PortalStyleManager();
                PortalTrace.Write("PortalView", "PortalStyle", "PortalStyleManager created.");
                return portalStyle;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnLoad(EventArgs e)
        {
            if (Header != null)
                Header.Title = Portal.Request.Title ?? "Colossus Portal";

            base.OnLoad(e);

            if (DesignMode)
                return;

            RegisterScript();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnPreRender(EventArgs e)
        {
            PortalScript.RegisterClientScriptBlock(typeof(PortalView), "appPath", "    p$.relativeAppPath = '" + Portal.HttpContext.Request.ApplicationPath + "';");
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void Render(HtmlTextWriter writer)
        {
            PortalTrace.Write("PortalView", "Render", "Begin Render");
            ViewRenderer renderer = ViewRenderer.Create(this, viewTemplate);
            renderer.RenderAll();
            writer.Write(renderer.ToString());

            PortalTrace.Write("PortalView", "Render", "End Render");
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void CreateChildControls()
        {
            base.CreateChildControls();

            if (!Compare.IsNullOrEmpty(ViewTemplate))
                ApplyTemplate();
        }

        private void ApplyTemplate()
        {
            viewTemplate = PortalControlFactory.LoadTemplate("~/Views/Templates/" + ViewTemplate + ".ascx");
            if (viewTemplate == null)
                return;

            while (Controls.Count > 0)
                MoveControlsToTemplate();

            Controls.Add(viewTemplate);
        }

        private void MoveControlsToTemplate()
        {
            Content content = Controls[0] as Content;
            if (content == null)
            {
                LiteralControl literal = Controls[0] as LiteralControl;
                if (literal == null || !string.IsNullOrWhiteSpace(literal.Text))
                    throw new InvalidOperationException("A content page can only contain content controls.");

                Controls.RemoveAt(0);
                return;
            }

            Box box = viewTemplate.FindChild<Box>(content.BoxId);
            if (box == null)
                throw new InvalidOperationException("Unable to find a box with the id '" + content.BoxId + "'.");

            ControlCollection contentControls = content.Controls;
            while (content.Controls.Count > 0)
                box.Controls.Add(contentControls[0]);

            Controls.RemoveAt(0);
        }

        private void RegisterScript()
        {
            PortalScript.RegisterModernizrInclude();
            PortalScript.RegisterJQueryInclude(Portal.Config.JQuery);
            PortalScript.RegisterClientScriptInclude(typeof(PortalView), "portalScript", ResolveUrl("~/scriptresource/cportal-0.1.js"));
        }
    }
}