#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:HyperLinkEx runat=server></{0}:HyperLinkEx>")]
    public class HyperLinkEx : HyperLink, ILocalizable
    {
        // TODO: Generate key
        private const string ObfuscateKey = "sJ8rFd$pNf7An4DbGu9O";

        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<HyperLinkEx> extender;

        #endregion

        private readonly Image rolloverImage = new Image();

        public HyperLinkEx()
        {
            extender = new ControlExtender<HyperLinkEx>(this);
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool Obfuscate
        {
            get
            {
                object o = ViewState["AntiSpam"];
                return o != null ? (bool) o : false;
            }

            set => ViewState["AntiSpam"] = value;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool OpenInNewWindow
        {
            get
            {
                object o = ViewState["OpenInNewWindow"];
                return o != null ? (bool) o : false;
            }

            set => ViewState["OpenInNewWindow"] = value;
        }

        /// <summary>
        ///     Gets or sets the path to an image to display for the <see cref="HyperLinkEx" /> control during a mouse-over.
        /// </summary>
        /// <value>
        ///     The path to the image to display for the <see cref="HyperLinkEx" /> control. The default value is
        ///     <see cref="string.Empty" />.
        /// </value>
        /// <remarks>
        ///     The <see cref="RolloverImageUrl" /> is only used if <see cref="HyperLink.ImageUrl" /> is also set.
        ///     The image will be shown when the user puts the mouse arrow over the image.
        /// </remarks>
        [Bindable(true)]
        [UrlProperty]
        [Category("Appearance")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string RolloverImageUrl
        {
            get
            {
                object savedState = ViewState["RolloverImageUrl"];
                if (savedState != null)
                    return (string) savedState;

                return string.Empty;
            }
            set => ViewState["RolloverImageUrl"] = value;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The WindowName is largly irrelevant unless client-side script is
        ///         going to access it.
        ///     </para>
        ///     <para>
        ///         If no name is specified the control will generate its own name which is likely to be unique.
        ///     </para>
        /// </remarks>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets the WindowName of the RemoteWindow.")]
        [DefaultValue("")]
        public string WindowName
        {
            get
            {
                object savedState = ViewState["WindowName"];
                if (savedState != null)
                    return Convert.ToString(savedState);

                return ClientID;
            }
            set => ViewState["WindowName"] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets if scrollbars are added to the remote window.")]
        [DefaultValue(false)]
        public bool Scrollbars
        {
            get
            {
                object savedState = ViewState["Scrollbars"];
                if (savedState != null)
                    return Convert.ToBoolean(savedState);

                return false;
            }
            set => ViewState["Scrollbars"] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets if the user can resize the RemoteWindow.")]
        [DefaultValue(true)]
        public bool ResizableWindow
        {
            get
            {
                object savedState = ViewState["ResizableWindow"];
                if (savedState != null)
                    return Convert.ToBoolean(savedState);

                return false;
            }
            set => ViewState["ResizableWindow"] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets the distance, in pixels, from the top of the screen to the top of the RemoteWindow.")]
        [DefaultValue("")]
        public Unit Top
        {
            get
            {
                object savedState = ViewState["Top"];
                if (savedState != null)
                    return (Unit) savedState;

                return Unit.Empty;
            }
            set => ViewState["Top"] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets the distance, in pixels, from the left of the screen to the left of the RemoteWindow.")]
        [DefaultValue("")]
        public Unit Left
        {
            get
            {
                object savedState = ViewState["Left"];
                if (savedState != null)
                    return (Unit) savedState;

                return Unit.Empty;
            }
            set => ViewState["Left"] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets the WindowHeight, in pixels, of the RemoteWindow.")]
        [DefaultValue("")]
        public Unit WindowHeight
        {
            get
            {
                object savedState = ViewState["WindowHeight"];
                if (savedState != null)
                    return (Unit) savedState;

                return Unit.Empty;
            }
            set => ViewState["WindowHeight"] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("New Window")]
        [Description("Gets or sets the WindowWidth, in pixels, of the RemoteWindow.")]
        [DefaultValue("")]
        public Unit WindowWidth
        {
            get
            {
                object savedState = ViewState["WindowWidth"];
                if (savedState != null)
                    return (Unit) savedState;

                return Unit.Empty;
            }
            set => ViewState["WindowWidth"] = value;
        }

        [Bindable(true)]
        [Category("Thumbnail")]
        [DefaultValue(false)]
        public bool ResizeImage
        {
            get
            {
                object o = ViewState["Resize"];
                return o != null ? (bool) o : false;
            }

            set => ViewState["Resize"] = value;
        }

        [Bindable(true)]
        [Category("Thumbnail")]
        [DefaultValue(typeof(Size), "100,100")]
        public Size MaximumThumbnailSize
        {
            get
            {
                object obj = ViewState["MaximumThumbnailSize"];
                if (obj != null)
                    return (Size) obj;

                return new Size(100, 100);
            }
            set => ViewState["MaximumThumbnailSize"] = value;
        }

        [Bindable(true)]
        [Category("Thumbnail")]
        [DefaultValue(70)]
        public int ThumbnailQuality
        {
            get
            {
                object obj = ViewState["Quality"];
                if (obj != null)
                    return (int) obj;

                return 70;
            }
            set => ViewState["Quality"] = value;
        }

        /// <summary>
        ///     Gets or sets the time in minutes to cache the image.
        /// </summary>
        [Description("The time in minutes to cache the image.")]
        [Category("Thumbnail")]
        [DefaultValue(60)]
        public int ThumbnailCacheDuration
        {
            get
            {
                object obj = ViewState["CacheDuration"];
                if (obj != null)
                    return (int) obj;
                return 60;
            }
            set => ViewState["CacheDuration"] = value;
        }

        /// <summary>
        ///     Gets or sets the storage key.
        /// </summary>
        /// <value></value>
        [Browsable(false)]
        protected internal string StorageKey
        {
            get => ViewState["StorageKey"] as string;
            set => ViewState["StorageKey"] = value;
        }

        protected internal string OriginalImageUrl
        {
            get => ViewState["OriginalImageUrl"] as string;
            set
            {
                if (ViewState["OriginalImageUrl"] == null)
                    ViewState["OriginalImageUrl"] = value;
            }
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Obfuscate)
                ObfuscateUrl();

            if (ResizeImage && !DesignMode)
                ImageCacher.StoreImage(this);

            if (string.IsNullOrEmpty(RolloverImageUrl))
                return;

            rolloverImage.ID = ClientID + "_img";
            Controls.Add(rolloverImage);
            RegisterSwapImageScript();
        }

        /// <summary>
        ///     Adds to the specified writer those HTML attributes and styles that need to be rendered. This method is primarily
        ///     used by control developers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        /// <remarks>
        ///     Overridden to set additional attributes.
        /// </remarks>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (OpenInNewWindow)
                RenderNewWindowAttributes(writer);
        }

        private void RenderNewWindowAttributes(HtmlTextWriter writer)
        {
            if (Obfuscate)
                throw new InvalidOperationException("Cannot obfuscate a url when opening in a new window.");

            StringBuilder options = new StringBuilder();
            options.Append("javascript:window.open( '");
            options.Append(ResolveUrl(NavigateUrl));
            options.Append("', '");
            options.Append(WindowName);
            options.Append("', 'menubar=no,toolbar=no,resizable=");

            if (ResizableWindow)
                options.Append("yes,");
            else
                options.Append("no,");

            options.Append("scrollbars=");
            if (Scrollbars)
                options.Append("yes,");
            else
                options.Append("no,");

            //window sizes
            if (Top != Unit.Empty)
            {
                options.Append("top=");
                options.Append(Top.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",screenY=");
                options.Append(Top.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",");
            }

            if (Left != Unit.Empty)
            {
                options.Append("left=");
                options.Append(Left.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",screenX=");
                options.Append(Left.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",");
            }

            if (WindowHeight != Unit.Empty)
            {
                options.Append("height=");
                options.Append(WindowHeight.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",innerHeight=");
                options.Append(WindowHeight.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",");
            }

            if (WindowWidth != Unit.Empty)
            {
                options.Append("width=");
                options.Append(WindowWidth.Value.ToString(NumberFormatInfo.InvariantInfo));
                options.Append(",innerWidth=");
                options.Append(WindowWidth.Value.ToString(NumberFormatInfo.InvariantInfo));
            }

            options.Append("'); return false;");
            writer.AddAttribute("onclick", options.ToString(), true);
        }

        private void ObfuscateUrl()
        {
            Page.ClientScript.RegisterClientScriptResource(typeof(HyperLinkEx), WebResourcePath.SafeLink);

            string encodedUrl = BitConverter.ToString(Encoding.ASCII.GetBytes(ObfuscateKey + NavigateUrl)).Replace("-", string.Empty);
            NavigateUrl = "javascript:antiSpamLink('" + encodedUrl + "');";
        }

        /// <summary>
        ///     Emits the client scripts which support the rollover behavior.
        /// </summary>
        protected virtual void RegisterSwapImageScript()
        {
            SwapImageProvider.Register(rolloverImage, RolloverImageUrl);
        }

        /// <summary>
        ///     Renders the contents of the control into the specified writer. This method is primarily used by control developers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        /// <remarks>
        ///     Overridden to allow script to access the inner image.
        /// </remarks>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(RolloverImageUrl))
            {
                base.RenderContents(writer);
                return;
            }

            string imageUrl = ImageUrl;
            if (imageUrl.Length > 0)
            {
                rolloverImage.ImageUrl = ResolveClientUrl(imageUrl);
                imageUrl = ToolTip;
                if (imageUrl.Length != 0)
                    rolloverImage.ToolTip = imageUrl;

                imageUrl = Text;
                if (imageUrl.Length != 0)
                    rolloverImage.AlternateText = imageUrl;

                rolloverImage.RenderControl(writer);
            }
            else
            {
                base.RenderContents(writer);
            }
        }
    }
}