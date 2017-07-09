#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ToolboxData("<{0}ImageButton runat=\"server\"></{0}:ImageButton>")]
    public class ImageButton : Input
    {
        public string AlternateText { get; set; }
        public string ImageUrl { get; set; }

        protected override string InputType => "image";

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveUrl(ImageUrl));
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, AlternateText ?? Tooltip ?? string.Empty);
        }
    }
}