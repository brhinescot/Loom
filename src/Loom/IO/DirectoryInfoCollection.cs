#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Loom.Collections;

#endregion

namespace Loom.IO
{
    /// <summary>
    ///     A Collection of DirectoryInfo objects, together with
    ///     advanced options for finding and sorting Directories.
    /// </summary>
    public class DirectoryInfoCollection : CollectionList<DirectoryInfo>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectoryInfoCollection" /> class.
        /// </summary>
        public DirectoryInfoCollection() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectoryInfoCollection" /> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public DirectoryInfoCollection(IEnumerable<DirectoryInfo> list) : base(list) { }

        /// <summary>
        ///     Deletes all.
        /// </summary>
        public void DeleteAll()
        {
            foreach (DirectoryInfo info in Items)
                info.Delete();
        }

        /// <summary>Sorts the DirectoryInfo objects in the collection by Name.</summary>
        public void SortByName()
        {
            Sort(new DirectoryInfoNameComparer());
        }

        /// <summary>Sorts the DirectoryInfo objects in the collection by LastWriteTime.</summary>
        public void SortByLastWriteTime()
        {
            Sort(new DirectoryInfoLastWriteTimeComparer());
        }

        /// <summary>Does the specified directory exist within the collection?</summary>
        /// <param name="directoryPath">The directory to look for.</param>
        /// <returns>Whether the directory exists within the collection.</returns>
        public bool Contains(string directoryPath)
        {
            Argument.Assert.IsNotNull(directoryPath, "directoryPath");

            foreach (DirectoryInfo directoryInfo in this)
                if (directoryInfo.FullName == directoryPath)
                    return true;

            return false;
        }

        /// <summary>
        ///     Searches within the collection for any DirectoryInfo objects who's name
        ///     matches the supplied expression.
        /// </summary>
        /// <param name="match">The Regular Expression to search with.</param>
        /// <returns>A collection of matching DirectoryInfo objects.</returns>
        public DirectoryInfoCollection FilterByName(Regex match)
        {
            Argument.Assert.IsNotNull(match, "match");

            DirectoryInfoCollection filteredByName = new DirectoryInfoCollection();
            foreach (DirectoryInfo directoryInfo in this)
                if (match.IsMatch(directoryInfo.Name))
                    filteredByName.Add(directoryInfo);

            return filteredByName;
        }

        /// <summary>
        ///     Searches within the collection for any DirectoryInfo objects who's name
        ///     matches the supplied pattern.
        /// </summary>
        /// <param name="pattern">The Regular Expression to search with.</param>
        /// <returns>A collection of matching DirectoryInfo objects.</returns>
        public DirectoryInfoCollection FilterByName(string pattern)
        {
            Argument.Assert.IsNotNull(pattern, "pattern");
            return FilterByName(new Regex(pattern));
        }

        #region Nested type: DirectoryInfoLastWriteTimeComparer

        internal class DirectoryInfoLastWriteTimeComparer : IComparer<DirectoryInfo>
        {
            #region IComparer<DirectoryInfo> Members

            public int Compare(DirectoryInfo x, DirectoryInfo y)
            {
                Argument.Assert.IsNotNull(y, "y");
                Argument.Assert.IsNotNull(x, "x");

                return x.LastWriteTime.CompareTo(y.LastWriteTime);
            }

            #endregion
        }

        #endregion

        #region Nested type: DirectoryInfoNameComparer

        internal class DirectoryInfoNameComparer : IComparer<DirectoryInfo>
        {
            #region IComparer<DirectoryInfo> Members

            public int Compare(DirectoryInfo x, DirectoryInfo y)
            {
                Argument.Assert.IsNotNull(y, "y");
                Argument.Assert.IsNotNull(x, "x");

                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }

            #endregion
        }

        #endregion
    }
}