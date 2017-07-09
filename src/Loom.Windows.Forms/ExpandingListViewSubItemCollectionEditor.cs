#region Using Directives

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewSubItemCollectionEditor : CollectionEditor
    {
        private ExpandingListViewItem.ExpandingListViewSubItem firstSubItem;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewSubItemCollectionEditor" /> class.
        /// </summary>
        /// <param name="type">The type of the collection for this editor to edit.</param>
        public ExpandingListViewSubItemCollectionEditor(Type type) : base(type) { }

        /// <summary>
        ///     Creates a new instance of the specified collection item type.
        /// </summary>
        /// <param name="itemType">The type of item to create.</param>
        /// <returns>A new instance of the specified object.</returns>
        protected override object CreateInstance(Type itemType)
        {
            object obj = base.CreateInstance(itemType);
            //if (obj is ExpandingListViewItem.ExpandingListViewSubItem)
            //{
            //    ((ExpandingListViewItem.ExpandingListViewSubItem)obj).Name = System.Design.SR.GetString("ListViewSubItemBaseName") + ExpandingListViewSubItemCollectionEditor.count++;
            //}
            return obj;
        }

        /// <summary>
        ///     Retrieves the display text for the given list item.
        /// </summary>
        /// <param name="value">The list item for which to retrieve display text.</param>
        /// <returns>The display text for value.</returns>
        protected override string GetDisplayText(object value)
        {
            string text;
            if (value == null)
                return string.Empty;

            PropertyDescriptor descriptor = TypeDescriptor.GetDefaultProperty(CollectionType);
            if (descriptor != null && descriptor.PropertyType == typeof(string))
            {
                text = (string) descriptor.GetValue(value);
                if (text != null && text.Length > 0)
                    return text;
            }
            text = TypeDescriptor.GetConverter(value).ConvertToString(value);
            if (!Compare.IsNullOrEmpty(text))
                return text;

            return value.GetType().Name;
        }

        /// <summary>
        ///     Gets an array of objects containing the specified collection.
        /// </summary>
        /// <param name="editValue">The collection to edit.</param>
        /// <returns>
        ///     An array containing the collection objects, or an empty object array if the specified collection does not inherit
        ///     from <see cref="ICollection"></see>.
        /// </returns>
        protected override object[] GetItems(object editValue)
        {
            ExpandingListViewItem.ExpandingListViewSubItemCollection collection = (ExpandingListViewItem.ExpandingListViewSubItemCollection) editValue;
            object[] items = new object[collection.Count];
            ((ICollection) collection).CopyTo(items, 0);
            if (items.Length > 0)
            {
                firstSubItem = collection[0];
                object[] objects = new object[items.Length - 1];
                Array.Copy(items, 1, objects, 0, objects.Length);
                items = objects;
            }
            return items;
        }

        /// <summary>
        ///     Sets the specified array as the items of the collection.
        /// </summary>
        /// <param name="editValue">The collection to edit.</param>
        /// <param name="value">An array of objects to set as the collection items.</param>
        /// <returns>
        ///     The newly created collection object or, otherwise, the collection indicated by the editValue parameter.
        /// </returns>
        protected override object SetItems(object editValue, object[] value)
        {
            IList list = editValue as IList;
            if (list == null)
                return editValue;

            list.Clear();
            list.Add(firstSubItem);
            for (int i = 0; i < value.Length; i++)
                list.Add(value[i]);

            return editValue;
        }
    }
}