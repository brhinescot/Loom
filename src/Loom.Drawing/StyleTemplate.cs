#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#endregion

namespace Loom.Drawing
{
    public class StyleTemplate : IXmlSerializable
    {
        public StyleTemplate()
        {
            Font = new Font(SystemFonts.DefaultFont, SystemFonts.DefaultFont.Style);
            ImageAlpha = 1;
        }

        public Font Font { get; private set; }
        public int BorderWidth { get; set; }
        public Color BorderColor { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public Color BackColor2 { get; set; }
        public int GradientAngle { get; set; }
        public int Rotation { get; set; }
        public string Image { get; set; }
        public ImageRepeat ImageRepeat { get; set; }
        public float ImageAlpha { get; set; }

        #region IXmlSerializable Members

        /// <summary>
        ///     This method is reserved, apply the
        ///     <see cref="XmlSchemaProviderAttribute">
        ///     </see>
        ///     to the class instead.
        /// </summary>
        /// <returns>
        ///     An <see cref="XmlSchema"></see> that describes the XML representation of the
        ///     object that is produced by the <see cref="IXmlSerializable.WriteXml(XmlWriter)"></see> method
        ///     and consumed by the <see cref="IXmlSerializable.ReadXml(XmlReader)"></see> method.
        /// </returns>
        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotSupportedException("This method is reserved, apply the XmlSchemaProviderAttribute to the class instead.");
        }

        /// <summary>
        ///     Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="XmlReader"></see> stream from which the object
        ///     is deserialized.
        /// </param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            for (int i = 0; i < reader.AttributeCount; i++)
            {
                reader.MoveToAttribute(i);
                SetProperty(reader.Name, reader.Value);
            }

            if (BackColor2 == Color.Empty)
                BackColor2 = BackColor;

            string fontFamilyValue = reader.GetAttribute("fontName");
            string fontSizeValue = reader.GetAttribute("fontSize");
            string fontStyleValue = reader.GetAttribute("fontStyle");

            int fontsize = !string.IsNullOrEmpty(fontSizeValue)
                ? Convert.ToInt32(fontSizeValue)
                : 0;

            FontStyle fontStyle = !string.IsNullOrEmpty(fontStyleValue)
                ? (FontStyle) Enum.Parse(typeof(FontStyle), fontStyleValue, true)
                : FontStyle.Regular;

            Font = new Font(
                !string.IsNullOrEmpty(fontFamilyValue) ? fontFamilyValue : SystemFonts.DefaultFont.Name,
                fontsize > 0 ? fontsize : SystemFonts.DefaultFont.Size,
                fontStyle);
        }

        /// <summary>
        ///     Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"></see> stream to which the object is
        ///     serialized.
        /// </param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            Dictionary<string, string> attributeValues = new Dictionary<string, string>();

            if (Font.Name != SystemFonts.DefaultFont.Name)
                attributeValues.Add("fontName", Font.Name);
            if (Font.Size != SystemFonts.DefaultFont.Size)
                attributeValues.Add("fontSize", Font.Size.ToString(CultureInfo.InvariantCulture));
            if (Font.Style != FontStyle.Regular)
                attributeValues.Add("fontStyle", Font.Style.ToString());
            if (BorderWidth > 0)
                attributeValues.Add("borderWidth", BorderWidth.ToString(CultureInfo.InvariantCulture));
            if (BorderColor != Color.Empty)
                attributeValues.Add("borderColor", BorderColor.Name);
            if (ForeColor != Color.Empty)
                attributeValues.Add("foreColor", ForeColor.Name);
            if (BackColor != Color.Empty)
                attributeValues.Add("backColor", BackColor.Name);
            if (BackColor2 != Color.Empty && BackColor != BackColor2)
                attributeValues.Add("backColor2", BackColor2.Name);
            if (GradientAngle > 0)
                attributeValues.Add("gradientAngle", GradientAngle.ToString(CultureInfo.InvariantCulture));
            if (Rotation != 0)
                attributeValues.Add("rotation", Rotation.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(Image))
                attributeValues.Add("image", Image);
            if (ImageRepeat != ImageRepeat.None)
                attributeValues.Add("imageRepeat", ImageRepeat.ToString());

            if (attributeValues.Count == 0)
                return;

            writer.WriteStartElement("style");
            foreach (KeyValuePair<string, string> attribute in attributeValues)
                writer.WriteAttributeString(attribute.Key, attribute.Value);
            writer.WriteEndElement();
        }

        #endregion

        private static int GetNumber(string name, object value)
        {
            if (value == null)
                return 0;

            int result;
            if (int.TryParse(value.ToString(), out result))
                return result;

            throw new FormatException("Invalid format for attribute " + name + ". Expected a number.");
        }

        private static float GetFloat(string name, object value)
        {
            if (value == null)
                return 0;

            float result;
            if (float.TryParse(value.ToString(), out result))
                return result;

            throw new FormatException("Invalid format for attribute " + name + ". Expected a floating point number less than or equal to 1.");
        }

        private static Color GetColor(string name, object value)
        {
            if (value == null)
                return Color.Empty;

            string colorText = value.ToString();

            Color color = ColorTranslator.FromHtml(colorText);
            if (!color.IsEmpty)
                return color;

            if (colorText.IndexOf(',') > 1)
            {
                string[] rgb = colorText.Split(',');
                if (rgb.Length == 3)
                    return Color.FromArgb(Convert.ToInt32(rgb[0].Trim()), Convert.ToInt32(rgb[1].Trim()), Convert.ToInt32(rgb[2].Trim()));
                if (rgb.Length == 4)
                    return Color.FromArgb(Convert.ToInt32(rgb[0].Trim()), Convert.ToInt32(rgb[1].Trim()), Convert.ToInt32(rgb[2].Trim()), Convert.ToInt32(rgb[3].Trim()));
            }

            throw new FormatException("Invalid format for attribute " + name + ". Expected a named or HTML color format.");
        }

        private static string GetString(object value)
        {
            return value == null ? null : value.ToString();
        }

        internal static T GetEnum<T>(string name, object value) where T : struct
        {
            if (value == null)
                return default(T);

            string enumText = value.ToString().ToUpperInvariant();
            if (enumText.Length == 0)
                return default(T);

            if (new List<string>(Enum.GetNames(typeof(T))).ConvertAll(input => input.ToUpperInvariant()).Contains(enumText))
                return (T) Enum.Parse(typeof(T), enumText, true);

            throw new FormatException("Invalid format for attribute " + name + ".");
        }

        private void SetProperty(string name, object value)
        {
            switch (name.ToUpperInvariant())
            {
                case "BORDERWIDTH":
                    BorderWidth = GetNumber(name, value);
                    break;
                case "GRADIENTANGLE":
                    GradientAngle = GetNumber(name, value);
                    break;
                case "BORDERCOLOR":
                    BorderColor = GetColor(name, value);
                    break;
                case "FORECOLOR":
                    ForeColor = GetColor(name, value);
                    break;
                case "BACKCOLOR":
                    BackColor = GetColor(name, value);
                    break;
                case "BACKCOLOR2":
                    BackColor2 = GetColor(name, value);
                    break;
                case "IMAGE":
                    Image = GetString(value);
                    break;
                case "IMAGEREPEAT":
                    ImageRepeat = GetEnum<ImageRepeat>(name, value);
                    break;
                case "IMAGEALPHA":
                    ImageAlpha = GetFloat(name, value);
                    break;
                case "ROTATION":
                    Rotation = GetNumber(name, value);
                    break;
            }
        }
    }
}