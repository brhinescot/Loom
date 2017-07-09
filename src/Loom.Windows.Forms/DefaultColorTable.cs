#region Using Directives

using System.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Provides a default implementation of a <see cref="ColorTable" />.
    /// </summary>
    public class DefaultColorTable : ColorTable
    {
        /// <summary>
        ///     Gets the item background.
        /// </summary>
        /// <value>The item background.</value>
        public override Color ItemBackground => SystemColors.Window;

        /// <summary>
        ///     Gets the item background selected.
        /// </summary>
        /// <value>The item background selected.</value>
        public override Color ItemBackgroundSelected => SystemColors.Highlight;

        /// <summary>
        ///     Gets the item background selected no focus.
        /// </summary>
        /// <value>The item background selected no focus.</value>
        public override Color ItemBackgroundSelectedNoFocus => SystemColors.Control;

        /// <summary>
        ///     Gets the item background2.
        /// </summary>
        /// <value>The item background2.</value>
        public override Color ItemBackground2 => SystemColors.Window;

        /// <summary>
        ///     Gets the item background selected2.
        /// </summary>
        /// <value>The item background selected2.</value>
        public override Color ItemBackgroundSelected2 => SystemColors.Highlight;

        /// <summary>
        ///     Gets the item background selected no focus2.
        /// </summary>
        /// <value>The item background selected no focus2.</value>
        public override Color ItemBackgroundSelectedNoFocus2 => SystemColors.Control;

        /// <summary>
        ///     Gets the item border.
        /// </summary>
        /// <value>The item border.</value>
        public override Color ItemBorder => SystemColors.Window;

        /// <summary>
        ///     Gets the item border selected.
        /// </summary>
        /// <value>The item border selected.</value>
        public override Color ItemBorderSelected => SystemColors.Highlight;

        /// <summary>
        ///     Gets the item border selected no focus.
        /// </summary>
        /// <value>The item border selected no focus.</value>
        public override Color ItemBorderSelectedNoFocus => SystemColors.Control;

        /// <summary>
        ///     Gets the item text dim.
        /// </summary>
        /// <value>The item text dim.</value>
        public override Color ItemTextDim => Color.Blue;

        /// <summary>
        ///     Gets the item text dim selected.
        /// </summary>
        /// <value>The item text dim selected.</value>
        public override Color ItemTextDimSelected => SystemColors.HighlightText;

        /// <summary>
        ///     Gets the item text dim selected no focus.
        /// </summary>
        /// <value>The item text dim selected no focus.</value>
        public override Color ItemTextDimSelectedNoFocus => SystemColors.WindowText;

        /// <summary>
        ///     Gets the item text.
        /// </summary>
        /// <value>The item text.</value>
        public override Color ItemText => SystemColors.WindowText;

        /// <summary>
        ///     Gets the item text selected.
        /// </summary>
        /// <value>The item text selected.</value>
        public override Color ItemTextSelected => SystemColors.HighlightText;

        /// <summary>
        ///     Gets the item text selected no focus.
        /// </summary>
        /// <value>The item text selected no focus.</value>
        public override Color ItemTextSelectedNoFocus => SystemColors.WindowText;

        /// <summary>
        ///     Gets the item text new.
        /// </summary>
        /// <value>The item text new.</value>
        public override Color ItemTextNew => SystemColors.WindowText;

        /// <summary>
        ///     Gets the item text new selected.
        /// </summary>
        /// <value>The item text new selected.</value>
        public override Color ItemTextNewSelected => SystemColors.HighlightText;

        /// <summary>
        ///     Gets the item text new selected no focus.
        /// </summary>
        /// <value>The item text new selected no focus.</value>
        public override Color ItemTextNewSelectedNoFocus => SystemColors.WindowText;

        /// <summary>
        ///     Gets the item horizontal seperator.
        /// </summary>
        /// <value>The item horizontal seperator.</value>
        public override Color ItemHorizontalSeparator => SystemColors.Control;

        /// <summary>
        ///     Gets the group header text.
        /// </summary>
        /// <value>The group header text.</value>
        public override Color GroupHeaderText => SystemColors.Highlight;

        /// <summary>
        ///     Gets the group header text selected.
        /// </summary>
        /// <value>The group header text selected.</value>
        public override Color GroupHeaderTextSelected => SystemColors.HighlightText;

        /// <summary>
        ///     Gets the group header text selected no focus.
        /// </summary>
        /// <value>The group header text selected no focus.</value>
        public override Color GroupHeaderTextSelectedNoFocus => SystemColors.WindowText;

        /// <summary>
        ///     Gets the group header background.
        /// </summary>
        /// <value>The group header background.</value>
        public override Color GroupHeaderBackground => SystemColors.Window;

        /// <summary>
        ///     Gets the group header background selected.
        /// </summary>
        /// <value>The group header background selected.</value>
        public override Color GroupHeaderBackgroundSelected => SystemColors.Highlight;

        /// <summary>
        ///     Gets the group header background selected no focus.
        /// </summary>
        /// <value>The group header background selected no focus.</value>
        public override Color GroupHeaderBackgroundSelectedNoFocus => SystemColors.Control;

        /// <summary>
        ///     Gets the group header background2.
        /// </summary>
        /// <value>The group header background2.</value>
        public override Color GroupHeaderBackground2 => SystemColors.Window;

        /// <summary>
        ///     Gets the group header background selected2.
        /// </summary>
        /// <value>The group header background selected2.</value>
        public override Color GroupHeaderBackgroundSelected2 => SystemColors.Highlight;

        /// <summary>
        ///     Gets the group header background selected no focus2.
        /// </summary>
        /// <value>The group header background selected no focus2.</value>
        public override Color GroupHeaderBackgroundSelectedNoFocus2 => SystemColors.Control;

        /// <summary>
        ///     Gets the group header seperator.
        /// </summary>
        /// <value>The group header seperator.</value>
        public override Color GroupHeaderSeparator => SystemColors.Highlight;

        /// <summary>
        ///     Gets the group header border.
        /// </summary>
        /// <value>The group header border.</value>
        public override Color GroupHeaderBorder => SystemColors.Window;

        /// <summary>
        ///     Gets the group header border selected.
        /// </summary>
        /// <value>The group header border selected.</value>
        public override Color GroupHeaderBorderSelected => SystemColors.Highlight;

        /// <summary>
        ///     Gets the group header border selected no focus.
        /// </summary>
        /// <value>The group header border selected no focus.</value>
        public override Color GroupHeaderBorderSelectedNoFocus => SystemColors.Control;
    }
}