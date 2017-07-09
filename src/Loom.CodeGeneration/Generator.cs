#region Using Directives

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

#endregion

namespace Loom.CodeGeneration
{
    public class Generator
    {
        public Generator(CodeDomProvider codeProvider, string extension)
        {
            DefaultExtension = extension;
            CodeProvider = codeProvider;
        }

        public string DefaultExtension { get; }
        public CodeDomProvider CodeProvider { get; set; }

        public void WriteProjectItemChildren(string projectItemPath, IFileProcessor processor)
        {
            ProjectItem parentProjectItem = RetrieveProjectItem(projectItemPath);

            if (parentProjectItem == null)
                throw new InvalidOperationException("Could not create a project item from the path '" + projectItemPath + ".");

            List<ProjectItem> previousItems = new List<ProjectItem>();
            List<ProjectItem> currentItems = new List<ProjectItem>();

            foreach (ProjectItem item in parentProjectItem.ProjectItems)
                previousItems.Add(item);

            foreach (GeneratedObject genedClass in processor.GenerateObjects())
            {
                string fileName = genedClass.Name.ToPascalCase() + DefaultExtension;
                string path = Path.Combine(projectItemPath.Substring(0, projectItemPath.LastIndexOf(Path.DirectorySeparatorChar)), fileName);

                WriteProjectFile(Messages.FileHeader(projectItemPath) + genedClass.Content, path);

                ProjectItem previousItem = previousItems.Find(p => p.Name == Path.GetFileName(path));
                currentItems.Add(previousItem == default(ProjectItem) ? parentProjectItem.ProjectItems.AddFromFile(path) : previousItem);
            }

            DeleteRemovedItems(parentProjectItem, currentItems);
        }

        public byte[] GenerateFromCode(string code)
        {
            CodeSnippetCompileUnit unit = new CodeSnippetCompileUnit {Value = code};
            using (StringWriter writer = new StringWriter())
            {
                CodeProvider.GenerateCodeFromCompileUnit(unit, writer, null);
                return Encoding.UTF8.GetBytes(writer.ToString());
            }
        }

        private static void WriteProjectFile(string content, string path)
        {
            File.WriteAllBytes(path, Encoding.UTF8.GetBytes(content));
        }

        private static void DeleteRemovedItems(ProjectItem parentItem, List<ProjectItem> currentItems)
        {
            string defaultGenedClass = Path.GetFileNameWithoutExtension(parentItem.Name);
            foreach (ProjectItem previousItem in parentItem.ProjectItems)
            {
                string previousItemName = previousItem.Name;
                if (currentItems.Find(p => p.Name == previousItemName) == null && Path.GetFileNameWithoutExtension(previousItem.Name) != defaultGenedClass)
                    previousItem.Delete();
            }
        }

        private static ProjectItem RetrieveProjectItem(string documentPath)
        {
            int itemFound;
            uint itemId;
            VSDOCUMENTPRIORITY[] pdwPriority = new VSDOCUMENTPRIORITY[1];

            DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            if (dte == null)
                throw new InvalidOperationException("Cannot get the global service from package. DTE is null. Document path is " + documentPath);

            Array ary = dte.ActiveSolutionProjects as Array;
            if (ary == null)
                throw new InvalidOperationException("Cannot get the active solution projects. Document path is " + documentPath);

            Project project = ary.GetValue(0) as Project;
            if (project == null)
                throw new InvalidOperationException("Cannot get the first project. Document path is " + documentPath);

            IVsProject vsProject = VsHelper.ToVsProject(project);
            if (project == null)
                throw new InvalidOperationException("Cannot convert project to VS project. Document path is " + documentPath);

            vsProject.IsDocumentInProject(documentPath, out itemFound, pdwPriority, out itemId);

            if (itemFound == 0 || itemId == 0)
                throw new InvalidOperationException("VsProject.IsDocumentInProject failed to find the document in the project. Document path is " + documentPath);

            IServiceProvider oleSp;
            vsProject.GetItemContext(itemId, out oleSp);
            if (oleSp == null)
                throw new InvalidOperationException("Cannot get item context. Document path is " + documentPath);

            ServiceProvider sp = new ServiceProvider(oleSp);
            ProjectItem item = sp.GetService(typeof(ProjectItem)) as ProjectItem;

            return item;
        }
    }
}