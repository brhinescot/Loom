#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

#endregion

namespace Loom.Web.IO
{
    /*
     * Cache is a singleton objects that keeps track of cached urls.
     * Cache retrieves and validates configuration and creates
     * Tracker objects for each configured url.
     * 
     * Module calls into Cache on every request to lookup the Tracker
     * 
     */

    internal class DiskOutputCache : IRegisteredObject
    {
        // statics related to cache initialization
        private static readonly object initLock = new object();

        private static Exception initError;
        private static DiskOutputCache theCache;
        private readonly LinkedList<ScavangerEntry> fileRemovalList = new LinkedList<ScavangerEntry>();

        // table of trackers per path
        private readonly Dictionary<string, DiskOutputCacheTracker> trackers = new Dictionary<string, DiskOutputCacheTracker>(StringComparer.OrdinalIgnoreCase);

        // config settings
        private TimeSpan fileRemovalDelay;

        private TimeSpan fileScavangingDelay;
        private TimeSpan fileValidationDelay;
        private string location;

        // scavanging
        private Timer scavangingTimer;

        // shutdown
        private volatile bool shuttingDown;

        private int varyByLimit;

        internal static TimeSpan FileValidationDelay => theCache.fileValidationDelay;

        internal static int VaryByLimit => theCache.varyByLimit;

        internal static string Location => theCache.location;

        internal static bool ShuttingDown => theCache.shuttingDown;

        #region IRegisteredObject Members

        void IRegisteredObject.Stop(bool immediate)
        {
            if (immediate)
            {
                // wait for scavanging to complete
                while (!DiskOutputCacheTracker.ScavangingCompleted)
                    Thread.Sleep(50);

                HostingEnvironment.UnregisterObject(this);
            }
            else
            {
                shuttingDown = true;

                Cleanup();

                // delay unregistration if the initial scavanging is still in progress
                if (DiskOutputCacheTracker.ScavangingCompleted)
                    HostingEnvironment.UnregisterObject(this);
            }
        }

        #endregion

        public static void EnsureInitialized()
        {
            lock (initLock)
            {
                if (theCache == null)
                {
                    theCache = new DiskOutputCache();

                    try
                    {
                        theCache.Startup();
                    }
                    catch (Exception e)
                    {
                        initError = e;
                        throw;
                    }

                    HostingEnvironment.RegisterObject(theCache);
                }

                CheckInitialized();
            }
        }

        private static void CheckInitialized()
        {
            if (theCache == null)
                throw new InvalidOperationException("Cache is not initialized");

            if (initError != null)
                throw new InvalidOperationException("Cache failed to initialize", initError);
        }

        public static DiskOutputCacheTracker Lookup(HttpContext context)
        {
            CheckInitialized();
            return theCache.LookupTracker(context);
        }

        public static void ScheduleFileDeletion(string filename)
        {
            theCache.AddToRemovalList(filename);
            theCache.ScheduleScavanger();
        }

        private void Startup()
        {
            DiskOutputCacheSettingsSection config = (DiskOutputCacheSettingsSection)
                WebConfigurationManager.GetWebApplicationSection("diskOutputCacheSettings");

            if (config == null)
                return;

            // remember deletion delay
            fileRemovalDelay = config.FileRemovalDelay;

            // remember validation delay
            fileValidationDelay = config.FileValidationDelay;

            // remember scavanging delay
            fileScavangingDelay = config.FileScavangingDelay;

            // vary by limit (max count of entries per url)
            varyByLimit = config.VaryByLimitPerUrl;

            // calculate location
            if (Compare.IsNullOrEmpty(config.Location))
            {
                // the default location is in temporary asp.net files
                location = Path.Combine(HttpRuntime.CodegenDir, "DiskOutputCache");

                if (!Directory.Exists(location))
                    Directory.CreateDirectory(location);
            }
            else
            {
                location = config.Location;

                if (!Directory.Exists(location))
                    throw new InvalidDataException(string.Format("Invalid location '{0}'", location));
            }

            // make sure we can write to the location
            try
            {
                string testFile = Path.Combine(location, "test" + DateTime.UtcNow.ToFileTime() + ".txt");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
            }
            catch (Exception)
            {
                throw new InvalidDataException(string.Format("Invalid location '{0}' -- failed to write", location));
            }

            // read settings for individual URls
            foreach (CachedUrlsElement e in config.CachedUrls)
            {
                // normalize path

                string path = e.Path;

                if (!VirtualPathUtility.IsAppRelative(path) && !VirtualPathUtility.IsAbsolute(path))
                    throw new InvalidDataException(string.Format("Invalid path '{0}', absolute or app-relative path expected", path));

                path = VirtualPathUtility.ToAbsolute(path);

                // create file path prefix for this path
                string relPathPrefix = VirtualPathUtility.ToAppRelative(path);
                if (relPathPrefix.StartsWith("~/"))
                    relPathPrefix = relPathPrefix.Substring(2);
                relPathPrefix = relPathPrefix.Replace('.', '_');
                relPathPrefix = relPathPrefix.Replace('/', '_');

                // list of verbs
                string[] verbs = ParseStringList(e.Verbs);
                if (verbs.Length == 0)
                    throw new InvalidDataException(string.Format("Invalid list of verbs '{0}'", e.Verbs));

                // vary-by
                string[] varyBy = ParseStringList(e.VaryBy);

                // remember the tracker object
                trackers[path] = new DiskOutputCacheTracker(path, Path.Combine(location, relPathPrefix), e.Duration,
                    verbs, varyBy, e.EmptyQueryStringOnly, e.EmptyPathInfoOnly, e.ServeFromMemory);
            }

            if (CheckIfNeedToScavangeFilesThisTimeOnAppDomainStarted())
                DiskOutputCacheTracker.ScavangeFilesOnAppDomainStartup();
        }

