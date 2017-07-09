#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Loom.IO
{
    [Flags]
    public enum SearchType
    {
        FileName = 1,
        TextInFile = 2,
        LastModifiedDate = 4
    }

    public class Search
    {
        public static SearchAlgorithm BreadthFirst => new BreadthFirstSearhAlgorithm();

        #region Nested type: BreadthFirstSearhAlgorithm

        internal class BreadthFirstSearhAlgorithm : SearchAlgorithm
        {
            public override IEnumerable<FileInfo> Execute(string fileName)
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                foreach (FileInfo info in Execute(fileName, drive.RootDirectory.FullName))
                    yield return info;
            }

            public override IEnumerable<FileInfo> Execute(string fileName, string searchRoot)
            {
                foreach (FileInfo file in new DirectoryInfo(searchRoot).GetFiles(fileName, SearchOption.AllDirectories))
                    yield return file;
            }

            //private IEnumerable<DirectoryInfo> GetDirectoriesRecursive(DirectoryInfo directory)
            //{
            //    Queue<DirectoryInfo> subs = new Queue<DirectoryInfo>();
            //    foreach (DirectoryInfo info in directory.GetDirectories())
            //    {
            //        subs.Enqueue(info);
            //    }
            //}
        }

        #endregion

        #region Nested type: SearchAlgorithm

        public abstract class SearchAlgorithm
        {
            public abstract IEnumerable<FileInfo> Execute(string fileName);
            public abstract IEnumerable<FileInfo> Execute(string fileName, string searchRoot);
        }

        #endregion
    }
}