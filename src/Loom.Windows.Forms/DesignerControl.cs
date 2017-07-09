#region Using Directives

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Summary description for DesignerControl.
    /// </summary>
    public class DesignerControl : MovableContainerControl
    {
        private const int BorderWidth = 2;
        private const int BackArcSize = 10;
        private const int BackMarginWidth = BackArcSize / 2;
        private const int LabelOffset = BackArcSize - 1;
        private const int TextOffsetX = LabelOffset + 5;
        private const int TextOffsetY = LabelOffset + 15;
        private const float SweepAngle = 90.0f;
        private const int CornerBottomRightAngle = 360;
        private const int CornerBottomLeftAngle = 90;
        private const int CornerTopRightAngle = 270;
        private const int CornerTopLeftAngle = 180;
        private const double GradientHeight = 1.5;
        private const int DefaultControlHeight = 56;
        private const int DefaultControlWidth = 85;

        private static readonly int DrawBorderFlag = BitVector32.CreateMask();
        private static readonly int DrawShadowFlag = BitVector32.CreateMask(DrawBorderFlag);
        private static readonly int MouseHotTrackingFlag = BitVector32.CreateMask(DrawShadowFlag);
        private static readonly int DrawAlternatingBorderFlag = BitVector32.CreateMask(MouseHotTrackingFlag);
        private static readonly int IsOverValidDropTargetFlag = BitVector32.CreateMask(DrawAlternatingBorderFlag);
        private static readonly int SelectedFlag = BitVector32.CreateMask(IsOverValidDropTargetFlag);
        private static readonly int AllowDesignerDropFlag = BitVector32.CreateMask(SelectedFlag);
        private readonly Size cornerSize = new Size(BackArcSize, BackArcSize);
        private readonly Color hoverBorderColor = Color.IndianRed;
        private Color borderColor = Color.MidnightBlue;
        private string controlValue;
        private Color currentBorderColor = Color.MidnightBlue;
        private Color currentGadientColor2 = Color.CornflowerBlue;

        [NonSerialized] private BitVector32 designerFlags = new BitVector32(0);

        private Color gradientColor1 = Color.White;
        private Color gradientColor2 = Color.CornflowerBlue;

        private Point offset;
        private Rectangle parentRectangle;

        /// <summary>
        /// </summary>
        public DesignerControl()
        {
            Initialize();
        }

        /// <summary>
        ///     Gets the <see cref="Designer" /> this control belongs to.
        /// </summary>
        [Browsable(false)]
        public IDesignableSurface Designer { get; private set; }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Determines if the designer supports dropping other designers into itself at runtime.")]
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.All)]
        public bool AllowDesignerDrop
        {
            get => designerFlags[AllowDesignerDropFlag];
            set => designerFlags[AllowDesignerDropFlag] = value;
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        public override bool AllowDrop
        {
            get => base.AllowDrop;
            set => base.AllowDrop = value;
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool DrawBorder
        {
            get => designerFlags[DrawBorderFlag];
            set
            {
                designerFlags[DrawBorderFlag] = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        public Color GradientColor1
        {
            get => gradientColor1;
            set
            {
                gradientColor1 = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        public Color GradientColor2
        {
            get => gradientColor2;
            set
            {
                gradientColor2 = value;
                currentGadientColor2 = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                currentBorderColor = value;
                borderColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        public string Value
        {
            get => controlValue;
            set
            {
                controlValue = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool DrawAlternatingBorder
        {
            get => designerFlags[DrawAlternatingBorderFlag];
            set
            {
                designerFlags[DrawAlternatingBorderFlag] = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        public virtual bool DrawShadow
        {
            get => designerFlags[DrawShadowFlag];
            set
            {
                designerFlags[DrawShadowFlag] = value;
                Refresh();
            }
        }

        /// <summary>
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool MouseHotTracking
        {
            get => designerFlags[MouseHotTrackingFlag];
            set => designerFlags[MouseHotTrackingFlag] = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DesignerControl" /> is selected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Selected
        {
            get => designerFlags[SelectedFlag];
            set
            {
                if (designerFlags[SelectedFlag] == value)
                {
                    currentGadientColor2 = ControlPaint.Light(GradientColor2);
                    currentBorderColor = hoverBorderColor;
                }
                else
                {
                    currentGadientColor2 = GradientColor2;
                    currentBorderColor = borderColor;
                }
                Refresh();
            }
        }

        private void Initialize()
        {
            QueryContinueDrag += HandleDesignerControlQueryContinueDrag;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.CacheText |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor
                , true);
            BackColor = Color.Transparent;
            Size = new Size(DefaultControlWidth, DefaultControlHeight);
            ResizeBorderWidth = BackArcSize + 5;
            AllowDrop = true;

            designerFlags[DrawBorderFlag] = true;
            designerFlags[DrawShadowFlag] = true;
            designerFlags[MouseHotTrackingFlag] = true;
        }

        private void HandleDesignerControlQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.Action == DragAction.Drop && !designerFlags[IsOverValidDropTargetFlag])
            {
                Location = MouseDownLocation;
                Visible = true;
            }
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.GiveFeedback"></see> event.
        /// </summary>
        /// <param name="gfbevent">A <see cref="System.Windows.Forms.GiveFeedbackEventArgs"></see> that contains the event data.</param>
        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            designerFlags[IsOverValidDropTargetFlag] = gfbevent.Effect == DragDropEffects.Move;
            base.OnGiveFeedback(gfbevent);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.DragOver" /> event.
        /// </summary>
        /// <param name="drgevent">A <see cref="System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if (!DesignMode)
                HandleOnDragOver(drgevent);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.DragDrop" /> event.
        /// </summary>
        /// <param name="drgevent">A <see cref="System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            if (!DesignMode)
                HandleOnDragDrop(drgevent);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!DesignMode)
                HandleOnMouseDown(e);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!DesignMode)
                HandleOnMouseUp(e);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!DesignMode)
                HandleOnMouseMove(e);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!DesignMode && designerFlags[MouseHotTrackingFlag])
                HandleOnMouseEnter();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && !Selected)
                HandleOnMouseLeave();
            base.OnMouseLeave(e);
        }

        /// <summary>
        ///     Ons the text changed.
        /// </summary>
        /// <param name="e">Exception.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();
            base.OnTextChanged(e);
        }

        private void HandleOnMouseLeave()
        {
            currentBorderColor = borderColor;
            Refresh();
        }

        private void HandleOnMouseEnter()
        {
            currentBorderColor = hoverBorderColor;
            Refresh();
        }

        private void HandleOnMouseMove(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                TryDragDrop();
        }

        private void HandleOnMouseUp(MouseEventArgs e)
        {
            parentRectangle = Rectangle.Empty;
            if ((MouseButtons.Right & e.Button) == MouseButtons.Right)
                OnContextMenuRequested(e);
        }

        private void HandleOnMouseDown(MouseEventArgs e)
        {
            parentRectangle = Parent.ClientRectangle;
            parentRectangle.Inflate(-4, -4);
            offset = new Point(e.X, e.Y);
        }

        private void HandleOnDragOver(DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move &&
                designerFlags[AllowDesignerDropFlag])
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void HandleOnDragDrop(DragEventArgs e)
        {
            DesignerControlWrapper controlWrapper = (DesignerControlWrapper) e.Data.GetData(typeof(DesignerControlWrapper));
            if (controlWrapper != null)
            {
                DesignerControl control = controlWrapper.Control;
                Controls.Add(control);
                control.Location = PointToClient(new Point(e.X - 4, e.Y - 4));
                control.BringToFront();
                control.Click += delegate
                {
                    if (!DesignMode)
                        if (control != null && Designer != null)
                            Designer.SelectedControl = control;
                };
                control.SetDesigner(Designer);
                control.Visible = true;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            PaintControl(e.Graphics);
        }

        private void PaintControl(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            int xr = Convert.ToInt32(Width - (BackMarginWidth + cornerSize.Width));
            int yr = Convert.ToInt32(Height - (BackMarginWidth + cornerSize.Height));

            // Create 4 rectangles for our arcs to fit in.
            Rectangle topLeft = new Rectangle(BackMarginWidth, BackMarginWidth, cornerSize.Width, cornerSize.Height);
            Rectangle topRight = new Rectangle(xr, BackMarginWidth, cornerSize.Width, cornerSize.Height);
            Rectangle bottomLeft = new Rectangle(BackMarginWidth, yr, cornerSize.Width, cornerSize.Height);
            Rectangle bottomRight = new Rectangle(xr, yr, cornerSize.Width, cornerSize.Height);

            using (LinearGradientBrush backgroundBrush = (LinearGradientBrush) CreateBackGroudBrush())
            using (GraphicsPath path = new GraphicsPath())
            {
                PaintShadow(graphics, bottomLeft, topRight, bottomRight, bottomLeft);
                PaintBackground(graphics, path, backgroundBrush, topLeft, topRight, bottomRight, bottomLeft);
                PaintBorders(graphics, path);
            }

            ClipDrawRegion(topLeft, topRight, bottomRight, bottomLeft);
            PaintText(graphics);
        }

        private void PaintShadow(Graphics graphics, Rectangle topLeft, Rectangle topRight, Rectangle bottomRight, Rectangle bottomLeft)
        {
            if (!designerFlags[DrawShadowFlag])
                return;

            using (GraphicsPath path = new GraphicsPath())
            {
                Color parentBackColor;
                DesignerControl parent = Parent as DesignerControl;
                if (parent == null)
                    parentBackColor = Color.LightGray;
                else
                    parentBackColor = Color.FromArgb(90, ControlPaint.Dark(parent.GradientColor2));

                using (SolidBrush borderPen = new SolidBrush(parentBackColor))
                {
                    topLeft.Offset(BorderWidth + 1, BorderWidth + 1);
                    topRight.Offset(BorderWidth + 1, BorderWidth + 1);
                    bottomRight.Offset(BorderWidth + 1, BorderWidth + 1);
                    bottomLeft.Offset(BorderWidth + 1, BorderWidth + 1);

                    path.AddArc(topLeft, CornerTopLeftAngle, SweepAngle);
                    path.AddArc(topRight, CornerTopRightAngle, SweepAngle);
                    path.AddArc(bottomRight, CornerBottomRightAngle, SweepAngle);
                    path.AddArc(bottomLeft, CornerBottomLeftAngle, SweepAngle);

                    graphics.FillPath(borderPen, path);
                }
            }
        }

        private static void PaintBackground(Graphics graphics, GraphicsPath path, Brush backgroundBrush,
            Rectangle topLeft, Rectangle topRight, Rectangle bottomRight, Rectangle bottomLeft)
        {
            path.AddArc(topLeft, CornerTopLeftAngle, SweepAngle);
            path.AddArc(topRight, CornerTopRightAngle, SweepAngle);
            path.AddArc(bottomRight, CornerBottomRightAngle, SweepAngle);
            path.AddArc(bottomLeft, CornerBottomLeftAngle, SweepAngle);
            path.CloseFigure();

            graphics.FillPath(backgroundBrush, path);
        }

        private void PaintBorders(Graphics graphics, GraphicsPath path)
        {
            if (designerFlags[DrawBorderFlag])
                using (Pen borderPen = new Pen(currentBorderColor, BorderWidth))
                {
                    graphics.DrawPath(borderPen, path);
                }

            if (designerFlags[DrawAlternatingBorderFlag])
                using (Pen borderPen = new Pen(Color.White, BorderWidth))
                {
                    borderPen.DashStyle = DashStyle.Dash;
                    graphics.DrawPath(borderPen, path);
                }
        }

        private void PaintText(Graphics graphics)
        {
            using (SolidBrush textBrush = new SolidBrush(ForeColor))
            using (Font textFont = new Font(Font, FontStyle.Bold))
            {
                graphics.DrawString(Text, textFont, textBrush, LabelOffset, LabelOffset);
                graphics.DrawString(Value, Font, textBrush, TextOffsetX, TextOffsetY);
            }
        }

        private void ClipDrawRegion(Rectangle topLeft, Rectangle topRight, Rectangle bottomRight, Rectangle bottomLeft)
        {
            topLeft.Offset(-1, -1);
            topRight.Offset(4, -1);
            bottomRight.Offset(4, 4);
            bottomLeft.Offset(-1, 4);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(topLeft, CornerTopLeftAngle, SweepAngle);
                path.AddArc(topRight, CornerTopRightAngle, SweepAngle);
                path.AddArc(bottomRight, CornerBottomRightAngle, SweepAngle);
                path.AddArc(bottomLeft, CornerBottomLeftAngle, SweepAngle);
                path.CloseFigure();
                Region = new Region(path);
            }
        }

        private Brush CreateBackGroudBrush()
        {
            return new LinearGradientBrush(new RectangleF(0, 0, Width, (float) (Height * GradientHeight)),
                gradientColor1,
                currentGadientColor2,
                CornerBottomLeftAngle,
                true);
        }

        /// <summary>
        ///     Non-public method of setting the control's parent <see cref="Designer" />.
        /// </summary>
        /// <param name="controlsDesigner">The <see cref="Designer" /> this control belongs to.</param>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected internal void SetDesigner(IDesignableSurface controlsDesigner)
        {
            Designer = controlsDesigner;
        }

        private void TryDragDrop()
        {
            Point dragPoint = new Point(Location.X + offset.X, Location.Y + offset.Y);
            if (Designer != null && (MovedOutsideParent(dragPoint) && Parent != Designer || IsValidDropTarget(Parent, dragPoint)))
            {
//                Bitmap bitmap = new Bitmap(this.Width, this.Height);
//                this.DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size));
//                BitmapCursor cursor =
//                    new BitmapCursor(bitmap, Cursor.Current, PointToClient(MousePosition).X, PointToClient(MousePosition).Y);
//                Cursor.Current = cursor;
                Visible = false;
                DoDragDrop(new DesignerControlWrapper(this), DragDropEffects.Move | DragDropEffects.None);
            }
        }

        private bool MovedOutsideParent(Point dragPoint)
        {
            return parentRectangle != Rectangle.Empty && !parentRectangle.Contains(dragPoint);
        }

        private bool IsValidDropTarget(Control startControl, Point dragPoint)
        {
            if (startControl == null && Designer != null)
                startControl = (Control) Designer;
            if (startControl == null)
                return false;

            foreach (Control control in startControl.Controls)
            {
                DesignerControl designerControl = control as DesignerControl;
                if (designerControl != null && designerControl != this && designerControl != Parent)
                    if (new Rectangle(designerControl.Location, designerControl.Size).Contains(dragPoint) &&
                        designerControl.AllowDesignerDrop)
                        return true;
                // Call the method recursively to look for any child controls to drop on.
                IsValidDropTarget(control, dragPoint);
            }
            return false;
        }

        /// <summary>
        ///     Occurs when the mouse pointer is over the control and the right mouse button is released.
        /// </summary>
        public event EventHandler<MouseEventArgs> ContextMenuRequested;

        /// <summary>
        ///     Raises the context menu requested event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="System.Windows.Forms.MouseEventArgs" />
        ///     instance containing the event data.
        /// </param>
        protected virtual void OnContextMenuRequested(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = ContextMenuRequested;
            if (handler != null)
                handler(this, e);
        }
    }
}