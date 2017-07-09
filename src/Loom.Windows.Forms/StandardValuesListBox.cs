#region Using Directives

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     <strong>ListBox</strong> which is dropped when the type contains standard values.
    /// </summary>
    internal class StandardValuesListBox : ListBox
    {
        private GenericEditor editor;

        /// <summary>
        ///     Creates a <strong>DropListBox</strong>.
        /// </summary>
        public StandardValuesListBox(GenericEditor control)
        {
            InitializeControl(control);
        }

        private void InitializeControl(GenericEditor control)
        {
            editor = control;
            BorderStyle = BorderStyle.None;
            IntegralHeight = false;
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        /// <summary>
        ///     This member overrides <see cref="ListBox.OnDrawItem">ListBox.OnDrawItem</see>.
        /// </summary>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index < 0 || e.Index >= Items.Count)
                return;

            object value = Items[e.Index];
            Rectangle bounds = e.Bounds;

            if (editor.PaintValueSupported)
                using (Pen pen = new Pen(ForeColor))
                {
                    Rectangle r = e.Bounds;
                    r.Height -= 1;
                    if (editor.ShowPreviewOnly)
                    {
                        r.X += 2;
                        r.Width -= 5;
                    }
                    else
                    {
                        r.Width = GenericEditor.PaintValueWidth;
                        r.X += 2;
                        bounds.X += GenericEditor.PaintValueWidth + 2;
                        bounds.Width -= GenericEditor.PaintValueWidth + 2;
                    }
                    editor.Editor.PaintValue(value, e.Graphics, r);
                    e.Graphics.DrawRectangle(pen, r);
                }
            if (!editor.ShowPreviewOnly || !editor.PaintValueSupported)
                using (Brush brush = new SolidBrush(e.ForeColor))
                using (StringFormat format = new StringFormat())
                {
                    e.Graphics.DrawString(editor.GetValueAsText(value), Font, brush, bounds, format);
                }
        }

        /// <summary>
        ///     This member overrides <see cref="ListBox.OnMeasureItem">ListBox.OnMeasureItem</see>.
        /// </summary>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight += 1;
        }
    }
}