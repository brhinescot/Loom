#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     The form that contains the dropped down editor.
    /// </summary>
    internal class DropDownForm : Form
    {
        // Currently dropped control.
        private Control currentControl;

        // The service that dropped this form.
        private IWindowsFormsEditorService editorService;

        /// <summary>
        ///     Creates a <strong>DropDownForm</strong>.
        /// </summary>
        /// <param name="service">The service that drops this form.</param>
        public DropDownForm(IWindowsFormsEditorService service)
        {
            InitializeControl(service);
        }

        /// <summary>
        ///     Gets or sets the control displayed by the form.
        /// </summary>
        /// <value>A <see cref="Control" /> instance.</value>
        public Control Component
        {
            get => currentControl;
            set
            {
                if (currentControl != null)
                {
                    Controls.Remove(currentControl);
                    currentControl = null;
                }
                if (value != null)
                {
                    currentControl = value;
                    Controls.Add(currentControl);
                    Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                    currentControl.Location = new Point(0, 0);
                    currentControl.Visible = true;
                    currentControl.Resize += OnCurrentControlResize;
                }
                Enabled = currentControl != null;
            }
        }

        private void InitializeControl(IWindowsFormsEditorService service)
        {
            StartPosition = FormStartPosition.Manual;
            currentControl = null;
            ShowInTaskbar = false;
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = string.Empty;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Visible = false;
            editorService = service;
        }

        internal void SystemColorChanged()
        {
            OnSystemColorsChanged(EventArgs.Empty);
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
        /// </summary>
        /// <remarks>
        ///     Closes the form when the left button is clicked.
        /// </remarks>
        protected override void OnMouseDown(MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Left)
                editorService.CloseDropDown();
            base.OnMouseDown(me);
        }

        /// <summary>
        ///     This member overrides <see cref="Form.OnClosed">Form.OnClosed</see>.
        /// </summary>
        protected override void OnClosed(EventArgs args)
        {
            if (Visible)
                editorService.CloseDropDown();
            base.OnClosed(args);
        }

        /// <summary>
        ///     This member overrides <see cref="Form.OnDeactivate">Form.OnDeactivate</see>.
        /// </summary>
        protected override void OnDeactivate(EventArgs args)
        {
            if (Visible)
                editorService.CloseDropDown();
            base.OnDeactivate(args);
        }

        /// <summary>
        ///     Invoked when the dropped control is resized.
        ///     This resizes the form and realigns it.
        /// </summary>
        private void OnCurrentControlResize(object o, EventArgs e)
        {
            int width;
            if (currentControl != null)
            {
                width = Width;
                Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                Left -= Width - width;
            }
        }

        /// <summary>
        ///     Invoked when the form is resized.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (currentControl != null)
            {
                currentControl.SetBounds(0, 0, width - 2, height - 2);
                width = currentControl.Width;
                height = currentControl.Height;
                if (height == 0 && currentControl is ListBox)
                {
                    height = ((ListBox) currentControl).ItemHeight;
                    currentControl.Height = height;
                }
                width = width + 2;
                height = height + 2;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }
    }
}