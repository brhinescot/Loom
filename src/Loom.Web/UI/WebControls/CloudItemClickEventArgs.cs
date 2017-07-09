#region Using Directives

using System;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    /// </summary>
    public sealed class CloudItemClickEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CloudItemClickEventArgs" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        internal CloudItemClickEventArgs(CloudItem item)
        {
            Item = item;
        }

        /// <summary>
        ///     Gets the item which is clicked.
        /// </summary>
        public CloudItem Item { get; }
    }
}