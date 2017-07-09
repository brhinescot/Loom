#region Using Directives

using System.Web.UI;
using System.Web.UI.WebControls;
using Loom.Web.Resources;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Represents a class for adding rollover and mousedown image swapping to a <see cref="WebControl" />
    ///     that has an image src property.
    /// </summary>
    internal class SwapImageProvider
    {
        /// <summary>
        ///     Register client script for adding rollover image swapping.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="rolloverImageUrl"></param>
        public static void Register(WebControl control, string rolloverImageUrl)
        {
            Register(control, rolloverImageUrl, null);
        }

        /// <summary>
        ///     Register client script for adding rollover and mousedown image swapping.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="rolloverImageUrl"></param>
        /// <param name="mouseDownImageUrl"></param>
        public static void Register(WebControl control, string rolloverImageUrl, string mouseDownImageUrl)
        {
            if (Compare.IsNullOrEmpty(rolloverImageUrl) && Compare.IsNullOrEmpty(mouseDownImageUrl))
                return;

            Page page = control.Page;

            page.ClientScript.RegisterClientScriptResource(typeof(SwapImageProvider), WebResourcePath.SwapImage);
            if (!page.ClientScript.IsStartupScriptRegistered("DevInterop_Init"))
                page.ClientScript.RegisterStartupScript(typeof(SwapImageProvider), "DevInterop_Init", Resource.SwapImagePreloadScript, true);

            if (!Compare.IsNullOrEmpty(rolloverImageUrl))
            {
                page.ClientScript.RegisterArrayDeclaration("DevInterop_Images", "'" + rolloverImageUrl + "'");

                control.Attributes["OnMouseOver"] = string.Format("DevInterop_SwapImage('{0}','{1}');", control.ClientID, page.ResolveClientUrl(rolloverImageUrl));
                control.Attributes["OnMouseOut"] = "DevInterop_RestoreImage();";
            }

            if (!Compare.IsNullOrEmpty(mouseDownImageUrl))
            {
                page.ClientScript.RegisterArrayDeclaration("DevInterop_Images", "'" + mouseDownImageUrl + "'");

                control.Attributes["OnMouseDown"] = string.Format("DevInterop_SwapImage('{0}','{1}');", control.ClientID, page.ResolveClientUrl(mouseDownImageUrl));
                control.Attributes["OnMouseUp"] = "DevInterop_RestoreImage();";
            }
        }
    }
}