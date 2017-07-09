#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loom.Web.Syndication;

#endregion

namespace Loom.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:RssHyperLink runat=\"server\" Text=\"RSS\"></{0}:RssHyperLink>")]
    public class RssHyperLink : HyperLink, ILocalizable
    {
        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<RssHyperLink> extender;

        public RssHyperLink()
        {
            extender = new ControlExtender<RssHyperLink>(this);
        }

        /// <summary>
        ///     passed to RssHttpHandler
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        ///     when flag is set, the current user's name is passed to RssHttpHandler
        /// </summary>
        public bool IncludeUserName { get; set; }

        #region ILocalizable Members

        [Bindable(true)]
        [Category("Misc")]
        [DefaultValue("")]
        public string ResourceKey
        {
            get
            {
                object o = ViewState["ResourceKey"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["ResourceKey"] = value;
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            string channel = ChannelName ?? string.Empty;
            string user = IncludeUserName ? Context.User.Identity.Name : string.Empty;
            NavigateUrl = RssHttpHandlerHelper.GenerateChannelLink(NavigateUrl, channel, user);
            base.OnPreRender(e);
        }
    }
}