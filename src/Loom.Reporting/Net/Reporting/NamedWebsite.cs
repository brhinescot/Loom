#region Using Directives

using System;
using System.DirectoryServices;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for NamedWebSite.
    /// </summary>
    internal sealed class NamedWebsite : Website
    {
        private string logDirectory;

        private string logDirectoryRoot;
        private string webSiteNumber;

        internal NamedWebsite(string domainName)
        {
            using (DirectoryEntry iisWeb = new DirectoryEntry("IIS://localhost/W3SVC"),
                foundWeb = FindWebSite(iisWeb, domainName))
            {
                if (foundWeb == null)
                    throw
                        new DomainNotFoundException(
                            domainName, "A web with a host header containing '" + domainName + "' could not be found.");

                SetDirectoryProperties(foundWeb);
            }
        }

        internal override string LogDirectory
        {
            get
            {
                if (logDirectory == null)
                    logDirectory = string.Format(@"{0}\W3SVC{1}", logDirectoryRoot, WebsiteNumber);
                return logDirectory;
            }
        }

        internal override string WebsiteNumber => webSiteNumber;

        private static DirectoryEntry FindWebSite(DirectoryEntry iisWeb, string domainName)
        {
            foreach (DirectoryEntry entry in iisWeb.Children)
            {
                try
                {
                    foreach (string binding in entry.Properties["ServerBindings"])
                        if (binding.IndexOf(domainName) >= 0)
                            return entry;
                }
                catch (Exception) { }
                entry.Dispose();
            }
            return null;
        }

        private void SetDirectoryProperties(DirectoryEntry entry)
        {
            logDirectoryRoot = entry.Properties["LogFileDirectory"].Value.ToString();
            webSiteNumber = entry.Name;
        }
    }
}