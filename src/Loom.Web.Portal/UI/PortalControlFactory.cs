#region Using Directives

using System;
using System.Diagnostics;
using System.Web.UI;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.UI
{
    [DebuggerStepThrough]
    internal static class PortalControlFactory
    {
        private static readonly InnerLoader Loader = new InnerLoader();

        [DebuggerStepThrough]
        public static Tile LoadTile(TileDefinition tileDefinition)
        {
            return CreateTilePrivate(tileDefinition);
        }

        [DebuggerStepThrough]
        public static PortalMasterView LoadTemplate(string virtualPath)
        {
            return CreateMasterPrivate(virtualPath);
        }

        private static Tile CreateTilePrivate(TileDefinition tileDefinition)
        {
            Control control = Loader.LoadControl(tileDefinition.VirtualPath);
            Tile tile = control as Tile;
            if (tile == null)
            {
                PartialCachingControl pcc = control as PartialCachingControl;
                if (pcc != null)
                    tile = pcc.CachedControl as Tile;
            }
            if (tile == null)
                throw new ArgumentException("The control '" + tileDefinition.VirtualPath + "' is not of type '" + typeof(Tile).Name + "'.", "path");

            tile.LayoutId = tileDefinition.LayoutId;
            return tile;
        }

        private static PortalMasterView CreateMasterPrivate(string virtualPath)
        {
            Control control = Loader.LoadControl(virtualPath);
            PortalMasterView master = control as PortalMasterView;
            if (master == null)
            {
                PartialCachingControl pcc = control as PartialCachingControl;
                if (pcc != null)
                    master = pcc.CachedControl as PortalMasterView;
            }
            if (master == null)
                throw new ArgumentException("The control '" + virtualPath + "' is not of type '" + typeof(PortalMasterView).Name + "'.", "path");

            return master;
        }

        #region Nested type: InnerLoader

        private sealed class InnerLoader : TemplateControl { }

        #endregion
    }
}