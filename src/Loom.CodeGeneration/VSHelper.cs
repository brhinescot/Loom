#region Using Directives

using System;
using System.Diagnostics;
using System.Xml;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

#endregion

namespace Loom.CodeGeneration
{
    internal static class VsHelper
    {
        public static IVsProject ToVsProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            IVsProject vsProject = ToHierarchy(project) as IVsProject;
            if (vsProject == null)
                throw new ArgumentException("Project is not a VS project.");

            return vsProject;
        }

        private static IVsHierarchy ToHierarchy(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            string projectGuid = null;

            // DTE does not expose the project GUID that exists in the msbuild project file.        
            // Cannot use MSBuild object model because it uses a static instance of the Engine,         
            // and using the Project will cause it to be unloaded from the engine when the         
            // GC collects the variable that we declare.       
            using (XmlReader projectReader = XmlReader.Create(project.FileName))
            {
                projectReader.MoveToContent();
                object nodeName = projectReader.NameTable.Add("ProjectGuid");
                while (projectReader.Read())
                {
                    if (!Equals(projectReader.LocalName, nodeName))
                        continue;

                    projectGuid = projectReader.ReadElementContentAsString();
                    break;
                }
            }

            Debug.Assert(!string.IsNullOrEmpty(projectGuid));
            IServiceProvider serviceProvider = new ServiceProvider(project.DTE as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);
            return VsShellUtilities.GetHierarchy(serviceProvider, new Guid(projectGuid));
        }
    }
}