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
    [ToolboxData("<{0}:JavaScript runat=\"server\"></{0}:JavaScript>")]
    public class Script : PortalControl
    {
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(ScriptItem))]
        public ITemplate ClientScript { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(ScriptItem))]
        public ITemplate DocumentReadyScript { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Collection<ExternalResource> Externals { get; set; }

        [Browsable(false)]
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Compare.IsNullOrEmpty(Controls))
                RenderDataBoundScript();

            if (Externals != null && Externals.Count > 0)
                RenderReferences();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // Does not render any UI elements.
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (ClientScript != null)
                BindClientScript();

            if (DocumentReadyScript != null)
                BindDocumentReadyScript();
        }

        private void RenderDataBoundScript()
        {
            foreach (Control control in Controls)
            {
                ScriptItem item = control as ScriptItem;
                if (item == null)
                    continue;

                RegisterTemplatedScript(item);
            }
        }

        private void RenderReferences()
        {
            foreach (ExternalResource resource in Externals)
            {
                if (Compare.IsNullOrEmpty(resource.Path))
                    continue;

                string path = resource.Path.ToLower(CultureInfo.InvariantCulture);
                if (resource.HeadScript)
                    View.PortalScript.RegisterClientScriptHeadInclude(path, resource.Path.ToLower());
                else
                    View.PortalScript.RegisterClientScriptInclude(path, resource.Path.ToLower());
            }
        }

        private void BindClientScript()
        {
            ScriptItem clientItem = new ScriptItem(ViewData) {ScriptType = JavaScriptType.ClientScriptBlock};
            ClientScript.InstantiateIn(clientItem);
            Controls.Add(clientItem);
            if (ViewData != null)
                clientItem.DataBind();
        }

        private void BindDocumentReadyScript()
        {
            ScriptItem documentReadyItem = new ScriptItem(ViewData) {ScriptType = JavaScriptType.DocumentReadyBlock};
            DocumentReadyScript.InstantiateIn(documentReadyItem);
            Controls.Add(documentReadyItem);
            if (ViewData != null)
                documentReadyItem.DataBind();
        }

        private void RegisterTemplatedScript(ScriptItem item)
        {
            switch (item.ScriptType)
            {
                case JavaScriptType.ClientScriptBlock:
                    View.PortalScript.RegisterClientScriptBlock(GetType(), ID, item.RenderControl());
                    break;
                case JavaScriptType.DocumentReadyBlock:
                    View.PortalScript.RegisterDocumentReadyBlock(GetType(), ID, item.RenderControl());
                    break;
            }
        }
    }
}