#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#endregion

namespace Loom.Drawing
{
    [XmlRoot("imageTemplates")]
    public class ImageTemplateCollection : IEnumerable<ImageTemplate>, IXmlSerializable
    {
        private readonly Dictionary<string, ImageTemplate> templates = new Dictionary<string, ImageTemplate>();

        public ImageTemplate this[string id] => !templates.ContainsKey(id) ? null : templates[id];

        #region IEnumerable<ImageTemplate> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ImageTemplate>) this).GetEnumerator();
        }

        public IEnumerator<ImageTemplate> GetEnumerator()
        {
            return ((IEnumerable<ImageTemplate>) templates.Values).GetEnumerator();
        }

        #endregion

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

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            do
            {
                ImageTemplate definition = new ImageTemplate(this);
                ((IXmlSerializable) definition).ReadXml(reader.ReadSubtree());
                templates.Add(definition.Id, definition);
            } while (reader.ReadToFollowing("imageTemplate"));
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            foreach (ImageTemplate definition in templates.Values)
            {
                writer.WriteStartElement("imageTemplate");
                ((IXmlSerializable) definition).WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        #endregion

        public static ImageTemplateCollection FromXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImageTemplateCollection));
            ImageTemplateCollection imageTemplates;
            using (StringReader reader = new StringReader(xml))
            {
                imageTemplates = (ImageTemplateCollection) serializer.Deserialize(reader);
            }
            return imageTemplates;
        }

        public string ToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImageTemplateCollection));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        public void Add(ImageTemplate definition)
        {
            templates.Add(definition.Id, definition);
        }
    }
}