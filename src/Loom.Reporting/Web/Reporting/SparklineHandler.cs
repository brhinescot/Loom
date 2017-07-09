#region Using Directives

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;

#endregion

namespace Loom.Web.Reporting
{
    public class SparklineHandler : IHttpHandler
    {
        private const string Version = "2.3";

        private HttpRequest request;
        private HttpResponse response;

        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            request = context.Request;
            response = context.Response;

            if (!("GET" == request.RequestType || "HEAD" == request.RequestType))
                ReturnError(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");

            ArrayList dataRaw = new ArrayList(GetArg("d", "").Split(','));
            int[] data = new int[dataRaw.Count];
            int dataIndex = -1;

            for (int rawIndex = 0; rawIndex < dataRaw.Count; rawIndex++)
            {
                if (0 == ((string) dataRaw[rawIndex]).Trim().Length)
                    continue;

                try
                {
                    data[dataIndex + 1] = int.Parse((string) dataRaw[rawIndex]);
                }
                catch
                {
                    continue;
                }

                dataIndex++;
                if (data[dataIndex] < 0 || data[dataIndex] > 100)
                    ReturnError();
            }
            if (-1 == dataIndex)
                ReturnError();

            data = (int[]) Redim(data, dataIndex + 1);

            string type = GetArg("type", "");
            if (!("discrete" == type || "smooth" == type || "bars" == type))
                ReturnError();

            Ok();

            if ("discrete" == type)
                response.BinaryWrite(PlotSparklineDiscrete(data).ToArray());
            if ("smooth" == type)
                response.BinaryWrite(PlotSparklineSmooth(data).ToArray());
            if ("bars" == type)
                response.BinaryWrite(PlotSparklineBars(data).ToArray());
        }

        public bool IsReusable => false;

        #endregion

        private static MemoryStream PlotError()
        {
            MemoryStream m = new MemoryStream();

            using (Bitmap bitmap = new Bitmap(40, 15, PixelFormat.Format32bppArgb))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (SolidBrush br = new SolidBrush(Color.White))
                {
                    g.FillRectangle(br, 0, 0, bitmap.Width, bitmap.Height);
                }
                using (Pen p = new Pen(Color.Red))
                {
                    g.DrawLine(p, 0, 0, bitmap.Width, bitmap.Height);
                    g.DrawLine(p, 0, bitmap.Height, bitmap.Width, 0);
                }
                bitmap.Save(m, ImageFormat.Gif);
            }
            return MakeTransparent(m);
        }

        private static Array Redim(Array origArray, int newSize)
        {
            Type t = origArray.GetType().GetElementType();
            Array newArray = Array.CreateInstance(t, newSize);
            Array.Copy(origArray, 0, newArray, 0, Math.Min(origArray.Length, newSize));
            return newArray;
        }

        private static void DrawTick(Graphics g, Color color, Point coord)
        {
            SolidBrush br = new SolidBrush(color);
            g.FillRectangle(br, coord.X - 1, coord.Y - 1, 3, 3);
            br.Dispose();
        }

        private static Color GetColor(string color)
        {
            if (color.StartsWith("#"))
                return Color.FromArgb(IntFromHexRgbPart(color, RgbPart.RgbPartRed),
                    IntFromHexRgbPart(color, RgbPart.RgbPathGreen),
                    IntFromHexRgbPart(color, RgbPart.RgbPartBlue)
                );
            return Color.FromName(color);
        }

