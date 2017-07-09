#region Using Directives

using System;
using System.Web.UI;

#endregion

namespace Loom.Web.Tests
{
    public partial class DesignerSurface : Page
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Page.ClientScript.RegisterClientScriptResource(typeof(JQueryResourcePath), JQueryResourcePath.Core);
            Page.ClientScript.RegisterClientScriptResource(typeof(JQueryResourcePath), JQueryResourcePath.UI);
        }
    }
}