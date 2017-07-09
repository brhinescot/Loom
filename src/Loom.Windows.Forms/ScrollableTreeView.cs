#region Using Directives

using System;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Summary description for FusionTreeView.
    /// </summary>
    public class ScrollableTreeView : TreeView
    {
        /// <summary>
        ///     Sets the scroll position.
        /// </summary>
        /// <param name="scrollBar">Scroll bar.</param>
        /// <param name="position">Position.</param>
        /// <param name="redraw">Redraw.</param>
        public void SetScrollPosition(ScrollBarOrientation scrollBar, int position, bool redraw = false)
        {
            NativeMethods.SetScrollPos(Handle, Convert.ToInt32(scrollBar), position, redraw);
        }
    }
}