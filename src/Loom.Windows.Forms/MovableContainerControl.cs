#region Using Directives

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Represents a base class that allows its inheritors to be resized and dragged around the form at runtime.
    /// </summary>
    public class MovableContainerControl : ContainerControl
    {
        private ResizeRegion clickedResizeRegion;
        private Point mouseDownPoint;
        private Rectangle mouseDownRect;

        private Point offset;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MovableContainerControl" /> class.
        /// </summary>
        public MovableContainerControl()
        {
            SetStyle(ControlStyles.Selectable | ControlStyles.ResizeRedraw | ControlStyles.ContainerControl, true);
        }

        /// <summary>
        ///     Gets or sets the width of the area around the control which will be used as a resize handle.
        /// </summary>
        protected virtual int ResizeBorderWidth { get; set; } = 10;

        /// <summary>
        ///     Gets or sets a value indicating whether [bring to front on selected].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [bring to front on selected]; otherwise, <see langword="false" />.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("")]
        [DefaultValue(true)]
        public bool BringToFrontOnSelected { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether [allow resize].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [allow resize]; otherwise, <see langword="false" />.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("")]
        [DefaultValue(true)]
        public bool AllowResize { get; set; } = true;

        /// <summary>
        ///     Gets the mouse down location.
        /// </summary>
        /// <value>The mouse down location.</value>
        [Browsable(false)]
        public Point MouseDownLocation { get; private set; }

        /// <summary>
        ///     Raises the <see cref="Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            HandleMouseDown();
        }

        /// <summary>
        ///     Raises the <see cref="Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            HandleMouseMove(e);
        }

        /// <summary>
        ///     Raises the <see cref="Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            clickedResizeRegion = ResizeRegion.None;
        }

        /// <summary>
        ///     Initializes tracking for control moving and resizing.
        /// </summary>
        private void HandleMouseDown()
        {
            mouseDownPoint = MousePosition;
            clickedResizeRegion = GetResizeRegion(mouseDownPoint);
            if (AllowResize && clickedResizeRegion != ResizeRegion.None)
            {
                SetResizeCursor(clickedResizeRegion);
            }
            else if (Dock == DockStyle.None)
            {
                MouseDownLocation = Location;
                offset = new Point(mouseDownPoint.X - MouseDownLocation.X, mouseDownPoint.Y - MouseDownLocation.Y);
            }

            mouseDownRect = ClientRectangle;
            if (BringToFrontOnSelected)
            {
                Focus();
                BringToFront();
            }
        }

        private void HandleMouseMove(MouseEventArgs e)
        {
            bool mouseIsDownInResizeArea = clickedResizeRegion != ResizeRegion.None;

            if (AllowResize && mouseIsDownInResizeArea)
            {
                HandleResize();
            }
            else
            {
                ResizeRegion newRegion = GetResizeRegion(MousePosition);
                bool mouseDownDuringMove = e.Button == MouseButtons.Left;
                bool mouseIsOverResizeArea = newRegion != ResizeRegion.None && e.Button != MouseButtons.Left;

                if (Dock == DockStyle.None && mouseDownDuringMove)
                    Location = CalculateNewLocation();

                if (AllowResize && mouseIsOverResizeArea)
                    SetResizeCursor(newRegion);
                else if (AllowResize && !mouseIsDownInResizeArea)
                    Cursor = Cursors.Default;
            }
        }

        private void HandleResize()
        {
            int diffX = MousePosition.X - mouseDownPoint.X;
            int diffY = MousePosition.Y - mouseDownPoint.Y;

            switch (clickedResizeRegion)
            {
                case ResizeRegion.E:
                    Width = mouseDownRect.Width + diffX;
                    break;
                case ResizeRegion.S:
                    Height = mouseDownRect.Height + diffY;
                    break;
                case ResizeRegion.SE:
                    Width = mouseDownRect.Width + diffX;
                    Height = mouseDownRect.Height + diffY;
                    break;
            }
        }

        private Point CalculateNewLocation()
        {
            Point location = MousePosition;
            location.Offset(-offset.X, -offset.Y);

            return location;
        }

        private ResizeRegion GetResizeRegion(Point mousePosition)
        {
            Point clientCursorPos = PointToClient(mousePosition);
            if ((clientCursorPos.X >= Width - ResizeBorderWidth) & (clientCursorPos.Y >= Height - ResizeBorderWidth))
                return ResizeRegion.SE;

            if (clientCursorPos.X >= Width - ResizeBorderWidth)
                return ResizeRegion.E;

            return clientCursorPos.Y >= Height - ResizeBorderWidth ? ResizeRegion.S : ResizeRegion.None;
        }

        private void SetResizeCursor(ResizeRegion region)
        {
            switch (region)
            {
                case ResizeRegion.N:
                case ResizeRegion.S:
                    Cursor = Cursors.SizeNS;
                    break;
                case ResizeRegion.E:
                    Cursor = Cursors.SizeWE;
                    break;
                case ResizeRegion.W:
                case ResizeRegion.NW:
                case ResizeRegion.SE:
                    Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    Cursor = Cursors.Default;
                    break;
            }
        }

        #region Nested type: ResizeRegion

        private enum ResizeRegion
        {
            None,
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }

        #endregion
    }
}