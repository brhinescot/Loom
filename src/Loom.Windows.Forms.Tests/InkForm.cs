#if INK

using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

namespace Colossus.Framework.Windows.Forms.Tests
{

    public partial class InkForm : Form
    {
        private Microsoft.Ink.InkCollector collector;
        Point[] points;
        
        public InkForm()
        {
            InitializeComponent();
            collector = new Microsoft.Ink.InkCollector(Handle);
            collector.DefaultDrawingAttributes.Color = Color.Red;
            collector.DefaultDrawingAttributes.AntiAliased = true;
            collector.DefaultDrawingAttributes.Width = 40f;
            collector.Enabled = true;
            collector.Stroke += HandleCollectorStroke;
        }

        private void HandleCollectorStroke(object sender, Microsoft.Ink.InkCollectorStrokeEventArgs e)
        {
            points = e.Stroke.GetPoints();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (points != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                
                collector.Renderer.InkSpaceToPixel(e.Graphics, ref points);
                GraphicsPath path = new GraphicsPath();
                path.FillMode = FillMode.Winding;
                path.AddCurve(points);

                Region region = new Region(path);
                region.Complement(e.ClipRectangle);
                e.Graphics.FillRegion(Brushes.Gainsboro, region);
                e.Graphics.DrawRectangle(Pens.Black, InkSpaceToRectangle(e.Graphics, collector.Ink.GetBoundingBox()));
            }
        }

        private Rectangle InkSpaceToRectangle(Graphics g, Rectangle inkSpaceRectangle)
        {
            Point[] boundingPoints = new Point[] { inkSpaceRectangle.Location, new Point(inkSpaceRectangle.Width, inkSpaceRectangle.Height), };
            collector.Renderer.InkSpaceToPixel(g, ref boundingPoints);
            return new Rectangle(boundingPoints[0], new Size(boundingPoints[1]));
        }
    }
}

#endif
