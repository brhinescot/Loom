#region Using Directives

using System.ComponentModel;
using System.Web.UI;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(true, "Text")]
    [ToolboxData("<{0}:Literal runat=\"server\"></{0}:Literal>")]
    public class Literal : PortalControl, ITextControl
    {
        public bool DisableEncoding { get; set; }

        public string Format { get; set; }

        #region ITextControl Members

        [Localizable(true)]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        public virtual string Text { get; set; }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(DisableEncoding ? Text : AntiXss.HtmlEncode(Text));
        }

        protected override void OnSetViewData(object data)
        {
            if (data == null)
                return;

            string s = data as string;
            if (!Compare.IsNullOrEmpty(s))
            {
                Text = s;
                return;
            }

            Text = data.ToString();
        }
    }
}