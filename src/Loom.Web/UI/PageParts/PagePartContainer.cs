#region Using Directives

using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.PageParts
{
    [ToolboxData("<{0}:PagePartContainer runat=server></{0}:PagePartContainer>")]
    public class PagePartContainer : WebControl
    {
        private ContentRenderer contentRenderer;

        private PagePartCollection pageParts;

        public bool RenderInlineStyles { get; set; }

        public ColumnLayout ColumnLayout { get; set; }

        public PagePartCollection Parts
        {
            get
            {
                if (pageParts == null)
                    pageParts = new PagePartCollection(DefaultStyle);
                return pageParts;
            }
        }

        public PagePartStyle DefaultStyle { get; } = new PagePartStyle();

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void RenderContents(HtmlTextWriter writer)
        {
            contentRenderer = new ContentRenderer(writer);
            RenderColumns(contentRenderer);

            base.RenderContents(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);

            writer.Write(contentRenderer.ClientFunctionBlock);
        }

        private void RenderColumns(ContentRenderer renderer)
        {
            RenderColumnOne(renderer);
            if (ColumnLayout == ColumnLayout.OneColumn)
                return;

            RenderColumnTwo(renderer);
            if (ColumnLayout == ColumnLayout.TwoColumns)
                return;

            RenderColumnThree(renderer);
        }

        private void RenderColumnOne(ContentRenderer renderer)
        {
            if (RenderInlineStyles)
            {
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Left, "15px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Width, "195px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Top, "15px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px");
            }
            renderer.AddAttribute(HtmlTextWriterAttribute.Id, "leftColumn");
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            foreach (PagePart part in Parts)
            {
                part.RenderInlineStyles = RenderInlineStyles;
                part.Render(renderer);
            }
            renderer.InnerWriter.RenderEndTag();
        }

        private void RenderColumnTwo(ContentRenderer renderer)
        {
            if (RenderInlineStyles)
            {
                renderer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, "210px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, "15px");
                if (Page.Request.Browser.Id == "mozillafirefox")
                    renderer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "15px");
                else
                    renderer.AddStyleAttribute(HtmlTextWriterStyle.Top, "15px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px");
            }
            renderer.AddAttribute(HtmlTextWriterAttribute.Id, "centerColumn");
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            foreach (PagePart part in Parts)
            {
                part.RenderInlineStyles = RenderInlineStyles;
                part.Render(renderer);
            }
            renderer.InnerWriter.RenderEndTag();
        }

        private void RenderColumnThree(ContentRenderer renderer)
        {
            if (RenderInlineStyles)
            {
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
                renderer.AddStyleAttribute("right", "15px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Width, "195px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Top, "15px");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px");
            }
            renderer.AddAttribute(HtmlTextWriterAttribute.Id, "rightColumn");
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            foreach (PagePart part in Parts)
            {
                part.RenderInlineStyles = RenderInlineStyles;
                part.Render(renderer);
            }
            renderer.InnerWriter.RenderEndTag();
        }
    }
}