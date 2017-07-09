#region Using Directives

using System.Web;

#endregion

namespace Loom.Web
{
    public class VirtualImageProviderModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            HttpExtensions.RegisterVirtualPathProvider(new VirtualImageProvider());
        }

        public void Dispose() { }

        #endregion
    }
}