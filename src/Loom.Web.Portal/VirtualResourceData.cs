#region Using Directives

using Loom.Web.Portal.Configuration;

#endregion

namespace Loom.Web.Portal
{
    /// <summary>
    ///     Represents the data needed to serve files stored as embedded resources.
    /// </summary>
    /// <remarks>
    ///     This class is not intended to be instantiated by developers. Instead, virtual resources are
    ///     configured in the web.config file as shown in the example.
    /// </remarks>
    /// <example>
    ///     The following example demonstrates how to configure virtual resources using the
    ///     <see cref="PortalSettingsSection" /> configuration section.
    ///     <code>
    /// <![CDATA[
    /// <portalSettings>
    ///   <virtualResources>
    ///     <add name="MyResources" namespace="My.Namespace.Resources" assembly="My.Assembly"/> 
    ///   </virtualResources>
    /// </portalSettings>
    /// ]]>
    /// </code>
    ///     A new handlers entry must be also added to the <c>system.webServer</c> configuration section.
    ///     The path should contain a subdirectory with the same name as the <c>name</c> attribute in the
    ///     <see cref="PortalSettingsSection" /> configuration section as demonstrated above. In this
    ///     example, the name is <b>MyResources</b>.
    ///     <code>
    /// <![CDATA[
    /// <system.webServer>
    ///   <handlers>
    ///     <add name="MyVirtualImage" path="*/imageresource/MyResources/*.*" verb="GET" type="System.Web.StaticFileHandler, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
    ///   </handlers>
    /// </system.webServer>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class VirtualResourceData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirtualResourceData" /> class.
        /// </summary>
        /// <param name="resourceNamespace">
        ///     The namespace containing the virtual resources.
        /// </param>
        /// <param name="assembly">
        ///     The name of the assembly that contains the resources.
        /// </param>
        public VirtualResourceData(string resourceNamespace, string assembly)
        {
            Namespace = resourceNamespace;
            Assembly = assembly;
        }

        /// <summary>
        ///     The name of the assembly that contains the resources.
        /// </summary>
        public string Assembly { get; }

        /// <summary>
        ///     The namespace containing the virtual resources.
        /// </summary>
        public string Namespace { get; }
    }
}