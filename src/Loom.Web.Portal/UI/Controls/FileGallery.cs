#region Using Directives

using System.Collections.Generic;
using System.IO;
using Loom.Web.IO;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public class FileGallery : Repeater
    {
        public string SourceDirectory { get; set; }
        public string SearchPattern { get; set; }
        public bool IncludeSubDirectories { get; set; }
        public bool AllowMissingDirectory { get; set; }

        protected override void CreateChildControls()
        {
            string path = Page.Server.MapPath(SourceDirectory);
            if (!AllowMissingDirectory && !Directory.Exists(path))
                throw new DirectoryNotFoundException("The directory specified for the FileGallery control '" + path + "' could not be found.");

            string[] files = Directory.GetFiles(path, SearchPattern, IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            List<GalleryItem> items = new List<GalleryItem>();
            foreach (string file in files)
                items.Add(new GalleryItem
                {
                    VirtualPath = WebPath.GetVirtualPathFromPhysicalPath(file),
                    RelativePath = WebPath.GetApplicationPathFromPhysicalPath(file)
                });

            DataSource = items;

            base.CreateChildControls();
        }
    }

    public class GalleryItem
    {
        public string VirtualPath { get; set; }
        public string RelativePath { get; set; }
    }
}