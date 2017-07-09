#region Using Directives

using System;
using System.Threading;
using System.Web.UI;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.Tests
{
    public partial class Default : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
                WaitScreen1.Process += WaitScreen1_Process1;

            base.OnLoad(e);

            Page.Header.AddKeywords("Test", "Test2", "Test Three");
            Page.Header.AddDescription("This is a test page description");
            Page.Header.AddScript("function void func(){}", true);

            ControlLocalizer.Localize(hyperlink21, "TextBox21");
        }

        protected static void WaitScreen1_Process1(object sender, EventArgs e)
        {
            Thread.Sleep(4000);
        }

        protected void Button1_Click(object sender, EventArgs e) { }
    }
}