#region Using Directives

using System.Web.UI;
using Loom.Collections;

#endregion

namespace Loom.Web.UI.WebControls
{
    public static class ControlRecursiveSearch
    {
        public static Control Find(Control parent, string childId)
        {
            return Search.BreadthFirst(parent, c => c.ID == childId, c => c.Controls);
        }
    }
}