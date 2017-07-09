#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Summary description for IFrame.
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [DefaultProperty("Src")]
    [ToolboxData("<{0}:IFrame runat=server></{0}:IFrame>")]
    public class IFrame : WebControl
    {
        /// <summary>
        ///     Gets or sets the frame source;.
        /// </summary>
        /// <value>The source url.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string Src { get; set; }

        /// <summary>
        ///     Gets or sets the frame border.
        /// </summary>
        /// <value>The frame border.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("0")]
        public int FrameBorder { get; set; } = 0;

        /// <summary>
        ///     Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<iframe id='{0}' src='{1}' width={2} height={3} " +
                         "marginwidth=0 marginheight=0 hspace=0 vspace=0 " +
                         "frameborder={4} scrolling=no></iframe>", ID, Src, Width, Height, FrameBorder);
        }
    }
}