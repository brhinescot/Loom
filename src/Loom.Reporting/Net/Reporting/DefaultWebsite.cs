#region Using Directives

using System.DirectoryServices;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for DefaultWebSite.
    /// </summary>
    internal sealed class DefaultWebsite : Website
    {
        private string logDirectory;
        private string logDirectoryRoot;

        internal DefaultWebsite()
        {
            using (DirectoryEntry foundWeb = new DirectoryEntry("IIS://localhost/W3SVC/1"))
            {
                SetDirectoryProperties(foundWeb);
            }
        }

        internal override string LogDirectory
        {
            get
            {
                if (logDirectory == null)
                    logDirectory = string.Format(@"{0}\W3SVC1", logDirectoryRoot);
                return logDirectory;
            }
        }

        internal override string WebsiteNumber => "1";

        private void SetDirectoryProperties(DirectoryEntry entry)
        {
            logDirectoryRoot = entry.Properties["LogFileDirectory"].Value.ToString();
        }
    }
}