#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#endregion

namespace Loom.Drawing
{
    public class TransformTemplate : IXmlSerializable
    {
        public TransformTemplate()
        {
            Rotate = 0;
        }

        public int Rotate { get; set; }

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

            if (Rotate > 0)
                attributeValues.Add("rotate", Rotate.ToString(CultureInfo.InvariantCulture));

            if (attributeValues.Count == 0)
                return;

            writer.WriteStartElement("transform");
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

        private void SetProperty(string name, string value)
        {
            switch (name.ToUpperInvariant())
            {
                case "ROTATE":
                    Rotate = GetNumber(name, value);
                    break;
            }
        }
    }
}