#region Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Represents an HSL color.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ColorConverter))]
    [Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [DebuggerDisplay("HSL=({H}, {S}, {L})")]
    [StructLayout(LayoutKind.Auto)]
    public struct HslColor : IEquatable<HslColor>
    {
        /// <summary>
        ///     Represents an HSL color that is null;
        /// </summary>
        public static readonly HslColor Empty;

        #region Constants

        private const int DarkenAdjustment = -333;
        private const int LightenAdjustment = 500;
        private const byte HslMax = 240;
        private const byte RgbMax = 255;
        private const byte DesaturationLevel = 3;
        private const int Undefined = 160;
        private const short StateFromHslValid = 1;
        private const short StateFromColorValid = 2;

        #endregion

        #region Member Fields

        private readonly short state;

        #endregion

        #region Property Accessors

        //TODO: Invesigate rolling hue values over to the min value if the passed in value exceeds the max.
        /// <summary>
        ///     Gets the hue component value of this <see cref="HslColor" /> structure.
        /// </summary>
        /// <value>The hue component value of this <see cref="HslColor" />.</value>
        public byte H { get; }

        /// <summary>
        ///     Gets the saturation component value of this <see cref="HslColor" /> structure.
        /// </summary>
        /// <value>The saturation component value of this <see cref="HslColor" />.</value>
        public byte S { get; }

        /// <summary>
        ///     Gets the luminosity component value of this <see cref="HslColor" /> structure.
        /// </summary>
        /// <value>The luminosity component value of this <see cref="HslColor" />.</value>
        public byte L { get; }

        /// <summary>
        ///     Gets a value that specifies whether this <see cref="HslColor" /> structure is uninitialized.
        /// </summary>
        /// <value>
        ///     This property returns <see langword="true" /> if this color is uninitialized;
        ///     otherwise, <see langword="false" />.
        /// </value>
        public bool IsEmpty => state == 0;

        #endregion

        #region Factory Methods

        /// <summary>
        ///     Froms the HSL.
        /// </summary>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="luminosity">The luminosity.</param>
        /// <returns></returns>
        public static HslColor FromHsl(int hue, int saturation, int luminosity)
        {
            VerifyByteParameter(hue, "hue");
            VerifyByteParameter(saturation, "saturation");
            VerifyByteParameter(luminosity, "luminosity");

            return new HslColor((byte) hue, (byte) saturation, (byte) luminosity);
        }

        /// <summary>
        ///     Froms the RGB.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <returns></returns>
        public static HslColor FromRgb(int red, int green, int blue)
        {
            return FromRgb(red, green, blue, false);
        }

        /// <summary>
        ///     Froms the RGB.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="desaturate">if set to <c>true</c> [desaturate].</param>
        /// <returns></returns>
        public static HslColor FromRgb(int red, int green, int blue, bool desaturate)
        {
            VerifyByteParameter(red, "red");
            VerifyByteParameter(green, "green");
            VerifyByteParameter(blue, "blue");

            return new HslColor(Color.FromArgb(red, green, blue), desaturate);
        }

        /// <summary>
        ///     Froms the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static HslColor FromName(string name)
        {
            return new HslColor(Color.FromName(name), false);
        }

        /// <summary>
        ///     Froms the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static HslColor FromColor(Color color)
        {
            return new HslColor(color, false);
        }

        /// <summary>
        ///     Froms the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="desaturate">if set to <c>true</c> [desaturate].</param>
        /// <returns></returns>
        public static HslColor FromColor(Color color, bool desaturate)
        {
            return new HslColor(color, desaturate);
        }

        #endregion

        #region .ctors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HslColor" /> class.
        /// </summary>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="luminosity">The luminosity.</param>
        private HslColor(byte hue, byte saturation, byte luminosity)
        {
            H = hue;
            S = saturation;
            L = luminosity;
            state = StateFromHslValid;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HslColor" /> class.
        /// </summary>
        /// <param name="baseColor">Color of the base.</param>
        private HslColor(Color baseColor) : this(baseColor, false) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HslColor" /> class.
        /// </summary>
        /// <param name="baseColor">The color.</param>
        /// <param name="desaturate">if set to <c>true</c> [desaturate].</param>
        private HslColor(Color baseColor, bool desaturate)
        {
            state = StateFromColorValid;

            byte r = baseColor.R;
            byte g = baseColor.G;
            byte b = baseColor.B;
            byte maxColor = Math.Max(Math.Max(r, g), b);
            byte minColor = Math.Min(Math.Min(r, g), b);

            if (maxColor == minColor)
            {
                S = 0;
                H = Undefined;
                L = r;
                return;
            }

            // Calculate the luminosity.
            L = (byte) (((maxColor + minColor) * HslMax + RgbMax) / (2 * RgbMax));

            // Calculate the saturation.
            if (L <= HslMax / 2)
                S = (byte) (((maxColor - minColor) * HslMax + (maxColor + minColor) / 2) / (maxColor + minColor));
            else
                S = (byte) (((maxColor - minColor) * HslMax + (2 * RgbMax - (maxColor + minColor)) / 2) / (2 * RgbMax - (maxColor + minColor)));

            if (desaturate)
            {
                S = (byte) (S / 3);
                if (S < L)
                    S = L;
            }

            // Calculate the hue.
            int rDelta = ((maxColor - r) * HslMax / 6 + (maxColor - minColor) / 2) / (maxColor - minColor);
            int gDelta = ((maxColor - g) * HslMax / 6 + (maxColor - minColor) / 2) / (maxColor - minColor);
            int bDelta = ((maxColor - b) * HslMax / 6 + (maxColor - minColor) / 2) / (maxColor - minColor);

            if (r == maxColor)
                H = (byte) (bDelta - gDelta);
            else if (g == maxColor)
                H = (byte) (HslMax / 3 + rDelta - bDelta);
            else
                H = (byte) (2 * HslMax / 3 + gDelta - rDelta);

            if (H < 0)
                H += HslMax;

            if (H > HslMax)
                H -= HslMax;
        }

        #endregion

        #region Lightness Adjusting

        /// <summary>
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public HslColor Darken(float percent)
        {
            int adjustmentLuminosity = CalculateAdjustment(DarkenAdjustment);
            int newLuminosity = adjustmentLuminosity - (int) (adjustmentLuminosity * percent);
            return new HslColor(ColorFromHsl(H, S, newLuminosity));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public HslColor Dark()
        {
            return Darken(.5f);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public HslColor DarkDark()
        {
            return Darken(1f);
        }

        /// <summary>
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public HslColor Lighten(float percent)
        {
            int adjustmentLuminosity = CalculateAdjustment(LightenAdjustment);
            int newLuminosity = L + (int) ((adjustmentLuminosity - L) * percent);
            return new HslColor(ColorFromHsl(H, S, newLuminosity));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public HslColor Light()
        {
            return Lighten(.5f);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public HslColor LightLight()
        {
            return Lighten(1f);
        }

        #endregion

        #region Color Adjusting

        /// <summary>
        /// </summary>
        /// <param name="hue"></param>
        /// <returns></returns>
        public HslColor AdjustHue(int hue)
        {
            if (hue < 0 || hue > HslMax)
                throw new ArgumentOutOfRangeException("hue", hue, string.Format("The value must be between 0 and {0}.", HslMax));

            return new HslColor((byte) hue, S, L);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public HslColor Desaturate()
        {
            byte saturation = (byte) (S / DesaturationLevel);
            if (saturation < L)
                saturation = L;

            return new HslColor(H, saturation, L);
        }

        /// <summary>
        /// </summary>
        /// <param name="saturation"></param>
        /// <returns></returns>
        public HslColor AdjustSaturation(int saturation)
        {
            if (saturation < 0 || saturation > HslMax)
                throw new ArgumentOutOfRangeException("saturation", saturation, string.Format("The value must be between 0 and {0}.", HslMax));

            return new HslColor(H, (byte) saturation, L);
        }

        /// <summary>
        /// </summary>
        /// <param name="luminosity"></param>
        /// <returns></returns>
        public HslColor AdjustLuminosity(int luminosity)
        {
            if (luminosity < 0 || luminosity > HslMax)
                throw new ArgumentOutOfRangeException("luminosity", luminosity, string.Format("The value must be between 0 and {0}.", HslMax));

            return new HslColor(H, S, (byte) luminosity);
        }

        #endregion

        #region Conversion

        /// <summary>
        /// </summary>
        public Color ToColor()
        {
            if (this == Empty)
                return Color.Empty;

            return ColorFromHsl(H, S, L);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int ToArgb()
        {
            return ToColor().ToArgb();
        }

        #endregion

        #region Factory Properties

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor AliceBlue => new HslColor(Color.AliceBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor AntiqueWhite => new HslColor(Color.AntiqueWhite);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Aqua => new HslColor(Color.Aqua);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Aquamarine => new HslColor(Color.Aquamarine);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Azure => new HslColor(Color.Azure);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Beige => new HslColor(Color.Beige);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Bisque => new HslColor(Color.Bisque);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Black => new HslColor(Color.Black);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor BlanchedAlmond => new HslColor(Color.BlanchedAlmond);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Blue => new HslColor(Color.Blue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor BlueViolet => new HslColor(Color.BlueViolet);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Brown => new HslColor(Color.Brown);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor BurlyWood => new HslColor(Color.BurlyWood);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor CadetBlue => new HslColor(Color.CadetBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Chartreuse => new HslColor(Color.Chartreuse);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Chocolate => new HslColor(Color.Chocolate);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Coral => new HslColor(Color.Coral);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor CornflowerBlue => new HslColor(Color.CornflowerBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Cornsilk => new HslColor(Color.Cornsilk);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Crimson => new HslColor(Color.Crimson);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Cyan => new HslColor(Color.Cyan);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkBlue => new HslColor(Color.DarkBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkCyan => new HslColor(Color.DarkCyan);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkGoldenrod => new HslColor(Color.DarkGoldenrod);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkGray => new HslColor(Color.DarkGray);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkKhaki => new HslColor(Color.DarkKhaki);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkMagenta => new HslColor(Color.DarkMagenta);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkOliveGreen => new HslColor(Color.DarkOliveGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkOrange => new HslColor(Color.DarkOrange);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkRed => new HslColor(Color.DarkRed);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkSalmon => new HslColor(Color.DarkSalmon);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkSeaGreen => new HslColor(Color.DarkSeaGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkSlateBlue => new HslColor(Color.DarkSlateBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkSlateGray => new HslColor(Color.DarkSlateGray);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkTurquoise => new HslColor(Color.DarkTurquoise);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor DarkViolet => new HslColor(Color.DarkViolet);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Firebrick => new HslColor(Color.Firebrick);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor FloralWhite => new HslColor(Color.FloralWhite);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor ForestGreen => new HslColor(Color.ForestGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Fuchsia => new HslColor(Color.Fuchsia);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Gainsboro => new HslColor(Color.Gainsboro);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor GhostWhite => new HslColor(Color.GhostWhite);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Gold => new HslColor(Color.Gold);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Goldenrod => new HslColor(Color.Goldenrod);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Gray => new HslColor(Color.Gray);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Green => new HslColor(Color.Green);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor GreenYellow => new HslColor(Color.GreenYellow);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Honeydew => new HslColor(Color.Honeydew);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor HotPink => new HslColor(Color.HotPink);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor IndianRed => new HslColor(Color.IndianRed);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Indigo => new HslColor(Color.Indigo);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Ivory => new HslColor(Color.Ivory);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Khaki => new HslColor(Color.Khaki);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Lavender => new HslColor(Color.Lavender);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LavenderBlush => new HslColor(Color.LavenderBlush);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LawnGreen => new HslColor(Color.LawnGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LemonChiffon => new HslColor(Color.LemonChiffon);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightBlue => new HslColor(Color.LightBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightCoral => new HslColor(Color.LightCoral);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightCyan => new HslColor(Color.LightCyan);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightGoldenrodYellow => new HslColor(Color.LightGoldenrodYellow);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightGray => new HslColor(Color.LightGray);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightGreen => new HslColor(Color.LightGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightPink => new HslColor(Color.LightPink);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightSalmon => new HslColor(Color.LightSalmon);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightSeaGreen => new HslColor(Color.LightSeaGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightSkyBlue => new HslColor(Color.LightSkyBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightSlateGray => new HslColor(Color.LightSlateGray);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightSteelBlue => new HslColor(Color.LightSteelBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LightYellow => new HslColor(Color.LightYellow);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Lime => new HslColor(Color.Lime);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor LimeGreen => new HslColor(Color.LimeGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Linen => new HslColor(Color.Linen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Magenta => new HslColor(Color.Magenta);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Maroon => new HslColor(Color.Maroon);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumAquamarine => new HslColor(Color.MediumAquamarine);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumBlue => new HslColor(Color.MediumBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumOrchid => new HslColor(Color.MediumOrchid);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumPurple => new HslColor(Color.MediumPurple);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumSeaGreen => new HslColor(Color.MediumSeaGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumSlateBlue => new HslColor(Color.MediumSlateBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumSpringGreen => new HslColor(Color.MediumSpringGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumTurquoise => new HslColor(Color.MediumTurquoise);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MediumVioletRed => new HslColor(Color.MediumVioletRed);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MidnightBlue => new HslColor(Color.MidnightBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MintCream => new HslColor(Color.MintCream);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor MistyRose => new HslColor(Color.MistyRose);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Moccasin => new HslColor(Color.Moccasin);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor NavajoWhite => new HslColor(Color.NavajoWhite);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Navy => new HslColor(Color.Navy);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor OldLace => new HslColor(Color.OldLace);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Olive => new HslColor(Color.Olive);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor OliveDrab => new HslColor(Color.OliveDrab);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Orange => new HslColor(Color.Orange);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor OrangeRed => new HslColor(Color.OrangeRed);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Orchid => new HslColor(Color.Orchid);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PaleGoldenrod => new HslColor(Color.PaleGoldenrod);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PaleGreen => new HslColor(Color.PaleGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PaleTurquoise => new HslColor(Color.PaleTurquoise);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PaleVioletRed => new HslColor(Color.PaleVioletRed);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PapayaWhip => new HslColor(Color.PapayaWhip);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PeachPuff => new HslColor(Color.PeachPuff);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Peru => new HslColor(Color.Peru);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Pink => new HslColor(Color.Pink);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Plum => new HslColor(Color.Plum);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor PowderBlue => new HslColor(Color.PowderBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Purple => new HslColor(Color.Purple);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Red => new HslColor(Color.Red);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor RosyBrown => new HslColor(Color.RosyBrown);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor RoyalBlue => new HslColor(Color.RoyalBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SaddleBrown => new HslColor(Color.SaddleBrown);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Salmon => new HslColor(Color.Salmon);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SandyBrown => new HslColor(Color.SandyBrown);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SeaGreen => new HslColor(Color.SeaGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SeaShell => new HslColor(Color.SeaShell);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Sienna => new HslColor(Color.Sienna);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Silver => new HslColor(Color.Silver);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SkyBlue => new HslColor(Color.SkyBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SlateBlue => new HslColor(Color.SlateBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SlateGray => new HslColor(Color.SlateGray);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Snow => new HslColor(Color.Snow);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SpringGreen => new HslColor(Color.SpringGreen);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor SteelBlue => new HslColor(Color.SteelBlue);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Tan => new HslColor(Color.Tan);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Teal => new HslColor(Color.Teal);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Thistle => new HslColor(Color.Thistle);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Tomato => new HslColor(Color.Tomato);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Turquoise => new HslColor(Color.Turquoise);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Violet => new HslColor(Color.Violet);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Wheat => new HslColor(Color.Wheat);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor White => new HslColor(Color.White);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor WhiteSmoke => new HslColor(Color.WhiteSmoke);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor Yellow => new HslColor(Color.Yellow);

        /// <summary>
        ///     Gets a system defined <see cref="HslColor" />.
        /// </summary>
        public static HslColor YellowGreen => new HslColor(Color.YellowGreen);

        #endregion

        #region Private Helper Methods

        private static void VerifyByteParameter(int parameter, string name)
        {
            if (parameter < 0 || parameter > HslMax)
                throw new ArgumentOutOfRangeException(name, parameter, string.Format("The value must be between 0 and {0}.", HslMax));
        }

        private int CalculateAdjustment(int adjustmentValue)
        {
            return CalculateAdjustment(adjustmentValue, true);
        }

        private int CalculateAdjustment(int adjustmentValue, bool scaleColors)
        {
            return CalculateAdjustment(adjustmentValue, scaleColors, L);
        }

        private static int CalculateAdjustment(int adjustmentValue, bool scaleColors, int startingValue)
        {
            if (adjustmentValue == 0)
                return startingValue;

            if (scaleColors)
            {
                if (adjustmentValue > 0)
                    return (int) ((startingValue * (1000 - adjustmentValue) + 241 * adjustmentValue) / (long) 1000);
                return startingValue * (adjustmentValue + 1000) / 1000;
            }

            int newValue = startingValue;
            newValue += (int) (adjustmentValue * HslMax / (long) 1000);

            if (newValue < 0)
                newValue = 0;

            if (newValue > HslMax)
                newValue = HslMax;
            return newValue;
        }

        private static Color ColorFromHsl(int hue, int sat, int lum)
        {
            if (lum < 0) lum = 0;
            else if (lum > HslMax) lum = HslMax;

            byte red;
            byte green;
            byte blue;

            if (sat == 0)
            {
                red = green = blue = (byte) (lum * RgbMax / HslMax);
                if (hue == Undefined) { }
            }
            else
            {
                int tempSat;
                if (lum <= 120)
                    tempSat = (lum * (HslMax + sat) + 120) / HslMax;
                else
                    tempSat = lum + sat - (lum * sat + 120) / HslMax;

                int temp = 2 * lum - tempSat;
                red = (byte) ((HueToRGB(temp, tempSat, hue + 80) * RgbMax + 120) / HslMax);
                green = (byte) ((HueToRGB(temp, tempSat, hue) * RgbMax + 120) / HslMax);
                blue = (byte) ((HueToRGB(temp, tempSat, hue - 80) * RgbMax + 120) / HslMax);
            }
            return Color.FromArgb(red, green, blue);
        }

        private static int HueToRGB(int n1, int n2, int hue)
        {
            if (hue < 0)
                hue += HslMax;
            if (hue > HslMax)
                hue -= HslMax;
            if (hue < 40)
                return n1 + ((n2 - n1) * hue + 20) / 40;
            if (hue < 120)
                return n2;
            if (hue < Undefined)
                return n1 + ((n2 - n1) * (Undefined - hue) + 20) / 40;
            return n1;
        }

        #endregion

        #region Object Overrides

        /// <summary>
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o == null)
                return false;

            if (o is HslColor)
                return Equals((HslColor) o);

            return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="hlsColor"></param>
        /// <returns></returns>
        public bool Equals(HslColor hlsColor)
        {
            return H == hlsColor.H && S == hlsColor.S && L == hlsColor.L;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return (H << 6) | (S << 2) | L;
        }

        #endregion

        #region Operator Overloads

        /// <summary>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(HslColor a, HslColor b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(HslColor a, HslColor b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static explicit operator Color(HslColor color)
        {
            return color.ToColor();
        }

        /// <summary>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static explicit operator HslColor(Color color)
        {
            return FromColor(color);
        }

        #endregion
    }
}