#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loom.Web.Resources;
using Loom.Web.UI.Design.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [Designer(typeof(DesignerSurfaceDesigner))]
    [DefaultEvent("DesignerOrderChanged")]
    [ToolboxData("<{0}:DesignerSurface runat=server></{0}:DesignerSurface>")]
    [ParseChildren(false)]
    public class DesignerSurface : CompositeControl
    {
        private const string TransparentDragNameKey = "TransparentDragNameKey";
        private const string BorderColorKey = "BorderColorKey";
        private const string DesignerBorderStyleKey = "DesignerBorderStyleKey";
        private const string DesignerBorderWidthKey = "DesignerBorderWidthKey";
        private const string DesignerBackColorKey = "DesignerBackColorKey";
        private const string DesignerOrder = "__DesignerOrder";
        private const string LeftKey = "LeftKey";
        private const string TopKey = "TopKey";
        private static readonly object designerOrderChangedEventKey = new object();

        protected HiddenField field = new HiddenField();

        [Browsable(true)]
        [Description("The absolute top position of the control.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Unit Top
        {
            get
            {
                object obj = ViewState[TopKey];
                return obj == null ? Unit.Empty : new Unit(obj.ToString());
            }
            set => ViewState[TopKey] = value;
        }

        [Browsable(true)]
        [Description("The absolute left position of the control.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Unit Left
        {
            get
            {
                object obj = ViewState[LeftKey];
                return obj == null ? Unit.Empty : new Unit(obj.ToString());
            }
            set => ViewState[LeftKey] = value;
        }

        [Browsable(true)]
        [Description("Determines if the designer controls contained in this control are transparent while dragging.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public bool TransparentDrag
        {
            get
            {
                object obj = ViewState[TransparentDragNameKey];
                return obj != null && bool.Parse(ViewState[TransparentDragNameKey].ToString());
            }
            set => ViewState[TransparentDragNameKey] = value;
        }

        [Browsable(true)]
        [Description("The border color of the designer controls.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Color DesignerBorderColor
        {
            get
            {
                object obj = ViewState[BorderColorKey];
                return obj == null ? Color.Empty : (Color) obj;
            }
            set => ViewState[BorderColorKey] = value;
        }

        [Browsable(true)]
        [Description("The border color of the designer controls.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Color DesignerBackColor
        {
            get
            {
                object obj = ViewState[DesignerBackColorKey];
                return obj == null ? Color.Empty : (Color) obj;
            }
            set => ViewState[DesignerBackColorKey] = value;
        }

        [Browsable(true)]
        [Description("The width of the designer controls border.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Unit DesignerBorderWidth
        {
            get
            {
                object obj = ViewState[DesignerBorderWidthKey];
                return obj == null ? Unit.Empty : new Unit(obj.ToString());
            }
            set => ViewState[DesignerBorderWidthKey] = value;
        }

        [Browsable(true)]
        [Description("The designer controls border style.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public BorderStyle DesignerBorderStyle
        {
            get
            {
                object obj = ViewState[DesignerBorderStyleKey];
                return obj == null ? BorderStyle.NotSet : (BorderStyle) obj;
            }
            set => ViewState[DesignerBorderStyleKey] = value;
        }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        public event EventHandler<DesignerOrderChangedEventArgs> DesignerOrderChanged
        {
            add => Events.AddHandler(designerOrderChangedEventKey, value);
            remove => Events.RemoveHandler(designerOrderChangedEventKey, value);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            field.ID = DesignerOrder;
        }

        private static void AddScript(Control control)
        {
            foreach (Control subControl in control.Controls)
                AddScript(subControl);

            if (!(control is IPostBackDataHandler))
                return;

            WebControl webControl = control as WebControl;
            if (webControl == null)
                return;

            webControl.Attributes.Add("onClick", "cycleState(this)");
        }

        protected override void OnPreRender(EventArgs e)
        {
            RearrangeControls();

            StringBuilder dragBuilder = new StringBuilder();
            StringBuilder arraybuilder = new StringBuilder();

            // Cache values to deserialize ViewState only once.
            Unit top = Top;
            Unit left = Left;
            Unit width = Width;
            Unit designerBorderWidth = DesignerBorderWidth;
            BorderStyle designerBorderStyle = DesignerBorderStyle;
            Color designerBorderColor = DesignerBorderColor;
            Color designerBackColor = DesignerBackColor;

            for (int i = 0; i < Controls.Count; i++)
            {
                DesignerControl designerControl = Controls[i] as DesignerControl;
                if (designerControl == null)
                    continue;

                designerControl.Width = width;
                Style style = new Style();
                style.BorderColor = designerBorderColor;
                style.BorderWidth = designerBorderWidth;
                style.BorderStyle = designerBorderStyle;
                style.BackColor = designerBackColor;
                designerControl.MergeStyle(style);
                designerControl.Left = left;
                designerControl.Top = top;
                top = new Unit(top.Value + designerControl.Height.Value + 12);

                AddScript(designerControl);

                if (dragBuilder.Length == 0)
                    dragBuilder.Append("'" + designerControl.ClientID + "'+VERTICAL");
                else
                    dragBuilder.Append(",'" + designerControl.ClientID + "'+VERTICAL");

                if (arraybuilder.Length == 0)
                    arraybuilder.Append("dd.elements." + designerControl.ClientID);
                else
                    arraybuilder.Append(",dd.elements." + designerControl.ClientID);
            }

            RegisterClientScript(arraybuilder.ToString(), dragBuilder.ToString());

            base.OnPreRender(e);
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].RenderControl(writer);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(field);
        }

        /// <summary>
        ///     Adds a new <see cref="DesignerControl" /> object to this instance wrapping the specified
        ///     <paramref name="control" />.
        /// </summary>
        /// <param name="control">The <see cref="WebControl" /> to wrap with the new <see cref="DesignerControl" />.</param>
        /// <param name="id">The <see cref="Control.ID" /> assigned to the new <see cref="DesignerControl" />.</param>
        /// <returns>A reference to the new <see cref="DesignerControl" />.</returns>
        /// <exception cref="ArgumentNullException">
        ///     The parameter <paramref name="control" /> or <paramref name="id" /> is null;
        ///     or the parameter <paramref name="id" /> is empty.
        /// </exception>
        /// <param name="headerText"></param>
        public DesignerControl AddDesigner(Control control, string id, string headerText)
        {
            if (control == null)
                throw new ArgumentNullException("control", "The hosted web control can not be null.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "The id for the new designer control can not be null or empty.");

            WebControl webControl = control as WebControl;
            if (webControl == null)
                throw new ArgumentException("Only objects inheriting from System.web.UI.Webcontrols.WebControl may be added to the designer surface.", "control");

            DesignerControl designerControl = new DesignerControl();
            designerControl.ID = id;
            designerControl.Height = new Unit(webControl.Height.Value + 10);
            designerControl.HeaderText = headerText;
            designerControl.Controls.Add(webControl);

            Controls.Add(designerControl);
            return designerControl;
        }

        /// <summary>
        ///     Enumerates a collection of <see cref="DesignerControl" /> objects contained by this <see cref="DesignerSurface" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The <see cref="DesignerControl" /> objects are enumerated in the order they appear on the client.
        ///     </para>
        /// </remarks>
        /// <returns>
        ///     An <see cref="IEnumerable{DesignerControl}" /> representing the <see cref="DesignerControl" /> objects contained
        ///     in this instance.
        /// </returns>
        public IEnumerable<DesignerControl> GetDesignerControls()
        {
            RearrangeControls(true);
            for (int i = 0; i < Controls.Count; i++)
            {
                DesignerControl designerControl = Controls[i] as DesignerControl;
                if (designerControl != null)
                    yield return designerControl;
            }
        }

        protected virtual void OnDesignerOrderChanged(DesignerOrderChangedEventArgs e)
        {
            EventHandler<DesignerOrderChangedEventArgs> handler = (EventHandler<DesignerOrderChangedEventArgs>) Events[designerOrderChangedEventKey];
            if (handler != null)
                handler(this, e);
        }

        private void RearrangeControls()
        {
            RearrangeControls(false);
        }

        private void RearrangeControls(bool suppressEvent)
        {
            List<string> controlIdsInorder = new List<string>(field.Value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries));
            if (!Page.IsPostBack || controlIdsInorder.Count == 0)
                return;

            List<DesignerControl> currentControls = new List<DesignerControl>();

            for (int i = 0; i < controlIdsInorder.Count; i++)
            for (int j = 0; j < Controls.Count; j++)
            {
                DesignerControl designerControl = Controls[j] as DesignerControl;
                if (designerControl == null)
                    continue;

                if (designerControl.ClientID == controlIdsInorder[i])
                    currentControls.Add(designerControl);
            }

            List<int> positions = new List<int>();

            for (int i = 0; i < Controls.Count; i++)
            {
                DesignerControl designerControl = Controls[i] as DesignerControl;
                if (designerControl == null)
                    continue;
                positions.Add(i);
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                DesignerControl designerControl = Controls[i] as DesignerControl;
                if (designerControl == null)
                    continue;
                Controls.RemoveAt(i);
            }

            int index = 0;
            for (int i = 0; i < positions.Count; i++)
                Controls.AddAt(positions[i], currentControls[index++]);

            if (!suppressEvent)
                OnDesignerOrderChanged(new DesignerOrderChangedEventArgs(new DesignerControlCollection(currentControls)));
        }

        private void RegisterClientScript(string arrayControls, string dragControls)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "MoveScript", "SET_DHTML(CURSOR_MOVE," + (TransparentDrag ? "TRANSPARENT," : string.Empty) + dragControls + ");", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "MoveArray", "var dy = 115;var margTop = " + Top.Value + ";var posOld;var aElts = [" + arrayControls + "];", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "DragSnapDrop", Resource.DragDropSnap.Replace("$LEFT$", Left.Value.ToString()).Replace("$SURFACE$", field.ClientID), true);

            Page.ClientScript.RegisterClientScriptResource(GetType(), WebResourcePath.WzDragDrop);
        }
    }
}