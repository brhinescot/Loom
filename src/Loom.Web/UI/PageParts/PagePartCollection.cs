#region Using Directives

using System.Collections.ObjectModel;

#endregion

namespace Loom.Web.UI.PageParts
{
    public class PagePartCollection : Collection<IPagePart>
    {
        private readonly PagePartStyle style;

        public PagePartCollection(PagePartStyle style)
        {
            this.style = style;
        }

        protected override void InsertItem(int index, IPagePart item)
        {
            item.PagePartStyle.ContentBackgroundColor = style.ContentBackgroundColor;
            item.PagePartStyle.ContentColor = style.ContentColor;
            item.PagePartStyle.HeaderBackgroundColor = style.HeaderBackgroundColor;
            item.PagePartStyle.HeaderColor = style.HeaderColor;
            item.PagePartStyle.HeaderHeight = style.HeaderHeight;
            item.PagePartStyle.HeaderFontNames = style.HeaderFontNames;
            item.PagePartStyle.HeaderFontHeight = style.HeaderFontHeight;
            item.PagePartStyle.BorderColor = style.BorderColor;
            item.PagePartStyle.BorderWidth = style.BorderWidth;

            base.InsertItem(index, item);
        }
    }
}