#region Using Directives

using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class Designer : Form
    {
        public Designer()
        {
            InitializeComponent();
            DesignerControl control = designer1.SelectedControl;
            if (control != null)
                label1.Text = control.Text;
            else
                label1.Text = "'null'";
        }

        private void designerControl2_ContextMenuRequested(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Context Menu Requested");
        }

        private void designer1_SelectionChanged(object sender, DesignerEventArgs e)
        {
            DesignerControl control = e.Control;
            if (control != null)
                label1.Text = control.Text;
            else
                label1.Text = "'null'";
        }
    }
}