namespace Loom.Web.UI.PageParts
{
    public interface IListRenderer<T>
    {
        void RenderHeader(ContentRenderer renderer);
        void RenderItem(ContentRenderer renderer, T dataItem);
        void RenderFooter(ContentRenderer renderer);
    }
}