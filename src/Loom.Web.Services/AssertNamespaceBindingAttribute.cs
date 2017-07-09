#region Using Directives

using System;
using System.Xml.Serialization;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Specifies namespace bindings required by the XPath (assert)
    ///     expressions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AssertNamespaceBindingAttribute : Attribute
    {
        /// <summary>
        ///     Namespace uri to which prefix is bound
        /// </summary>
        private string _namespace;

        /// <summary>
        ///     prefix used to refer to a namespace in an XML document
        /// </summary>
        private string _prefix;

        /// <summary>
        ///     default constructor required of all attributes. In
        ///     practice not used.
        /// </summary>
        public AssertNamespaceBindingAttribute()
        {
            _prefix = string.Empty;
            _namespace = string.Empty;
        }

        /// <summary>
        ///     Constructs and attribute that binds a prefix to a namespace
        /// </summary>
        /// <param name="prefix">prefix being bound to a namespace</param>
        /// <param name="ns">Namespace</param>
        public AssertNamespaceBindingAttribute(string prefix, string ns)
        {
            _prefix = prefix;
            _namespace = ns;
        }

        /// <summary>
        ///     Accesses the _prefix and specifies XML serialization
        /// </summary>
        [XmlElement("prefix")]
        public string Prefix
        {
            get => _prefix;
            set => _prefix = value;
        }

        /// <summary>
        ///     Accesses the _namespace and specifies XML serialization
        /// </summary>
        [XmlElement("namespace")]
        public string Namespace
        {
            get => _namespace;
            set => _namespace = value;
        }
    }
}