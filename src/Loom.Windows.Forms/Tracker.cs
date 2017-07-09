#region Using Directives

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Summary description for Tracker.
    /// </summary>
    [DefaultProperty("Text")]
    [Description("A tracker control.")]
    public partial class Tracker : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Tracker" /> class.
        /// </summary>
        public Tracker() : this(null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tracker" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public Tracker(string text) : this(text, 0) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tracker" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public Tracker(string text, int value)
        {
            InitializeComponent();
            this.text.Text = text == null ? Name : text;
            trackbar.Value = value;
            this.value.Text = value.ToString();
        }

        /// <summary>
        ///     Gets or sets the text of the controls label.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => text.Text;
            set => text.Text = value;
        }

        /// <summary>
        ///     Gets or sets a numeric value that represents the current position of the scroll
        ///     box on the tracker.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The position of the slider on the tracker.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual int Value
        {
            get => trackbar.Value;
            set => trackbar.Value = value;
        }

        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The maximum value for the position of the slider on the tracker.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual int Maximum
        {
            get => trackbar.Maximum;
            set => trackbar.Maximum = value;
        }

        /// <summary>
        ///     Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The minimum value for the position of the slider on the tracker.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual int Minimum
        {
            get => trackbar.Minimum;
            set => trackbar.Minimum = value;
        }

        /// <summary>
        ///     Gets or sets the large change.
        /// </summary>
        /// <value>The large change.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The number of positions the slider moves in response to mouse clicks or the PAGE UP and PAGE DOWN keys.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int LargeChange
        {
            get => trackbar.LargeChange;
            set => trackbar.LargeChange = value;
        }

        /// <summary>
        ///     Gets or sets the small change.
        /// </summary>
        /// <value>The small change.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The number of positions the slider moves in response to mouse dragging or the arrow keys.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SmallChange
        {
            get => trackbar.SmallChange;
            set => trackbar.SmallChange = value;
        }

        /// <summary>
        ///     Gets or sets the tick style.
        /// </summary>
        /// <value>The tick style.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Indicates where the ticks appear on the tracker.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TickStyle TickStyle
        {
            get => trackbar.TickStyle;
            set => trackbar.TickStyle = value;
        }

        /// <summary>
        ///     Gets or sets the tick frequency.
        /// </summary>
        /// <value>The tick frequency.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The number of positions between tick marks.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TickFrequency
        {
            get => trackbar.TickFrequency;
            set => trackbar.TickFrequency = value;
        }

        /// <summary>
        ///     Occurs when the Value property changes,
        ///     either by movement of the scroll box or by manipulation in code.
        /// </summary>
        [Browsable(true)]
        [Category("Action")]
        [Description("Occurs when the Value property changes.")]
        public event EventHandler ValueChanged
        {
            add => trackbar.ValueChanged += value;
            remove => trackbar.ValueChanged += value;
        }

        /// <summary>
        ///     Raises the <see cref="ValueChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnValueChanged(EventArgs e) { }

        private void HandleValueValidated(object sender, EventArgs e)
        {
            SetTrackbarValue();
        }

        private void HandleTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SetTrackbarValue();
        }

        private void SetTrackbarValue()
        {
            if (value.Text.Trim().Length > 0)
            {
                int newValue;
                if (int.TryParse(value.Text, out newValue))
                    if (newValue >= Minimum && newValue <= Maximum)
                        Value = newValue;
            }
        }

        private void HandleTrackbarValueChanged(object sender, EventArgs e)
        {
            value.Text = trackbar.Value.ToString();
            OnValueChanged(e);
        }
    }
}