        private void Cleanup()
        {
            Timer t = scavangingTimer;
            if (t != null)
                t.Dispose();
        }

        private DiskOutputCacheTracker LookupTracker(HttpContext context)
        {
            HttpRequest request = context.Request;
            DiskOutputCacheTracker tracker;

            if (trackers.TryGetValue(request.FilePath, out tracker))
                return tracker.FindTrackerForRequest(request);

            // not cacheable
            return null;
        }

        private bool CheckIfNeedToScavangeFilesThisTimeOnAppDomainStarted()
        {
            // keep the timestamp of the last disk scavanging on disk
            string scavangingTimestampFilename = Path.Combine(location, "scavanger.timestamp");

            // try to read it from disk
            try
            {
                if (File.Exists(scavangingTimestampFilename))
                {
                    DateTime lastScavangingUtc = DateTime.FromBinary(long.Parse(File.ReadAllText(scavangingTimestampFilename)));

                    if (DateTime.UtcNow < lastScavangingUtc + fileScavangingDelay)
                        return false;
                }
            }
            catch (IOException) { }
            catch (ArgumentException) { }

            // update the timestamp on disk
            try
            {
                File.WriteAllText(scavangingTimestampFilename, DateTime.UtcNow.ToBinary().ToString());
            }
            catch (IOException) { }
            catch (ArgumentException) { }

            return true;
        }

        private void AddToRemovalList(string filename)
        {
            ScavangerEntry entry = new ScavangerEntry(filename, DateTime.UtcNow.Add(fileRemovalDelay));

            lock (fileRemovalList)
            {
                fileRemovalList.AddLast(entry);
            }
        }

        private void ScheduleScavanger()
        {
            if (scavangingTimer != null)
                return;

            int timeout = (int) (fileRemovalDelay.TotalMilliseconds * 1.1);

            lock (this)
            {
                if (scavangingTimer == null)
                    scavangingTimer = new Timer(ScavangerCallback, null, timeout, Timeout.Infinite);
            }
        }

        private void ScavangerCallback(object state)
        {
            int numSkipped = 0;

            try
            {
                DateTime utcNow = DateTime.UtcNow;
                List<string> filesToBeDeleted = new List<string>();

                lock (fileRemovalList)
                {
                    LinkedListNode<ScavangerEntry> next = fileRemovalList.First;

                    while (next != null)
                    {
                        LinkedListNode<ScavangerEntry> current = next;
                        next = current.Next;

                        if (utcNow > current.Value.UtcDelete)
                        {
                            filesToBeDeleted.Add(current.Value.Filename);
                            fileRemovalList.Remove(current);
                        }
                        else
                        {
                            numSkipped++;
                        }
                    }
                }

                // delete the files outside of the lock
                foreach (string filename in filesToBeDeleted)
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException)
                    {
                        // move to the next file if one cannot be deleted
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // move to the next file if one cannot be deleted
                    }
                    catch (ArgumentException)
                    {
                        // move to the next file if one cannot be deleted
                    }
            }
            finally
            {
                scavangingTimer = null;
            }

            if (numSkipped > 0)
                ScheduleScavanger();
        }

        private static string[] ParseStringList(string listAsString)
        {
            string[] list = listAsString.Trim().Split(',');
            string[] result = new string[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                string s = list[i].Trim();
                if (s.Length > 0)
                    result[i] = s;
            }

            return result;
        }

        #region Nested type: ScavangerEntry

        // helper class to hold the data file name and the removal time
        private class ScavangerEntry
        {
            internal ScavangerEntry(string filename, DateTime utcDelete)
            {
                Filename = filename;
                UtcDelete = utcDelete;
            }

            internal string Filename { get; }

            internal DateTime UtcDelete { get; }
        }

        #endregion
    }
}