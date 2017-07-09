#region Using Directives

using System;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.WebControls
{
    public class TabItemCollection : ControlCollection
    {
        public TabItemCollection(Control owner) : base(owner) { }

        public new TabItem this[int i] => (TabItem) base[i];

        public override void Add(Control v)
        {
            if (!(v is TabItem))
                throw new ArgumentException();
            base.Add(v);
        }

        public override void AddAt(int index, Control v)
        {
            if (!(v is TabItem))
                throw new ArgumentException();
            base.AddAt(index, v);
        }
    }
}