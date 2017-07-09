#region Using Directives

using System.Web;
using System.Web.Hosting;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Registers the <see cref="AssemblyResourceProvider" /> as a virtual path provider,
    ///     enabling usage of the RegistrationWizard.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to register the module in the
    ///     applications web.config file.
    ///     <code>
    /// <![CDATA[
    /// <system.web>
    ///   <httpModules>
    ///     <add name="AssemblyResourceProviderModule" type="Loom.Web.AssemblyResourceProviderModule, Loom.Web"/>
    ///   </httpModules>
    /// [...]
    /// <system.web>
    /// ]]>
    /// </code>
    /// </example>
    public class AssemblyResourceProviderModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());
        }

        public void Dispose() { }

        #endregion
    }
}