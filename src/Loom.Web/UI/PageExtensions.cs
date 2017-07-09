#region Using Directives

using System.IO;
using System.Web.UI;

#endregion

namespace Loom.Web.UI
{
    public static class PageExtensions
    {
        public static string GetFileName(this Page page)
        {
            return Path.GetFileName(page.Request.Url.AbsolutePath);
        }
    }
}