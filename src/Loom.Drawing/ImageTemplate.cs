#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#endregion

namespace Loom.Drawing
{
    [XmlRoot("imageTemplate")]
    public class ImageTemplate : IXmlSerializable
    {
        public ImageTemplate() : this(null) { }

        public ImageTemplate(ImageTemplateCollection parentCollection)
        {
            ParentCollection = parentCollection;
            Canvas = new Collection<BoxTemplate>();
            Style = new StyleTemplate();
            Transform = new TransformTemplate();
        }

        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Collection<BoxTemplate> Canvas { get; }
        public StyleTemplate Style { get; }
        public TransformTemplate Transform { get; }
        public string Inherits { get; private set; }
        private ImageTemplateCollection ParentCollection { get; }

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
        ///     and consumed by the <see cref="IXmlSerializable.ReadXml"></see> method.
        /// </returns>
        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotSupportedException("This method is reserved, apply the XmlSchemaProviderAttribute to the class instead.");
        }

        /// <summary>
        ///     Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="XmlReader"></see> stream from which
        ///     the object is deserialized.
        /// </param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if ((reader.NodeType == XmlNodeType.None || reader.Name != "imageTemplate") && !reader.ReadToFollowing("imageTemplate"))
                throw new InvalidOperationException("Expected element 'imageTemplate'.");

            Id = reader.GetAttribute("id");
            if (string.IsNullOrEmpty(Id))
                throw new InvalidOperationException("The required attribute 'Id' was not found.");

            string width = reader.GetAttribute("width");
            if (!string.IsNullOrEmpty(width))
                Width = Convert.ToInt32(width);

            string height = reader.GetAttribute("height");
            if (!string.IsNullOrEmpty(height))
                Height = Convert.ToInt32(height);

            string inherits = reader.GetAttribute("inherits");
            if (!string.IsNullOrEmpty(inherits))
                Inherits = inherits;

            if (reader.ReadToDescendant("style"))
            {
                ((IXmlSerializable) Style).ReadXml(reader);
                if (!reader.ReadToFollowing("canvas"))
                    throw new InvalidOperationException("Expected the element 'canvas' to follow the 'imageTemplate' and 'style' elements.");
            }

            if (reader.Name != "canvas")
                throw new InvalidOperationException(
                    "Expected the element 'style' or 'canvas' to follow the 'imageTemplate' element.");

            while (reader.Read() && reader.Name == "box")
            {
                BoxTemplate box = new BoxTemplate(this);
                Canvas.Add(box);
                ((IXmlSerializable) box).ReadXml(reader);
            }
        }

        /// <summary>
        ///     Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"></see> stream to which the
        ///     object is serialized.
        /// </param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            AddRootAttributes(writer);
            ((IXmlSerializable) Style).WriteXml(writer);
            AddAreas(writer);
        }

        #endregion

        internal ImageTemplate GetInherited()
        {
            if (ParentCollection == null || string.IsNullOrEmpty(Inherits))
                return null;

            ImageTemplate inherited = ParentCollection[Inherits];
            if (inherited == null)
                throw new InvalidOperationException(string.Format(" Unable to render the template with Id '{1}'. An inherited template with the Id '{0}' could not be found.", Inherits, Id));

            return inherited;
        }

        public static ImageTemplate FromXml(string xml)
        {
            Argument.Assert.IsNotNullOrEmpty(xml, nameof(xml));

            XmlSerializer serializer = new XmlSerializer(typeof(ImageTemplate));
            ImageTemplate imageTemplate;
            using (StringReader reader = new StringReader(xml))
            {
                imageTemplate = (ImageTemplate) serializer.Deserialize(reader);
            }
            return imageTemplate;
        }

        public string ToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImageTemplate));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        private void AddRootAttributes(XmlWriter writer)
        {
            writer.WriteAttributeString("id", Id);

            if (Width > 0)
                writer.WriteAttributeString("width", Width.ToString(CultureInfo.InvariantCulture));
            if (Height > 0)
                writer.WriteAttributeString("height", Height.ToString(CultureInfo.InvariantCulture));
            if (Inherits != null)
                writer.WriteAttributeString("inherits", Inherits);
        }

        private void AddAreas(XmlWriter writer)
        {
            writer.WriteStartElement("canvas");
            foreach (BoxTemplate area in Canvas)
                ((IXmlSerializable) area).WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}