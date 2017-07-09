#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class ColorRichTextBoxTest : Form
    {
        public ColorRichTextBoxTest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                panel1.BackColor = colorDialog1.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorRichTextBox1.AppendText(textBox1.Text, panel1.BackColor, Color.Yellow);
        }
    }
}