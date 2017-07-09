#region Using Directives

using System;
using System.Collections.Specialized;
using System.IO;
using System.Security;
using System.Web;
using System.Web.Caching;
using Loom.IO;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Represents a class for storing a list of string items contained in a flat file
    ///     in the ASP.NET server cache.
    /// </summary>
    /// <remarks>The ASP.NET server cache entry is refreshed when the file changes.</remarks>
    internal class FileListCache
    {
        private readonly string cacheKey;
        private readonly string fileName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileListCache" /> class.
        /// </summary>
        /// <param name="fileName">
        ///     Name of the file containing the list of items
        ///     to cache.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         The list of items should be contained in a text file with
        ///         one item per line.
        ///     </para>
        ///     <para>
        ///         The pound(#) symbol may be used to exclude items in the list from
        ///         being cached or to indicate a comment line.
        ///     </para>
        /// </remarks>
        public FileListCache(string fileName)
        {
            Argument.Assert.IsNotNullOrEmpty(fileName, nameof(fileName));

            this.fileName = HttpContext.Current.Server.MapPath(fileName);
            Argument.Assert.FileExists(this.fileName);

            cacheKey = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Determines whether the <see cref="FileListCache" /> contains a
        ///     specific value.
        /// </summary>
        /// <param name="value">
        ///     The value to locate in the <see cref="FileListCache" />.
        ///     The value can not be null.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <see cref="FileListCache" /> contains an
        ///     element with the specified value; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is null</exception>
        internal bool Contains(string value)
        {
            return GetSafeDictionaryEntries(HttpContext.Current).ContainsKey(value);
        }

        private StringDictionary GetSafeDictionaryEntries(HttpContext context)
        {
            StringDictionary dictionaryEntries = context.Cache[cacheKey] as StringDictionary;
            if (dictionaryEntries == null)
            {
                dictionaryEntries = new StringDictionary();
                try
                {
                    foreach (string address in EnumerableFile.ReadToEnd(fileName, "#"))
                        dictionaryEntries.Add(address, null);
                    context.Cache.Insert(cacheKey, dictionaryEntries, new CacheDependency(fileName));
                }
                catch (IOException)
                {
                    dictionaryEntries = new StringDictionary();
                }
                catch (SecurityException)
                {
                    dictionaryEntries = new StringDictionary();
                }
            }
            return dictionaryEntries;
        }
    }
}