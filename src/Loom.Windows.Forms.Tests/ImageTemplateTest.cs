#region Using Directives

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Loom.Drawing;

#endregion

namespace Loom.Windows.Forms.Tests
{
    /// <summary>
    ///     Summary description for Form1.
    /// </summary>
    public partial class ImageTemplateTest : Form
    {
        private Button button1;

        private ComboBox combo;
        private ImageTemplateCollection definitions;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private Panel panel1;

        private PictureBox pictureBox1;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;

        public ImageTemplateTest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadXml();
        }

        private void LoadXml()
        {
            using (TextReader reader = File.OpenText(Application.StartupPath + @"\FeatureTemplates.xml"))
            {
                definitions = ImageTemplateCollection.FromXml(reader.ReadToEnd());

                combo.Items.Clear();
                foreach (ImageTemplate template in definitions)
                    combo.Items.Add(template.Id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (combo.SelectedItem == null)
                return;

            string selected = combo.SelectedItem.ToString();
            LoadXml();
            combo.SelectedItem = selected;

            GenerateImage();
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateImage();
        }

        private void GenerateImage()
        {
            if (combo.SelectedItem == null)
                return;

            ImageTemplate template = definitions[combo.SelectedItem.ToString()];
            if (template == null)
                return;

            pictureBox1.Image = ImageTemplateEngine.GenerateImage(template, new {Name = name.Text, Sport = "Snowboard"});
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            JpgFormat.Save(Application.StartupPath + @"\testimage.jpg", pictureBox1.Image, 100);
            Process.Start(Application.StartupPath + @"\testimage.jpg");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Application.StartupPath + @"\FeatureTemplates.xml");
        }
    }
}