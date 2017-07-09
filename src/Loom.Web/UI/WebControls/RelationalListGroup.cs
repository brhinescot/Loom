#region Using Directives

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RelationalListGroup runat=server></{0}:RelationalListGroup>")]
    public class RelationalListGroup : CompositeControl
    {
        private static readonly object ListAddedEventKey = new object();

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                string s = (string) ViewState["Text"];
                return s ?? string.Empty;
            }

            set => ViewState["Text"] = value;
        }

        public event EventHandler<ListControlAddedEventArgs> ListAdded
        {
            add => Events.AddHandler(ListAddedEventKey, value);
            remove => Events.RemoveHandler(ListAddedEventKey, value);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            DropDownList list = new DropDownList();
            ListControlAddedEventArgs e = new ListControlAddedEventArgs(list, null);
            OnListAdded(e);
            if (!e.Cancel)
                Controls.Add(list);

            output.Write(Text);
        }

        protected virtual void OnListAdded(ListControlAddedEventArgs e)
        {
            EventHandler<ListControlAddedEventArgs> handler = (EventHandler<ListControlAddedEventArgs>) Events[ListAddedEventKey];
            if (handler != null)
                handler(this, e);
        }
    }

    public class ListControlAddedEventArgs : EventArgs
    {
        public ListControlAddedEventArgs(ListControl listControl, string parentSelectedValue)
        {
            ListControl = listControl;
            ParentSelectedValue = parentSelectedValue;
        }

        public string ParentSelectedValue { get; set; }
        public ListControl ListControl { get; }
        public bool Cancel { get; set; }
    }
}