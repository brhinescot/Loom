#region Using Directives

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    [Description("Description")]
    [DefaultEvent("Event")]
    [Docking(DockingBehavior.Ask)]
    [DefaultProperty("Property")]
    public partial class ExpandingListView : Control
    {
        private const int HeaderHeight = 18;

        private static readonly int CtrlPressed = BitVector32.CreateMask();
        private static readonly int Updating = BitVector32.CreateMask(CtrlPressed);

        private BorderStyle borderStyle;

        //private int currrentSelectionIndex = -1;
        private BitVector32 flags = new BitVector32(0);

        private ExpandingListViewGroupCollection groups;
        private int scrollPosition;
        private int totalItemHeight;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListView" /> class.
        /// </summary>
        public ExpandingListView()
            : this(new ExpandingListViewRenderer(new DefaultColorTable())) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListView" /> class.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public ExpandingListView(ExpandingListViewRenderer renderer)
        {
            InitializeComponent();
            Initialize();
            Renderer = renderer;
        }

        /// <summary>
        ///     Gets or sets the renderer.
        /// </summary>
        /// <value>The renderer.</value>
        [Browsable(false)]
        public ExpandingListViewRenderer Renderer { get; set; }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [MergableProperty(false)]
        [Localizable(true)]
        [Description("ListViewItemsDescr")]
        public ExpandingListViewItemCollection Items { get; } = new ExpandingListViewItemCollection(null);

        /// <summary>
        ///     Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [MergableProperty(false)]
        [Localizable(true)]
        [Description("ListViewItemsDescr")]
        public ExpandingListViewGroupCollection Groups => groups ?? (groups = new ExpandingListViewGroupCollection());

        /// <summary>
        ///     Gets or sets the image list.
        /// </summary>
        /// <value>The image list.</value>
        public ImageList ImageList { get; set; }

        /// <summary>
        ///     Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.Fixed3D)]
        [Description("The border style of the control.")]
        public BorderStyle BorderStyle
        {
            get => borderStyle;
            set
            {
                if (borderStyle == value)
                    return;
                Argument.Assert.EnumValueExists(BorderStyle, (int) value, nameof(value));

                borderStyle = value;
                UpdateStyles();
                RecreateHandle();
            }
        }

        /// <summary>
        ///     Gets the required creation parameters when the control handle is
        ///     created.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     A <see cref="System.Windows.Forms.CreateParams"></see> that
        ///     contains the required creation parameters when the handle to the
        ///     control is created.
        /// </returns>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cParams = base.CreateParams;
                cParams.Style &= ~NativeMethods.WS_BORDER;
                cParams.ExStyle &= ~NativeMethods.WS_EX_CLIENTEDGE;
                switch (borderStyle)
                {
                    case BorderStyle.FixedSingle:
                    {
                        cParams.Style |= NativeMethods.WS_BORDER;
                        return cParams;
                    }
                    case BorderStyle.Fixed3D:
                    {
                        cParams.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                        return cParams;
                    }
                }
                return cParams;
            }
        }

        /// <summary>
        /// </summary>
        public void BeginUpdate()
        {
            flags[Updating] = true;
        }

        /// <summary>
        /// </summary>
        public void EndUpdate()
        {
            flags[Updating] = false;
            UpdateTotalItemHeight();
            SetScrollBar();
            Refresh();
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseDown"></see> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="System.Windows.Forms.MouseEventArgs"></see> that
        ///     contains the event data.
        /// </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            bool foundClickItem = false;

            foreach (ExpandingListViewGroup group in Groups)
            {
                if (foundClickItem && flags[CtrlPressed])
                    break;

                foundClickItem = ToggleItemSelection(e.Location, foundClickItem, false, group);

                foreach (ExpandingListViewItem item in group.Items)
                {
                    if (foundClickItem && flags[CtrlPressed])
                        break;

                    foundClickItem = ToggleItemSelection(e.Location, foundClickItem, group.Selected, item);
                }
            }

            Invalidate();
            base.OnMouseDown(e);
        }

        private bool ToggleItemSelection(Point location, bool foundClickItem, bool parentSelected, ExpandingListViewItemBase item)
        {
            if (!foundClickItem && item.RenderBounds.Contains(location))
            {
                foundClickItem = true;
                if (flags[CtrlPressed])
                    item.Selected = !item.Selected;
                else if (!item.Selected)
                    item.Selected = true;
            }
            else if (!flags[CtrlPressed] && !parentSelected)
            {
                item.Selected = false;
            }
            return foundClickItem;
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.MouseWheel"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0)
            {
                if (vScrollBar.Enabled)
                    if (vScrollBar.Value - vScrollBar.LargeChange < 0)
                        vScrollBar.Value = 0;
                    else
                        vScrollBar.Value = (int) Math.Round((double) (vScrollBar.Value - vScrollBar.LargeChange));
            }
            else if (e.Delta < 0)
            {
                if (vScrollBar.Enabled)
                    if (vScrollBar.Value + vScrollBar.LargeChange > vScrollBar.Maximum - vScrollBar.LargeChange)
                        vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange + 1;
                    else
                        vScrollBar.Value = (int) Math.Round((double) (vScrollBar.Value + vScrollBar.LargeChange));
            }
        }

        /// <summary>
        ///     Raises the <see cref="Control.KeyDown"></see> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="KeyEventArgs"></see> that contains
        ///     the event data.
        /// </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            flags[CtrlPressed] = e.Control;
            //shiftPressed = e.Shift;
        }

        /// <summary>
        ///     Raises the <see cref="Control.KeyUp"></see> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="KeyEventArgs"></see> that contains
        ///     the event data.
        /// </param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            flags[CtrlPressed] = e.Control;
            //shiftPressed = e.Shift;
        }

        /// <summary>
        ///     Raises the <see cref="Control.LostFocus"></see> event.
        /// </summary>
        /// <param name="e">
        ///     An <see cref="EventArgs"></see> that
        ///     contains the event data.
        /// </param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        /// <summary>
        ///     Raises the <see cref="Control.GotFocus"></see> event.
        /// </summary>
        /// <param name="e">
        ///     An <see cref="EventArgs"></see> that contains
        ///     the event data.
        /// </param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        /// <summary>
        ///     Raises the <see cref="Control.Resize"></see> event.
        /// </summary>
        /// <param name="e">
        ///     An <see cref="EventArgs"></see> that contains
        ///     the event data.
        /// </param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetScrollBar();
        }

        /// <summary>
        ///     Raises the <see cref="Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="PaintEventArgs"></see> that contains
        ///     the event data.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SetGraphicsQuality(e.Graphics);
            DrawView(e.Graphics);

            if (VisualStyleInformation.IsEnabledByUser)
                RenderVisualStyleHeaders(e);
        }

        private void RenderVisualStyleHeaders(PaintEventArgs e)
        {
            VisualStyleRenderer headerRenderer = new
                VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
            headerRenderer.DrawBackground(
                e.Graphics, new Rectangle(Point.Empty, new Size(300, HeaderHeight)));
            headerRenderer.SetParameters(
                VisualStyleElement.Header.Item.Hot);
            headerRenderer.DrawBackground(
                e.Graphics, new Rectangle(new Point(300, 0), new Size(Width - vScrollBar.Width - 300, HeaderHeight)));
        }

        private void DrawView(Graphics g)
        {
            Rectangle groupBounds =
                new Rectangle(0, scrollPosition + HeaderHeight, Width - vScrollBar.Width, Renderer.GroupHeaderHeight);
            Rectangle itemBounds =
                new Rectangle(1, groupBounds.Bottom + 1, Width - vScrollBar.Width - 3, Renderer.ItemHeight);

            DrawGroupHeaderArgs groupArgs = new DrawGroupHeaderArgs();
            groupArgs.Graphics = g;
            groupArgs.Focused = Focused;

            DrawItemArgs itemArgs = new DrawItemArgs();
            itemArgs.Graphics = g;
            itemArgs.Focused = Focused;

            for (int i = 0; i < Groups.Count; i++)
            {
                ExpandingListViewGroup group =
                    RenderGroup(groupArgs, groupBounds, i);
                for (int j = 0; j < group.Items.Count; j++)
                {
                    RenderItem(group, itemArgs, itemBounds, j);
                    itemBounds.Y += Renderer.ItemHeight + 1;
                }

                groupBounds.Y = itemBounds.Bottom + 1 - Renderer.ItemHeight;
                itemBounds.Y = groupBounds.Bottom + 1;
            }
        }

        private ExpandingListViewGroup RenderGroup(DrawGroupHeaderArgs groupArgs, Rectangle groupBounds, int index)
        {
            ExpandingListViewGroup group = Groups[index];
            if (ClientRectangle.Contains(groupBounds.Location) ||
                ClientRectangle.Contains(groupBounds.Right, groupBounds.Bottom))
            {
                groupArgs.Selected = group.Selected;
                groupArgs.Bounds = groupBounds;
                groupArgs.Group = group;

                Renderer.DrawGroupHeaderBackground(groupArgs);
                Renderer.DrawGroupHeaderBorder(groupArgs);
                Renderer.DrawGroupHeaderText(groupArgs);
                Renderer.DrawGroupHeaderSeparator(groupArgs);

                group.RenderBounds = groupBounds;
            }
            else
            {
                group.RenderBounds = groupBounds;
            }
            return group;
        }

        private void RenderItem(ExpandingListViewGroup group, DrawItemArgs itemArgs, Rectangle itemBounds, int index)
        {
            ExpandingListViewItem item = group.Items[index];
            if (ClientRectangle.Contains(itemBounds.Location) ||
                ClientRectangle.Contains(itemBounds.Right, itemBounds.Bottom))
            {
                itemArgs.Selected = item.Selected;
                itemArgs.Bounds = itemBounds;
                itemArgs.Item = item;

                Renderer.DrawItemBackground(itemArgs);
                Renderer.DrawItemBorder(itemArgs);
                Renderer.DrawItemText(itemArgs);
                Renderer.DrawItemSeparator(itemArgs);

                item.RenderBounds = itemBounds;
            }
            else
            {
                item.RenderBounds = itemBounds;
            }
        }

        private static void SetGraphicsQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.None;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        private void HandleVScrollBarValueChanged(object sender, EventArgs e)
        {
            scrollPosition = -vScrollBar.Value;
            Invalidate();
        }

        private void SetScrollBar()
        {
            vScrollBar.LargeChange = Renderer.ItemHeight * 3 + 3;
            vScrollBar.SmallChange = Renderer.ItemHeight;

            if (ClientRectangle.Height < totalItemHeight + HeaderHeight)
            {
                vScrollBar.Enabled = true;
                vScrollBar.Maximum = totalItemHeight - ClientRectangle.Height + vScrollBar.LargeChange + HeaderHeight;
                int scrollOffset = vScrollBar.Value + vScrollBar.LargeChange - vScrollBar.Maximum;
                int scrollValue = vScrollBar.Maximum - vScrollBar.LargeChange;
                if (scrollOffset > 0 && scrollValue > 0)
                {
                    scrollPosition += scrollOffset;
                    vScrollBar.Value = scrollValue;
                    int clientOffset = scrollPosition + totalItemHeight;
                    if (clientOffset < ClientRectangle.Height)
                        scrollPosition -= clientOffset - (ClientRectangle.Height - HeaderHeight - 1);
                }
            }
            else
            {
                vScrollBar.Enabled = false;
                scrollPosition = 0;
                vScrollBar.Value = vScrollBar.Minimum;
            }
        }

        private void Initialize()
        {
            Groups.Changed += HandleListItemAdded;
            Groups.ItemChanged += HandleListItemAdded;
            Items.Changed += HandleListItemAdded;

            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.CacheText | ControlStyles.Selectable | ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, false);

            vScrollBar.Parent = this;

            BorderStyle = BorderStyle.Fixed3D;
            BackColor = SystemColors.Window;
            ResumeLayout();
        }

        private void HandleListItemAdded(object sender, EventArgs e)
        {
            if (flags[Updating]) return;
            UpdateTotalItemHeight();
            SetScrollBar();
            Refresh();
        }

        private void UpdateTotalItemHeight()
        {
            totalItemHeight = 0;
            foreach (ExpandingListViewGroup group in Groups)
                totalItemHeight += Renderer.GroupHeaderHeight + 2 + group.Items.Count * (Renderer.ItemHeight + 1);
        }

//        private static readonly int shiftPressed = BitVector32.CreateMask(updating);
    }
}