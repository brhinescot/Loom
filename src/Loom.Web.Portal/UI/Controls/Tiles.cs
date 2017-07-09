#region Using Directives

using System.Collections.Generic;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public sealed class Tiles
    {
        private Dictionary<string, List<TileDefinition>> paneLookup;

        public void Add(TileDefinition tile)
        {
            Argument.Assert.IsNotNull(tile, "tile");

            if (Compare.IsNullOrEmpty(tile.LayoutId))
                throw new PortalFatalException("Tiles require the 'LayoutId' property to be initialized before being added to a view.");

            if (paneLookup == null)
                paneLookup = new Dictionary<string, List<TileDefinition>>();

            List<TileDefinition> tileList = null;
            if (paneLookup.ContainsKey(tile.LayoutId))
                tileList = paneLookup[tile.LayoutId];

            if (tileList == null)
            {
                tileList = new List<TileDefinition>();
                paneLookup.Add(tile.LayoutId, tileList);
            }

            tileList.Add(tile);
        }

        internal bool HasBox(string paneId)
        {
            return paneLookup.ContainsKey(paneId);
        }

        internal List<TileDefinition> GetBoxTiles(string layoutId)
        {
            return !paneLookup.ContainsKey(layoutId) ? new List<TileDefinition>(0) : paneLookup[layoutId];
        }
    }
}