        private static int IntFromHexRgbPart(string hexRgb, RgbPart part)
        {
            if (Compare.IsNullOrEmpty(hexRgb) || !hexRgb.StartsWith("#"))
                return 0;
            try
            {
                switch (part)
                {
                    case RgbPart.RgbPartRed:
                        return hexRgb.Length < 3 ? 0 : IntFromHex(hexRgb.Substring(1, 2));
                    case RgbPart.RgbPathGreen:
                        return hexRgb.Length < 5 ? 0 : IntFromHex(hexRgb.Substring(3, 2));
                    case RgbPart.RgbPartBlue:
                        return hexRgb.Length < 7 ? 0 : IntFromHex(hexRgb.Substring(5, 2));
                    default:
                        return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        private static int IntFromHex(string hex)
        {
            return byte.Parse(hex, NumberStyles.HexNumber);
        }

        private static MemoryStream MakeTransparent(MemoryStream origBitmapMemoryStream)
        {
            Color transparentColor = GetColor("White");
            int transparentArgb = transparentColor.ToArgb();

            using (Bitmap origBitmap = new Bitmap(origBitmapMemoryStream))
            using (Bitmap newBitmap = new Bitmap(origBitmap.Width, origBitmap.Height, origBitmap.PixelFormat))
            {
                ColorPalette origPalette = origBitmap.Palette;
                ColorPalette newPalette = newBitmap.Palette;

                int index = 0;
                int transparentIndex = -1;

                foreach (Color origColor in origPalette.Entries)
                {
                    newPalette.Entries[index] = Color.FromArgb(255, origColor);
                    if (origColor.ToArgb() == transparentArgb) transparentIndex = index;
                    index += 1;
                }

                if (-1 == transparentIndex)
                    return origBitmapMemoryStream;

                newPalette.Entries[transparentIndex] = Color.FromArgb(0, transparentColor);
                newBitmap.Palette = newPalette;

                Rectangle rect = new Rectangle(0, 0, origBitmap.Width, origBitmap.Height);

                BitmapData origBitmapData = origBitmap.LockBits(rect, ImageLockMode.ReadOnly, origBitmap.PixelFormat);
                BitmapData newBitmapData = newBitmap.LockBits(rect, ImageLockMode.WriteOnly, newBitmap.PixelFormat);

                for (int y = 0; y < origBitmap.Height; y++)
                for (int x = 0; x < origBitmap.Width; x++)
                {
                    byte origBitmapByte = Marshal.ReadByte(origBitmapData.Scan0, origBitmapData.Stride * y + x);
                    Marshal.WriteByte(newBitmapData.Scan0, newBitmapData.Stride * y + x, origBitmapByte);
                }

                newBitmap.UnlockBits(newBitmapData);
                origBitmap.UnlockBits(origBitmapData);

                MemoryStream m = new MemoryStream();
                newBitmap.Save(m, ImageFormat.Gif);
                return m;
            }
        }

        private static ResultsInfo EvaluateResults(int[] results, bool scale)
        {
            ResultsInfo ri = EvaluateResults(results);
            if (!scale) return ri;
            float range = ri.Max - ri.Min;
            for (int x = 0; x < results.Length; x++)
                results[x] = Math.Max((int) ((results[x] - ri.Min) / range * 100F), 1);
            return EvaluateResults(results);
        }

        private static ResultsInfo EvaluateResults(int[] results)
        {
            int min = 100, minIndex = -1;
            int max = 0, maxIndex = -1;
            for (int x = 0; x < results.Length; x++)
            {
                int r = results[x];
                if (r < min)
                {
                    min = r;
                    minIndex = x;
                }

                if (r <= max)
                    continue;

                max = r;
                maxIndex = x;
            }
            return new ResultsInfo(min, minIndex, max, maxIndex);
        }

        private MemoryStream PlotSparklineDiscrete(int[] results)
        {
            try
            {
                int height = int.Parse(GetArg("height", "14"));
                int upper = int.Parse(GetArg("upper", "50"));
                Color belowColor = GetColor(GetArg("below-color", "gray"));
                Color aboveColor = GetColor(GetArg("above-color", "red"));
                bool transparent = bool.Parse(GetArg("transparent", "false"));
                bool scale = bool.Parse(GetArg("scale", "false"));

                EvaluateResults(results, scale);

                using (Bitmap bitmap = new Bitmap(results.Length * 2 - 1, height, PixelFormat.Format32bppArgb))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    using (SolidBrush br = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(br, 0, 0, bitmap.Width, height);
                    }

                    for (int x = 0; x < results.Length; x++)
                    {
                        int r = results[x];
                        int y = height - (int) Math.Ceiling(r / (101F / (height - 4))) - 4;
                        using (Pen p = new Pen(r >= upper ? aboveColor : belowColor))
                        {
                            g.DrawLine(p, x * 2, y, x * 2, y + 3);
                        }
                    }

                    MemoryStream m = new MemoryStream();
                    bitmap.Save(m, ImageFormat.Gif);
                    return transparent ? MakeTransparent(m) : m;
                }
            }
            catch
            {
                return PlotError();
            }
        }

        private MemoryStream PlotSparklineSmooth(int[] results)
        {
            try
            {
                int step = int.Parse(GetArg("step", "2"));
                int height = int.Parse(GetArg("height", "20"));
                Color minColor = GetColor(GetArg("min-color", "#0000FF"));
                Color maxColor = GetColor(GetArg("max-color", "#00FF00"));
                Color lastColor = GetColor(GetArg("last-color", "#FF0000"));
                Color rangeColor = GetColor(GetArg("range-color", "#CCCCCC"));
                bool hasMin = bool.Parse(GetArg("min-m", "false"));
                bool hasMax = bool.Parse(GetArg("max-m", "false"));
                bool hasLast = bool.Parse(GetArg("last-m", "false"));
                int rangeLower = int.Parse(GetArg("range-lower", "0"));
                int rangeUpper = int.Parse(GetArg("range-upper", "0"));
                bool transparent = bool.Parse(GetArg("transparent", "false"));
                bool scale = bool.Parse(GetArg("scale", "false"));

                ResultsInfo resultsInfo = EvaluateResults(results, scale);

                if (rangeLower < 0 || rangeLower > 100) return PlotError();
                if (rangeUpper < 0 || rangeUpper > 100) return PlotError();

                using (Bitmap bitmap = new Bitmap((results.Length - 1) * step + 4, height, PixelFormat.Format32bppArgb))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        using (SolidBrush br = new SolidBrush(Color.White))
                        {
                            g.FillRectangle(br, 0, 0, bitmap.Width, height);
                        }

                        if (!(0 == rangeLower && 0 == rangeUpper) && rangeLower <= rangeUpper)
                            using (SolidBrush br = new SolidBrush(rangeColor))
                            {
                                int y = height - 3 - (int) Math.Ceiling(rangeUpper / (101F / (height - 4)));
                                int h = height - 3 - (int) Math.Ceiling(rangeLower / (101F / (height - 4))) - y + 1;
                                g.FillRectangle(br, 1, y, bitmap.Width - 2, h);
                            }

                        Point[] coords = new Point[results.Length];
                        for (int x = 0; x < results.Length; x++)
                        {
                            int r = results[x];
                            int y = height - 3 - (int) Math.Ceiling(r / (101F / (height - 4)));
                            coords[x] = new Point(x * step + 1, y);
                        }
                        using (Pen p = new Pen(GetColor("#999999")))
                        {
                            g.DrawLines(p, coords);
                        }

                        if (hasMin)
                            DrawTick(g, minColor, coords[resultsInfo.MinIndex]);
                        if (hasMax)
                            DrawTick(g, maxColor, coords[resultsInfo.MaxIndex]);
                        if (hasLast)
                            DrawTick(g, lastColor, coords[results.Length - 1]);

                        MemoryStream m = new MemoryStream();
                        bitmap.Save(m, ImageFormat.Gif);
                        return transparent ? MakeTransparent(m) : m;
                    }
                }
            }
            catch
            {
                return PlotError();
            }
        }

