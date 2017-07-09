#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [ParseChildren(false)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:SliderItem runat=\"server\"></{0}:SliderItem>")]
    public class SliderItem : CompositeControl
    {
        [Browsable(true)]
        [Description("The title of the slider panel.")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string Title
        {
            get
            {
                object obj = ViewState["Title"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["Title"] = value;
        }

        [Browsable(true)]
        [Description("")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string AltText
        {
            get
            {
                object obj = ViewState["AltText"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["AltText"] = value;
        }

        [Browsable(true)]
        [Description("The path to the panel's thumbnail.")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string ThumbnailUrl
        {
            get
            {
                object obj = ViewState["ThumbnailUrl"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["ThumbnailUrl"] = value;
        }

        [Browsable(true)]
        [Description("")]
        [Category("")]
        [Bindable(true)]
        [DefaultValue("")]
        public bool ResizeThumbnail
        {
            get
            {
                object obj = ViewState["ResizeThumbnail"];
                return obj == null ? false : (bool) obj;
            }
            set => ViewState["ResizeThumbnail"] = value;
        }

        [Browsable(true)]
        [Description("")]
        [Category("")]
        [Bindable(true)]
        [DefaultValue("")]
        public Size MaximumThumbnailSize
        {
            get
            {
                object obj = ViewState["MaximumThumbnailSize"];
                return obj == null ? Size.Empty : (Size) obj;
            }
            set => ViewState["MaximumThumbnailSize"] = value;
        }

        [Browsable(true)]
        [Description("The path to the panel's thumbnail.")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string ImageCaption
        {
            get
            {
                object obj = ViewState["ImageCaption"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["ImageCaption"] = value;
        }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsureChildControls();
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel");
            if (!Compare.IsNullOrEmpty(Title))
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "wrapper");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            foreach (Control control in Controls)
                control.RenderControl(writer);

            if (!Compare.IsNullOrEmpty(ImageCaption))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "photo-meta-data");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.WriteLine(ImageCaption);
                writer.RenderEndTag();
                writer.WriteLine();
            }
            writer.RenderEndTag();
        }
    }
}