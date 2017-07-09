#region Using Directives

using System;
using System.ComponentModel;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [DefaultProperty("Cases")]
    [ParseChildren(true, "Cases")]
    [ToolboxData("<{0}:Switch runat=\"server\"></{0}:Switch>")]
    public class Switch : PortalControl
    {
        private CaseCollection caseCollection;

        [DefaultValue(null)]
        [MergableProperty(false)]
        [Browsable(false)]
        public CaseCollection Cases => caseCollection ?? (caseCollection = new CaseCollection());

        public string Expression { get; set; }

        public string DefaultCaseId { get; set; }

        protected override void RenderBeginTag(HtmlTextWriter writer) { }

        protected override void RenderEndTag(HtmlTextWriter writer) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataBind();
        }

        protected override void CreateChildControls()
        {
            Control current = null;

            foreach (Case c in Cases)
            {
                if (Expression != c.Value)
                    continue;

                current = c;
                break;
            }

            if (current == null && (Compare.IsNullOrEmpty(DefaultCaseId) || (current = Cases.FindCase(DefaultCaseId)) == null))
                throw new IndexOutOfRangeException("Error rendering switch control. Unable to find a default case control with ID '" + DefaultCaseId + "'.");

            current.DataBind();

            Controls.Clear();
            Controls.Add(current);
        }
    }
}