#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class Form1 : Form
    {
        private LayeredFormTest layered;
        private string Message = "You must not click in this box!";
        private string Title = "Hey!";

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.ShowBalloonTip(30);
            editor.Type = typeof(Color);
            editor.Value = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Designer().Show();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            TextBoxBalloonTip.Show(textBox1, Message, Title, BalloonTipIcon.Error);
            Message = "What, are you deaf? Stop it!";
            Title = "Hello???";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Size size = new Size(250, 75);
            Point location = new Point(Screen.PrimaryScreen.WorkingArea.Right - size.Width - 5, Screen.PrimaryScreen.WorkingArea.Bottom - size.Height - 5);

            layered = new LayeredFormTest {Opacity = 1, Bounds = new Rectangle(location, size)};
            layered.Show(AnimationStyle.Fade, 500);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExpandingListViewTest lvTest = new ExpandingListViewTest();
            lvTest.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new HttpDownload().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorRichTextBoxTest test = new ColorRichTextBoxTest();
            test.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Painting painting = new Painting();
            painting.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UserControls controls = new UserControls();
            controls.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Thumbnails thumbnails = new Thumbnails();
            thumbnails.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
#if INK
            InkForm form = new InkForm();
            form.Show();
#endif
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void button10_Click(object sender, EventArgs e)
        {
//            ActiveDataServiceTest test = new ActiveDataServiceTest();
//            test.Show();
        }

        private void button11_Click(object sender, EventArgs e) { }

        private void button11_Click_1(object sender, EventArgs e)
        {
            UIEditors editors = new UIEditors();
            editors.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ImageTemplateTest templateTest = new ImageTemplateTest();
            templateTest.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Size size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Point location = new Point(0, 0);

            layered = new LayeredFormTest {Opacity = 1, Bounds = new Rectangle(location, size)};
            layered.Show();
        }
    }
}