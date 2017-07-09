#region Using Directives

using System;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Provides a ballon tip for a <see cref="TextBox" /> control.
    /// </summary>
    public static class TextBoxBalloonTip
    {
        /// <summary>
        ///     Displays a ballon tip on the specified <see cref="TextBox" />.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox" /> on which to display the balloon tip.</param>
        /// <param name="text">The text of the balloon tip.</param>
        /// <param name="title">The title of the balloon tip.</param>
        /// <param name="icon">The <see cref="BalloonTipIcon" /> to show.</param>
        public static void Show(TextBox textBox, string text, string title, BalloonTipIcon icon)
        {
            Argument.Assert.IsNotNull(textBox, "textBox");

            if (Environment.OSVersion.Version.Major >= 5 && Environment.OSVersion.Version.Minor >= 1 ||
                Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.EDITBALLOONTIP stTip = new NativeMethods.EDITBALLOONTIP(title, text, (int) icon);
                NativeMethods.SendMessage(textBox.Handle, NativeMethods.EM_SHOWBALLOONTIP, IntPtr.Zero, ref stTip);
            }
        }

        /// <summary>
        ///     Displays a ballon tip on the specified <see cref="TextBox" />.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox" /> on which to display the balloon tip.</param>
        /// <param name="text">The text of the balloon tip.</param>
        /// <param name="title">The title of the balloon tip.</param>
        public static void Show(TextBox textBox, string text, string title)
        {
            Show(textBox, text, title, BalloonTipIcon.None);
        }

        /// <summary>
        ///     Displays a ballon tip on the specified <see cref="TextBox" />.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox" /> on which to display the balloon tip.</param>
        /// <param name="text">The text of the balloon tip.</param>
        public static void Show(TextBox textBox, string text)
        {
            Show(textBox, text, null, BalloonTipIcon.None);
        }
    }
}