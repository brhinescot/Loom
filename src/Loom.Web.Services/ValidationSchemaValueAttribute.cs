#region Using Directives

using System;
using System.IO;
using System.Xml.Schema;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     A schema used to validate a web method. In practice only use
    ///     on client (proxy).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ValidationSchemaValueAttribute : Attribute
    {
        /// <summary>
        ///     Constructs schema from literal string
        /// </summary>
        /// <param name="xmlString">XML schema in the form of a literal string</param>
        public ValidationSchemaValueAttribute(string xmlString)
        {
            Argument.Assert.IsNotNull(xmlString, "xmlString");

            StringReader stringReader = new StringReader(xmlString);
            Schema = XmlSchema.Read(stringReader, null);
        }

        /// <summary>
        ///     Compiled schema used to validate message
        /// </summary>
        public XmlSchema Schema { get; set; }
    }
}