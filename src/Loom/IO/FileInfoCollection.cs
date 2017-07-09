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
    ///     A Collection of FileInfo objects, together with advanced options for finding and sorting files.
    /// </summary>
    public class FileInfoCollection : CollectionList<FileInfo>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileInfoCollection" /> class.
        /// </summary>
        public FileInfoCollection() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileInfoCollection" /> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public FileInfoCollection(IEnumerable<FileInfo> list) : base(list) { }

        /// <summary>
        ///     Deletes all.
        /// </summary>
        public void DeleteAll()
        {
            foreach (FileInfo info in Items)
                info.Delete();
        }

        /// <summary>Sorts the FileInfo objects in the collection by Name.</summary>
        public void SortByName()
        {
            Sort(new FileInfoNameComparer());
        }

        /// <summary>Sorts the FileInfo objects in the collection by Extension.</summary>
        public void SortByExtension()
        {
            Sort(new FileInfoExtensionComparer());
        }

        /// <summary>Sorts the FileInfo objects in the collection by LastWriteTime.</summary>
        public void SortByLastWriteTime()
        {
            Sort(new FileInfoLastWriteTimeComparer());
        }

        /// <summary>Sorts the FileInfo objects in the collection by Length.</summary>
        public void SortByLength()
        {
            Sort(new FileInfoLengthComparer());
        }

        /// <summary>Does the specified file exist within the collection?</summary>
        /// <param name="filePath">The file to look for.</param>
        /// <returns>Whether the file exists within the collection.</returns>
        public bool Contains(string filePath)
        {
            Argument.Assert.IsNotNull(filePath, "filePath");

            foreach (FileInfo fileInfo in this)
                if (fileInfo.FullName == filePath)
                    return true;

            return false;
        }

        /// <summary>
        ///     Searches within the collection for any FileInfo objects who's name
        ///     matches the supplied expression.
        /// </summary>
        /// <param name="match">The Regular Expression to search with.</param>
        /// <returns>A collection of matching FileInfo objects.</returns>
        public FileInfoCollection FilterByName(Regex match)
        {
            Argument.Assert.IsNotNull(match, "match");

            FileInfoCollection filteredByName = new FileInfoCollection();
            foreach (FileInfo fileInfo in this)
                if (match.IsMatch(fileInfo.Name))
                    filteredByName.Add(fileInfo);

            return filteredByName;
        }

        /// <summary>
        ///     Searches within the collection for any DirectoryInfo objects who's name
        ///     matches the supplied pattern.
        /// </summary>
        /// <param name="pattern">The Regular Expression to search with.</param>
        /// <returns>A collection of matching DirectoryInfo objects.</returns>
        public FileInfoCollection FilterByName(string pattern)
        {
            Argument.Assert.IsNotNull(pattern, "pattern");
            return FilterByName(new Regex(pattern));
        }

        #region Nested type: FileInfoExtensionComparer

        internal class FileInfoExtensionComparer : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Members

            public int Compare(FileInfo x, FileInfo y)
            {
                Argument.Assert.IsNotNull(y, "y");
                Argument.Assert.IsNotNull(x, "x");

                return string.Compare(x.Extension, y.Extension, StringComparison.Ordinal);
            }

            #endregion
        }

        #endregion

        #region Nested type: FileInfoLastWriteTimeComparer

        internal class FileInfoLastWriteTimeComparer : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Members

            public int Compare(FileInfo x, FileInfo y)
            {
                Argument.Assert.IsNotNull(y, "y");
                Argument.Assert.IsNotNull(x, "x");

                return x.LastWriteTime.CompareTo(y.LastWriteTime);
            }

            #endregion
        }

        #endregion

        #region Nested type: FileInfoLengthComparer

        internal class FileInfoLengthComparer : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Members

            public int Compare(FileInfo x, FileInfo y)
            {
                // TODO: Validate these parameters and handle nulls.
                return x.Length.CompareTo(y.Length);
            }

            #endregion
        }

        #endregion

        #region Nested type: FileInfoNameComparer

        internal class FileInfoNameComparer : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Members

            public int Compare(FileInfo x, FileInfo y)
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