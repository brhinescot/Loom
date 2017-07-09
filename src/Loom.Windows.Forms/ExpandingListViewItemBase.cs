#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    [Serializable]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [ToolboxItem(false)]
    public class ExpandingListViewItemBase
    {
        private Rectangle bounds = Rectangle.Empty;
        private Font font;
        private Color foreColor = Color.Empty;
        private int imageIndex;
        private bool selected;
        private object tag;
        private string text;

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public virtual string Text
        {
            get => text;
            set => text = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="ExpandingListViewItemBase" /> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public virtual bool Selected
        {
            get => selected;
            set => selected = value;
        }

        /// <summary>
        ///     Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        [TypeConverter(typeof(ImageIndexConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue(-1)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int ImageIndex
        {
            get => imageIndex;
            set => imageIndex = value;
        }

        /// <summary>
        ///     Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font
        {
            get => font;
            set => font = value;
        }

        /// <summary>
        ///     Gets or sets the render bounds.
        /// </summary>
        /// <value>The render bounds.</value>
        internal Rectangle RenderBounds
        {
            get => bounds;
            set => bounds = value;
        }

        /// <summary>
        ///     Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get => foreColor;
            set => foreColor = value;
        }

        /// <summary>
        ///     Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        [Browsable(false)]
        public object Tag
        {
            get => tag;
            set => tag = value;
        }

        internal void SetSelected(bool isSelected)
        {
            selected = isSelected;
        }
    }
}