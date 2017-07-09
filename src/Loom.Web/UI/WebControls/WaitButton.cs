#region Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Represents a class for hiding a button when clicked and showing a wait message while waiting for a page reload.
    /// </summary>
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    [DebuggerDisplay("Text={Text}, WaitMessage={WaitMessage}, ImageUrl={ImageUrl}")]
    [ToolboxData("<{0}:WaitButton runat=\"server\" WaitMessage=\"Please wait...\"/>")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class WaitButton : Button, ILocalizable, ISpamGuardian
    {
        private const string ButtonId = "{0}_Button";
        private const string ClientScriptKey = "ShowWaitMessage";
        private const string ImageUrlStateKey = "ImageUrl";
        private const string LoadingImageId = "{0}_LoadingImage";
        private const string OnClickMethod = "DevInterop_ShowWaitMessage('{0}_WaitMessage', '{0}_Button', '{0}_LoadingImage')";
        private const string WaitMessageId = "{0}_WaitMessage";
        private const string WaitMessageStateKey = "WaitMessage";

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<WaitButton> extender;

        public WaitButton()
        {
            extender = new ControlExtender<WaitButton>(this);
        }

        /// <summary>
        ///     Gets or sets the message to display when the button is clicked.
        /// </summary>
        [Browsable(true)]
        [Description("The message to display when the button is clicked.")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string WaitMessage
        {
            get
            {
                object obj = ViewState[WaitMessageStateKey];
                if (obj == null)
                    return string.Empty;

                return obj.ToString();
            }
            set => ViewState[WaitMessageStateKey] = value;
        }

        /// <summary>
        ///     Gets or sets the url of the image to display when the button is clicked.
        /// </summary>
        [Browsable(true)]
        [Description("The url of the image to display when the button is clicked.")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        public string ImageUrl
        {
            get
            {
                object obj = ViewState[ImageUrlStateKey];
                if (obj == null)
                    return string.Empty;

                return obj.ToString();
            }
            set => ViewState[ImageUrlStateKey] = value;
        }

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

        #region ISpamGuardian Members

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool AntiSpam
        {
            get
            {
                object o = ViewState["AntiSpam"];
                return o != null ? (bool) o : false;
            }

            set => ViewState["AntiSpam"] = value;
        }

        #endregion

        /// <summary>
        ///     This member overrides <see cref="Button.AddAttributesToRender" />.
        /// </summary>
        /// <param name="writer">
        ///     An <see cref="HtmlTextWriter" /> that contains the output stream to render on the
        ///     client.
        /// </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format(OnClickMethod, ClientID));
            base.AddAttributesToRender(writer);
        }

        /// <summary>
        ///     Writes the opening tag of the specified element to the HTML 3.2 output stream.
        /// </summary>
        /// <remarks>
        ///     If a div element is specified and the <see cref="Html32TextWriter.ShouldPerformDivTableSubstitution" /> property
        ///     value is
        ///     true, the RenderBeginTag method performs basic table element formatting to present the
        ///     content that is contained in the div element.
        /// </remarks>
        /// <param name="writer">
        ///     The <see cref="HtmlTextWriterTag" /> enumeration value that indicates which HTML element to write.
        /// </param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "116");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format(WaitMessageId, ClientID));
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.AddStyleAttribute("float", "left");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteLine();
            writer.Indent = writer.Indent + 2;

            if (!string.IsNullOrEmpty(ImageUrl))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(ImageUrl));
                writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format(LoadingImageId, ClientID));
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }

            if (!string.IsNullOrEmpty(WaitMessage))
            {
                writer.Write("&nbsp;");
                writer.WriteLine();
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(WaitMessage);
                writer.RenderEndTag();
                writer.WriteLine();
            }

            writer.Indent = writer.Indent - 1;
            writer.RenderEndTag();
            writer.WriteLine();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format(ButtonId, ClientID));
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Width.ToString());
//            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteLine();

            writer.Indent = writer.Indent + 1;
            base.RenderBeginTag(writer);
        }

        /// <summary>
        ///     Writes the end tag of a markup element to the output stream.
        /// </summary>
        /// <remarks>
        ///     Call the RenderEndTag method after the <see cref="RenderBeginTag" /> overload is called and after all
        ///     content between the opening and closing tags (inner markup) of the element has been
        ///     rendered.
        /// </remarks>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
//            writer.WriteLine();
//            writer.Indent = writer.Indent - 1;
//            writer.RenderEndTag();
            base.RenderEndTag(writer);
            writer.WriteLine();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnPreRender" />.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            string script = "   <script language='javascript' type='text/javascript'>" + Environment.NewLine +
                            "   <!--" + Environment.NewLine +
                            "       function DevInterop_ShowWaitMessage(message, button, image){" + Environment.NewLine +
                            "           messageEl = document.all ? document.all[message] : document.getElementById(message);" + Environment.NewLine +
                            "           buttonEl = document.all ? document.all[button] : document.getElementById(button);" + Environment.NewLine +
                            "           imageEl = document.all ? document.all[image] : document.getElementById(image);" + Environment.NewLine +
                            "           messageEl.style.display = 'block';" + Environment.NewLine +
                            "           buttonEl.style.display = 'none';" + Environment.NewLine +
                            "           setTimeout('imageEl.src = imageEl.src', 0);" + Environment.NewLine +
                            "       }" + Environment.NewLine +
                            "   // -->" + Environment.NewLine +
                            "   </script>" + Environment.NewLine;

            Page.ClientScript.RegisterClientScriptBlock(GetType(), ClientScriptKey, script);
            base.OnPreRender(e);
        }
    }
}