#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    ///<summary>
    ///</summary>
    [PermissionSet(SecurityAction.LinkDemand)]
    [PermissionSet(SecurityAction.InheritanceDemand)]
    [DefaultEvent("Process ")]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WaitScreen runat=\"server\" Text=\"Please wait...\"/>")]
    [Designer("Loom.Web.UI.Design.WebControls.WaitScreenDesigner, Loom.Design, Version=0.9.1.0, Culture=neutral, PublicKeyToken=f77fee4373937a8c")]
    public class WaitScreen : WebControl
    {
        /// <summary>
        ///     Backing field for the ImageUrl property.
        /// </summary>
        private string imageUrl = string.Empty;

        /// <summary>
        ///     Backing field for the Text property.
        /// </summary>
        private string text;

        /// <summary>
        ///     Gets or sets the url of an image to display.
        /// </summary>
        /// <value>
        ///     A <see cref="string">string</see> containing the url pointing
        ///     to an image.
        /// </value>
        [Description("The url of an image to display.")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        [Browsable(true)]
        [DefaultValue("")]
        [Category("Appearance")]
        public string ImageUrl
        {
            get => imageUrl;
            set => imageUrl = value;
        }

        /// <summary>
        ///     Gets or sets the text to display.
        /// </summary>
        /// <value>The text to display.</value>
        [Description("The url of an image to display.")]
        [Editor(EditorType.MultiLineString, typeof(UITypeEditor))]
        [Browsable(true)]
        [DefaultValue("")]
        [Category("Appearance")]
        public string Text
        {
            get => text;
            set => text = value;
        }

        /// <summary>
        ///     Occurs during the OnLoad method of the control to allow server side processing while the wait screen is displayed.
        /// </summary>
        public event EventHandler<EventArgs> Process;

        /// <summary>
        ///     Raises the <see cref="System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            EventHandler<EventArgs> handler = Process;
            if (handler != null)
            {
                AddStyleAttributes();
                Page.Response.Buffer = true;

                #region SCRIPT: Show splash screen

                StringBuilder showSplash = new StringBuilder();
                showSplash.Append(@"
<script language=""javascript"" type=""text/javascript"">
    //<![CDATA[
    <!--
    if(document.getElementById){ 
        var upLevel = true;}
    else if(document.layers){ 
        var ns4 = true;}
    else if(document.all) { 
        var ie4 = true; }
    function showObject(obj) {
        if (ns4){
            obj.visibility = 'show';}
        else if (ie4 || upLevel){
            obj.style.visibility = 'visible';} }
    function hideObject(obj) {
        if (ns4){
            obj.visibility = 'hide';
            obj.display = 'none';}
        if (ie4 || upLevel){
            obj.style.display = 'none';}}
    // --> //]]>
</script>");
                showSplash.AppendFormat("<div id=\"{0}\" ", ClientID);
                if (CssClass.Length > 0)
                    showSplash.AppendFormat("class=\"{0}\" ", CssClass);
                if (Style.Value.Length > 0)
                    showSplash.AppendFormat("style=\"{0}text-align:center;\" ", Style.Value);

                showSplash.AppendFormat(">{0}<br><br>", text);

                if (imageUrl.Length > 0)
                    showSplash.AppendFormat("<img src=\"{0}\" alt=\"{1}\" />", ResolveUrl(imageUrl), text);

                showSplash.Append("</div>");

                Page.Response.Write(showSplash.ToString());

                #endregion

                Page.Response.Flush();
                OnProcess(EventArgs.Empty);
                Page.Response.Flush();

                #region SCRIPT: Hide splash screen

                Page.Response.Write(@"
<script language=""javascript"" type=""text/javascript"">
    //<![CDATA[
    <!--
    var splash
    if(upLevel){
        splash = document.getElementById('" + ClientID + @"');}
    else if(ns4){
        splash = document." + ClientID + @";}
    else if(ie4){
       splash = document.all." + ClientID + @";}
    hideObject(splash);
    // -->//]]>
</script>");

                #endregion
            }

            base.OnLoad(e);
        }

        /// <summary>
        ///     Raises the <see cref="Process" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected virtual void OnProcess(EventArgs e)
        {
            EventHandler<EventArgs> handler = Process;
            if (handler != null)
                Process(this, e);
        }

        /// <summary>
        ///     Renders the control to the specified HTML writer.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="System.Web.UI.HtmlTextWriter"></see> object that
        ///     receives the control content.
        /// </param>
        /// <remarks>
        ///     As implemented in this control, this method returns without performing any rendering.
        /// </remarks>
        protected override void Render(HtmlTextWriter writer) { }

        private void AddStyleAttributes()
        {
            if (Font.Names.Length > 0)
                Style[HtmlTextWriterStyle.FontFamily] = string.Join(",", Font.Names);
            if (Font.Bold)
                Style[HtmlTextWriterStyle.FontWeight] = "bold";
            if (Font.Italic)
                Style[HtmlTextWriterStyle.FontStyle] = "italic";
            if (!Font.Size.IsEmpty)
                Style[HtmlTextWriterStyle.FontSize] = Font.Size.ToString();
            if (ForeColor != Color.Empty)
                Style[HtmlTextWriterStyle.Color] = ColorTranslator.ToHtml(ForeColor);
        }
    }
}