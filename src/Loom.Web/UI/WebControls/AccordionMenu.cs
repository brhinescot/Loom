#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:AccordionMenu runat=server></{0}:AccordionMenu>")]
    public class AccordionMenu : WebControl
    {
        public MenuItemCollection Items { get; } = new MenuItemCollection();

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Ul;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Page.ClientScript.RegisterClientScriptResource(GetType(), JQueryResourcePath.Core);
            Page.ClientScript.RegisterClientScriptResource(GetType(), WebResourcePath.CmsUi);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
            RenderMenuItems(writer);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "items");
        }

        private void RenderMenuItems(HtmlTextWriter writer)
        {
            writer.Indent++;
            foreach (MenuItem item in Items)
            {
                if (item.IsSelected)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "active");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(item.Text);
                writer.RenderEndTag(); // span
                RenderSubMenuItem(item, writer);
                writer.WriteLine();
                writer.RenderEndTag(); // li
                writer.WriteLine();
            }
        }

        private static void RenderSubMenuItem(MenuItem item, HtmlTextWriter writer)
        {
            writer.WriteLine();
            writer.Indent++;
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (MenuSubItem subItem in item.Items)
            {
                if (!subItem.Visible)
                    continue;

                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, subItem.NavigateUrl ?? "#");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, subItem.Text);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(subItem.Text);
                writer.RenderEndTag(); // a
                writer.RenderEndTag(); // li
                writer.WriteLine();
            }
            writer.RenderEndTag(); // ul
            writer.Indent--;
        }
    }

    public class MenuItem : MenuSubItem
    {
        private bool isSelected;

        public MenuItem()
        {
            Items = new Collection<MenuSubItem>();
        }

        public Collection<MenuSubItem> Items { get; }

        public bool IsSelected
        {
            get => isSelected;
            internal set
            {
                if (value == isSelected)
                    return;

                isSelected = value;

                if (isSelected)
                    OnSelected(new MenuItemEventArgs(this));
            }
        }

        public event EventHandler<MenuItemEventArgs> Selected;

        protected virtual void OnSelected(MenuItemEventArgs e)
        {
            EventHandler<MenuItemEventArgs> handler = Selected;
            if (handler != null)
                handler(this, e);
        }
    }

    public class MenuSubItem
    {
        public string Text { get; set; }
        public string NavigateUrl { get; set; }

        public bool Visible { get; set; } = true;
    }

    public class MenuItemCollection : Collection<MenuItem>
    {
        public MenuItem SelectedItem
        {
            get
            {
                foreach (MenuItem item in this)
                    if (item.IsSelected)
                        return item;
                return null;
            }
            set
            {
                if (!Contains(value))
                    return;

                ClearOtherSelections(value);
                value.IsSelected = true;
            }
        }

        protected override void InsertItem(int index, MenuItem item)
        {
            item.Selected += HandleItemSelected;
            base.InsertItem(index, item);
        }

        private void HandleItemSelected(object sender, MenuItemEventArgs e)
        {
            ClearOtherSelections(e.MenuItem);
        }

        private void ClearOtherSelections(MenuItem selectedItem)
        {
            foreach (MenuItem menuItem in this)
                if (menuItem != selectedItem)
                    menuItem.IsSelected = false;
        }
    }

    public class MenuItemEventArgs : EventArgs
    {
        public MenuItemEventArgs() { }

        public MenuItemEventArgs(MenuItem menuItem)
        {
            MenuItem = menuItem;
        }

        public MenuItem MenuItem { get; set; }
    }
}