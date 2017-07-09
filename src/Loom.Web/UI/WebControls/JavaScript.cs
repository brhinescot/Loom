#region Using Directives

using System;
using System.Globalization;
using System.Web;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.WebControls
{
    [ToolboxData("<{0}:JavaScript runat=server></{0}:JavaScript>")]
    public class JavaScript : Control
    {
        public string Text { get; private set; }
        public object DataSource { get; set; }

        protected override void AddParsedSubObject(object obj)
        {
            if (!(obj is LiteralControl))
                throw new HttpException("Cannot have children of type " + obj.GetType().Name.ToString(CultureInfo.InvariantCulture));
            Text = ((LiteralControl) obj).Text;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Compare.IsNullOrEmpty(Text))
                return;

            Page.ClientScript.RegisterStartupScript(GetType(), ID, Text, true);
        }

        protected override void Render(HtmlTextWriter writer) { }
    }
}