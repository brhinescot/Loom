#region Using Directives

using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Xml.Serialization;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Format extension used to customize automatic WSDL generation.
    /// </summary>
    [XmlFormatExtension("validation", ValidationNamespaces.DevInterop, typeof(InputBinding))]
    public class ValidationFormatExtension : ServiceDescriptionFormatExtension
    {
        /// <summary>
        ///     holds array of AssertAttributes (expression + description)
        /// </summary>
        private AssertAttribute[] assertions;

        /// <summary>
        ///     holds actual namespace declarations required by XPath expressions
        /// </summary>
        private XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

        /// <summary>
        ///     Used to determine whether or not to apply a schema validation
        /// </summary>
        private bool schemaValidation = true;

        /// <summary>
        ///     Accesses schemaValidataion
        /// </summary>
        [XmlAttribute("SchemaValidation")]
        public bool SchemaValidation
        {
            get => schemaValidation;
            set => schemaValidation = value;
        }

        /// <summary>
        ///     holds array of AssertAttributes (expression + description)
        /// </summary>
        [XmlArray("assertions")]
        [XmlArrayItem("assert")]
        public AssertAttribute[] Assertions
        {
            get => assertions;
            set => assertions = value;
        }

        /// <summary>
        ///     holds actual namespace declarations required by XPath expressions
        /// </summary>
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces SerializerNamespaces
        {
            get => namespaces;
            set => namespaces = value;
        }
    }
}