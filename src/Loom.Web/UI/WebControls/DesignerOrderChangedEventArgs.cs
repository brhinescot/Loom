#region Using Directives

using System;

#endregion

namespace Loom.Web.UI.WebControls
{
    public class DesignerOrderChangedEventArgs : EventArgs
    {
        public DesignerOrderChangedEventArgs(DesignerControlCollection designersControls)
        {
            DesignersControls = designersControls;
        }

        public DesignerControlCollection DesignersControls { get; }
    }
}