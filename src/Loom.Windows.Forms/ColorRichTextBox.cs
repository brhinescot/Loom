#region Using Directives

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Represents a Windows rich text box control that allows appending colored text.
    /// </summary>
    public partial class ColorRichTextBox : RichTextBox
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorRichTextBox" /> class.
        /// </summary>
        public ColorRichTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorRichTextBox" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ColorRichTextBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        ///     Appends text to the current text of a text box in the specified color.
        /// </summary>
        /// <param name="text">The text to append to the current contents of the text box.</param>
        /// <param name="color">The color of the appended text.</param>
        public void AppendText(string text, Color color)
        {
            AppendText(text, color, BackColor);
        }

        /// <summary>
        ///     Appends text to the current text of a text box in the specified color and background color.
        /// </summary>
        /// <param name="text">The text to append to the current contents of the text box.</param>
        /// <param name="color">The color of the appended text.</param>
        /// <param name="backColor">The background color of the appended text.</param>
        public void AppendText(string text, Color color, Color backColor)
        {
            int start = TextLength;
            AppendText(text);
            int end = TextLength;

            Select(start, end - start);
            SelectionColor = color;
            SelectionBackColor = backColor;
            SelectionLength = 0;
        }
    }
}