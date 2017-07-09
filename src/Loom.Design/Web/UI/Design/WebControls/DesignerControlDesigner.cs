#region Using Directives

using System.Drawing;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.Design.WebControls
{
    public class DesignerControlDesigner : ContainerControlDesigner
    {
        private Style style;

        public override bool AllowResize => true;

        public override string FrameCaption => "Designer Content";

        public override Style FrameStyle
        {
            get
            {
                if (style == null)
                {
                    style = new Style();
                    style.Font.Name = "Verdana";
                    style.Font.Size = new FontUnit("XSmall");
                    style.BackColor = Color.LightBlue;
                    style.ForeColor = Color.Black;
                    style.Height = new Unit(20);
                }

                return style;
            }
        }
    }
}