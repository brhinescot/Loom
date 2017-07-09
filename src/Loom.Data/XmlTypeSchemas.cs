#region Using Directives

using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Schema;

#endregion

namespace Loom.Data
{
    public sealed class XmlTypeSchemas
    {
        private const string MsXmlns = "http://microsoft.com/wsdl/types/";
        private const string W3Xmlns = "http://www.w3.org/2001/XMLSchema";
        private const string GuidPattern = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";

        private XmlSchema guidSchema;

        /// <summary>
        ///     Retrieves a list of <see cref="XmlSchema" /> objects required by any
        ///     <see cref="System.Xml.Schema.XmlSchemaObject" />
        ///     objects retrieved from this instance.
        /// </summary>
        public List<XmlSchema> RequiredSchemas { get; } = new List<XmlSchema>();

        private XmlSchema GuidSchema
        {
            get
            {
                if (guidSchema != null)
                    return guidSchema;

                guidSchema = new XmlSchema {TargetNamespace = MsXmlns};

                XmlSchemaSimpleTypeRestriction guidRestriction = new XmlSchemaSimpleTypeRestriction
                {
                    BaseTypeName = new XmlQualifiedName("string", XmlSchema.Namespace)
                };

                XmlSchemaPatternFacet guidPattern = new XmlSchemaPatternFacet {Value = GuidPattern};
                guidRestriction.Facets.Add(guidPattern);

                XmlSchemaSimpleType guidType = new XmlSchemaSimpleType {Name = "Guid", Content = guidRestriction};
                guidSchema.Items.Add(guidType);

                return guidSchema;
            }
        }

        public XmlSchemaObject RetrieveXmlSchemaObject(string columnName, ColumnProperties properties, DbType dbType, bool allowNillable)
        {
            XmlSchemaElement element = new XmlSchemaElement
            {
                Name = columnName,
                IsNillable = allowNillable && (properties & ColumnProperties.Nullable) == ColumnProperties.Nullable
            };

            element.MinOccurs = element.IsNillable ? 0 : 1;
            element.MaxOccurs = 1;

            // TODO: Add remaining xml schema object types.
            switch (dbType)
            {
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    element.SchemaTypeName = new XmlQualifiedName("string", W3Xmlns);
                    break;
                case DbType.Boolean:
                    element.SchemaTypeName = new XmlQualifiedName("boolean", W3Xmlns);
                    break;
                case DbType.Int16:
                    element.SchemaTypeName = new XmlQualifiedName("short", W3Xmlns);
                    break;
                case DbType.Int32:
                    element.SchemaTypeName = new XmlQualifiedName("int", W3Xmlns);
                    break;
                case DbType.Int64:
                    element.SchemaTypeName = new XmlQualifiedName("long", W3Xmlns);
                    break;
                case DbType.Currency:
                case DbType.Decimal:
                    element.SchemaTypeName = new XmlQualifiedName("decimal", W3Xmlns);
                    break;
                case DbType.DateTime:
                case DbType.Date:
                    element.SchemaTypeName = new XmlQualifiedName("dateTime", W3Xmlns);
                    break;
                case DbType.Guid:
                    if (!RequiredSchemas.Contains(GuidSchema))
                        RequiredSchemas.Add(GuidSchema);
                    element.SchemaTypeName = new XmlQualifiedName("Guid", MsXmlns);
                    break;
                default:
                    element.SchemaTypeName = new XmlQualifiedName("string", W3Xmlns);
                    break;
            }

            return element;
        }
    }
}