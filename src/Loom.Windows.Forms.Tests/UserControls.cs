#region Using Directives

using System;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class UserControls : HotKeyForm
    {
        private Cursor cursor;

        public UserControls()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Text={" + shortcutTextBox1.Text + "} KeyData={" + shortcutTextBox1.KeyData + "}";
            Console.Out.WriteLine("shortcutTextBox1.KeyCode = {0}", shortcutTextBox1.KeyCode);
            RegisterHotKey("Test" + DateTime.Now.Ticks, shortcutTextBox1.KeyCode, shortcutTextBox1.Modifiers);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            label1.Text = e.Modifiers.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
//          string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Files\\BlueLace.bmp");
//          cursor = new BitmapCursor(path, Cursor);

            cursor = new DesktopCursor(MousePosition);
            Cursor = cursor;

            shortcutTextBox1.Modifiers = Keys.Alt;
            shortcutTextBox1.KeyCode = Keys.O;
            shortcutTextBox1.Alt = false;
            shortcutTextBox1.Shift = true;
            shortcutTextBox1.Control = true;
            shortcutTextBox1.KeyData = Keys.O | Keys.Alt | Keys.Shift | Keys.Control;
        }

        protected override void OnHotKeyPress(KeyEventArgs e)
        {
            base.OnHotKeyPress(e);
            MessageBox.Show(e.Modifiers + ", " + e.KeyCode, "Hot Key Pressed");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            UnregisterAllHotKeys();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            shortcutTextBox1.Text = textBox1.Text;
        }
    }
}