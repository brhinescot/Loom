#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
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
    [DefaultProperty("Src")]
    [ToolboxData("<{0}:FlashMovie runat=server></{0}:EmbeddedObject>")]
    public class FlashMovie : WebControl
    {
        private const string AdditionalParamsStateKey = "Params";
        private const string AutoplayStateKey = "AutoPlay";
        private const string FlashVarsStateKey = "FlashVars";
        private const string MediaUrlStateKey = "Src";

        private string classId;
        private string codeBase;
        private string pluginSpace;

        /// <summary>
        /// </summary>
        // TODO : Something besides Dictionary<T, K>
        [Bindable(true)]
        [Browsable(false)]
        [Category("Behavior")]
        [Description("Gets or sets additional parameters for the object.")]
        [DefaultValue("")]
        public AdditionalParametersCollection AdditionalParameters
        {
            get
            {
                object obj = ViewState[AdditionalParamsStateKey];
                if (obj == null)
                    return new AdditionalParametersCollection();

                return (AdditionalParametersCollection) obj;
            }
            set => ViewState[AdditionalParamsStateKey] = value;
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
                object obj = ViewState[MediaUrlStateKey];
                if (obj == null)
                    return false;

                return (bool) obj;
            }
            set => ViewState[AutoplayStateKey] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true, BindingDirection.TwoWay)]
        [Category("Behavior")]
        [Description("Gets or sets the FlashVars for the movie.")]
        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        [Editor(EditorType.MultiLineString, typeof(UITypeEditor))]
        public string FlashVars
        {
            get
            {
                object obj = ViewState[FlashVarsStateKey];
                if (obj == null)
                    return string.Empty;

                return (string) obj;
            }
            set => ViewState[FlashVarsStateKey] = value;
        }

        /// <summary>
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the url to the object.")]
        [Editor(EditorType.ImageUrl, typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string Src
        {
            get
            {
                object obj = ViewState[MediaUrlStateKey];
                if (obj == null)
                    return string.Empty;

                return (string) obj;
            }
            set => ViewState[MediaUrlStateKey] = value;
        }

        /// <summary>
        /// </summary>
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Object;

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
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
            writer.InnerWriter.WriteLine("<param name=\"src\" value=\"{0}\">", Src);
            writer.InnerWriter.WriteLine("<param name=\"autoplay\" value=\"{0}\">", AutoPlay);
            writer.InnerWriter.WriteLine("<param name=\"bgcolor\" value=\"{0}\">", ColorTranslator.ToHtml(BackColor));
            writer.InnerWriter.WriteLine("<param name=\"FlashVars\" value=\"{0}\">", FlashVars);
            writer.InnerWriter.WriteLine("<param name=\"allowScriptAccess\" value=\"sameDomain\" />");
            writer.InnerWriter.WriteLine("<param name='movie' value='playNow.swf' />");
            writer.InnerWriter.WriteLine("<param name='wmode' value='transparent' />");
            writer.InnerWriter.WriteLine("<param name='menu' value='false' />");
            writer.InnerWriter.WriteLine("<param name='quality' value='best' />");

            foreach (string s in AdditionalParameters.Keys)
                writer.InnerWriter.WriteLine("<param name=\"{0}\" value=\"{1}\">", s, AdditionalParameters[s]);

            writer.InnerWriter.WriteLine("<embed allowscriptaccess=\"sameDomain\"  wmode='transparent' menu='false' quality='best' name='playNow' align='middle' src=\"{0}\" width=\"{1}\" height=\"{2}\" bgcolor=\"{3}\" autoplay=\"{4}\" controller=\"true\" pluginspace=\"{5}\" flashvars=\"{6}\"/>",
                Src,
                Width.ToString(CultureInfo.InvariantCulture),
                Width.ToString(CultureInfo.InvariantCulture),
                ColorTranslator.ToHtml(BackColor),
                AutoPlay,
                pluginSpace,
                FlashVars);
        }

        private void InitializeMediaType()
        {
            codeBase = "http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0";
            classId = "CLSID:D27CDB6E-AE6D-11cf-96B8-444553540000";
            pluginSpace = "http://www.macromedia.com/go/getflashplayer";
        }
    }
}