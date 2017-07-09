#region Using Directives

using System.Collections.Generic;
using System.IO;

#endregion

namespace Loom.IO
{
    /// <summary>
    ///     Summary description for ArrayFile.
    /// </summary>
    public static class EnumerableFile
    {
        /// <summary>
        ///     Reads to end.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadToEnd(string fileName)
        {
            Argument.Assert.IsNotNull(fileName, "fileName");
            Argument.Assert.FileExists(fileName);

            using (StreamReader reader = File.OpenText(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    yield return line.Trim();
                    line = reader.ReadLine();
                }
            }
        }

        /// <summary>
        ///     Reads to end.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="commentIndicator">The comment indicator.</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadToEnd(string fileName, string commentIndicator)
        {
            Argument.Assert.IsNotNull(fileName, "fileName");
            Argument.Assert.FileExists(fileName);

            using (StreamReader reader = File.OpenText(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = line.Trim();
                    if (line.Length != 0 && (commentIndicator == null || !line.StartsWith(commentIndicator)))
                        yield return line;
                    line = reader.ReadLine();
                }
            }
        }

        /// <summary>
        ///     Writes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="header">The header.</param>
        public static void Write(IEnumerable<string> content, string fileName, string header = null)
        {
            Argument.Assert.IsNotNull(content, "content");
            Argument.Assert.IsNotNull(fileName, "fileName");

            using (StreamWriter writer = File.CreateText(fileName))
            {
                if (!Compare.IsNullOrEmpty(header))
                    writer.WriteLine(header);
                foreach (string item in content)
                    writer.WriteLine(item);
            }
        }

        /// <summary>
        ///     Appends the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="header">The header.</param>
        public static void Append(IEnumerable<string> content, string fileName, string header = null)
        {
            Argument.Assert.IsNotNull(content, "content");
            Argument.Assert.IsNotNull(fileName, "fileName");
            Argument.Assert.FileExists(fileName);

            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(header);
                foreach (string item in content)
                    writer.WriteLine(item);
            }
        }

        /// <summary>
        ///     Opens a file and places each line into a sorted <see cref="List{T}" />"/>
        ///     ready for binary searching.
        /// </summary>
        /// <param name="fileName">The path to the file to open.</param>
        /// <param name="commentIndicator">The comment indicator.</param>
        /// <returns>
        ///     A <see cref="List{T}" />"/> containing one entry for each
        ///     line in the file.
        /// </returns>
        /// <remarks>All lines starting with the comment indicator will be skipped.</remarks>
        internal static IList<string> OpenSearch(string fileName, string commentIndicator = null)
        {
            Argument.Assert.IsNotNull(fileName, "fileName");
            Argument.Assert.FileExists(fileName);

            List<string> internalList = new List<string>();
            using (StreamReader reader = File.OpenText(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length != 0 && !internalList.Contains(line) && (commentIndicator == null || !line.StartsWith(commentIndicator)))
                        internalList.Add(line);
                }
            }

            internalList.Sort();
            return internalList;
        }
    }
}