        private MemoryStream PlotSparklineBars(int[] results)
        {
            try
            {
                int barHeight = int.Parse(GetArg("bar-height", "3"));
                int height = (barHeight + 1) * results.Length + (results.Length - 1);
                int width = int.Parse(GetArg("width", "20")) + 1;
                ArrayList barColors = new ArrayList(GetArg("bar-colors", "blue").Split(','));
                Color shadowColor = GetColor(GetArg("shadow-color", "#222222"));
                bool alignRight = bool.Parse(GetArg("align-right", "false"));
                bool transparent = bool.Parse(GetArg("transparent", "false"));
                bool scale = bool.Parse(GetArg("scale", "false"));

                EvaluateResults(results, scale);

                using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    using (SolidBrush br = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(br, 0, 0, width, height);
                    }

                    using (Pen p = new Pen(shadowColor))
                    {
                        for (int y = 0; y < results.Length; y++)
                        {
                            int r = results[y];
                            if (0 == r)
                                continue;

                            int barWidth = (int) Math.Ceiling(r / (100F / (width - 1)));
                            int barTop = y * (barHeight + 1);
                            if (0 < y) barTop += y;
                            int barLeft = alignRight ? width - barWidth - 1 : 0;
                            Color barColor = GetColor((string) barColors[Math.Min(barColors.Count - 1, y)]);

                            using (SolidBrush br = new SolidBrush(barColor))
                            {
                                g.FillRectangle(br, barLeft, barTop, barWidth, barHeight);
                            }

                            if (barWidth > 1)
                                g.DrawLine(p, barLeft + 1, barTop + barHeight, barLeft + barWidth, barTop + barHeight);

                            g.DrawLine(p, barLeft + barWidth, barTop + 1, barLeft + barWidth, barTop + barHeight);
                        }
                    }

                    MemoryStream m = new MemoryStream();
                    bitmap.Save(m, ImageFormat.Gif);
                    return transparent ? MakeTransparent(m) : m;
                }
            }
            catch
            {
                return PlotError();
            }
        }

        private void Ok()
        {
            SetResponse(HttpStatusCode.OK, "Ok");
        }

        private void ReturnError(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string statusDescription = "Bad Request")
        {
            SetResponse(statusCode, statusDescription);
            response.BinaryWrite(PlotError().ToArray());
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private string GetArg(string argName, string argDefault)
        {
            string arg = request.QueryString[argName];
            return arg ?? argDefault;
        }

        private void SetResponse(HttpStatusCode statusCode, string statusDescription)
        {
            response.ContentType = "image/gif";
            response.StatusCode = (int) statusCode;
            response.StatusDescription = statusDescription;
            response.AddHeader("ETag", (request.QueryString + Version).GetHashCode().ToString());
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        #region Nested type: ResultsInfo

        private class ResultsInfo
        {
            public readonly int Max;
            public readonly int MaxIndex;
            public readonly int Min = 100;
            public readonly int MinIndex;

            public ResultsInfo(int min, int minIndex, int max, int maxIndex)
            {
                Min = min;
                MinIndex = minIndex;
                Max = max;
                MaxIndex = maxIndex;
            }
        }

        #endregion

        #region Nested type: RgbPart

        private enum RgbPart
        {
            RgbPartRed,
            RgbPathGreen,
            RgbPartBlue
        }

        #endregion
    }
}