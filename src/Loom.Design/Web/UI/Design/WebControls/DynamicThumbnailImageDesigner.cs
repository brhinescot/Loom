#region Using Directives

using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.Design;
using Loom.Drawing;

#endregion

namespace Loom.Web.UI.Design.WebControls
{
    ///<summary>
    ///</summary>
    [SupportsPreviewControl(true)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class DynamicThumbnailImageDesigner : ControlDesigner2
    {
        /// <summary>
        ///     Retrieves the HTML markup that is used to represent the control at design time.
        /// </summary>
        /// <returns>
        ///     The HTML markup used to represent the control at design time.
        /// </returns>
        public override string GetDesignTimeHtml()
        {
            string imageUrl = GetPropertyValue<string>("ImageUrl");

            bool resize = GetPropertyValue<bool>("Resize");
            if (!resize)
            {
                IWebApplication webApp = (IWebApplication) Component.Site.GetService(typeof(IWebApplication));
                IProjectItem item = webApp.GetProjectItemFromUrl(imageUrl);
                return string.Concat("<img src=\"", item.PhysicalPath, "\" />");
            }

            Size maximumSize = GetPropertyValue<Size>("MaximumSize");
            int quality = GetPropertyValue<int>("Quality");

            return Compare.IsNullOrEmpty(imageUrl) ? "<img src=\"\" />" : ReturnThumbnail(imageUrl, maximumSize, quality);
        }

        private string ReturnThumbnail(string imageUrl, Size size, int quality)
        {
            IWebApplication webApp = (IWebApplication) Component.Site.GetService(typeof(IWebApplication));
            IProjectItem item = webApp.GetProjectItemFromUrl(imageUrl);

            string basePath = item.PhysicalPath.Replace("\\", "-").Replace(":", "");
            string path = Path.Combine(Path.GetTempPath(), string.Format("{0}-{1}{2}{3}.jpg", basePath, size.Height, size.Width, quality));

            return File.Exists(path) ? string.Concat("<img src=\"", path, "\" />") : CreateNewThumbnail(item, size, path, quality);
        }

        private static string CreateNewThumbnail(IProjectItem item, Size size, string path, int quality)
        {
            using (Image image = Image.FromFile(item.PhysicalPath))
            using (Image thumb = Thumbnail.FromImage(image, size.Width, size.Height))
            {
                JpgFormat.Save(path, thumb, quality);
                return string.Concat("<img src=\"", path, "\" />");
            }
        }
    }
}