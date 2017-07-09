#region Using Directives

using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.Design.WebControls
{
    ///<summary>
    ///</summary>
    [SupportsPreviewControl(true)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class WaitScreenDesigner : ControlDesigner2
    {
        /// <summary>
        ///     Retrieves the HTML markup that is used to represent the control at design time.
        /// </summary>
        /// <returns>
        ///     The HTML markup used to represent the control at design time.
        /// </returns>
        public override string GetDesignTimeHtml()
        {
            string imageUrl = GetPropertyValue<string>("ImageUrl");
            string text = GetPropertyValue<string>("Text");
            string color = ColorTranslator.ToHtml(GetPropertyValue<Color>("ForeColor"));
            string fontNames = string.Join(",", GetPropertyValue<FontInfo>("Font").Names);

            IWebApplication webApp = (IWebApplication) Component.Site.GetService(typeof(IWebApplication));
            IProjectItem item = webApp.GetProjectItemFromUrl(imageUrl);

            return string.Concat("<div style=\"color:", color, ";",
                "text-align:center;",
                "padding-top:30;",
                "font-family:", fontNames, ";",
                "font-size:12px;\">",
                text,
                "<br><br><img src=\"", item.PhysicalPath,
                "\" alt=\"busy\" /></div>");
        }
    }
}