#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     A control that displays an image and responds to mouse clicks on the image.  The control displays the image
    ///     specified in <see cref="RolloverImageButton.RolloverImageUrl" /> when the mouse is over the control.
    /// </summary>
    /// <remarks>
    ///     The control is functionally the same as an <see cref="System.Web.UI.WebControls.ImageButton" /> unless
    ///     <see cref="RolloverImageButton.RolloverImageUrl" /> has been specified.
    /// </remarks>
    /// <example>
    ///     The following example demonstrates how to create a RolloverImageButton control that changes the display image when
    ///     a user hovers the mouse over the control.
    ///     <code>
    ///  <![CDATA[
    ///  <%@ Page Language="C#" AutoEventWireup="True" %>
    ///  <%@ Register TagPrefix="cc1" 
    ///      Namespace="DevInterop.WebControls" 
    ///      Assembly="DevInterop.WebControls.RolloverImageButton" %>
    ///  <html>
    ///    <head>
    ///    <script language="C#" runat="server">
    /// 
    ///      void RolloverImageButton_Click(object sender, ImageClickEventArgs e) 
    ///      {
    ///        Label1.Text = "You clicked the RolloverImageButton control at the coordinates: (" + 
    ///        e.X.ToString() + ", " + e.Y.ToString() + ")";
    ///      }
    /// 
    ///    </script>
    ///  </head>
    ///    <body>
    ///      <form runat="server">
    ///        <h3>RolloverImageButton Sample</h3>
    ///        Click anywhere on the image.<br><br>
    ///        <cc1:RolloverImageButton id="rolloverImageButton1" runat="server"
    ///          AlternateText="RolloverImageButton 1"
    ///          ImageAlign="left"
    ///          ImageUrl="images/pict.jpg"
    ///          RolloverImageUrl="images/pict_over.jpg"
    ///          OnClick="RolloverImageButton_Click"/>
    ///        <br><br>
    ///        <asp:label id="Label1" runat="server"/>
    ///      </form>
    ///    </body>
    ///  </html>
    ///  ]]>
    ///  </code>
    /// </example>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:RolloverImageButton runat=server></{0}:RolloverImageButton>")]
    [ToolboxBitmap(typeof(RolloverImageButton), "Loom.Web.UI.WebControls.RolloverImageButton.bmp")]
    public class RolloverImageButton : ImageButton
    {
        /// <summary>
        ///     Gets or sets the path to an image to display when the mouse is over
        ///     the <see cref="RolloverImageButton" /> control.
        /// </summary>
        /// <value>
        ///     The path of the image to display when the mouse is over
        ///     the <see cref="RolloverImageButton" /> control. The default value
        ///     is <see cref="string.Empty">String.Empty</see>.
        /// </value>
        /// <remarks>
        ///     The image is displayed while the mouse is over the control.
        ///     The <see cref="RolloverImageButton.RolloverImageUrl" /> is ignored
        ///     if <see cref="System.Web.UI.WebControls.Image.ImageUrl" /> is not set.
        /// </remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        [DefaultValue("")]
        [Description("The Url of the Image to be shown while the coursor is over the control.")]
        public virtual string RolloverImageUrl
        {
            get
            {
                object obj = ViewState["RolloverImageUrl"];
                if (obj == null)
                    return string.Empty;

                return (string) obj;
            }
            set => ViewState["RolloverImageUrl"] = value;
        }

        /// <summary>
        ///     Gets or sets the path to an image to display when the OnMouseDown event of
        ///     the <see cref="RolloverImageButton" /> control is fired.
        /// </summary>
        /// <value>
        ///     The path of the image to display when the OnMouseDown event of
        ///     the <see cref="RolloverImageButton" /> control is fired. The default value
        ///     is <see cref="string.Empty">String.Empty</see>.
        /// </value>
        /// <remarks>
        ///     The image is displayed when the OnMouseDown event of the control is fired.
        ///     The <see cref="RolloverImageButton.MouseDownImageUrl" /> is ignored if
        ///     <see cref="System.Web.UI.WebControls.Image.ImageUrl" /> is not set.
        ///     The <see cref="System.Web.UI.WebControls.Image.ImageUrl" /> or
        ///     <see cref="RolloverImageButton.RolloverImageUrl" /> is restored when OnMouseUp is fired.
        /// </remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        [DefaultValue("")]
        [Description("The Url of the Image to be shown when the OnMouseDown event of the control is fired.")]
        public virtual string MouseDownImageUrl
        {
            get
            {
                object obj = ViewState["MouseDownImageUrl"];
                if (obj == null)
                    return string.Empty;

                return (string) obj;
            }
            set => ViewState["MouseDownImageUrl"] = value;
        }

        /// <summary>
        ///     Emits client JavaScript to handle preloading of images and swapping images on a mousover event.
        /// </summary>
        /// <remarks>
        ///     Client script is only registered if the values of either <see cref="RolloverImageButton.RolloverImageUrl" /> or
        ///     <see cref="RolloverImageButton.MouseDownImageUrl" /> and
        ///     <see cref="System.Web.UI.WebControls.Image.ImageUrl" /> are set.
        /// </remarks>
        protected virtual void RegisterSwapImageScript()
        {
            SwapImageProvider.Register(this, RolloverImageUrl, MouseDownImageUrl);
        }

        /// <summary>
        ///     This member overrides <see cref="System.Web.UI.WebControls.ImageButton.OnPreRender">ImageButton.OnPreRender</see>.
        /// </summary>
        /// <remarks>
        ///     This method is overriden to register client script with
        ///     the <see cref="System.Web.UI.Page">System.Web.UI.Page</see>. The script enables rollover image preloading and
        ///     swapping.
        /// </remarks>
        /// <param name="e">
        ///     An parameter of type <see cref="System.EventArgs">System.EventArgs</see>
        ///     containing data related to this event.
        /// </param>
        protected override void OnPreRender(EventArgs e)
        {
            RegisterSwapImageScript();
            base.OnPreRender(e);
        }
    }
}