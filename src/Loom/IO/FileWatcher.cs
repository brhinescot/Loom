#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using Loom.Threading;

#endregion

namespace Loom.IO
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class FileWatcher : IDisposable
    {
        private static readonly List<WatchedFolder> WatchedFolders = new List<WatchedFolder>();
        private static readonly object WatchedFoldersLock = new object();
        private readonly string fileName;
        private readonly string folderPath;
        private readonly string fullPath;

        /// <summary>
        ///     Initializes a new instance of the WatchedFile class.
        /// </summary>
        /// <param name="filePath">The path to the file to watch.</param>
        public FileWatcher(string filePath)
        {
            fullPath = filePath;
            folderPath = Path.GetDirectoryName(filePath);
            fileName = Path.GetFileName(filePath);

            WatchedFolder watchedFolder = FindFolderWatcher(folderPath);
            if (watchedFolder == null)
            {
                watchedFolder = new WatchedFolder(folderPath, 1500);
                WatchedFolders.Add(watchedFolder);
            }
            watchedFolder.AddFile(fileName);
            watchedFolder.FileChanged += HandleFileChanged;
        }

        #region IDisposable Members

        /// <summary>
        ///     Releases all resources used by the WatchedFolder object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///     Occurs when the watched file changed.
        /// </summary>
        public event FileSystemEventHandler FileChanged;

        private static WatchedFolder FindFolderWatcher(string fileName)
        {
            string directoryName = Path.GetDirectoryName(fileName);

            using (TimedLock.Lock(WatchedFoldersLock))
            {
                foreach (WatchedFolder watchedFolder in WatchedFolders)
                    if (string.Compare(watchedFolder.FolderPath, directoryName, StringComparison.OrdinalIgnoreCase) == 0)
                        return watchedFolder;
            }
            return null;
        }

        private void HandleFileChanged(object sender, FileSystemEventArgs e)
        {
            FileSystemEventHandler handler = FileChanged;
            if (string.Compare(e.FullPath, fullPath, StringComparison.OrdinalIgnoreCase) == 0)
                handler?.Invoke(this, e);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WatchedFolder folder = FindFolderWatcher(fullPath);
                if (folder != null)
                {
                    folder.RemoveFile(fileName);
                    if (folder.WatchedFileCount == 0)
                        WatchedFolders.Remove(folder);
                }
            }
        }
    }
}