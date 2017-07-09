#region Using Directives

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.PageParts
{
    /// <summary>
    /// </summary>
    public class PagePart : IPagePart, ILocalizable
    {
        private PagePartCollection pageParts;
        private PagePartStyle pagePartStyle;

        protected PagePart() { }

        public Page Page { get; private set; }

        public bool RenderInlineStyles { get; set; }

        public bool RemoveHeader { get; set; }

        public string HeaderText { get; set; }

        #region ILocalizable Members

        [Bindable(true)]
        [Category("Misc")]
        [DefaultValue("")]
        public string ResourceKey { get; set; }

        #endregion

        #region IPagePart Members

        public PagePartStyle PagePartStyle
        {
            get
            {
                if (pagePartStyle == null)
                    pagePartStyle = new PagePartStyle();
                return pagePartStyle;
            }
        }

        public PagePartCollection PageParts
        {
            get
            {
                if (pageParts == null)
                    pageParts = new PagePartCollection(PagePartStyle);
                return pageParts;
            }
        }

        #endregion

        public static PagePart Create(Page page, string id)
        {
            return Create(page, id, null);
        }

        public static PagePart Create(Page page, string id, string cssClass)
        {
            PagePart part = new PagePart();
            part.Page = page;
            return part;
        }

        internal void Render(ContentRenderer renderer)
        {
            if (RenderInlineStyles)
            {
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Padding, PagePartStyle.BorderWidth.ToString());
                renderer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(PagePartStyle.BorderColor));
            }

            renderer.AddAttribute(HtmlTextWriterAttribute.Id, "pagePart");
            AddAttributesToRender(renderer);
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            RenderHeader(renderer);
            RenderContent(renderer);
            renderer.InnerWriter.RenderEndTag();
        }

        private void RenderHeader(ContentRenderer renderer)
        {
            if (RemoveHeader)
                return;

            if (RenderInlineStyles)
            {
                renderer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(PagePartStyle.HeaderBackgroundColor));
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(PagePartStyle.HeaderColor));
                renderer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "Bold");
                renderer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, PagePartStyle.HeaderFontHeight.ToString());
                renderer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, string.Join(",", PagePartStyle.HeaderFontNames));
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Height, PagePartStyle.HeaderHeight.ToString());
                renderer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "5px");
            }
            renderer.AddAttribute(HtmlTextWriterAttribute.Class, "header");
            AddHeaderAttributesToRender(renderer);
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            OnRenderHeaderContents(renderer);

            renderer.InnerWriter.RenderEndTag();
        }

        private void RenderContent(ContentRenderer renderer)
        {
            if (RenderInlineStyles)
            {
                renderer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(PagePartStyle.ContentBackgroundColor));
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(PagePartStyle.ContentColor));
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "5px");
            }
            renderer.AddAttribute(HtmlTextWriterAttribute.Class, "content");
            AddContentAttributesToRender(renderer);
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            OnRenderContent(renderer);

            renderer.InnerWriter.RenderEndTag();
        }

        protected virtual void AddAttributesToRender(ContentRenderer renderer) { }
        protected virtual void AddHeaderAttributesToRender(ContentRenderer renderer) { }
        protected virtual void AddContentAttributesToRender(ContentRenderer renderer) { }

        protected virtual void OnRenderHeaderContents(ContentRenderer renderer)
        {
            renderer.RenderLiteral(HeaderText);
        }

        protected virtual void OnRenderContent(ContentRenderer renderer)
        {
            foreach (PagePart part in PageParts)
                part.Render(renderer);
        }
    }
}