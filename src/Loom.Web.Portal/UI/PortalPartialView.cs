#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.UI
{
    public class PortalPartialView : Page
    {
        private IPortalContext portalContext;
        private Tiles tileList;
        private dynamic viewData;

        public override string ClientID => ID;

        public IPortalContext Portal
        {
            get
            {
                if (portalContext != null)
                    return portalContext;

                portalContext = PortalContext.Current;
                if (portalContext == null)
                    throw new HttpException("The portal context has not been initialized.");

                return portalContext;
            }
            private set => portalContext = value;
        }

        public IPortalRequest PortalRequest => Portal.Request;

        public IPortalResponse PortalResponse => Portal.Response;

        public override string UniqueID => ID;

        public dynamic ViewData
        {
            get => viewData ?? (viewData = new ViewData());
            internal set => viewData = value;
        }

        internal Tiles Tiles => tileList ?? (tileList = new Tiles());

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override void OnLoad(EventArgs e)
        {
            Collection<TileDefinition> tileDefinitions = PortalContext.Current.Response.Tiles;
            for (int i = 0; i < tileDefinitions.Count; i++)
                Tiles.Add(tileDefinitions[i]);

            if (tileList != null)
                LoadTiles();
        }

        internal void InitializeContext(IPortalContext context)
        {
            Portal = context;
        }

        internal virtual void RenderView(HtmlTextWriter writer)
        {
            Render(writer);
        }

        private void LoadTiles()
        {
            PortalTrace.Write("PortalView", "LoadTiles", "Begin LoadTiles");

            List<Box> boxes = this.FindBoxes();
            for (int index = 0; index < boxes.Count; index++)
            {
                Box box = boxes[index];
                if (!tileList.HasBox(box.ID))
                    continue;

                List<TileDefinition> modules = tileList.GetBoxTiles(box.ID);
                for (int i = 0; i < modules.Count; i++)
                    box.Controls.Add(CreateTile(modules[i]));
            }

            PortalTrace.Write("PortalView", "LoadTiles", "End LoadTiles");
        }

        private Tile CreateTile(TileDefinition tileDefinition)
        {
            Control control = LoadControl(tileDefinition.VirtualPath);

            Tile tile = control as Tile;
            if (tile == null)
            {
                PartialCachingControl pcc = control as PartialCachingControl;
                if (pcc != null)
                    tile = pcc.CachedControl as Tile;
            }
            if (tile == null)
                throw new ArgumentException("The control '" + tileDefinition.VirtualPath + "' is not of type '" + typeof(Tile).Name + "'.", "tileDefinition");

            tile.LayoutId = tileDefinition.LayoutId;
            tile.Settings = tileDefinition.Settings;
            tile.Data = tileDefinition.Data;
            return tile;
        }

        // ReSharper disable ValueParameterNotUsed

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PageAdapter PageAdapter => null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool MaintainScrollPositionOnPostBack
        {
            get => false;
            set => throw new NotSupportedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool EnableViewStateMac
        {
            get => false;
            set => throw new NotImplementedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Control AutoPostBackControl
        {
            get => null;
            set => throw new NotImplementedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ErrorPage
        {
            get => null;
            set => throw new NotImplementedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int MaxPageStateFieldLength
        {
            get => -1;
            set => throw new NotImplementedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete("Use of this property is not recommended because it is no longer useful. http://go.microsoft.com/fwlink/?linkid=14202")]
        protected override int AutoHandlers
        {
            get => base.AutoHandlers;
            set => base.AutoHandlers = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override bool EnableViewState
        {
            get => false;
            set => throw new NotSupportedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override bool EnableEventValidation
        {
            get => false;
            set => throw new NotSupportedException();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public sealed override Control NamingContainer => null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override PageStatePersister PageStatePersister => null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override bool SupportAutoEvents => false;

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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new void ExecuteRegisteredAsyncTasks() { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override NameValueCollection DeterminePostBackMode()
        {
            return null;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override ControlAdapter ResolveAdapter()
        {
            return null;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnError(EventArgs e)
        {
            base.OnError(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnCommitTransaction(EventArgs e)
        {
            base.OnCommitTransaction(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override bool OnBubbleEvent(object source, EventArgs args)
        {
            return base.OnBubbleEvent(source, args);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnAbortTransaction(EventArgs e)
        {
            base.OnAbortTransaction(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnPreInit(EventArgs e)
        {
            base.EnableViewStateMac = false;
            base.MaintainScrollPositionOnPostBack = false;
            base.AutoPostBackControl = null;
            base.ErrorPage = null;
            base.MaxPageStateFieldLength = -1;

            EnsureChildControls();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnInit(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override object LoadPageStateFromPersistenceMedium()
        {
            return null;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void SavePageStateToPersistenceMedium(object state) { }

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
        public sealed override void DataBind() { }

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
        protected sealed override void DataBind(bool raiseOnDataBinding) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void DataBindChildren() { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnDataBinding(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnInitComplete(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnLoadComplete(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnPreLoad(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnUnload(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnPreRenderComplete(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void OnSaveStateComplete(EventArgs e) { }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected sealed override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument) { }

        // ReSharper restore ValueParameterNotUsed
    }
}