#region Using Directives

using System.Collections.ObjectModel;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public class ListItemCollection : Collection<ListItem>
    {
        public ListItem FindByText(string text)
        {
            int num = FindByTextInternal(text, true);
            return num != -1 ? this[num] : null;
        }

        public ListItem FindByValue(string value)
        {
            int num = FindByValueInternal(value, true);
            return num != -1 ? this[num] : null;
        }

        internal int FindByTextInternal(string text, bool includeDisabled)
        {
            int num = 0;
            foreach (ListItem item in this)
            {
                if (item.Text.Equals(text) && (includeDisabled || !item.Disabled))
                    return num;
                num++;
            }
            return -1;
        }

        internal int FindByValueInternal(string value, bool includeDisabled)
        {
            int num = 0;
            foreach (ListItem item in this)
            {
                if (item.Value.Equals(value) && (includeDisabled || !item.Disabled))
                    return num;
                num++;
            }
            return -1;
        }
    }
}