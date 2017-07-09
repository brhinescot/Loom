#region Using Directives

using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#endregion

namespace Loom.Drawing
{
    public class BoxTemplate : IXmlSerializable
    {
        public BoxTemplate(ImageTemplate imageTemplate)
        {
            Style = new StyleTemplate();
            Transform = new TransformTemplate();
            Container = imageTemplate;
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public StyleTemplate Style { get; set; }
        public TransformTemplate Transform { get; set; }
        public ImageTemplate Container { get; }
        public DockStyle Dock { get; set; }
        public int Margin { get; set; }

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
            Id = reader.GetAttribute("id");
            Text = reader.GetAttribute("text");

            string height = reader.GetAttribute("height");
            Height = height == "max" ? Container.Height : Convert.ToInt32(height);

            string width = reader.GetAttribute("width");
            Width = width == "max" ? Container.Width : Convert.ToInt32(width);

            Top = Convert.ToInt32(reader.GetAttribute("top"));
            Left = Convert.ToInt32(reader.GetAttribute("left"));
            Margin = Convert.ToInt32(reader.GetAttribute("margin"));

            switch (StyleTemplate.GetEnum<DockStyle>("dock", reader.GetAttribute("dock")))
            {
                case DockStyle.Top:
                    Top = 0;
                    Left = 0;
                    Width = Container.Width;
                    break;
                case DockStyle.Bottom:
                    Left = 0;
                    Top = Container.Height - Height;
                    Width = Container.Width;
                    break;
                case DockStyle.Left:
                    Left = 0;
                    Top = 0;
                    Height = Container.Height;
                    break;
                case DockStyle.Right:
                    Left = Container.Width - Width;
                    Top = 0;
                    Height = Container.Height;
                    break;
            }

            XmlReader subtree = reader.ReadSubtree();
            while (subtree.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                if (nodeType == XmlNodeType.EndElement)
                    break;

                if (reader.Name == "style")
                    ((IXmlSerializable) Style).ReadXml(reader);

                if (reader.Name == "transform")
                    ((IXmlSerializable) Transform).ReadXml(reader);
            }
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
            writer.WriteStartElement("box");

            writer.WriteAttributeString("id", Id);
            if (!string.IsNullOrEmpty(Text))
                writer.WriteAttributeString("text", Text);
            if (Height > 0)
                writer.WriteAttributeString("height", Height.ToString(CultureInfo.InvariantCulture));
            if (Width > 0)
                writer.WriteAttributeString("width", Width.ToString(CultureInfo.InvariantCulture));
            if (Top > 0)
                writer.WriteAttributeString("top", Top.ToString(CultureInfo.InvariantCulture));
            if (Left > 0)
                writer.WriteAttributeString("left", Left.ToString(CultureInfo.InvariantCulture));
            if (Dock != DockStyle.None)
                writer.WriteAttributeString("dock", Dock.ToString());

            ((IXmlSerializable) Style).WriteXml(writer);
            ((IXmlSerializable) Transform).WriteXml(writer);

            writer.WriteEndElement();
        }

        #endregion
    }
}