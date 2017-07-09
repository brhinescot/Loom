#region Using Directives

using System;
using System.Drawing;
using NUnit.Framework;

#endregion

namespace Loom.Drawing
{
    [TestFixture]
    public class HslTests
    {
        [Test]
        public void FromColor()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);
            Assert.AreEqual(138, hsl.H);
            Assert.AreEqual(69, hsl.L);
            Assert.AreEqual(240, hsl.S);
        }

        [Test]
        public void FromColorGetColor()
        {
            Color color = Color.FromArgb(0, 81, 147);
            HslColor hsl = HslColor.FromColor(color);
            AssertNotEmpty(hsl);
            Assert.AreEqual(color, hsl.ToColor());
        }

        [Test]
        public void FromHsl()
        {
            HslColor hsl = HslColor.FromHsl(138, 240, 69);
            AssertNotEmpty(hsl);
            Assert.AreEqual(138, hsl.H);
            Assert.AreEqual(240, hsl.S);
            Assert.AreEqual(69, hsl.L);
        }

        [Test]
        public void FromHslGetColor()
        {
            HslColor hsl = HslColor.FromHsl(138, 240, 69);
            AssertNotEmpty(hsl);
            Assert.AreEqual(Color.FromArgb(0, 81, 147), hsl.ToColor());
        }

        [Test]
        public void FromName()
        {
            Color color = Color.FromName("Red");
            HslColor hsl = HslColor.FromName("Red");
            AssertNotEmpty(hsl);
            Assert.AreEqual(0, hsl.H);
            Assert.AreEqual(240, hsl.S);
            Assert.AreEqual(120, hsl.L);

            Color baseColor = hsl.ToColor();
            Assert.AreEqual(color.R, baseColor.R);
            Assert.AreEqual(color.G, baseColor.G);
            Assert.AreEqual(color.B, baseColor.B);
        }

        [Test]
        public void Lighten()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor color = hsl.Lighten(.5f);
            AssertNotEmpty(color);

            Color baseColor = color.ToColor();
            Assert.AreEqual(0, baseColor.R);
            Assert.AreEqual(131, baseColor.G);
            Assert.AreEqual(238, baseColor.B);
        }

        [Test]
        public void LightenMore()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor color = hsl.Lighten(1.0f);
            AssertNotEmpty(color);

            Color baseColor = color.ToColor();
            Assert.AreEqual(74, baseColor.R);
            Assert.AreEqual(174, baseColor.G);
            Assert.AreEqual(255, baseColor.B);
        }

        [Test]
        public void Darken()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            Assert.AreNotEqual(HslColor.Empty, hsl);

            HslColor darkenedHsl = hsl.Darken(.5f);
            AssertNotEmpty(darkenedHsl);

            Color darkenedColor = darkenedHsl.ToColor();
            AssertNotEmpty(darkenedHsl);
            Assert.AreEqual(0, darkenedColor.R);
            Assert.AreEqual(27, darkenedColor.G);
            Assert.AreEqual(49, darkenedColor.B);
        }

        [Test]
        public void DarkenMore()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor darkenedHsl = hsl.Darken(.8f);
            AssertNotEmpty(darkenedHsl);

            Color darkenedColor = darkenedHsl.ToColor();
            AssertNotEmpty(darkenedHsl);
            Assert.AreEqual(0, darkenedColor.R);
            Assert.AreEqual(13, darkenedColor.G);
            Assert.AreEqual(21, darkenedColor.B);
        }

        [Test]
        public void AdjustHue()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor adjusted = hsl.AdjustHue(200);
            Color color = adjusted.ToColor();
            Assert.AreEqual(147, color.R);
            Assert.AreEqual(0, color.G);
            Assert.AreEqual(147, color.B);

            Assert.AreEqual(200, adjusted.H);
            Assert.AreEqual(240, adjusted.S);
            Assert.AreEqual(69, adjusted.L);
        }

        [Test]
        public void AdjustSaturation()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor adjusted = hsl.AdjustSaturation(200);
            Color color = adjusted.ToColor();
            Assert.AreEqual(12, color.R);
            Assert.AreEqual(80, color.G);
            Assert.AreEqual(135, color.B);

            Assert.AreEqual(138, adjusted.H);
            Assert.AreEqual(200, adjusted.S);
            Assert.AreEqual(69, adjusted.L);
        }

        [Test]
        public void AdjustHueAndSaturationInline()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor adjusted = hsl.AdjustHue(200).AdjustSaturation(200);
            Color color = adjusted.ToColor();
            Assert.AreEqual(135, color.R);
            Assert.AreEqual(12, color.G);
            Assert.AreEqual(135, color.B);

            Assert.AreEqual(200, adjusted.H);
            Assert.AreEqual(200, adjusted.S);
            Assert.AreEqual(69, adjusted.L);
        }

        [Test]
        public void AdjustLuminosity()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            AssertNotEmpty(hsl);

            HslColor adjusted = hsl.AdjustLuminosity(200);
            Color color = adjusted.ToColor();
            Assert.AreEqual(170, color.R);
            Assert.AreEqual(217, color.G);
            Assert.AreEqual(255, color.B);

            Assert.AreEqual(138, adjusted.H);
            Assert.AreEqual(240, adjusted.S);
            Assert.AreEqual(200, adjusted.L);
        }

        [Test]
        public void EqualityTrueTest()
        {
            HslColor hls1 = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            HslColor hls2 = HslColor.FromColor(Color.FromArgb(0, 81, 147));

            Assert.AreEqual(hls1, hls2);
            Assert.IsTrue(hls1.Equals(hls2));
            Assert.IsTrue(hls1 == hls2);
        }

        [Test]
        public void EqualityFalseTest()
        {
            HslColor hls1 = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            HslColor hls2 = HslColor.FromColor(Color.FromArgb(147, 81, 0));

            Assert.AreNotEqual(hls1, hls2);
            Assert.IsFalse(hls1.Equals(hls2));
            Assert.IsTrue(hls1 != hls2);
        }

        [Test]
        public void EmptyHslColor()
        {
            HslColor color = new HslColor();
            AssertEmpty(color);
        }

        [Test]
        public void ExplicitCastToHsl()
        {
            Color argb = Color.FromArgb(0, 81, 147);
            HslColor hsl = (HslColor) argb;

            AssertNotEmpty(hsl);
            Assert.AreEqual(138, hsl.H);
            Assert.AreEqual(69, hsl.L);
            Assert.AreEqual(240, hsl.S);
        }

        [Test]
        public void ExplicitCastToColor()
        {
            HslColor hsl = HslColor.FromHsl(138, 240, 69);
            Color color = (Color) hsl;

            AssertNotEmpty(hsl);
            Assert.AreEqual(color, hsl.ToColor());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdjustHueOutOfRangeHigh()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            hsl.AdjustHue(256);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdjustHueOutOfRangeLow()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            hsl.AdjustHue(-1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdjustSaturationOutOfRangeHigh()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            hsl.AdjustSaturation(256);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdjustSaturationOutOfRangeLow()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            hsl.AdjustSaturation(-1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdjustLuminosityOutOfRangeHigh()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            hsl.AdjustLuminosity(256);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdjustLuminosityOutOfRangeLow()
        {
            HslColor hsl = HslColor.FromColor(Color.FromArgb(0, 81, 147));
            hsl.AdjustLuminosity(-1);
        }

        private static void AssertNotEmpty(HslColor color)
        {
            Assert.AreNotEqual(HslColor.Empty, color);
        }

        private static void AssertEmpty(HslColor color)
        {
            Assert.AreEqual(HslColor.Empty, color);
        }
    }
}