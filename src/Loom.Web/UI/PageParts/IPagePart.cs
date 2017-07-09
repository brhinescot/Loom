namespace Loom.Web.UI.PageParts
{
    public interface IPagePart
    {
        PagePartStyle PagePartStyle { get; }
        PagePartCollection PageParts { get; }
    }
}