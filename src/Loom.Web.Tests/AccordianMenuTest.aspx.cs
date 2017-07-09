#region Using Directives

using System;
using System.Web.UI;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.Tests
{
    public partial class AccordianMenuTest : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            sidebarMenu.Items.Add(new MenuItem {Text = "Products"});
            sidebarMenu.Items.Add(new MenuItem {Text = "Categories"});
            sidebarMenu.Items.Add(new MenuItem {Text = "Media"});
            sidebarMenu.Items.Add(new MenuItem {Text = "Portal"});

            sidebarMenu.Items.SelectedItem = sidebarMenu.Items[0];
            sidebarMenu.Items[0].Items.Add(new MenuSubItem {Text = "Add Product"});
            sidebarMenu.Items[0].Items.Add(new MenuSubItem {Text = "Edit Product"});

            sidebarMenu.Items[1].Items.Add(new MenuSubItem {Text = "Add Category"});
            sidebarMenu.Items[1].Items.Add(new MenuSubItem {Text = "Edit Category"});

            sidebarMenu.Items[2].Items.Add(new MenuSubItem {Text = "Add Media"});
            sidebarMenu.Items[2].Items.Add(new MenuSubItem {Text = "Edit Media"});

            sidebarMenu.Items[3].Items.Add(new MenuSubItem {Text = "Add Portal"});
            sidebarMenu.Items[3].Items.Add(new MenuSubItem {Text = "Edit Portal"});
        }
    }
}