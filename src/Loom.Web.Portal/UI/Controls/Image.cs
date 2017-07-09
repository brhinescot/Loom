#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ToolboxData("<{0}:Image runat=\"server\"></{0}:Image>")]
    public class Image : PortalControl
    {
        public string AlternateText { get; set; }
        public string DescriptionUrl { get; set; }
        public string ImageUrl { get; set; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Img;

        public string Tooltip { get; set; }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveUrl(ImageUrl));
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, AlternateText ?? Tooltip ?? string.Empty);
            if (!Compare.IsNullOrEmpty(DescriptionUrl))
                writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, ResolveUrl(DescriptionUrl));
        }
    }
}