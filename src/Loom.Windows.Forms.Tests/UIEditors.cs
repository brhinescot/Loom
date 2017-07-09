#region Using Directives

using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class UIEditors : Form
    {
        public UIEditors()
        {
            InitializeComponent();
            genericEditor1.Type = typeof(Keys);
            genericEditor1.Value = Keys.O;
//            genericEditor1.Editor = new HatchStyleEditor();

//            genericEditor1.Type = typeof(System.Drawing.Drawing2D.HatchStyle);
//            genericEditor1.Value = System.Drawing.Drawing2D.HatchStyle.Sphere;
//            genericEditor1.Editor = new HatchStyleEditor();
//
            genericEditor2.Type = typeof(string);
            genericEditor2.Editor = new MultilineStringEditor();

            genericEditor3.Type = typeof(Font);
            genericEditor3.Value = SystemFonts.MenuFont;
        }
    }
}