#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Loom.Web.UI.WebControls
{
    public class DesignerControlCollection : Collection<DesignerControl>
    {
        public DesignerControlCollection() { }
        public DesignerControlCollection(IList<DesignerControl> list) : base(list) { }
    }
}