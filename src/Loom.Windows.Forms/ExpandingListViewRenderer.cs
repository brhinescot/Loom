#region Using Directives

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewRenderer : IDisposable
    {
        private const int DrawRightSideMinWidth = 200;
        private const int IconSpace = 22;

        private readonly TextFormatFlags ellipsisFormat;
        private readonly TextFormatFlags groupFormat;
        private readonly Font groupHeaderFont = new Font("Tahoma", 8.25f, FontStyle.Bold);
        private readonly Font itemFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private readonly TextFormatFlags rightAlignFormat;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewRenderer" /> class.
        /// </summary>
        /// <param name="colorTable">The color table.</param>
        public ExpandingListViewRenderer(ColorTable colorTable)
        {
            ColorTable = colorTable;
            groupFormat = TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter |
                          TextFormatFlags.NoPrefix;
            ellipsisFormat = TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis |
                             TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix;
            rightAlignFormat = TextFormatFlags.Right | TextFormatFlags.SingleLine;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewRenderer" /> class.
        /// </summary>
        public ExpandingListViewRenderer() : this(new DefaultColorTable()) { }

        /// <summary>
        ///     Gets or sets the height of the item.
        /// </summary>
        /// <value>The height of the item.</value>
        public virtual int ItemHeight { get; set; } = 56;

        /// <summary>
        ///     Gets the color table.
        /// </summary>
        /// <value>The color table.</value>
        public virtual ColorTable ColorTable { get; } = new DefaultColorTable();

        /// <summary>
        ///     Gets the group header font.
        /// </summary>
        /// <value>The group header font.</value>
        public virtual Font GroupHeaderFont => groupHeaderFont;

        /// <summary>
        ///     Gets the item font.
        /// </summary>
        /// <value>The item font.</value>
        public virtual Font ItemFont => itemFont;

        /// <summary>
        ///     Gets the height of the group header.
        /// </summary>
        /// <value>The height of the group header.</value>
        public virtual int GroupHeaderHeight { get; } = 28;

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        /// <summary>
        ///     Draws the group header background.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawGroupHeaderBackground(DrawGroupHeaderArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            Rectangle bounds = new Rectangle(
                args.Bounds.Location,
                new Size(args.Bounds.Width, args.Bounds.Height - 1));

            Color backColor;
            if (args.Selected)
                if (args.Focused)
                    backColor = ColorTable.GroupHeaderBackgroundSelected;
                else
                    backColor = ColorTable.GroupHeaderBackgroundSelectedNoFocus;
            else
                backColor = ColorTable.GroupHeaderBackground;

            using (Brush brush = new SolidBrush(backColor))
            {
                args.Graphics.FillRectangle(brush, bounds);
            }
        }

        /// <summary>
        ///     Draws the group header border.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawGroupHeaderBorder(DrawGroupHeaderArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            Rectangle bounds = new Rectangle(
                args.Bounds.Location,
                new Size(args.Bounds.Width, args.Bounds.Height - 1));

            Color borderColor;
            if (args.Selected)
                if (args.Focused)
                    borderColor = ColorTable.GroupHeaderBorderSelected;
                else
                    borderColor = ColorTable.GroupHeaderBorderSelectedNoFocus;
            else
                borderColor = ColorTable.GroupHeaderBorder;

            using (Pen borderPen = new Pen(borderColor, 1f))
            {
                args.Graphics.DrawRectangle(borderPen, bounds);
            }
        }

        /// <summary>
        ///     Draws the group header text.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawGroupHeaderText(DrawGroupHeaderArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            Rectangle textBounds = new Rectangle
            (
                args.Bounds.Left + 5,
                args.Bounds.Bottom - GroupHeaderFont.Height - 5,
                args.Bounds.Width - 10,
                GroupHeaderFont.Height
            );

            Color textColor;
            if (args.Selected)
                if (args.Focused)
                    textColor = ColorTable.GroupHeaderTextSelected;
                else
                    textColor = ColorTable.GroupHeaderTextSelectedNoFocus;
            else
                textColor = ColorTable.GroupHeaderText;

            TextRenderer.DrawText(args.Graphics, args.Group.Text, GroupHeaderFont, textBounds, textColor, groupFormat);
        }

        /// <summary>
        ///     Draws the group header separator.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawGroupHeaderSeparator(DrawGroupHeaderArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            using (Pen pen = new Pen(ColorTable.GroupHeaderSeparator, 2f))
            {
                args.Graphics.DrawLine(pen, 0, args.Bounds.Bottom - 1, args.Bounds.Width, args.Bounds.Bottom - 1);
            }
        }

        /// <summary>
        ///     Draws the item background.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawItemBackground(DrawItemArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            Color backColor;
            Color backColor2;
            Rectangle bounds = new Rectangle(args.Bounds.Location, new Size(args.Bounds.Width, args.Bounds.Height - 1));

            if (args.Selected)
            {
                if (args.Focused)
                {
                    backColor = ColorTable.ItemBackgroundSelected;
                    backColor2 = ColorTable.ItemBackgroundSelected2;
                }
                else
                {
                    backColor = ColorTable.ItemBackgroundSelectedNoFocus;
                    backColor2 = ColorTable.ItemBackgroundSelectedNoFocus2;
                }
            }
            else
            {
                backColor = ColorTable.ItemBackground;
                backColor2 = ColorTable.ItemBackground2;
            }

            using (LinearGradientBrush brushBack = new LinearGradientBrush(bounds, backColor, backColor2, 90f))
            {
                args.Graphics.FillRectangle(brushBack, bounds);
            }

            //using (Brush brushBack = new SolidBrush(backColor))
            //    args.Graphics.FillRectangle(brushBack, bounds);
        }

        /// <summary>
        ///     Draws the item border.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawItemBorder(DrawItemArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            Color borderColor;
            Rectangle bounds = new Rectangle(args.Bounds.Location, new Size(args.Bounds.Width, args.Bounds.Height - 1));

            if (args.Selected)
                if (args.Focused)
                    borderColor = ColorTable.ItemBorderSelected;
                else
                    borderColor = ColorTable.ItemBorderSelectedNoFocus;
            else
                borderColor = ColorTable.ItemBorder;

            using (Pen borderPen = new Pen(borderColor, 1f))
            {
                args.Graphics.DrawRectangle(borderPen, bounds);
            }
        }

        /// <summary>
        ///     Draws the item text.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawItemText(DrawItemArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            SizeF dateSize = SizeF.Empty;
            if (args.Item.SubItems.Count > 1)
                dateSize = args.Graphics.MeasureString(args.Item.SubItems[1].Text, ItemFont);

            Color textColor;
            Color textDimColor;

            // calculate the bounds of the title, necessary to 
            // draw ellipsis if string is too long
            Rectangle titleBounds = new Rectangle(args.Bounds.Left + IconSpace,
                args.Bounds.Top + 5,
                args.Bounds.Width - IconSpace - 5,
                ItemFont.Height);

            //see if should leave room for the second column (date and username)
            if (args.Bounds.Width > DrawRightSideMinWidth)
                titleBounds.Width -= (int) dateSize.Width;

            if (args.Selected)
            {
                if (args.Focused)
                {
                    // Selected and Focused
                    textColor = ColorTable.ItemTextSelected;
                    textDimColor = ColorTable.ItemTextSelected;
                }
                else
                {
                    // Selected and No Focused
                    textColor = ColorTable.ItemTextSelectedNoFocus;
                    textDimColor = ColorTable.ItemTextDimSelectedNoFocus;
                }
            }
            else
            {
                // Not Selected
                textColor = ColorTable.ItemText;
                textDimColor = ColorTable.ItemTextDim;
            }

            // Text
            TextRenderer.DrawText(args.Graphics, args.Item.Text, ItemFont, titleBounds, textColor, ellipsisFormat);

            // Date
            if (args.Bounds.Width > DrawRightSideMinWidth)
                if (args.Item.SubItems.Count > 1)
                    TextRenderer.DrawText(args.Graphics, args.Item.SubItems[1].Text, ItemFont,
                        new Rectangle(new Point(args.Bounds.Width - 150, titleBounds.Top), new Size(145, titleBounds.Height)), textColor, rightAlignFormat);

            // Summary
            titleBounds.Y += ItemFont.Height + 3;
            titleBounds.Height = ItemFont.Height * 2 + 4;
            titleBounds.Width = args.Bounds.Width - IconSpace - 2;

            TextRenderer.DrawText(args.Graphics, args.Item.Summary, ItemFont, titleBounds, textDimColor, ellipsisFormat);
        }

        /// <summary>
        ///     Draws the item separator.
        /// </summary>
        /// <param name="args">The args.</param>
        public virtual void DrawItemSeparator(DrawItemArgs args)
        {
            Argument.Assert.IsNotNull(args, "args");

            using (Pen pen = new Pen(ColorTable.ItemHorizontalSeparator))
            {
                args.Graphics.DrawLine(pen, 0, args.Bounds.Bottom, args.Bounds.Width, args.Bounds.Bottom);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (groupHeaderFont != null)
                    groupHeaderFont.Dispose();
                if (itemFont != null)
                    itemFont.Dispose();
            }
        }
    }
}