#region Using Directives

using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;
using AttributeCollection = System.Web.UI.AttributeCollection;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class PortalControl : Control, IAttributeAccessor
    {
        private AttributeCollection attributes;

        private StateBag attributeState;
        private string id;
        private PortalView view;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AttributeCollection Attributes
        {
            get
            {
                if (attributes == null)
                {
                    if (attributeState == null)
                        attributeState = new StateBag(true);
                    attributes = new AttributeCollection(attributeState);
                }
                return attributes;
            }
        }

        /// <summary>
        ///     Determines if the control should be auto databound
        /// </summary>
        public bool AutoBind { get; set; }

        public sealed override string UniqueID => ID;

        public sealed override string ClientID => ID;

        public sealed override string ID
        {
            get
            {
#if TRACE
                if (id == null && Page != null && Page.TraceEnabled)
                    if (Parent != null)
                        id = Parent.ID + "$trc" + GenerateId();
                    else
                        id = "trc" + GenerateId();
#endif
                return id;
            }
            set { id = Compare.IsNullOrEmpty(value) ? null : value; }
        }

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 

        public CssStyleCollection Style => Attributes.CssStyle;

        protected virtual HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        public string CssClass { get; set; }

        protected PortalView View
        {
            get
            {
                if (view == null)
                    view = Page as PortalView;

                if (view == null)
                    throw new PortalFatalException("To access the 'View' property, portal controls must be contained within a 'PortalView'.");

                return view;
            }
        }

        protected dynamic ViewData => View.ViewData;

        public string ViewDataKey { get; set; }

        protected internal bool HasAttributes => attributes != null && attributes.Count > 0;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override bool EnableViewState
        {
            get => false;
            set => throw new NotSupportedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override Control NamingContainer => null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override StateBag ViewState => null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override ViewStateMode ViewStateMode
        {
            get => ViewStateMode.Disabled;
            set => throw new NotSupportedException();
        }

        #region IAttributeAccessor Members

        string IAttributeAccessor.GetAttribute(string name)
        {
            if (attributeState == null)
                return null;
            return (string) attributeState[name];
        }

        void IAttributeAccessor.SetAttribute(string name, string value)
        {
            Attributes[name] = value;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (!Compare.IsNullOrEmpty(ViewDataKey))
                OnSetViewData(ViewData.GetViewData(ViewDataKey));

            if (AutoBind)
                OnDataBinding(EventArgs.Empty);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderBeginTag(writer);
            base.Render(writer);
            RenderEndTag(writer);
        }

        protected virtual void OnSetViewData(object data) { }

        protected virtual void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!Compare.IsNullOrEmpty(ID))
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ID);
            if (!Compare.IsNullOrEmpty(CssClass))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);

            if (attributeState == null)
                return;

            foreach (string key in Attributes.Keys)
                writer.AddAttribute(key, Attributes[key]);
        }

        protected virtual void RenderBeginTag(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.RenderBeginTag(TagKey);
        }

        protected virtual void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        protected string GetParsedLiteralControlText(object obj)
        {
            LiteralControl literal = obj as LiteralControl;
            if (literal != null)
                return literal.Text;

            DataBoundLiteralControl dbLiteral = obj as DataBoundLiteralControl;
            if (dbLiteral != null)
            {
                dbLiteral.DataBind();
                return dbLiteral.Text;
            }

            throw new HttpException("Cannot have children of type " + obj.GetType().Name.ToString(CultureInfo.InvariantCulture));
        }

        private static string GenerateId()
        {
            long i = 1;
            byte[] array = Guid.NewGuid().ToByteArray();
            for (int j = 0; j < array.Length; j++)
                i *= array[j] + 1;

            return (i - DateTime.Now.Ticks).ToString("x", CultureInfo.InvariantCulture);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnInit(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnPreRender(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnUnload(EventArgs e) { }

        // TODO: Another solution for data binding ?
//        public override sealed void DataBind()
//        {
//            return;
//        }
//
//        protected override sealed void DataBind(bool raiseOnDataBinding)
//        {
//            return;
//        }
//
//        protected override sealed void DataBindChildren()
//        {
//            return;
//        }
//
//        protected override sealed void OnDataBinding(EventArgs e)
//        {
//            return;
//        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override object SaveControlState()
        {
            return null;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override object SaveViewState()
        {
            return null;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void TrackViewState() { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void LoadControlState(object savedState) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void LoadViewState(object savedState) { }
    }
}