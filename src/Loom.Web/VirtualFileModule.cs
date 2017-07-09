#region Using Directives

using System.Web;

#endregion

namespace Loom.Web
{
    public class VirtualFileModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            HttpExtensions.RegisterVirtualPathProvider(new VirtualFileFactory());
        }

        public void Dispose() { }

        #endregion
    }
}