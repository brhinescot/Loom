#region Using Directives

using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

#endregion

namespace Loom.Web
{
    internal class AssemblyResourceVirtualFile : VirtualFile
    {
        private const string Extension = ".dll";
        private static readonly char[] Seperator = {'/'};

        public AssemblyResourceVirtualFile(string virtualPath) : base(virtualPath)
        {
            Argument.Assert.IsNotNullOrEmpty(virtualPath, nameof(virtualPath));
        }

        public override Stream Open()
        {
            string[] parts = VirtualPath.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                throw new FileNotFoundException(string.Format("The assembly resource url {0} is invalid. The required url format is '/Virtual_Resource/[Assembly File Name Without Extension]/[Assembly Resource Path]'.", VirtualPath));

            string assemblyName = parts[parts.Length - 2] + Extension;
            string resourceName = parts[parts.Length - 1];

            string filePath = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format("The assembly resource path {0} is invalid. The required format is '/Virtual_Resource/[Assembly File Name Without Extension]/[Assembly Resource Path]'.", VirtualPath), filePath);

            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFile(filePath);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(string.Format("The assembly resource file '{0}' could not be found. Assembly resource files should be compiled as an embedded resource.", filePath), ex);
            }

            Stream stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
                throw new FileNotFoundException(string.Format("The assembly resource path '{0}' could not be found. The required url format is '/Virtual_Resource/[Assembly File Name Without Extension]/[Assembly Resource Path]'.", resourceName));

            return stream;
        }
    }
}