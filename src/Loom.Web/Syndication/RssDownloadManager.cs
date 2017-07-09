#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Hosting;
using System.Xml;

#endregion

namespace Loom.Web.Syndication
{
    // helper class that provides memory and disk caching of the downloaded feeds
    internal class RssDownloadManager
    {
        private static readonly RssDownloadManager theManager = new RssDownloadManager();

        private readonly Dictionary<string, RssChannelDom> cache;
        private readonly int defaultTtlMinutes;
        private readonly string directoryOnDisk;

        private RssDownloadManager()
        {
            // create in-memory cache
            cache = new Dictionary<string, RssChannelDom>();

            // get default ttl value from config
            defaultTtlMinutes = GetTtlFromString(ConfigurationManager.AppSettings["defaultRssTtl"], 1);

            // prepare disk directory
            directoryOnDisk = PrepareTempDir();
        }

        private RssChannelDom DownloadChannelDom(string url)
        {
            // look for disk cache first
            RssChannelDom dom = TryLoadFromDisk(url);

            if (dom != null)
                return dom;

            // download the feed
            byte[] feed = new WebClient().DownloadData(url);

            // parse it as XML
            XmlDocument doc = new XmlDocument();
            doc.Load(new MemoryStream(feed));

            // parse into DOM
            dom = RssXmlHelper.ParseChannelXml(doc);

            // set expiry
            string ttlString;
            dom.Channel.TryGetValue("ttl", out ttlString);
            int ttlMinutes = GetTtlFromString(ttlString, defaultTtlMinutes);
            DateTime utcExpiry = DateTime.UtcNow.AddMinutes(ttlMinutes);
            dom.SetExpiry(utcExpiry);

            // save to disk
            TrySaveToDisk(doc, url, utcExpiry);

            return dom;
        }

        private RssChannelDom TryLoadFromDisk(string url)
        {
            if (directoryOnDisk == null)
                return null; // no place to cache

            // look for all files matching the prefix
            // looking for the one matching url that is not expired
            // removing expired (or invalid) ones
            string pattern = GetTempFileNamePrefixFromUrl(url) + "_*.rss";
            string[] files = Directory.GetFiles(directoryOnDisk, pattern, SearchOption.TopDirectoryOnly);

            foreach (string rssFilename in files)
            {
                XmlDocument rssDoc = null;
                bool isRssFileValid = false;
                DateTime utcExpiryFromRssFile = DateTime.MinValue;
                string urlFromRssFile = null;

                try
                {
                    rssDoc = new XmlDocument();
                    rssDoc.Load(rssFilename);

                    // look for special XML comment (before the root tag)'
                    // containing expiration and url
                    XmlComment comment = rssDoc.DocumentElement.PreviousSibling as XmlComment;

                    if (comment != null)
                    {
                        string c = comment.Value;
                        int i = c.IndexOf('@');
                        long expiry;

                        if (long.TryParse(c.Substring(0, i), out expiry))
                        {
                            utcExpiryFromRssFile = DateTime.FromBinary(expiry);
                            urlFromRssFile = c.Substring(i + 1);
                            isRssFileValid = true;
                        }
                    }
                } // error processing one file shouldn't stop processing other files
                catch (IOException) { }
                catch (SecurityException) { }
                catch (UnauthorizedAccessException) { }
                catch (NotSupportedException) { }
                catch (ArgumentException) { }
                catch (XmlException) { }

                // remove invalid or expired file
                if (!isRssFileValid || utcExpiryFromRssFile < DateTime.UtcNow)
                {
                    try
                    {
                        File.Delete(rssFilename);
                    }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                    catch (NotSupportedException) { }
                    catch (ArgumentException) { }

                    // try next file
                    continue;
                }

                // match url
                if (urlFromRssFile == url)
                {
                    // found a good one - create DOM and set expiry (as found on disk)
                    RssChannelDom dom = RssXmlHelper.ParseChannelXml(rssDoc);
                    dom.SetExpiry(utcExpiryFromRssFile);
                    return dom;
                }
            }

            // not found
            return null;
        }

        private void TrySaveToDisk(XmlDocument doc, string url, DateTime utcExpiry)
        {
            if (directoryOnDisk == null)
                return;

            doc.InsertBefore(doc.CreateComment(string.Format(
                "{0}@{1}", utcExpiry.ToBinary(), url
            )), doc.DocumentElement);

            string fileName = string.Format("{0}_{1:x8}.rss",
                GetTempFileNamePrefixFromUrl(url),
                Guid.NewGuid().ToString().GetHashCode());

            try
            {
                doc.Save(Path.Combine(directoryOnDisk, fileName));
            } // can't save to disk - not a problem
            catch (XmlException) { }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (NotSupportedException) { }
            catch (ArgumentException) { }
        }

        private RssChannelDom GetChannelDom(string url)
        {
            RssChannelDom dom;

            lock (cache)
            {
                if (cache.TryGetValue(url, out dom))
                    if (DateTime.UtcNow > dom.UtcExpiry)
                    {
                        cache.Remove(url);
                        dom = null;
                    }
            }

            if (dom == null)
            {
                dom = DownloadChannelDom(url);

                lock (cache)
                {
                    cache[url] = dom;
                }
            }

            return dom;
        }

        public static RssChannelDom GetChannel(string url)
        {
            return theManager.GetChannelDom(url);
        }

        private static int GetTtlFromString(string ttlString, int defaultTtlMinutes)
        {
            if (!Compare.IsNullOrEmpty(ttlString))
            {
                int ttlMinutes;
                if (int.TryParse(ttlString, out ttlMinutes))
                    if (ttlMinutes >= 0)
                        return ttlMinutes;
            }

            return defaultTtlMinutes;
        }

        private static string PrepareTempDir()
        {
            string tempDir = null;

            try
            {
                string d = ConfigurationManager.AppSettings["rssTempDir"];

                if (Compare.IsNullOrEmpty(d))
                {
                    if (HostingEnvironment.IsHosted)
                    {
                        d = HttpRuntime.CodegenDir;
                    }
                    else
                    {
                        d = Environment.GetEnvironmentVariable("TEMP");

                        if (Compare.IsNullOrEmpty(d))
                        {
                            d = Environment.GetEnvironmentVariable("TMP");

                            if (Compare.IsNullOrEmpty(d))
                                d = Directory.GetCurrentDirectory();
                        }
                    }

                    d = Path.Combine(d, "rss");
                }

                if (!Directory.Exists(d))
                    Directory.CreateDirectory(d);

                tempDir = d;
            }
            catch (Exception)
            {
                // don't cache on disk if can't do it
            }

            return tempDir;
        }

        private static string GetTempFileNamePrefixFromUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                return string.Format("{0}_{1:x8}",
                    uri.Host.Replace('.', '_'), uri.AbsolutePath.GetHashCode());
            }
            catch (Exception)
            {
                return "rss";
            }
        }
    }
}