#region Using Directives

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Loom.Drawing;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class Painting : Form
    {
        private Color color1;
        private Color color2;
        private Color color3;
        private Color color4;
        private HslColor hslColor;
        private Color outline;

        public Painting()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();

            borderColor.Type = typeof(Color);
            borderColor.Value = Color.FromArgb(0, 81, 147);

            SetColors();
        }

        private void SetColors()
        {
            hslColor = HslColor.FromColor((Color) borderColor.Value, true);
            BackColor = hslColor.Lighten(1.8f).ToColor();
            color1 = hslColor.Lighten(.8f).ToColor();
            color2 = hslColor.Lighten(.2f).ToColor();
            color3 = hslColor.Lighten(.0f).ToColor();
            color4 = hslColor.Lighten(.4f).ToColor();
            outline = hslColor.Lighten(0f).ToColor();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen borderPen = new Pen(outline, (float) borderSize.Value);
            Pen borderShodowPen = new Pen(Brushes.Gainsboro, (float) borderSize.Value);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            Rectangle drawRec = new Rectangle(3, 3, ClientSize.Width - 6, (int) rectHeight.Value);

            GraphicsPath roundedPath = CalculateRoundedRectanglePath(drawRec, (int) arcDiameter.Value);
            drawRec.Offset(3, 3);
            GraphicsPath shadowPath = CalculateRoundedRectanglePath(drawRec, (int) arcDiameter.Value + 1);

            if (checkBox1.Checked)
            {
                e.Graphics.FillPath(Brushes.LightGray, shadowPath);
                e.Graphics.DrawPath(borderShodowPen, shadowPath);
            }

            e.Graphics.FillPath(new SolidBrush(hslColor.Lighten(0).ToColor()), roundedPath);
            e.Graphics.FillPath(CreateBackGroudBrush(drawRec), roundedPath);
            e.Graphics.DrawPath(borderPen, roundedPath);

            //PaintJaggedLine(e.Graphics);
        }

        private Brush CreateBackGroudBrush(Rectangle drawRec)
        {
            Rectangle rect = new Rectangle(drawRec.X, drawRec.Y - 2, drawRec.Width, drawRec.Height);

            ColorBlend blend = new ColorBlend();
            blend.Colors = new[] {color1, color2, color3, color4};
            blend.Positions = new[] {0.0f, 0.2f, 0.2f, 1.0f};
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
            brush.InterpolationColors = blend;

            return brush;
        }

        public GraphicsPath CalculateRoundedRectanglePath(Rectangle rectangle, int cornerRadius)
        {
            int diameter = 2 * cornerRadius;
            Rectangle arcRectangle = new Rectangle(rectangle.Location, new Size(diameter, diameter));

            GraphicsPath path = new GraphicsPath();

            // Top left
            path.AddArc(arcRectangle, 180, 90);

            // Top right
            arcRectangle.X = rectangle.Right - diameter;
            path.AddArc(arcRectangle, 270, 90);

            // Bottom right
            arcRectangle.Y = rectangle.Bottom - diameter;
            path.AddArc(arcRectangle, 0, 90);

            // Bottom left
            arcRectangle.X = rectangle.Left;
            path.AddArc(arcRectangle, 90, 90);

            path.CloseFigure();

            return path;
        }

        private void borderSize_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void arcDiameter_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void borderColor_ValueChanged(object sender, EventArgs e)
        {
            SetColors();
            Refresh();
        }

        private void rectHeight_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        //private Random rand = new Random(DateTime.Now.Millisecond);
        //private Pen tearPen;
        //int maxDepth = 6;
        //int minDepth = 0;
        //int length = 500;
        //int height = 300;
        //int xOffset = 10;
        //int yOffset = 100;
        //int maxStep = 11;
        //int minStep = 1;
        //float tension = 1f;
        //bool recalculateLines = true;

        //List<Point> points = new List<Point>();
        //private void PaintJaggedLine(Graphics g)
        //{
        //    GraphicsPath path = new GraphicsPath();
        //    if(recalculateLines)
        //    {
        //        points.Clear();
        //        int spread = length/(int)numberOfPoints.Value;
        //        points.Add(new Point(10, height - rand.Next(minDepth, maxDepth)));

        //        for (int i = spread; i <= length + xOffset; i += spread)
        //        {
        //            points.Add(new Point(i + rand.Next(minStep, maxStep), height - rand.Next(minDepth, maxDepth)));
        //        }

        //        points.Add(new Point(length + xOffset, height - rand.Next(minDepth, maxDepth)));
        //    }
        //    path.AddLine(new Point(xOffset, yOffset), points[0]);
        //    path.AddCurve(points.ToArray(), tension);
        //    path.AddLine(points[points.Count - 1], new Point(length + xOffset, yOffset));
        //    path.CloseFigure();

        //    Region clip = g.Clip;
        //    Bitmap b = new Bitmap(@"C:\Documents and Settings\All Users\Documents\My Pictures\Sample Pictures\Sunset.jpg");
        //    g.Clip = new Region(path);

        //    g.DrawImage(b, 0, 0);
        //    g.Clip = clip;
        //    tearPen = new Pen(new HatchBrush((HatchStyle)hatchSelection.Value, Color.Transparent, Color.FromArgb(191, 219, 255)), 4f);
        //    g.DrawCurve(tearPen, points.ToArray(), tension);
        //    tearPen = new Pen(new HatchBrush((HatchStyle)hatchSelection2.Value, Color.FromArgb(191, 219, 255), Color.Transparent), 4f);
        //    g.DrawCurve(tearPen, points.ToArray(), tension);

        //    recalculateLines = false;
        //}

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //maxDepth = (int)numericUpDown1.Value;
            //recalculateLines = true;
            Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //minDepth = (int)numericUpDown2.Value;
            //recalculateLines = true;
            Invalidate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            //maxStep = (int)numericUpDown4.Value;
            //recalculateLines = true;
            Invalidate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            //minStep = (int)numericUpDown3.Value;
            //recalculateLines = true;
            Invalidate();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            //tension = (float)numericUpDown5.Value;
            //recalculateLines = true;
            Invalidate();
        }

        private void Painting_Load(object sender, EventArgs e) { }

        private void numberOfPoints_ValueChanged(object sender, EventArgs e)
        {
            //recalculateLines = true;
            Invalidate();
        }
    }
}