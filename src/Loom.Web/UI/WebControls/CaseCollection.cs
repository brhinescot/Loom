#region Using Directives

using System;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.WebControls
{
    public sealed class CaseCollection : ControlCollection
    {
        public CaseCollection(Control owner) : base(owner) { }

        public new Case this[int i] => (Case) base[i];

        public override void Add(Control c)
        {
            if (!(c is Case))
                throw new ArgumentException();
            base.Add(c);
        }

        public override void AddAt(int index, Control c)
        {
            if (!(c is Case))
                throw new ArgumentException();
            base.AddAt(index, c);
        }
    }
}