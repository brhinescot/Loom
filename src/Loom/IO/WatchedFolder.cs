#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using Loom.Threading;

#endregion

namespace Loom.IO
{
    internal class WatchedFolder : IDisposable
    {
        private readonly int delay;
        private readonly Dictionary<string, FileSystemEventArgs> notifications;
        private readonly Dictionary<string, int> watchedFiles;
        private readonly FileSystemWatcher watcher;
        private Timer timer;

        public WatchedFolder(string folderPath, int delay)
        {
            watchedFiles = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            notifications = new Dictionary<string, FileSystemEventArgs>(StringComparer.CurrentCultureIgnoreCase);

            FolderPath = folderPath;
            this.delay = delay;
            watcher = new FileSystemWatcher(folderPath, "*.*");
            watcher.Changed += HandleWatcherChanged;
            watcher.Deleted += HandleWatcherChanged;
            watcher.Renamed += HandleWatcherRenamed;
            watcher.EnableRaisingEvents = true;
        }

        public string FolderPath { get; }

        public int WatchedFileCount => watchedFiles.Count;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public event FileSystemEventHandler FileChanged;

        public void AddFile(string fileName)
        {
            PrivateAddFile(fileName);
        }

        public void RemoveFile(string fileName)
        {
            PrivateRemoveFile(fileName);
        }

        protected virtual void OnFileChanged(FileSystemEventArgs e)
        {
            FileSystemEventHandler handler = FileChanged;
            handler?.Invoke(this, e);
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            using (TimedLock.Lock(this))
            {
                foreach (string watchedFile in notifications.Keys)
                {
                    FileSystemEventArgs eventArgs = notifications[watchedFile];
                    notifications.Remove(watchedFile);
                    OnFileChanged(eventArgs);
                }
            }
        }

        private void HandleWatcherChanged(object sender, FileSystemEventArgs e)
        {
            using (TimedLock.Lock(this))
            {
                ResetTimer();
                if (!watchedFiles.ContainsKey(e.Name))
                    return;

                if (notifications.ContainsKey(e.Name))
                    notifications.Remove(e.Name);

                notifications.Add(e.Name, e);
            }
        }

        private void HandleWatcherRenamed(object sender, RenamedEventArgs e)
        {
            HandleWatcherChanged(sender, e);
        }

        private void PrivateAddFile(string fileName)
        {
            using (TimedLock.Lock(this))
            {
                if (!watchedFiles.ContainsKey(fileName))
                {
                    watchedFiles.Add(fileName, 1);
                }
                else
                {
                    int fileIndex = watchedFiles[fileName];
                    watchedFiles[fileName] = fileIndex + 1;
                }
            }
        }

        private void PrivateRemoveFile(string fileName)
        {
            using (TimedLock.Lock(this))
            {
                if (!watchedFiles.ContainsKey(fileName))
                    return;

                int fileIndex = watchedFiles[fileName];

                if (fileIndex == 1)
                    watchedFiles.Remove(fileName);
                else
                    watchedFiles[fileName] = fileIndex - 1;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (watcher != null)
                {
                    watcher.EnableRaisingEvents = false;
                    watcher.Dispose();
                }

                timer?.Dispose();
            }
        }

        private void ResetTimer()
        {
            timer = new Timer(delay);
            timer.Elapsed += HandleTimerElapsed;
            timer.Enabled = true;
            timer.Start();
        }
    }
}