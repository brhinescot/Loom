#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.UI.PageParts
{
    public class HtmlListRenderer<T> : IListRenderer<T>
    {
        #region IListRenderer<T> Members

        public void RenderHeader(ContentRenderer renderer)
        {
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void RenderItem(ContentRenderer renderer, T dataItem)
        {
            renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.Li);
            OnRenderItem(renderer, dataItem);
            renderer.InnerWriter.RenderEndTag();
        }

        public void RenderFooter(ContentRenderer renderer)
        {
            renderer.InnerWriter.RenderEndTag();
        }

        #endregion

        protected virtual void OnRenderItem(ContentRenderer renderer, T dataItem)
        {
            renderer.RenderLiteral(dataItem.ToString());
        }
    }
}