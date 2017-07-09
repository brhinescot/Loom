#region Using Directives

using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     The button that opens <see cref="UITypeEditor" /> controls.
    /// </summary>
    internal class EditorButton : Button
    {
        private static readonly int dialog = BitVector32.CreateMask();
        private static readonly int pushed = BitVector32.CreateMask(dialog);
        private static readonly int hover = BitVector32.CreateMask(pushed);

        private BitVector32 buttonFlags = new BitVector32(0);

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditorButton" /> class.
        /// </summary>
        public EditorButton()
        {
            Initialize();
        }

        /// <summary>
        ///     Gets or sets a value indicating if the button should be
        ///     drawn as a drop dialog button or as a drop button.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the button should be
        ///     drawn as a drop dialog button; <see langword="false" /> otherwise.
        /// </value>
        public bool IsDialog
        {
            get => buttonFlags[dialog];
            set
            {
                buttonFlags[dialog] = value;
                Invalidate();
            }
        }

        private void Initialize()
        {
            SetStyle(ControlStyles.Selectable, true);
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            TabStop = false;
            IsDefault = false;
            buttonFlags[dialog] = false;
            Cursor = Cursors.Default;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = SystemColors.ControlDarkDark;
            FlatAppearance.BorderSize = 1;
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            buttonFlags[hover] = true;
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            buttonFlags[hover] = false;
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="arg">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs arg)
        {
            base.OnMouseDown(arg);
            if (arg.Button != MouseButtons.Left)
                return;

            buttonFlags[pushed] = true;
            Invalidate();
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="arg">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs arg)
        {
            base.OnMouseUp(arg);
            if (arg.Button != MouseButtons.Left)
                return;

            buttonFlags[pushed] = false;
            Invalidate();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnPaint">Control.OnPaint</see>.
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Application.RenderWithVisualStyles)
            {
                ComboBoxRenderer.DrawDropDownButton(pe.Graphics, ClientRectangle, !Enabled ? ComboBoxState.Disabled : (buttonFlags[pushed] ? ComboBoxState.Pressed : (buttonFlags[hover] ? ComboBoxState.Hot : ComboBoxState.Normal)));
            }
            else
            {
                // TODO: EditorButton dialog state rendering.
                Graphics g = pe.Graphics;
                Rectangle r = ClientRectangle;

                if (IsDialog)
                {
                    base.OnPaint(pe);
                    // draws dot dot dot.
                    int x = r.X + r.Width / 2 - 5;
                    int y = r.Bottom - 5;
                    using (Brush brush = new SolidBrush(Enabled ? SystemColors.ControlText : SystemColors.GrayText))
                    {
                        g.FillRectangle(brush, x, y, 2, 2);
                        g.FillRectangle(brush, x + 4, y, 2, 2);
                        g.FillRectangle(brush, x + 8, y, 2, 2);
                    }
                }
                else
                {
                    ControlPaint.DrawComboButton(g, ClientRectangle, !Enabled ? ButtonState.Inactive : (buttonFlags[pushed] ? ButtonState.Pushed : ButtonState.Normal));
                }
            }
        }
    }
}