#region Using Directives

using System;
using System.Web;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ToolboxData("<{0}:AntiForgeryToken runat=\"server\"></{0}:AntiForgeryToken>")]
    [ParseChildren(false)]
    public sealed class AntiForgeryToken : PortalControl
    {
        private string formToken;

        /// <summary>
        ///     Gets or sets the domain associated with the anti-forgery cookie.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        ///     Gets or sets the path associated with the anti-forgery cookie.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     Gets or sets the salt used to generate the anti-forgery token and cookie values.
        /// </summary>
        public string Salt { get; set; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Input;

        protected override void OnLoad(EventArgs e)
        {
            HttpCookie existingCookie = Page.Request.Cookies[AntiForgeryData.GetTokenName(Page.Request.ApplicationPath)];
            formToken = existingCookie == null
                ? AntiForgeryData.GetDataAndSetCookie(Salt, Domain, Path)
                : AntiForgeryData.GetDataFromCookie(existingCookie, Salt, Domain, Path);

            base.OnLoad(e);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, AntiForgeryData.GetTokenName(null));
            writer.AddAttribute(HtmlTextWriterAttribute.Value, formToken);
        }
    }
}