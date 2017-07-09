#region Using Directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Loom.Annotations;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Represents a Windows control that allows you to edit a value of any type.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         The <strong>GenericValueEditor</strong> control allows the user to edit
    ///         values of a specific type. Use the <see cref="Value" /> property to access
    ///         the edited value.
    ///     </p>
    ///     <p>
    ///         The type of objects to edit is defined by the
    ///         <see cref="Type" /> property of this control. The
    ///         <strong>GenericValueEditor</strong> uses the <see cref="UITypeEditor" /> and
    ///         <see cref="TypeConverter" /> installed on that type to edit and validate values.
    ///     </p>
    ///     <p>
    ///         When the <see cref="UITypeEditor" /> associated with the edited type has the style
    ///         <strong>DropDown</strong> (see <see cref="UITypeEditorEditStyle" />), then
    ///         this control will display a down arrow button that drops the custom editor.
    ///         When the <see cref="UITypeEditor" /> associated with the edited type has the style
    ///         <strong>Modal</strong>, then this control will display a <strong>...</strong> button
    ///         that opens the modal dialog.
    ///     </p>
    ///     <p>
    ///         When no <see cref="UITypeEditor" /> is associated with the edited type or the
    ///         associated editor is of style <strong>None</strong>, then the behavior of the
    ///         control depends on the edited type. If the type is enumerated, then the control acts
    ///         like a combo box of the enumerated values. If the type is not an enumerated type,
    ///         then the control acts like a text box.
    ///     </p>
    ///     <p>
    ///         If the editor associated with the edited type can display a representation of
    ///         the edited value (see
    ///         <see cref="UITypeEditor.GetPaintValueSupported()">UITypeEditor.GetPaintValueSupported</see>),
    ///         then a small rectangle showing this representation will be displayed in addition to the
    ///         textual value.
    ///     </p>
    /// </remarks>
    /// <example>
    ///     <para lang="C#,Visual Basic">
    ///         The following code sample shows how to create a <strong>GenericValueEditor</strong> for editing
    ///         a <see cref="System.Drawing.Color" /> stucture.
    ///     </para>
    ///     <code lang="C#">
    ///  private GenericValueEditor GetColorEditor(Color startColor) 
    ///  {
    /// 		GenericValueEditor editor = new GenericValueEditor();
    /// 		editor.Type = typeof(Color);
    /// 		editor.Value = startColor;
    /// 		return editor;
    ///  }
    ///  </code>
    ///     <code lang="Visual Basic">
    ///  Private Funtion GetColorEditor(ByVal startColor As Color) as GenericValueEditor
    /// 		Dim editor as GenericValueEditor = New GenericValueEditor()
    /// 		editor.Type = GetType(Color)
    /// 		editor.Value = startColor
    /// 		Return editor
    ///  End Function
    ///  </code>
    /// </example>
    [ToolboxItem(true)]
    [DefaultEvent("ValueChanged")]
    public sealed class GenericEditor : Control
    {
        /// <summary>
        ///     Default width of the paint value rectangle.
        /// </summary>
        internal const int PaintValueWidth = 20;

        private static readonly int HideSelectionFlag = BitVector32.CreateMask();
        private static readonly int ReadOnlyFlag = BitVector32.CreateMask(HideSelectionFlag);
        private static readonly int AutoSizeFlag = BitVector32.CreateMask(ReadOnlyFlag);
        private static readonly int ShowPreviewOnlyFlag = BitVector32.CreateMask(AutoSizeFlag);
        private static readonly int HasStandardValuesFlag = BitVector32.CreateMask(ShowPreviewOnlyFlag);
        private static readonly int PaintValueSupportedFlag = BitVector32.CreateMask(HasStandardValuesFlag);
        private static readonly int HasButtonFlag = BitVector32.CreateMask(PaintValueSupportedFlag);

        private static readonly object ValueChangedEvent = new object();
        private static readonly object BorderStyleChangedEvent = new object();
        private static readonly object TextAlignChangedEvent = new object();
        private static readonly object ReadOnlyChangedEvent = new object();

        /// <summary>
        ///     A button used to drop UI type editors, if any.
        /// </summary>
        private readonly EditorButton editorButton;

        /// <summary>
        ///     The <strong>IWindowsFormsEditorService</strong> that
        ///     allows you to drop UI type editors.
        /// </summary>
        private readonly EditorService editorService;

        /// <summary>
        ///     A control used to paint the current value.
        /// </summary>
        private readonly PreviewControl previewControl;

        /// <summary>
        ///     The text box for editing text.
        /// </summary>
        private readonly TextBox textBox;

        /// <summary>
        ///     The border style. Note that initialization must be done here.
        /// </summary>
        private BorderStyle borderStyle = BorderStyle.Fixed3D;

        /// <summary>
        ///     The type converter for the edited type.
        /// </summary>
        private TypeConverter converter;

        /// <summary>
        ///     Current value of the editor.
        /// </summary>
        private object currentValue;

        /// <summary>
        ///     The editor for the currently edited type.
        /// </summary>
        private UITypeEditor editor;

        private BitVector32 flags = new BitVector32(0);

        /// <summary>
        ///     UITypeEditor for types with standard values.
        /// </summary>
        private StandardValuesUIEditor standardValuesUIEditor;

        /// <summary>
        ///     Edited type.
        /// </summary>
        private Type type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericEditor" /> class.
        /// </summary>
        /// <remarks>The default edited type is <see cref="string" />.</remarks>
        [PublicAPI]
        public GenericEditor() : this(typeof(string))
        {
            Value = string.Empty;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericEditor" /> class using
        ///     the specified type.
        /// </summary>
        /// <param name="editedType">The <see cref="System.Type" /> of object that can be edited by this control.</param>
        [PublicAPI]
        public GenericEditor(Type editedType)
        {
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.FixedHeight, true);
            flags[AutoSizeFlag] = true;

            SuspendLayout();

            // Text box Control
            textBox = new TextBox();
            InitTextBox();

            // editor button
            editorButton = new EditorButton();
            editorButton.Click += ButtonClicked;

            // Paint value box
            previewControl = new PreviewControl(this);
            previewControl.Click += PreviewControlClicked;

            // Add the sub-controls

            Controls.AddRange(new Control[] {previewControl, textBox, editorButton});

            editorService = new EditorService(this);

            Type = editedType;
            ResumeLayout();
        }

        /// <summary>
        ///     Gets or sets the background color of the control.
        /// </summary>
        /// <value>
        ///     A <see cref="System.Drawing.Color" /> that represents the background color of the control.
        ///     The default value is the value for window text (<see cref="SystemColors.Window">SystemColors.Window</see>).
        /// </value>
        [Description("The background color.")]
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get => textBox.BackColor;
            set
            {
                if (BackColor != value)
                {
                    textBox.BackColor = value;
                    Invalidate(true);
                    OnBackColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control automatically adjusts its height to the font height.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the control adjusts its height to closely fit
        ///     its contents; <see langword="false" /> otherwise. The default value is <see langword="true" />.
        /// </value>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Indicating whether the control automatically adjusts its height to the font height.")]
        public override bool AutoSize
        {
            get => flags[AutoSizeFlag];
            set
            {
                if (value != flags[AutoSizeFlag])
                {
                    flags[AutoSizeFlag] = value;
                    AdjustHeight();
                    SetStyle(ControlStyles.FixedHeight, value);
                    OnAutoSizeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the border style of the control.
        /// </summary>
        /// <value>
        ///     One of the <see cref="System.Windows.Forms.BorderStyle" /> values. The default value
        ///     is <see cref="System.Windows.Forms.BorderStyle.Fixed3D" />.
        /// </value>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.Fixed3D)]
        [Description("The border style of the control.")]
        [Localizable(true)]
        [PublicAPI]
        public BorderStyle BorderStyle
        {
            get => borderStyle;
            set
            {
                if (borderStyle != value)
                {
                    borderStyle = value;
                    UpdateStyles();
                    AdjustHeight();
                    LayoutSubControls();
                    Invalidate(true);
                    OnBorderStyleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the way text is aligned in a <see cref="GenericEditor" /> control.
        /// </summary>
        /// <value>
        ///     One of the <see cref="System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies
        ///     how text is aligned in the control. The default value is
        ///     <see cref="System.Windows.Forms.HorizontalAlignment.Left" />.
        /// </value>
        [Category("Appearance")]
        [DefaultValue(HorizontalAlignment.Left)]
        [Description("The alignment of text.")]
        [Localizable(true)]
        [PublicAPI]
        public HorizontalAlignment TextAlign
        {
            get => textBox.TextAlign;
            set
            {
                if (TextAlign != value)
                {
                    textBox.TextAlign = value;
                    OnTextAlignChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether text in the text box is read-only.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the text box is read-only; <see langword="false" /> otherwise. The default value is
        ///     <see langword="false" />.
        /// </value>
        /// <remarks>
        ///     When this property is set to <see langword="true" />, the contents of the control cannot be
        ///     changed by the user at runtime. With this property set to <see langword="true" />, you can still set
        ///     the value of the <see cref="Text" /> property in code. You can use this feature instead of disabling
        ///     the control with the <see cref="Control.Enabled" /> property to allow the contents to be copied.
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Controls whether the text in the control can be changed or not.")]
        [PublicAPI]
        public bool ReadOnly
        {
            get => textBox.ReadOnly;
            set
            {
                if (ReadOnly != value)
                {
                    textBox.ReadOnly = value;
                    previewControl.Enabled = !value;
                    editorButton.Enabled = !value;
                    Invalidate(true);
                    OnReadOnlyChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     This member overrides <see cref="Control.Text">Control.Text</see>.
        /// </summary>
        public override string Text
        {
            get => textBox.Text;
            set
            {
                if (textBox.Text != value)
                    ValidateText(value);
            }
        }

        /// <summary>
        ///     Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>
        ///     A <see cref="System.Drawing.Color" /> that represents the foreground color of the control.
        ///     The default value is the value for window text (<see cref="SystemColors.WindowText">SystemColors.WindowText</see>).
        /// </value>
        [Description("The foreground color.")]
        [DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get => textBox.ForeColor;
            set
            {
                if (ForeColor != value)
                {
                    textBox.ForeColor = value;
                    OnForeColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to show only the rectangle
        ///     that displays a representation of the edited value.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the control shows only the rectangle that displays
        ///     a representation of the edited value; <see langword="false" /> otherwise. The textual value is then not visible.
        /// </value>
        /// <remarks>
        ///     When the editor can paint a representation of the value
        ///     (see <see cref="UITypeEditor.GetPaintValueSupported()">UITypeEditor.GetPaintValueSupported</see>)
        ///     this control will show both a textual value and a rectangle that displays a
        ///     representation of the value.
        ///     Setting this property to <see langword="true" /> will hide the textual value.
        ///     Not all editors can paint a representation of the edited value. If the
        ///     editor cannot paint the edited value, then the value
        ///     of this property is meaningless.
        /// </remarks>
        [DefaultValue(false)]
        [Category("Appearance")]
        [Description("Indicates whether the control only displays the rectangle that previews the value and not the text.")]
        public bool ShowPreviewOnly
        {
            get => flags[ShowPreviewOnlyFlag];
            set
            {
                flags[ShowPreviewOnlyFlag] = value;
                LayoutSubControls();
                Invalidate(true);
            }
        }

        /// <summary>
        ///     Indicates if the UITypeEditor can paint the value.
        /// </summary>
        public bool PaintValueSupported => flags[PaintValueSupportedFlag];

        /// <summary>
        ///     This member overrides <see cref="Control.BackgroundImage">Control.BackgroundImage</see>.
        /// </summary>
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get => base.BackgroundImage;
            set => base.BackgroundImage = value;
        }

        /// <summary>
        ///     This member overrides <see cref="Control.CreateParams">Control.CreateParams</see>.
        /// </summary>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams createParams = base.CreateParams;

                createParams.ClassName = "EDIT";
                createParams.Style |= 0xc0;

                if (!flags[HideSelectionFlag])
                    createParams.Style |= NativeMethods.ES_NOHIDESEL;

                if (flags[ReadOnlyFlag])
                    createParams.Style |= NativeMethods.ES_READONLY;

                createParams.ExStyle &= NativeMethods.DISPID_FORECOLOR;
                createParams.Style &= -8388609;

                switch (borderStyle)
                {
                    case BorderStyle.FixedSingle:
                        createParams.Style |= NativeMethods.WS_BORDER;
                        break;

                    case BorderStyle.Fixed3D:
                        createParams.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                        break;
                }

                return createParams;
            }
        }

        /// <summary>
        ///     This member overrides <see cref="Control.DefaultSize">Control.DefaultSize</see>.
        /// </summary>
        protected override Size DefaultSize => new Size(100, PreferredHeight);

        /// <summary>
        ///     Gets or sets the value edited by the control.
        /// </summary>
        /// <value>The current value of the editor.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get => currentValue;
            set
            {
                if (value != null && !Type.IsInstanceOfType(value))
                    throw new InvalidCastException("GenericValueEditor.Value : Bad value type.");
                currentValue = value;
                UpdateTextBoxWithValue();
                if (flags[PaintValueSupportedFlag])
                    Invalidate(true);
                OnValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Gets or sets the starting point of text selected in the control.
        /// </summary>
        /// <value>The starting position of text selected in the control.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => textBox.SelectionStart;
            set => textBox.SelectionStart = value;
        }

        /// <summary>
        ///     Gets or sets the number of characters selected in the control.
        /// </summary>
        /// <value>The number of characters selected in the control.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => textBox.SelectionLength;
            set => textBox.SelectionLength = value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="System.Type" /> this control can edit.
        /// </summary>
        /// <value>
        ///     A <see cref="System.Type" /> instance that represents the type of object that can be edited
        ///     by the editor.
        /// </value>
        /// <exception cref="ArgumentNullException">
        ///     The property value is
        ///     <see langword="null" />.
        /// </exception>
        /// <remarks>
        ///     Changing this property also changes the <see cref="Value" />,
        ///     <see cref="Converter" />, and <see cref="Editor" /> properties.
        /// </remarks>
        [Browsable(false)]
        [DefaultValue(typeof(string))]
        [PublicAPI]
        public Type Type
        {
            get => type;
            set
            {
                Argument.Assert.IsNotNull(value, "value");

                if (type != value)
                {
                    type = value;
                    converter = TypeDescriptor.GetConverter(type);
                    editor = (UITypeEditor) TypeDescriptor.GetEditor(type, typeof(UITypeEditor));

                    OnConverterOrEditorChanged();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the type converter used by the editor.
        /// </summary>
        /// <value>
        ///     A <see cref="TypeConverter" /> instance that is used to convert the edited value from and
        ///     to text.
        /// </value>
        [Browsable(false)]
        [AmbientValue(null)]
        [PublicAPI]
        public TypeConverter Converter
        {
            get => converter;
            set
            {
                if (converter != value)
                {
                    converter = value;
                    OnConverterOrEditorChanged();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the type editor for this control.
        /// </summary>
        /// <value>A <see cref="UITypeEditor" /> instance that defines the way this control will edit the value.</value>
        /// <remarks>
        ///     <p>
        ///         When the editor has the style <strong>DropDown</strong>
        ///         (see <see cref="UITypeEditorEditStyle" />), then this control will display a
        ///         down-arrow button that drops the custom editor. When the editor has the style
        ///         <strong>Modal</strong>, then this control will display a <strong>...</strong>
        ///         button that opens the modal dialog.
        ///     </p>
        ///     <p>
        ///         When no editor is set or the editor is of style <strong>None</strong>, then
        ///         the behavior of the control depends on the edited type. If the type is enumerated
        ///         then the control acts like a combo box of the enumerated values. If the type is
        ///         not an enumerated type, then the control acts like a text box.
        ///     </p>
        ///     <p>
        ///         If the editor can display a representation of the edited value
        ///         (see <see cref="UITypeEditor.GetPaintValueSupported()">UITypeEditor.GetPaintValueSupported</see>),
        ///         then a small rectangle showing this representation will be displayed in addition
        ///         to the textual value.
        ///     </p>
        /// </remarks>
        [Browsable(false)]
        [AmbientValue(null)]
        public UITypeEditor Editor
        {
            get => editor;
            set
            {
                if (editor != value)
                {
                    editor = value;
                    OnConverterOrEditorChanged();
                }
            }
        }

        /// <summary>
        ///     This member overrides <see cref="Control.Focused">Control.Focused</see>.
        /// </summary>
        public override bool Focused => textBox.Focused;

        private int PreferredHeight
        {
            get
            {
                int preferred = Font.Height;
                if (borderStyle != BorderStyle.None)
                {
                    Size size = SystemInformation.BorderSize;
                    preferred += size.Height * 4 + 3;
                }
                return preferred;
            }
        }

        /// <summary>
        ///     Gets the picture box of the control.
        /// </summary>
        internal PreviewControl PreviewControl => previewControl;

        /// <summary>
        ///     Event fired when the <see cref="Value" /> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the Value property is changed on the control.")]
        public event EventHandler ValueChanged
        {
            add => Events.AddHandler(ValueChangedEvent, value);
            remove => Events.RemoveHandler(ValueChangedEvent, value);
        }

        /// <summary>
        ///     Event fired when the <see cref="BorderStyle" /> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the BorderStyle property is changed on the control.")]
        [PublicAPI]
        public event EventHandler BorderStyleChanged
        {
            add => Events.AddHandler(BorderStyleChangedEvent, value);
            remove => Events.RemoveHandler(BorderStyleChangedEvent, value);
        }

        /// <summary>
        ///     Event fired when the <see cref="TextAlign" /> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the TextAlign property is changed on the control.")]
        [PublicAPI]
        public event EventHandler TextAlignChanged
        {
            add => Events.AddHandler(TextAlignChangedEvent, value);
            remove => Events.RemoveHandler(TextAlignChangedEvent, value);
        }

        /// <summary>
        ///     Event fired when the <see cref="ReadOnly" /> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the ReadOnly property is changed on the control.")]
        [PublicAPI]
        public event EventHandler ReadOnlyChanged
        {
            add => Events.AddHandler(ReadOnlyChangedEvent, value);
            remove => Events.RemoveHandler(ReadOnlyChangedEvent, value);
        }

        /// <summary>
        ///     Initializes the text box .
        /// </summary>
        private void InitTextBox()
        {
            textBox.AcceptsReturn = false;
            textBox.AcceptsTab = false;
            textBox.AutoSize = false;
            textBox.CausesValidation = false;
            textBox.BorderStyle = BorderStyle.None;

            textBox.KeyDown += TextBoxKeyDown;
            textBox.KeyUp += TextBoxKeyUp;
            textBox.KeyPress += TextBoxKeyPress;
            textBox.TextChanged += TextBoxTextChanged;
            textBox.Validating += TextBoxValidating;
            textBox.Validated += TextBoxValidated;
            textBox.GotFocus += TextBoxGotFocus;
            textBox.LostFocus += TextBoxLostFocus;
        }

        /// <summary>
        ///     Resets the <see cref="ForeColor" /> property to its default value.
        /// </summary>
        public override void ResetForeColor()
        {
            ForeColor = SystemColors.WindowText;
        }

        /// <summary>
        ///     Resets the <see cref="BackColor" /> property to its default value.
        /// </summary>
        public override void ResetBackColor()
        {
            BackColor = SystemColors.Window;
        }

        private void OnConverterOrEditorChanged()
        {
            flags[PaintValueSupportedFlag] = editor != null && editor.GetPaintValueSupported();
            flags[HasStandardValuesFlag] = converter != null &&
                                           converter.GetStandardValuesSupported() &&
                                           converter.GetStandardValues().Count != 0;
            flags[HasButtonFlag] = editor != null &&
                                   editor.GetEditStyle() != UITypeEditorEditStyle.None
                                   || flags[HasStandardValuesFlag];

            editorButton.IsDialog = editor != null && editor.GetEditStyle() == UITypeEditorEditStyle.Modal;
            LayoutSubControls();
            UpdateTextBoxWithValue();
        }

        /// <summary>
        ///     Invoked when the <see cref="BorderStyle" /> property is changed on the control.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        /// <remarks>Called when the <strong>BorderStyle</strong> property is changed.</remarks>
        private void OnBorderStyleChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) Events[BorderStyleChangedEvent];
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Invoked when the <see cref="Value" /> property is changed on the control.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs" /> that contains the event data.</param>
        /// <remarks>Called when the <strong>Value</strong> property is changed.</remarks>
        private void OnValueChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) Events[ValueChangedEvent];
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Invoked when the <see cref="ReadOnly" /> property is changed on the control.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        /// <remarks>Called when the <strong>ReadOnly</strong> property is changed.</remarks>
        private void OnReadOnlyChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) Events[ReadOnlyChangedEvent];
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Invoked when the <see cref="TextAlign" /> property is changed on the control.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        /// <remarks>Called when the <strong>TextAlign</strong> property is changed.</remarks>
        private void OnTextAlignChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) Events[TextAlignChangedEvent];
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     This members overrides <see cref="Control.OnSystemColorsChanged">Control.OnSystemColorsChanged</see>.
        /// </summary>
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            // Must delegate to the editors....
            if (editorService != null)
                editorService.SystemColorsChanged();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnCursorChanged">Control.OnCursorChanged</see>.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnCursorChanged(EventArgs args)
        {
            base.OnCursorChanged(args);
            textBox.Cursor = Cursor;
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnFontChanged">Control.OnFontChanged</see>.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AdjustHeight();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnHandleCreated">Control.OnHandleCreated</see>.
        /// </summary>
        protected override void OnHandleCreated(EventArgs args)
        {
            base.OnHandleCreated(args);
            AdjustHeight();
            LayoutSubControls();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnValidating">Control.OnValidating</see>.
        /// </summary>
        protected override void OnValidating(CancelEventArgs e)
        {
            editorService.HideForm();
            base.OnValidating(e);
            if (!ValidateText())
                e.Cancel = true;
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnLeave">Control.OnLeave</see>.
        /// </summary>
        protected override void OnLeave(EventArgs e)
        {
            editorService.HideForm();
            base.OnLeave(e);
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnGotFocus">Control.OnGotFocus</see>.
        /// </summary>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            textBox.Focus();
            Invalidate(true);
        }

        /// <summary>
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            using (Brush brush = new SolidBrush(Enabled ? BackColor : SystemColors.Control))
            {
                pevent.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        /// <summary>
        ///     This member overrides <see cref="Control.SetBoundsCore">Control.SetBoundsCore</see>.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (flags[AutoSizeFlag] && height != Height)
                height = PreferredHeight;
            base.SetBoundsCore(x, y, width, height, specified);
            LayoutSubControls();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (!ReadOnly && !IsTextEditable())
                DropEditor();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnEnabledChanged">Control.OnEnabledChanged</see>.
        /// </summary>
        protected override void OnEnabledChanged(EventArgs args)
        {
            base.OnEnabledChanged(args);
            textBox.Enabled = Enabled;
        }

        private void AdjustHeight()
        {
            if (flags[AutoSizeFlag])
                Height = PreferredHeight;
        }

        /// <summary>
        ///     Invoked when clicking the picture box.
        /// </summary>
        private void PreviewControlClicked(object sender, EventArgs args)
        {
            Focus();
            if (!IsTextEditable())
                DropEditor();
        }

        /// <summary>
        ///     Invoked when clicking the drop button.
        /// </summary>
        private void ButtonClicked(object sender, EventArgs args)
        {
            DropEditor();
        }

        private void LayoutSubControls()
        {
            Rectangle cRect = ClientRectangle;
            int buttonWidth = flags[HasButtonFlag] ? SystemInformation.VerticalScrollBarWidth : 0;

            previewControl.Visible = flags[PaintValueSupportedFlag];
            editorButton.Visible = flags[HasButtonFlag];

            if (flags[PaintValueSupportedFlag])
                previewControl.SetBounds(cRect.X + 1, cRect.Y + 1,
                    ShowPreviewOnly
                        ? Math.Max(0, cRect.Width - buttonWidth - 2)
                        : Math.Min(PaintValueWidth,
                            Math.Max(0, cRect.Width - buttonWidth - 2)),
                    Math.Max(0, cRect.Height - 2));

            if (flags[HasButtonFlag])
                editorButton.SetBounds(cRect.Right - buttonWidth,
                    cRect.Y, buttonWidth, cRect.Height);

            if (!(ShowPreviewOnly && flags[PaintValueSupportedFlag]))
            {
                int leftMargin = flags[PaintValueSupportedFlag] ? PaintValueWidth + 5 : 1;
                int topMargin = 0;
                switch (BorderStyle)
                {
                    case BorderStyle.Fixed3D:
                        topMargin = 1;
                        break;
                    case BorderStyle.FixedSingle:
                        topMargin = 2;
                        break;
                }
                textBox.SetBounds(cRect.X + leftMargin,
                    cRect.Y + topMargin,
                    Math.Max(0, cRect.Width - buttonWidth - leftMargin),
                    Math.Max(0, cRect.Height));
            }
            else
            {
                textBox.Width = 0;
            }
        }

        internal string GetValueAsText(object value)
        {
            if (value == null)
                return string.Empty;

            string valueAsText = value as string;
            if (valueAsText != null)
                return valueAsText;

            try
            {
                if (converter != null && converter.CanConvertTo(typeof(string)))
                    return converter.ConvertToString(value);
            }
            catch (FormatException) { }
            catch (ArgumentException) { }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    throw;

                if (!(converter is ColorConverter))
                    throw;

                if (!(ex.InnerException is FormatException))
                    throw;
            }
            return value.ToString();
        }

        /// <summary>
        ///     Gets the list of standard values from the converter.
        /// </summary>
        /// <returns></returns>
        internal object[] GetStandardValues()
        {
            object[] values = null;

            TypeConverter typeConverter = Converter;
            if (typeConverter.GetStandardValuesSupported())
            {
                ICollection standard = typeConverter.GetStandardValues();
                values = new object[standard.Count];
                standard.CopyTo(values, 0);
            }
            return values;
        }

        /// <summary>
        ///     Drops the <see cref="UITypeEditor" /> associated with the edited value.
        /// </summary>
        /// <remarks>
        ///     The method may also drop a list box if the edited value does not
        ///     have any editor and the type proposes standard values.
        /// </remarks>
        private void DropEditor()
        {
            UITypeEditor typeEditor = Editor;

            if ((typeEditor == null ||
                 typeEditor.GetEditStyle() == UITypeEditorEditStyle.None) &&
                flags[HasStandardValuesFlag])
            {
                if (standardValuesUIEditor == null)
                    standardValuesUIEditor = new StandardValuesUIEditor(this);
                typeEditor = standardValuesUIEditor;
            }

            if (typeEditor != null)
            {
                if (currentValue is string)
                    currentValue = textBox.Text;
                object result = typeEditor.EditValue(editorService, currentValue);
                Value = result;
            }
        }

        private void SelectTextBox()
        {
            textBox.SelectAll();
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 0;
        }

        private bool IsTextEditable()
        {
            if (flags[ShowPreviewOnlyFlag] && flags[PaintValueSupportedFlag])
                return false;

            TypeConverter typeConverter = Converter;
            if (typeConverter != null)
                return !typeConverter.GetStandardValuesSupported() || !typeConverter.GetStandardValuesExclusive();
            return false;
        }

        private void TextBoxValidating(object sender, CancelEventArgs e)
        {
            OnValidating(e);
        }

        private void TextBoxValidated(object sender, EventArgs e)
        {
            OnValidated(e);
        }

        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs ke)
        {
            OnKeyPress(ke);
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs ke)
        {
            OnKeyDown(ke);
        }

        private void TextBoxKeyUp(object sender, KeyEventArgs ke)
        {
            OnKeyUp(ke);
        }

        private void TextBoxLostFocus(object sender, EventArgs e)
        {
            Invalidate(true);
        }

        private void TextBoxGotFocus(object sender, EventArgs e)
        {
            Invalidate(true);
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnKeyPress">Control.OnKeyPress</see>.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs ke)
        {
            if (!IsTextEditable())
                ke.Handled = true;
            else if (ke.KeyChar == (char) 13 || ke.KeyChar == (char) 27)
                ke.Handled = true; // avoid beep done by TextBox when
            base.OnKeyPress(ke);
        }

        private void SelectStandardValue(bool next)
        {
            if (!flags[HasStandardValuesFlag])
                return;
            object[] values = GetStandardValues();
            int validation = next
                ? values.Length - 1
                : 0;
            for (int i = 0; i < values.Length; i++)
                if (values[i].Equals(currentValue))
                {
                    if (next)
                    {
                        if (i == 0)
                            return;
                        validation = i - 1;
                    }
                    else
                    {
                        if (i == values.Length - 1)
                            return;
                        validation = i + 1;
                    }
                    break;
                }
            editorService.CloseDropDown();
            Value = values[validation];
            SelectTextBox();
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnMouseWheel">Control.OnMouseWheel</see>.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs" /> that contains the data.</param>
        /// <remarks>
        ///     The default implementation iterates on the standard values proposed by
        ///     the edited type, if any.
        /// </remarks>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (textBox.Focused && !ReadOnly)
                if (flags[HasStandardValuesFlag])
                    SelectStandardValue(e.Delta > 0);
            base.OnMouseWheel(e);
        }

        /// <summary>
        ///     This member overrides <see cref="Control.OnKeyDown">Control.OnKeyDown</see>.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs ke)
        {
            if (!ReadOnly)
            {
                bool alt = ke.Alt;
                if (!alt && ke.KeyCode == Keys.Down || ke.KeyCode == Keys.Up)
                    if (flags[HasStandardValuesFlag])
                    {
                        SelectStandardValue(ke.KeyCode == Keys.Down);
                        SelectTextBox();
                    }
                if (alt && ke.KeyCode == Keys.Down && flags[HasButtonFlag])
                {
                    ke.Handled = true;
                    DropEditor();
                }
                else if (ke.KeyCode == Keys.Enter)
                {
                    ke.Handled = true;
                    ValidateText();
                }
                else if (ke.KeyCode == Keys.Escape)
                {
                    // ??
                }
            }
            base.OnKeyDown(ke);
        }

        private void UpdateTextBoxWithValue()
        {
            textBox.Text = GetValueAsText(currentValue);
        }

        /// <summary>
        ///     Is called to validate the text that is currently edited by the control.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the string has been successfully converted into
        ///     the type defined by the property <see cref="Type" />; <see langword="false" /> otherwise.
        /// </returns>
        private bool ValidateText()
        {
            if (!ValidateText(textBox.Text))
            {
                UpdateTextBoxWithValue();
                return false;
            }
            return true;
        }

        private bool ValidateText(string text)
        {
            object value = null;
            try
            {
                if (converter != null && converter.CanConvertFrom(typeof(string)))
                    value = converter.ConvertFromString(null, CultureInfo.CurrentCulture, text);
            }
            catch (FormatException) { }
            catch (ArgumentException) { }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    throw;

                if (!(converter is ColorConverter))
                    throw;

                if (!(ex.InnerException is FormatException))
                    throw;
            }
            if (value == null)
                return false;

            editorService.CloseDropDown();
            Value = value;

            return true;
        }

        #region Nested type: EditorService

        /// <summary>
        ///     The <strong>IWindowsFormsEditorService</strong> that allows you to
        ///     drop dialog and UI type editors for a <see cref="GenericEditor" />.
        /// </summary>
        private class EditorService : IServiceProvider, IWindowsFormsEditorService
        {
            /// <summary>
            ///     The control that uses this service.
            /// </summary>
            private readonly GenericEditor editor;

            /// <summary>
            ///     Indicates whether we are currently closing the drop-down form.
            /// </summary>
            private bool closingDropDown;

            /// <summary>
            ///     A control that holds the dropped editors.
            /// </summary>
            private DropDownForm dropDownForm;

            /// <summary>
            ///     Creates the editor service.
            /// </summary>
            /// <param name="editor">The cell editor.</param>
            public EditorService(GenericEditor editor)
            {
                this.editor = editor;
            }

            #region IServiceProvider Members

            /// <summary>
            ///     Gets the service object of the specified type.
            /// </summary>
            /// <param name="serviceType">An object that specifies the type of service object to get.</param>
            /// <returns>A service object of type <paramref name="serviceType" />.</returns>
            public object GetService(Type serviceType)
            {
                return serviceType == typeof(IWindowsFormsEditorService) ? this : null;
            }

            #endregion

            #region IWindowsFormsEditorService Members

            /// <summary>
            ///     Drops the editor control.
            /// </summary>
            /// <param name="ctl">The control to drop.</param>
            public void DropDownControl(Control ctl)
            {
                if (dropDownForm == null)
                    dropDownForm = new DropDownForm(this);

                dropDownForm.Visible = false;
                dropDownForm.Component = ctl;

                Rectangle editorBounds = editor.Bounds;

                Size size = dropDownForm.Size;

                // location of the form
                Point location
                    = new Point(editorBounds.Right - size.Width,
                        editorBounds.Bottom + 1);
                // location in screen coordinate
                location = editor.Parent.PointToScreen(location);

                // check the form is in the screen working area
                Rectangle screenWorkingArea = Screen.FromControl(editor).WorkingArea;

                location.X = Math.Min(screenWorkingArea.Right - size.Width,
                    Math.Max(screenWorkingArea.X, location.X));

                if (size.Height + location.Y + editor.textBox.Height > screenWorkingArea.Bottom)
                    location.Y = location.Y - size.Height - editorBounds.Height - 1;

                dropDownForm.SetBounds(location.X, location.Y, size.Width, size.Height);
                dropDownForm.Visible = true;
                ctl.Focus();

                editor.SelectTextBox();
                // wait for the end of the editing

                while (dropDownForm.Visible)
                {
                    Application.DoEvents();
                    NativeMethods.MsgWaitForMultipleObjects(0, 0, true, 250, 255);
                }

                // editing is done or aborted
            }

            /// <summary>
            ///     Closes the dropped editor.
            /// </summary>
            public void CloseDropDown()
            {
                if (closingDropDown)
                    return;
                try
                {
                    closingDropDown = true;
                    if (dropDownForm != null && dropDownForm.Visible)
                    {
                        dropDownForm.Component = null;
                        dropDownForm.Visible = false;

                        if (editor.textBox.Visible)
                            editor.textBox.Focus();
                    }
                }
                finally
                {
                    closingDropDown = false;
                }
            }

            /// <summary>
            ///     Opens a dialog editor.
            /// </summary>
            /// <param name="dialog">The dialog to open.</param>
            public DialogResult ShowDialog(Form dialog)
            {
                dialog.ShowDialog(editor);
                return dialog.DialogResult;
            }

            #endregion

            /// <summary>
            ///     Hides the drop-down editor.
            /// </summary>
            public void HideForm()
            {
                if (dropDownForm != null && dropDownForm.Visible)
                    dropDownForm.Visible = false;
            }

            /// <summary>
            ///     Is Called when the SystemColorsChanged event is received
            ///     by the GenericValueEditor.
            /// </summary>
            public void SystemColorsChanged()
            {
                if (dropDownForm != null)
                    dropDownForm.SystemColorChanged();
            }
        }

        #endregion
    }
}