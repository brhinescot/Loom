#region Using Directives

using System;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    [Flags]
    public enum AnimationStyle
    {
        /// <summary>
        /// </summary>
        None = 0x0001,

        /// <summary>
        /// </summary>
        Fade = 0x0002,

        /// <summary>
        /// </summary>
        SlideUp = 0x0004,

        /// <summary>
        /// </summary>
        SlideDown = 0x0008,

        /// <summary>
        /// </summary>
        SlideRight = 0x0010,

        /// <summary>
        /// </summary>
        SlideLeft = 0x0010
    }
}