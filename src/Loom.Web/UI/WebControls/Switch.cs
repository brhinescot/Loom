#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [ToolboxData("<{0}:Switch runat=server></{0}:Switch>")]
    [ParseChildren(true, "Cases")]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class Switch : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [MergableProperty(false)]
        public CaseCollection Cases => (CaseCollection) Controls;

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        public string Value { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataBind();
        }

        protected override ControlCollection CreateControlCollection()
        {
            return new CaseCollection(this);
        }

        protected override void CreateChildControls()
        {
            Case defaultCase = null;

            foreach (Case item in Cases)
            {
                if (!item.Default && Value == item.Value)
                {
                    Controls.Clear();
                    Controls.Add(item);
                    return;
                }

                if (!item.Default)
                    continue;

                if (defaultCase != null)
                    throw new HttpException("More than one default Case control was found.");
                defaultCase = item;
            }

            Controls.Clear();
            if (defaultCase != null)
                Controls.Add(defaultCase);
        }
    }
}