#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Xml;

#endregion

namespace Loom.Web.IO
{
    /*
     * Tracker represents individual cacheable url
     * 
     * On disk the information is stored in 2 files: .info and .data
     * Each file has page url prefix, plus a unique hash-code to avoid
     * conflicts.
     * 
     * Tracker is responsible for capturing and serving cached data.
     * 
     * To implement support for vary-by caching each top-level Tracker
     * associated with a path keeps a dictionary of vary-by trackers
     * 
     */

    internal class DiskOutputCacheTracker
    {
        // strings
        private const string InfoFileExt = ".info";

        private const string DataFileExt = ".data";
        private const string TempFileExt = ".temp";
        private const string InfoTagName = "diskOutputCacheItem";

        // static to indicate the completion of scavanging on app domain startup
        private static volatile bool scavangingCompleted;

        private readonly ManualResetEvent capturingEvent;
        private readonly TimeSpan duration;
        private readonly bool emptyPathInfoOnly;
        private readonly bool emptyQueryStringOnly;

        // current state of the Tracker
        private readonly string filenamePrefix; // wi            thout the extension or the random part

        // configuration parameters
        private readonly string outputPath;

        // support for vary-by
        // BUG: The DiskOutputCacheTracker parent field was never accessed. May indicate bug.
        //private DiskOutputCacheTracker parent; // when not-null indicates child tracker
        private readonly string outputVaryById; // unique vary-by key for this tracker

        private readonly bool serveFromMemory;
        private readonly string[] varyBy;
        private readonly Dictionary<string, DiskOutputCacheTracker> varyByTrackers;
        private readonly string[] verbs;
        private byte[] cachedResponseBytes;
        private DateTime cachedResponseExpiry;
        private long cachedResponseHash;
        private volatile bool cachedResponseLoaded;

        // data related to capturing response
        private DiskOutputCacheResponseFilter capturingFilter;

        private HttpResponse capturingResponse;
        private DateTime nextResponseValidationTime;
        private string outputDataFilename;
        private string outputInfoFilename;
        private volatile bool triedToLoadCachedResponse;

        // ctor just stores config - doesn't go to disk to read cached response
        public DiskOutputCacheTracker(string path, string filePrefix, TimeSpan duration, string[] verbs, string[] varyBy,
            bool emptyQueryStringOnly, bool emptyPathInfoOnly, bool serveFromMemory)
        {
            outputPath = path;
            this.duration = duration;
            this.verbs = (string[]) verbs.Clone();
            this.varyBy = (string[]) varyBy.Clone();
            this.emptyQueryStringOnly = emptyQueryStringOnly;
            this.emptyPathInfoOnly = emptyPathInfoOnly;
            this.serveFromMemory = serveFromMemory;
            outputVaryById = string.Empty;
            filenamePrefix = filePrefix;

            capturingEvent = new ManualResetEvent(true);
            varyByTrackers = new Dictionary<string, DiskOutputCacheTracker>();
        }

        // private ctor to create a child tracker for a specific vary-by value
        private DiskOutputCacheTracker(DiskOutputCacheTracker parent, string varyById)
        {
            //this.parent = parent;
            outputPath = parent.outputPath;
            duration = parent.duration;
            verbs = parent.verbs;
            varyBy = parent.varyBy;
            serveFromMemory = parent.serveFromMemory;
            outputVaryById = varyById;
            filenamePrefix = string.Format("{0}_q_{1:x8}", parent.filenamePrefix, varyById.GetHashCode());

            capturingEvent = new ManualResetEvent(true);
            varyByTrackers = null; // no need for sub-sub-trackers
        }

        internal static bool ScavangingCompleted => scavangingCompleted;

