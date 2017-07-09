#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [PersistChildren(false)]
    [ParseChildren(true)]
    [ToolboxData("<{0}:StyleSheet runat=\"server\"></{0}:StyleSheet>")]
    public class StyleSheet : PortalControl
    {
        [Browsable(false)]
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(StyleItem))]
        public ITemplate Embedded { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Collection<ExternalResource> Externals { get; set; }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (Embedded != null)
                BindEmbeddedStyle();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Compare.IsNullOrEmpty(Controls))
                RenderDataBoundStyle();

            if (Externals != null && Externals.Count > 0)
                RenderReferences();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // Does not render any UI elements.
        }

        private void RenderDataBoundStyle()
        {
            foreach (Control control in Controls)
            {
                StyleItem item = control as StyleItem;
                if (item == null)
                    continue;

                View.PortalStyle.RegisterEmbeddedCss(GetType(), ID, item.RenderControl());
            }
        }

        private void BindEmbeddedStyle()
        {
            StyleItem clientItem = new StyleItem(ViewData);
            Embedded.InstantiateIn(clientItem);
            Controls.Add(clientItem);
            if (ViewData != null)
                clientItem.DataBind();
        }

        private void RenderReferences()
        {
            foreach (ExternalResource reference in Externals)
            {
                if (Compare.IsNullOrEmpty(reference.Path))
                    continue;

                string path = reference.Path.ToLower(CultureInfo.InvariantCulture);
                View.PortalStyle.RegisterCssInclude(path, reference.Path.ToLower());
            }
        }
    }
}