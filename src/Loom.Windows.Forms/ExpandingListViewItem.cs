#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    [Serializable]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [ToolboxItem(false)]
    public class ExpandingListViewItem : ExpandingListViewItemBase
    {
        private ExpandingListViewGroup parent;
        private ExpandingListViewSubItemCollection subItems;

        //private Rectangle bounds = Rectangle.Empty;
        private string summary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItem" /> class.
        /// </summary>
        public ExpandingListViewItem() : this(new string[0]) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItem" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ExpandingListViewItem(string[] items)
        {
            subItems = new ExpandingListViewSubItemCollection(this);
            foreach (string text in items)
                subItems.Add(new ExpandingListViewSubItem(text));
        }

        /// <summary>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Localizable(true)]
        [Category("Appearance")]
        public override string Text
        {
            get
            {
                if (subItems.Count == 0)
                    return string.Empty;
                return subItems[0].Text;
            }
            set => SubItems[0].Text = value;
        }

        // TODO: Move to Design assembly.
        /// <summary>
        /// </summary>
        [Editor(typeof(ExpandingListViewSubItemCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Data")]
        [Description("ListViewItemSubItemsDescr")]
        public ExpandingListViewSubItemCollection SubItems
        {
            get
            {
                if (subItems == null || subItems == default(ExpandingListViewSubItemCollection))
                    subItems = new ExpandingListViewSubItemCollection(this);
                Debug.Assert(subItems != null);
                Debug.Assert(subItems != default(ExpandingListViewSubItemCollection));
                if (subItems.Count == 0)
                    subItems.Add(new ExpandingListViewSubItem(Text));

                return subItems;
            }
        }

        /// <summary>
        ///     Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary
        {
            get => summary;
            set => summary = value;
        }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public ExpandingListViewGroup Parent
        {
            get => parent;
            set => parent = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="ExpandingListViewItemBase" /> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;
                if (!Selected)
                    parent.SetSelected(false);
            }
        }

        #region Nested type: ExpandingListViewSubItem

        /// <summary>
        /// </summary>
        [Serializable]
        [DesignTimeVisible(false)]
        [DefaultProperty("Text")]
        [ToolboxItem(false)]
        public class ExpandingListViewSubItem
        {
            private string text;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ExpandingListViewSubItem" /> class.
            /// </summary>
            public ExpandingListViewSubItem() { }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ExpandingListViewSubItem" /> class.
            /// </summary>
            /// <param name="text">The text.</param>
            public ExpandingListViewSubItem(string text)
            {
                this.text = text;
            }

            /// <summary>
            ///     Gets or sets the text.
            /// </summary>
            /// <value>The text.</value>
            public string Text
            {
                get => text;
                set => text = value;
            }
        }

        #endregion

        #region Nested type: ExpandingListViewSubItemCollection

        /// <summary>
        /// </summary>
        [Serializable]
        [DesignTimeVisible(false)]
        [DefaultProperty("Text")]
        [ToolboxItem(false)]
        public class ExpandingListViewSubItemCollection : List<ExpandingListViewSubItem>
        {
            // TODO: Code for list owner
            private ExpandingListViewItem owner;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ExpandingListViewSubItemCollection" /> class.
            /// </summary>
            public ExpandingListViewSubItemCollection() { }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ExpandingListViewSubItemCollection" /> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public ExpandingListViewSubItemCollection(ExpandingListViewItem owner) : this()
            {
                this.owner = owner;
            }
        }

        #endregion
    }
}