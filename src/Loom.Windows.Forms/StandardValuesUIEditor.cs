#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

#endregion

namespace Loom.Windows.Forms
{
    internal class StandardValuesUIEditor : UITypeEditor
    {
        private readonly GenericEditor genericEditor;
        private IWindowsFormsEditorService editorService;
        private StandardValuesListBox listbox;

        public StandardValuesUIEditor(GenericEditor editor)
        {
            genericEditor = editor;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // Uses the IWindowsFormsEditorService to display a drop-down UI
            if (editorService == null)
                editorService = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
            if (editorService != null)
            {
                if (listbox == null)
                {
                    listbox = new StandardValuesListBox(genericEditor);
                    listbox.SelectedIndexChanged += HandleListBoxChanged;
                }
                object[] values = genericEditor.GetStandardValues();
                listbox.Items.Clear();

                int width = 0;
                Font font = listbox.Font;

                // Add the standard values in the list box and
                // measure the text at the same time.

                using (Graphics g = listbox.CreateGraphics())
                {
                    foreach (object item in values)
                        if (!listbox.Items.Contains(item))
                        {
                            string valueString = genericEditor.GetValueAsText(item);
                            if (!genericEditor.ShowPreviewOnly)
                                width = (int) Math.Max(width, g.MeasureString(valueString, font).Width);
                            listbox.Items.Add(item);
                        }
                }

                if (genericEditor.PaintValueSupported)
                    width += GenericEditor.PaintValueWidth + 4;

                Rectangle bounds = genericEditor.Bounds;
                listbox.SelectedItem = value;
                listbox.Height =
                    Math.Max(font.Height + 2, Math.Min(200, listbox.PreferredHeight));
                listbox.Width = Math.Max(width, bounds.Width);

                editorService.DropDownControl(listbox);

                return listbox.SelectedItem ?? value;
            }
            return value;
        }

        private void HandleListBoxChanged(object sender, EventArgs e)
        {
            editorService.CloseDropDown();
        }
    }
}