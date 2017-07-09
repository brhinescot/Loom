#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Summary description for EmbeddedObject.
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [DefaultProperty("MediaUrl")]
    [ToolboxData("<{0}:EmbeddedObject runat=server></{0}:EmbeddedObject>")]
    public class EmbeddedObject : WebControl
    {
//        private string type;

        private const string MediaUrlStateKey = "MediaUrl";
        private const string MediaTypeStateKey = "MediaType";
        private const string AutoplayStateKey = "AutoPlay";
        private const string AdditionalParamsStateKey = "Params";
        private string classId;

        private string codeBase;
        private string pluginSpace;

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("Misc")]
        [Description("Gets or sets the Object Type.")]
        [DefaultValue("NotSet")]
        public EmbeddedMediaType MediaType
        {
            get
            {
                object savedState;

                savedState = ViewState[MediaTypeStateKey];
                if (savedState != null)
                    return (EmbeddedMediaType) savedState;

                return EmbeddedMediaType.NotSet;
            }
            set => ViewState[MediaTypeStateKey] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the Url to the object.")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string MediaUrl
        {
            get
            {
                object savedState;

                savedState = ViewState[MediaUrlStateKey];
                if (savedState != null)
                    return (string) savedState;

                return string.Empty;
            }
            set => ViewState[MediaUrlStateKey] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the AutoPlay value of the object.")]
        [DefaultValue("true")]
        public bool AutoPlay
        {
            get
            {
                object savedState;

                savedState = ViewState[AutoplayStateKey];
                if (savedState != null)
                    return (bool) savedState;
                return false;
            }
            set => ViewState[AutoplayStateKey] = value;
        }

        /// <summary>
        /// </summary>
        // TODO : Something besides Dictionary<T, K>
        [Bindable(true)]
        [Browsable(false)]
        [Category("Behavior")]
        [Description("Gets or sets additional parameters for the object.")]
        [DefaultValue("")]
        public Dictionary<string, string> AdditionalParameters
        {
            get
            {
                object savedState;

                savedState = ViewState[AdditionalParamsStateKey];
                if (savedState != null)
                    return (Dictionary<string, string>) savedState;

                return new Dictionary<string, string>();
            }
            set => ViewState[AdditionalParamsStateKey] = value;
        }

        /// <summary>
        /// </summary>
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Object;

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (MediaType != EmbeddedMediaType.NotSet)
                InitializeMediaType();
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            //if(MediaType != EmbeddedMediaType.NotSet)
            PrivateRenderContents(writer);
            base.RenderContents(writer);
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            //if(MediaType != EmbeddedMediaType.NotSet)
            PrivateAddAttributesToRender(writer);
            base.AddAttributesToRender(writer);
        }

        private void PrivateAddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute("codeBase", codeBase);
            writer.AddAttribute("classid", classId);
            writer.AddAttribute(HtmlTextWriterAttribute.Height,
                Height.ToString(CultureInfo.InvariantCulture));
            writer.AddAttribute(HtmlTextWriterAttribute.Width,
                Width.ToString(CultureInfo.InvariantCulture));
        }

        private void PrivateRenderContents(HtmlTextWriter writer)
        {
            writer.InnerWriter.WriteLine("<param name=\"src\" value=\"{0}\">", MediaUrl);
            writer.InnerWriter.WriteLine("<param name=\"autoplay\" value=\"{0}\">", AutoPlay);
            writer.InnerWriter.WriteLine("<param name=\"bgcolor\" value=\"{0}\">", BackColor);

            foreach (string s in AdditionalParameters.Keys)
                writer.InnerWriter.WriteLine("<param name=\"{0}\" value=\"{1}\">", s, AdditionalParameters[s]);

            writer.InnerWriter.WriteLine("<embed src=\"{0}\" width=\"{1}\" height=\"{2}\" bgcolor=\"{3}\"",
                MediaUrl,
                Width.ToString(CultureInfo.InvariantCulture),
                Width.ToString(CultureInfo.InvariantCulture),
                BackColor);
            writer.InnerWriter.WriteLine("autoplay=\"{0}\" controller=\"true\"", AutoPlay);
            writer.InnerWriter.WriteLine("pluginspace=\"{0}\">", pluginSpace);
            writer.InnerWriter.WriteLine("</embed>");
        }

        private void InitializeMediaType()
        {
            switch (MediaType)
            {
                case EmbeddedMediaType.QuickTime:
                    codeBase = "http://www.apple.com/qtactivex/qtplugin.cab";
                    classId = "CLSID:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B";
                    pluginSpace = "http://www.apple.com/quicktime/download/";
                    break;
                case EmbeddedMediaType.Flash:
                    codeBase = "http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0";
                    classId = "CLSID:D27CDB6E-AE6D-11cf-96B8-444553540000";
                    pluginSpace = "http://www.macromedia.com/go/getflashplayer";
//                    type = "application/x-shockwave-flash";
                    break;
                case EmbeddedMediaType.WindowsMedia:
                    codeBase = "http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701";
                    classId = "CLSID:22D6f312-B0F6-11D0-94AB-0080C74C7E95";
                    pluginSpace = string.Empty;
                    break;
            }
        }
    }
}