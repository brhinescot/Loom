#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.Tests
{
    public partial class ManagementConsoleMiddle : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string param = Page.Request.QueryString["id"];

            switch (param)
            {
                case "1":
                {
                    content.Items.Add(new ListItem {Text = "Planes"});
                    content.Items.Add(new ListItem {Text = "Trains"});
                    content.Items.Add(new ListItem {Text = "Automobiles"});
                    break;
                }
                case "2":
                {
                    content.Items.Add(new ListItem {Text = "Cats"});
                    content.Items.Add(new ListItem {Text = "Dogs"});
                    content.Items.Add(new ListItem {Text = "Fish"});
                    break;
                }
                case "3":
                {
                    content.Items.Add(new ListItem {Text = "PC"});
                    content.Items.Add(new ListItem {Text = "Mac"});
                    content.Items.Add(new ListItem {Text = "Linux"});
                    break;
                }
                case "4":
                {
                    content.Items.Add(new ListItem {Text = "Yes"});
                    content.Items.Add(new ListItem {Text = "No"});
                    content.Items.Add(new ListItem {Text = "Maybe"});
                    break;
                }
                default:
                {
                    content.Items.Add(new ListItem {Text = "Error"});
                    break;
                }
            }
        }
    }
}