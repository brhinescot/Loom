#region Using Directives

using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Xml.Serialization;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Used to add extsibility element to binding
    /// </summary>
    [XmlFormatExtension("validationNS", ValidationNamespaces.DevInterop, typeof(Binding))]
    public class NamespaceFormatExtension : ServiceDescriptionFormatExtension
    {
        /// <summary>
        ///     holds array of namespace binding attributes
        /// </summary>
        [XmlArray("namespaces")]
        [XmlArrayItem("namespace")]
        public AssertNamespaceBindingAttribute[] Namespaces { get; set; }
    }
}