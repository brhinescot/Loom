#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Data.Diff
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [DebuggerDisplay("Visible={Visible}, FriendlyName={FriendlyName}, Format={Format}")]
    public sealed class DiffVisibleAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffVisibleAttribute" /> class.
        /// </summary>
        public DiffVisibleAttribute() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffVisibleAttribute" /> class.
        /// </summary>
        public DiffVisibleAttribute(bool visible)
        {
            Visible = visible;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the property is visible to the <see cref="Diff{T}" />
        ///     engine.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; } = true;

        /// <summary>
        ///     Gets or sets the friendly name.
        /// </summary>
        /// <value>The name of the friendly.</value>
        public string FriendlyName { get; set; }

        /// <summary>
        ///     Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        public string Format { get; set; }

        /// <summary>
        ///     Gets or sets the format provider.
        /// </summary>
        /// <value>The format provider.</value>
        public IFormatProvider FormatProvider { get; set; }
    }
}