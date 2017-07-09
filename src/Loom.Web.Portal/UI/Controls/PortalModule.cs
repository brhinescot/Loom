#region Using Directives

using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public class PortalModule : UserControl
    {
        private const string ImageResourcePath = "~/imageresource/";
        private const string ScriptResourcePath = "~/scriptresource/";
        private const string StyleResourcePath = "~/styleresource/";

        private string id;
        private PortalView view;

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

        protected PortalView View
        {
            get
            {
                if (view == null)
                    view = Page as PortalView;

                if (view == null)
                    throw new PortalFatalException("To access the 'View', portal controls must be contained within a 'PortalView' page.");

                return view;
            }
        }

        public dynamic ViewData => View.ViewData;

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
        protected override bool SupportAutoEvents => false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override ViewStateMode ViewStateMode
        {
            get => ViewStateMode.Disabled;
            set => throw new NotSupportedException();
        }

        protected virtual string ResolveImageResource(string name)
        {
            return ResolveUrl(ImageResourcePath + name);
        }

        protected virtual string ResolveStyleResource(string name)
        {
            return ResolveUrl(StyleResourcePath + name);
        }

        protected virtual string ResolveScriptResource(string name)
        {
            return ResolveUrl(ScriptResourcePath + name);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnCommitTransaction(EventArgs e)
        {
            base.OnCommitTransaction(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            return base.OnBubbleEvent(source, args);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnAbortTransaction(EventArgs e)
        {
            base.OnAbortTransaction(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnLoad(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnInit(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnPreRender(EventArgs e) { }

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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override void DataBind() { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void DataBind(bool raiseOnDataBinding) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void DataBindChildren() { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnDataBinding(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnUnload(EventArgs e) { }

        private static string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= b + 1;
            return (i - DateTime.Now.Ticks).ToString("x", CultureInfo.InvariantCulture);
        }
    }
}