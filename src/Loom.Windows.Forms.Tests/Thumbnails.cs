#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;
using Loom.Drawing;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class Thumbnails : Form
    {
        public Thumbnails()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const int maxSize = 100;
            if (browser.ShowDialog() != DialogResult.OK)
                return;

            int x = 5;
            int y = 60;
            foreach (Bitmap image in Thumbnail.FromFolder(browser.SelectedPath, maxSize))
            {
                Graphics g = CreateGraphics();
                g.DrawImage(image, new Point(x, y));
                x += maxSize + 10;
                if (x + maxSize <= Width)
                    continue;

                y += maxSize + 10;
                x = 5;
            }
        }
    }
}