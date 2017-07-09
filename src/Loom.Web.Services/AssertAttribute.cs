#region Using Directives

using System;
using System.Xml.Serialization;
using System.Xml.XPath;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Specifies an XPath expression evaluated as an assertion.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Must evaluate to true.
    ///     </para>
    ///     <para>
    ///         Can be used on an individual method or the entire class - if
    ///         used on the class, the expression applies to all WebMethods
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class AssertAttribute : Attribute
    {
        /// <summary>
        ///     string to be returned in error message if this assertion is not true.
        /// </summary>
        private string description;

        /// <summary>
        ///     Compiled XMPath expression for rule.
        /// </summary>
        private XPathExpression expression;

        /// <summary>
        ///     string that represents an XPath expression
        /// </summary>
        private string rule;

        /// <summary>
        ///     Required default constructor that all attributes require.
        ///     In practice this is not used
        /// </summary>
        public AssertAttribute() : this(string.Empty, string.Empty) { }

        /// <summary>
        ///     Construction an assertion
        /// </summary>
        /// <param name="rule">string that repesents XPath expression</param>
        /// <param name="description">
        ///     message to be passed back in error message when
        ///     assertion is not true
        /// </param>
        public AssertAttribute(string rule, string description)
        {
            this.rule = rule;
            this.description = description;
            expression = null;
        }

        /// <summary>
        ///     Constructs Assert with no error description.
        /// </summary>
        /// <remarks>
        ///     In practice this should never be used.
        /// </remarks>
        /// <param name="rule"></param>
        public AssertAttribute(string rule) : this(rule, string.Empty) { }

        /// <summary>
        ///     Provides expression specification of XML serialization
        /// </summary>
        [XmlElement("expression")]
        public string Rule
        {
            get => rule;
            set => rule = value;
        }

        /// <summary>
        ///     Provides description specification of XML serialization
        /// </summary>
        [XmlElement("description")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        /// <summary>
        ///     Accesses _expression
        /// </summary>
        internal XPathExpression Expression
        {
            get => expression;
            set => expression = value;
        }
    }
}