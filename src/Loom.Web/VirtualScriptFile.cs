#region Using Directives

using System.IO;
using System.Web;
using System.Web.Hosting;

#endregion

namespace Loom.Web
{
    public class VirtualScriptFile : VirtualFile
    {
        public VirtualScriptFile(string virtualPath) : base(virtualPath) { }

        public override Stream Open()
        {
            string storageKey = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Replace("~/Virtual_Script/", string.Empty);

            if (Compare.IsNullOrEmpty(storageKey))
                throw new HttpException(404, string.Format("The script virtual path '{0}' is invalid. The required format is '/Virtual_Script/[Cache Key]'.", VirtualPath));

            string cachedScript = HttpContext.Current.Cache[storageKey] as string;
            if (cachedScript == null)
                throw new HttpException(404, string.Format("The script virtual path '{0}' is invalid or script is not cached. The required format is '/Virtual_Script/[Cache Key]'.", VirtualPath));

            Stream stream = cachedScript.ToStream();
            if (stream == null || stream.Length == 0)
                throw new HttpException(404, string.Format("The script virtual path '{0}' is invalid or not a script file. The required format is '/Virtual_Script/[Cache Key]'.", VirtualPath));

            return stream;
        }
    }
}