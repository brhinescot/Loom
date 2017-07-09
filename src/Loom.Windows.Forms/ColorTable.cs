#region Using Directives

using System.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public abstract class ColorTable
    {
        /// <summary>
        ///     Gets the item background.
        /// </summary>
        /// <value>The item background.</value>
        public abstract Color ItemBackground { get; }

        /// <summary>
        ///     Gets the item background selected.
        /// </summary>
        /// <value>The item background selected.</value>
        public abstract Color ItemBackgroundSelected { get; }

        /// <summary>
        ///     Gets the item background selected no focus.
        /// </summary>
        /// <value>The item background selected no focus.</value>
        public abstract Color ItemBackgroundSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the item background2.
        /// </summary>
        /// <value>The item background2.</value>
        public abstract Color ItemBackground2 { get; }

        /// <summary>
        ///     Gets the item background selected2.
        /// </summary>
        /// <value>The item background selected2.</value>
        public abstract Color ItemBackgroundSelected2 { get; }

        /// <summary>
        ///     Gets the item background selected no focus2.
        /// </summary>
        /// <value>The item background selected no focus2.</value>
        public abstract Color ItemBackgroundSelectedNoFocus2 { get; }

        /// <summary>
        ///     Gets the item border.
        /// </summary>
        /// <value>The item border.</value>
        public abstract Color ItemBorder { get; }

        /// <summary>
        ///     Gets the item border selected.
        /// </summary>
        /// <value>The item border selected.</value>
        public abstract Color ItemBorderSelected { get; }

        /// <summary>
        ///     Gets the item border selected no focus.
        /// </summary>
        /// <value>The item border selected no focus.</value>
        public abstract Color ItemBorderSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the item text dim.
        /// </summary>
        /// <value>The item text dim.</value>
        public abstract Color ItemTextDim { get; }

        /// <summary>
        ///     Gets the item text dim selected.
        /// </summary>
        /// <value>The item text dim selected.</value>
        public abstract Color ItemTextDimSelected { get; }

        /// <summary>
        ///     Gets the item text dim selected no focus.
        /// </summary>
        /// <value>The item text dim selected no focus.</value>
        public abstract Color ItemTextDimSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the item text.
        /// </summary>
        /// <value>The item text.</value>
        public abstract Color ItemText { get; }

        /// <summary>
        ///     Gets the item text selected.
        /// </summary>
        /// <value>The item text selected.</value>
        public abstract Color ItemTextSelected { get; }

        /// <summary>
        ///     Gets the item text selected no focus.
        /// </summary>
        /// <value>The item text selected no focus.</value>
        public abstract Color ItemTextSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the item text new.
        /// </summary>
        /// <value>The item text new.</value>
        public abstract Color ItemTextNew { get; }

        /// <summary>
        ///     Gets the item text new selected.
        /// </summary>
        /// <value>The item text new selected.</value>
        public abstract Color ItemTextNewSelected { get; }

        /// <summary>
        ///     Gets the item text new selected no focus.
        /// </summary>
        /// <value>The item text new selected no focus.</value>
        public abstract Color ItemTextNewSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the item horizontal seperator.
        /// </summary>
        /// <value>The item horizontal seperator.</value>
        public abstract Color ItemHorizontalSeparator { get; }

        /// <summary>
        ///     Gets the group header text.
        /// </summary>
        /// <value>The group header text.</value>
        public abstract Color GroupHeaderText { get; }

        /// <summary>
        ///     Gets the group header text selected.
        /// </summary>
        /// <value>The group header text selected.</value>
        public abstract Color GroupHeaderTextSelected { get; }

        /// <summary>
        ///     Gets the group header text selected no focus.
        /// </summary>
        /// <value>The group header text selected no focus.</value>
        public abstract Color GroupHeaderTextSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the group header background.
        /// </summary>
        /// <value>The group header background.</value>
        public abstract Color GroupHeaderBackground { get; }

        /// <summary>
        ///     Gets the group header background selected.
        /// </summary>
        /// <value>The group header background selected.</value>
        public abstract Color GroupHeaderBackgroundSelected { get; }

        /// <summary>
        ///     Gets the group header background selected no focus.
        /// </summary>
        /// <value>The group header background selected no focus.</value>
        public abstract Color GroupHeaderBackgroundSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the group header background2.
        /// </summary>
        /// <value>The group header background2.</value>
        public abstract Color GroupHeaderBackground2 { get; }

        /// <summary>
        ///     Gets the group header background selected2.
        /// </summary>
        /// <value>The group header background selected2.</value>
        public abstract Color GroupHeaderBackgroundSelected2 { get; }

        /// <summary>
        ///     Gets the group header background selected no focus2.
        /// </summary>
        /// <value>The group header background selected no focus2.</value>
        public abstract Color GroupHeaderBackgroundSelectedNoFocus2 { get; }

        /// <summary>
        ///     Gets the group header border.
        /// </summary>
        /// <value>The group header border.</value>
        public abstract Color GroupHeaderBorder { get; }

        /// <summary>
        ///     Gets the group header border selected.
        /// </summary>
        /// <value>The group header border selected.</value>
        public abstract Color GroupHeaderBorderSelected { get; }

        /// <summary>
        ///     Gets the group header border selected no focus.
        /// </summary>
        /// <value>The group header border selected no focus.</value>
        public abstract Color GroupHeaderBorderSelectedNoFocus { get; }

        /// <summary>
        ///     Gets the group header seperator.
        /// </summary>
        /// <value>The group header seperator.</value>
        public abstract Color GroupHeaderSeparator { get; }
    }
}