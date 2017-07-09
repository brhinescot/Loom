#region Using Directives

using System;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Summary description for WhiteboardEventArgs.
    /// </summary>
    public sealed class DesignerEventArgs : EventArgs
    {
        /// <summary>
        /// </summary>
        /// <param name="control"></param>
        public DesignerEventArgs(DesignerControl control)
        {
            Control = control;
        }

        /// <summary>
        /// </summary>
        public DesignerControl Control { get; }
    }
}