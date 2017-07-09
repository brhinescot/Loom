#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand)]
    [PermissionSet(SecurityAction.InheritanceDemand)]
    [DefaultProperty("ImageUrl")]
    [ToolboxData("<{0}:DynamicImage runat=server></{0}:DynamicImage>")]
    public class DynamicImage : Image, ILocalizable, ISpamGuardian
    {
        private const string ImageGenerationServiceBaseUrl = "cachedimageservice.axd?data={0}";

        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<DynamicImage> extender;

        #endregion

        public DynamicImage()
        {
            extender = new ControlExtender<DynamicImage>(this);
        }

        /// <summary>
        ///     Gets or sets the time in minutes to cache the image.
        /// </summary>
        [Description("The time in minutes to cache the image.")]
        [Category("Behavior")]
        [DefaultValue(5)]
        public int CacheDuration
        {
            get
            {
                object obj = ViewState["CacheDuration"];
                if (obj != null)
                    return (int) obj;
                return 5;
            }
            set => ViewState["CacheDuration"] = value;
        }

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        /// <value></value>
        [Browsable(false)]
        public System.Drawing.Image Image { get; set; }

        /// <summary>
        ///     Gets or sets the storage key.
        /// </summary>
        /// <value></value>
        protected string StorageKey
        {
            get => ViewState["StorageKey"] as string;
            set => ViewState["StorageKey"] = value;
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
            if (Image != null)
            {
                if (Compare.IsNullOrEmpty(StorageKey))
                    StorageKey = Guid.NewGuid().ToString();

                StoreData(Image);
                ImageUrl = string.Format(ImageGenerationServiceBaseUrl, StorageKey);
            }

            base.OnPreRender(e);
        }

        /// <summary>
        ///     Stores the data.
        /// </summary>
        /// <param name="data">Data.</param>
        private void StoreData(object data)
        {
            if (Page.Cache[StorageKey] == null)
                Page.Cache.Add(StorageKey, data, null, Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(CacheDuration), CacheItemPriority.High, null);
        }
    }
}