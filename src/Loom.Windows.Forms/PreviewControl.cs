#region Using Directives

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     The small rectangle that paints the current edited value.
    /// </summary>
    internal class PreviewControl : Button
    {
        private GenericEditor genericEditor;

        public PreviewControl(GenericEditor editor)
        {
            Initialze(editor);
        }

        private void Initialze(GenericEditor editor)
        {
            genericEditor = editor;
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle rect = ClientRectangle;
            using (Brush b = new SolidBrush(genericEditor.BackColor))
            {
                pe.Graphics.FillRectangle(b, rect);
            }

            genericEditor.Editor.PaintValue(genericEditor.Value, pe.Graphics, rect);
            pe.Graphics.DrawRectangle(SystemPens.WindowText, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
        }
    }
}