        // finds and matches a tracker (this a or sub-tracker in varyby cases) for a HttpRequest
        public DiskOutputCacheTracker FindTrackerForRequest(HttpRequest request)
        {
            // path
            if (string.Compare(outputPath, request.FilePath, StringComparison.OrdinalIgnoreCase) != 0)
                return null;

            // verbs
            bool verbMatchFound = string.Compare(verbs[0], request.HttpMethod, StringComparison.OrdinalIgnoreCase) == 0;

            if (!verbMatchFound && verbs.Length > 1)
                for (int i = 1; i < verbs.Length; i++)
                    if (string.Compare(verbs[i], request.HttpMethod, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        verbMatchFound = true;
                        break;
                    }

            if (!verbMatchFound)
                return null;

            // path info
            if (emptyPathInfoOnly && !Compare.IsNullOrEmpty(request.PathInfo))
                return null;

            // query string
            if (emptyQueryStringOnly && request.QueryString.Count > 0)
                return null;

            // in non-vary-by case return this tracker
            if (varyBy == null || varyBy.Length == 0)
                return this;

            // get the child tracker corresponding to vary-by key for this request
            DiskOutputCacheTracker childTracker;
            string varyByKey = CalculateVaryByKey(varyBy, request);

            lock (varyByTrackers)
            {
                if (!varyByTrackers.TryGetValue(varyByKey, out childTracker))
                    if (varyByTrackers.Count < DiskOutputCache.VaryByLimit)
                    {
                        childTracker = new DiskOutputCacheTracker(this, varyByKey);
                        varyByTrackers.Add(varyByKey, childTracker);
                    }
            }

            return childTracker;
        }

        public bool TrySendResponseOrStartResponseCapture(HttpResponse response)
        {
            byte[] responseData = null;
            string responseFile = null;

            // loop while trying to either send or capture the response
            // (the loop is needed for cases when another thread does the capture)
            for (;;)
            {
                lock (this)
                {
                    // attempt to find the cached response on disk (only once)
                    if (!triedToLoadCachedResponse)
                    {
                        LookForCachedResponseOnDisk();
                        triedToLoadCachedResponse = true;
                    }

                    if (cachedResponseLoaded && ValidateLoadedCachedResponse())
                    {
                        // serve the cached response if validated
                        if (serveFromMemory)
                            responseData = cachedResponseBytes;
                        else
                            responseFile = outputDataFilename;

                        // send the response outside of the lock
                        break;
                    }

                    // couldn't send the response - try to capture it under lock
                    // (don't attempt to capture the same response from 2 threads at the same time)
                    if (capturingResponse == null)
                    {
                        // generate new file name
                        string filename = string.Format("{0}_{1:x8}{2}",
                            filenamePrefix, Guid.NewGuid().ToString().GetHashCode(), TempFileExt);

                        capturingFilter = new DiskOutputCacheResponseFilter(response.Filter, filename);
                        response.Filter = capturingFilter;

                        // move the event - non-signaled state
                        capturingEvent.Reset();
                        // remember the response
                        capturingResponse = response;
                        // started capturing - return from this method
                        break;
                    }
                }

                // capturing started from another thread - wait until done and continue (outside of the lock)
                capturingEvent.WaitOne();
            }

            // send the cached response if available (outside of the lock)
            if (responseData != null)
            {
                response.OutputStream.Write(responseData, 0, responseData.Length);
                return true;
            }

            if (responseFile == null)
                return false;

            try
            {
                response.TransmitFile(responseFile);
            }
            catch (Exception)
            {
                // if there is a problem sending data file, invalidate the cached response
                InvalidateCachedResponse();
                throw;
            }

            return true;
        }

        public void FinishCaptureAndSaveResponse(HttpResponse response)
        {
            if (capturingResponse != response)
                throw new InvalidOperationException("Attempt to complete response capture occured on wrong HttpResponse");

            FinishOrCancelCapture(response, false /*cancel*/);
        }

        public void CancelCapture(HttpResponse response)
        {
            if (capturingResponse != null && capturingResponse != response)
                throw new InvalidOperationException("Attempt to cancel response capture occured on wrong HttpResponse");

            FinishOrCancelCapture(response, true /*cancel*/);
        }

        // try to find an existing cached file on disk
        private void LookForCachedResponseOnDisk()
        {
            // walk trough all files on disk matching <prefix>????????.info, finding the right one

            string dir = Path.GetDirectoryName(filenamePrefix);
            string pattern = Path.GetFileName(filenamePrefix) + "_????????" + InfoFileExt;
            string[] files = Directory.GetFiles(dir, pattern, SearchOption.TopDirectoryOnly);

            foreach (string infoFilename in files)
                try
                {
                    string path;
                    string varyById;
                    DateTime timestamp;
                    DateTime expiry;
                    long hash;

                    if (TryReadInfoFile(infoFilename, out path, out varyById, out timestamp, out expiry, out hash))
                    {
                        // check that this info file matches current file path
                        if (string.Compare(path, outputPath, StringComparison.OrdinalIgnoreCase) != 0)
                            continue;

                        // check that vary-by key matches
                        if (string.Compare(varyById, outputVaryById, StringComparison.OrdinalIgnoreCase) != 0)
                            continue;

                        // check that data file exists
                        string dataFilename = infoFilename.Replace(InfoFileExt, DataFileExt);
                        if (!File.Exists(dataFilename))
                            continue;

                        // calculate response expiration
                        DateTime newExpiry = timestamp + duration;

                        if (newExpiry > expiry)
                            newExpiry = expiry;

                        // check that not expired (only timestamp)
                        DateTime utcNow = DateTime.UtcNow;

                        if (expiry <= utcNow)
                        {
                            DeleteFiles(infoFilename, dataFilename);
                            continue;
                        }

                        // found the right file (and data file)
                        outputInfoFilename = infoFilename;
                        outputDataFilename = dataFilename;
                        cachedResponseHash = hash;
                        cachedResponseExpiry = newExpiry;
                        nextResponseValidationTime = utcNow;

                        // read the data if needed
                        cachedResponseBytes = new byte[0];
                        if (serveFromMemory)
                            cachedResponseBytes = File.ReadAllBytes(dataFilename);

                        cachedResponseLoaded = true;
                        break;
                    }
                }
                catch (IOException)
                {
                    // if one file failed to load move to the next one
                }
                catch (ArgumentException)
                {
                    // if one file failed to load move to the next one
                }
        }

        private bool ValidateLoadedCachedResponse()
        {
            DateTime utcNow = DateTime.UtcNow;

            // check time-based expiration
            bool expired = cachedResponseExpiry <= utcNow;

            // check for recompilation (not too often)
            if (!expired && nextResponseValidationTime <= utcNow)
            {
                expired = cachedResponseHash != CalculateHandlerHash(outputPath);
                nextResponseValidationTime = utcNow + DiskOutputCache.FileValidationDelay;
            }

            if (!expired)
                return true;

            // if expired delete the info file and schedule deletion of the data file
            InvalidateCachedResponse();
            return false;
        }

        private void InvalidateCachedResponse()
        {
            DeleteFiles(outputInfoFilename, outputDataFilename);
            cachedResponseLoaded = false;
        }

        private void FinishOrCancelCapture(HttpResponse response, bool cancel)
        {
            if (capturingResponse != response)
                return;

            if (capturingFilter == null)
                throw new InvalidOperationException("Response capturing filter is missing.");

            lock (this)
            {
                try
                {
                    capturingFilter.StopFiltering(cancel);

                    if (!cancel)
                    {
                        // remember the captured response
                        string tempFilename = capturingFilter.CaptureFilename;
                        outputInfoFilename = tempFilename.Replace(TempFileExt, InfoFileExt);
                        outputDataFilename = tempFilename.Replace(TempFileExt, DataFileExt);

                        DateTime timestamp = DateTime.UtcNow;
                        cachedResponseHash = CalculateHandlerHash(outputPath);
                        cachedResponseExpiry = timestamp + duration;
                        nextResponseValidationTime = timestamp + DiskOutputCache.FileValidationDelay;

                        // save info file
                        string info = string.Format(
                            "<{0} path=\"{1}\" vary=\"{2}\" timestamp=\"{3}\" expiry=\"{4}\" hash=\"{5}\" />",
                            InfoTagName, outputPath, outputVaryById,
                            timestamp.ToBinary(), cachedResponseExpiry.ToBinary(),
                            cachedResponseHash);
                        File.WriteAllText(outputInfoFilename, info, Encoding.UTF8);

                        // rename temp file into data file
                        File.Move(tempFilename, outputDataFilename);

                        // read the data into memory if needed
                        cachedResponseBytes = new byte[0];
                        if (serveFromMemory)
                            cachedResponseBytes = File.ReadAllBytes(outputDataFilename);

                        // now everything's ready
                        cachedResponseLoaded = true;
                    }
                }
                finally
                {
                    // notify any waiting threads that capturing is complete
                    capturingFilter = null;
                    capturingResponse = null;
                    capturingEvent.Set();
                }
            }
        }

        // schedule all expired disk cache entries for deletion
        internal static void ScavangeFilesOnAppDomainStartup()
        {
            ThreadPool.QueueUserWorkItem(ScavangeFiles);
        }

        private static void ScavangeFiles(object state)
        {
            try
            {
                // walk trough all *.data files, look for missing info files
                string pattern = "*" + DataFileExt;
                string[] files = Directory.GetFiles(DiskOutputCache.Location, pattern, SearchOption.TopDirectoryOnly);

                foreach (string dataFilename in files)
                {
                    try
                    {
                        string infoFilename = dataFilename.Replace(DataFileExt, InfoFileExt);

                        if (!File.Exists(infoFilename))
                            DiskOutputCache.ScheduleFileDeletion(dataFilename);
                    }
                    catch (IOException)
                    {
                        // if one file failed to load move to the next one
                    }
                    catch (ArgumentException)
                    {
                        // if one file failed to load move to the next one
                    }

                    if (DiskOutputCache.ShuttingDown)
                        return;
                }

                // walk through all *.info files, look for expired ones
                pattern = "*" + InfoFileExt;
                files = Directory.GetFiles(DiskOutputCache.Location, pattern, SearchOption.TopDirectoryOnly);

                foreach (string infoFilename in files)
                {
                    try
                    {
                        string path;
                        string varyById;
                        DateTime timestamp;
                        DateTime expiry;
                        long hash;

                        if (TryReadInfoFile(infoFilename, out path, out varyById, out timestamp, out expiry, out hash))
                            if (expiry < DateTime.UtcNow)
                            {
                                string dataFilename = infoFilename.Replace(InfoFileExt, DataFileExt);
                                DeleteFiles(infoFilename, dataFilename);
                            }
                    }
                    catch (IOException)
                    {
                        // if one file failed to load move to the next one
                    }
                    catch (ArgumentException)
                    {
                        // if one file failed to load move to the next one
                    }

                    if (DiskOutputCache.ShuttingDown)
                        return;
                }
            }
            finally
            {
                scavangingCompleted = true;
            }
        }

        // helper to parse info file
        private static bool TryReadInfoFile(string infoFilename, out string path, out string vary,
            out DateTime timestamp, out DateTime expiry, out long hash)
        {
            path = string.Empty;
            vary = string.Empty;
            timestamp = DateTime.MinValue;
            expiry = DateTime.MinValue;
            hash = 0;

            // read the XML file: 
            // <diskOutputCacheItem path="..." vary="..." timestamp="..." expiry="..." hash="..." />

            XmlDocument doc = new XmlDocument();
            doc.Load(infoFilename);

            XmlNode rootNode = null;
            for (XmlNode node = doc.FirstChild; node != null; node = node.NextSibling)
                if (node.NodeType == XmlNodeType.Element)
                {
                    rootNode = node;
                    break;
                }

            if (rootNode != null && rootNode.Name == InfoTagName)
            {
                path = rootNode.Attributes["path"].Value;
                vary = rootNode.Attributes["vary"].Value;
                timestamp = DateTime.FromBinary(long.Parse(rootNode.Attributes["timestamp"].Value));
                expiry = DateTime.FromBinary(long.Parse(rootNode.Attributes["expiry"].Value));
                hash = long.Parse(rootNode.Attributes["hash"].Value);
                return true;
            }

            return false;
        }

        // helper to delete the info file and schedule data file for deletion in the future
        private static void DeleteFiles(string infoFilename, string dataFilename)
        {
            try
            {
                File.Delete(infoFilename);
            }
            catch (IOException)
            {
                // could be deleted already by the scavenger
            }
            catch (ArgumentException)
            {
                // could be deleted already by the scavenger
            }

            DiskOutputCache.ScheduleFileDeletion(dataFilename);
        }

        // helper to calculate hash that would change when the page is recompiled
        private static long CalculateHandlerHash(string path)
        {
            long hash = 0;

            // use the type name (and the containing assembly) as the hash
            // assuming the assembly will get recompiled if a dependency changes

            try
            {
                Type t = BuildManager.GetCompiledType(path);

                if (t != null)
                {
                    string typeName = t.AssemblyQualifiedName;
                    string typeFileName = t.Module.FullyQualifiedName;
                    hash = (typeName.GetHashCode() & 0xffffffff) +
                           ((typeFileName.GetHashCode() & 0xffffffff) << 29);
                }
            }
            catch (Exception)
            {
                // failure to calculate hash is not fatal
            }

            return hash;
        }

        // helper to create a canonicalized representation of the query string
        // give vary-by and the current request
        private static string CalculateVaryByKey(IList<string> varyBy, HttpRequest request)
        {
            if (varyBy == null || varyBy.Count == 0)
                return string.Empty;

            if (varyBy.Count == 1 && varyBy[0] == "*")
                return request.QueryString.ToString();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < varyBy.Count; i++)
            {
                sb.Append(request.QueryString[varyBy[i]]);
                sb.Append('-');
            }

            return HttpUtility.UrlEncode(sb.ToString());
        }
    }
}