namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for WebSiteFactory.
    /// </summary>
    internal static class WebsiteFactory
    {
        /// <summary>
        ///     Returns a <see cref="DefaultWebsite" /> representing the web site hosting the
        ///     specified domain name.
        /// </summary>
        /// <param name="domainName">Domain name of the website to open.</param>
        /// <returns></returns>
        internal static Website Open(string domainName = null)
        {
            if (Compare.IsNullOrEmpty(domainName))
                return new DefaultWebsite();
            return new NamedWebsite(domainName);
        }
    }
}