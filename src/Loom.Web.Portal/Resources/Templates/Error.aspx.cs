#region Using Directives

using System;
using System.Web.UI;
using Colossus.Framework.Web.Portal.UI;
using Colossus.Framework.Web.Portal.UI.Controls;

#endregion

namespace Colossus.Framework.Web.Portal.Resources.Templates
{
    public partial class Error : PortalView
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PortalStyle.RegisterCssInclude("common", "~/styleresource/common.css");
            PortalStyle.RegisterCssInclude("tables", "~/styleresource/tables.css");
//            PortalStyle.RegisterCssInclude("form", "~/styleresource/form.css");
            PortalStyle.RegisterCssInclude("menu", "~/styleresource/menu.css");
            PortalStyle.RegisterCssInclude("ieportal", "/PortalTest/scriptresource/portal.admin.ie.js", "IE");
        }
    }
}