#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using Image = System.Web.UI.WebControls.Image;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Displays an image that has a random client id on a web page.
    /// </summary>
    /// <remarks>
    ///     The control generates a new random id on each request. The
    ///     control may be used in the same way as a standard <see cref="System.Web.UI.WebControls.Image" />
    ///     without any modifications. The changing client id is transparent to
    ///     the developer.
    /// </remarks>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [DefaultProperty("ImageUrl")]
    [ToolboxData("<{0}:ImageEx runat=server></{0}:ImageEx>")]
    [Designer("Loom.Web.UI.Design.WebControls.DynamicThumbnailImageDesigner, Loom.Design, Version=0.9.1.0, Culture=neutral, PublicKeyToken=f77fee4373937a8c")]
    public class ImageEx : Image, ILocalizable, ISpamGuardian
    {
        #region Designer generated code

        private readonly ControlExtender<ImageEx> extender;

        #endregion

        public ImageEx()
        {
            extender = new ControlExtender<ImageEx>(this);
        }

        [Bindable(true)]
        [Category("Thumbnail")]
        [DefaultValue(false)]
        public bool Resize
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
        [DefaultValue("")]
        public Size MaximumSize
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
        public int Quality
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
        public int CacheDuration
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
            set => ViewState["OriginalImageUrl"] = value;
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

        protected override void OnPreRender(EventArgs e)
        {
            if (Resize && !DesignMode)
                ImageCacher.StoreImage(this);
            base.OnPreRender(e);
        }
    }
}