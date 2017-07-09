#region Using Directives

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Body runat=server BodyID= CssClass=></{0}:Body>")]
    public class Body : WebControl
    {
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public string BodyID
        {
            get
            {
                string s = (string) ViewState["BodyID"];
                return s ?? string.Empty;
            }

            set => ViewState["BodyID"] = value;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public override string CssClass
        {
            get
            {
                string s = (string) ViewState["CssClass"];
                return s ?? string.Empty;
            }

            set => ViewState["CssClass"] = value;
        }

        protected override void OnPreRender(EventArgs e)
        {
            HtmlGenericControl body = Page.FindChild<HtmlGenericControl>(BodyID);
            if (body == null)
                throw new InvalidOperationException(string.Format("The body tag either does not have runat=\"server\", an id applied or the id does not match '{0}'.", BodyID));

            if (ViewState["CssClass"] != null)
                body.Attributes.Add("class", CssClass);

            base.OnPreRender(e);
        }
    }
}