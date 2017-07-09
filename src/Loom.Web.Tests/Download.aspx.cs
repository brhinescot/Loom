#region Using Directives

using System;
using System.Web.UI;
using Loom.Web.IO;

#endregion

namespace Loom.Web.Tests
{
    public partial class Download : Page
    {
        protected void Download1_Click(object sender, EventArgs e)
        {
            string[] additionalFiles = new string[2] {"files/16.png", "files/17.png"};
            WebFile.Download("files/15.png", "Images", 1, additionalFiles);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebFile.Download("files/15.png", "Images", 0);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            WebFile.Stream("files/15.png", "Logo.png");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            WebFile.Download("Files/BeethovenNo9.wma");
        }
    }
}