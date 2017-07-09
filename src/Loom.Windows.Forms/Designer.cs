#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    [DefaultEvent("SelectionChanged")]
    [DefaultProperty("SelectedControl")]
    public class Designer : ContainerControl, IDesignableSurface
    {
        private Point controlAddLocation;
        private Collection<DesignerControlError> errorCollection;

        private DesignerControl selectedControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Designer" /> class.
        /// </summary>
        public Designer()
        {
            InitializeControl();
        }

        #region IDesignableSurface Members

        /// <summary>
        ///     Occurs when the selected designer control has changed.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the selected designer control has changed.")]
        public event EventHandler<DesignerEventArgs> SelectionChanged;

        /// <summary>
        ///     Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool HasErrors => ControlErrors.Count > 0;

        /// <summary>
        ///     Gets or sets the selected control.
        /// </summary>
        /// <value>The selected control.</value>
        [Browsable(false)]
        public DesignerControl SelectedControl
        {
            get => selectedControl;
            set
            {
                // If the newly selected control is not the previous selected control.
                if (selectedControl == value)
                    return;

                if (selectedControl != null)
                    selectedControl.Selected = false;
                // Set the current selected control to the new control.
                selectedControl = value;
                if (selectedControl != null)
                {
                    // If the current selected control is not null, select it, focus it, and
                    // scroll it into view.
                    selectedControl.Selected = true;
                    selectedControl.Focus();
                    ScrollControlIntoView(selectedControl);
                }
                // Raise the SelectionChanged event to notify listening clients.
                OnSelectionChanged(new DesignerEventArgs(selectedControl));
            }
        }

        /// <summary>
        ///     Gets the control errors.
        /// </summary>
        /// <value>The control errors.</value>
        [Browsable(false)]
        public Collection<DesignerControlError> ControlErrors
        {
            get
            {
                if (errorCollection == null)
                    errorCollection = new Collection<DesignerControlError>();
                return errorCollection;
            }
        }

        /// <summary>
        ///     Adds the control error.
        /// </summary>
        /// <param name="error">The error.</param>
        public void AddControlError(DesignerControlError error)
        {
            ControlErrors.Add(error);
        }

        /// <summary>
        ///     Adds the control error.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="message">The message.</param>
        public void AddControlError(DesignerControl control, string message)
        {
            AddControlError(new DesignerControlError(control, message));
        }

        /// <summary>
        ///     Adds the control error.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        public void AddControlError(DesignerControl control, string message, ErrorSeverity severity)
        {
            AddControlError(new DesignerControlError(control, severity, message));
        }

        #endregion

        private void InitializeControl()
        {
            controlAddLocation = new Point(10, 10);
            SetStyle(
                //ControlStyles.UserPaint | 
                //ControlStyles.AllPaintingInWmPaint | 
                //ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.ContainerControl, true);
            AllowDrop = true;
            BackColor = Color.White;
            Size = new Size(100, 100);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.DragOver"></see> event.
        /// </summary>
        /// <param name="drgevent">
        ///     A <see cref="DragEventArgs"></see>
        ///     that contains the event data.
        /// </param>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if (!DesignMode)
                HandleOnDragOver(drgevent);
        }

        /// <summary>
        ///     Raises the selection changed event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="DesignerEventArgs" /> instance containing
        ///     the event data.
        /// </param>
        protected virtual void OnSelectionChanged(DesignerEventArgs e)
        {
            EventHandler<DesignerEventArgs> handler = SelectionChanged;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.DragDrop"></see> event.
        /// </summary>
        /// <param name="drgevent">
        ///     A <see cref="DragEventArgs"></see>
        ///     that contains the event data.
        /// </param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            if (!DesignMode)
                HandleOnDragDrop(drgevent);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.ControlAdded"></see> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="ControlEventArgs"></see>
        ///     that contains the event data.
        /// </param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (!DesignMode)
                HandleControlAdded(e);
        }

        /// <summary>
        ///     Raises the <see cref="System.Windows.Forms.Control.ControlRemoved"></see> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="ControlEventArgs"></see> that
        ///     contains the event data.
        /// </param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (!DesignMode)
                HandleControlRemoved(e);
        }

        private void HandleOnDragDrop(DragEventArgs e)
        {
            if (!DesignMode)
            {
                DesignerControlWrapper controlWrapper = (DesignerControlWrapper) e.Data.GetData(typeof(DesignerControlWrapper));
                if (controlWrapper != null)
                {
                    DesignerControl control = controlWrapper.Control;
                    Controls.Add(control);
                    control.Location = PointToClient(new Point(e.X - 4, e.Y - 4));
                    control.Visible = true;
                }
            }
        }

        private static void HandleOnDragOver(DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void HandleControlRemoved(ControlEventArgs e)
        {
            if (SelectedControl == e.Control)
                SelectedControl = null;
        }

        private void HandleControlAdded(ControlEventArgs e)
        {
            DesignerControl control = e.Control as DesignerControl;
            if (control == null)
                return;

            SubscribeToControlEvents(control);
            control.SetDesigner(this);
            control.Location = controlAddLocation;
            control.BringToFront();
            control.Focus();
            SelectedControl = control;
            controlAddLocation.Offset(20, 20);
            control.Visible = true;
        }

        private void SubscribeToControlEvents(DesignerControl control)
        {
            control.ControlAdded += delegate(object sender, ControlEventArgs e) { OnControlAdded(e); };

            control.ControlRemoved += delegate(object sender, ControlEventArgs e) { OnControlRemoved(e); };

            control.MouseDown += delegate
            {
                if (control != null)
                    SelectedControl = control;
            };
        }
    }
}