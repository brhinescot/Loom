#region Using Directives

using System;
using System.Web.UI;
using Enyim.Caching.Memcached;
using NorthScale.Store;

#endregion

namespace Loom.Web.Portal.WebTest
{
    public partial class MemCacheD : Page
    {
        private static readonly NorthScaleClient DistCache = new NorthScaleClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            string s = DistCache.Get<string>("Test");
            if (s == null)
            {
                s = "Test";
                Test.Text = "Not from cache: " + s;
                DistCache.Store(StoreMode.Set, "Test", s);
                return;
            }

            Test.Text = "From cache: " + s;
        }
    }
}