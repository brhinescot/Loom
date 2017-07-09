#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using Loom.Collections;

#endregion

namespace Loom.Extensibility
{
    /// <summary>
    ///     Represents a class used to load plug-ins into the current <see cref="AppDomain" />
    /// </summary>
    public sealed class PluginLoader : MarshalByRefObject
    {
        private PluginLoader() { }

        /// <summary>
        ///     Loads the plug-ins at the specified path with the specified extension into
        ///     the current <see cref="AppDomain" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The plug-ins are first loaded into a temporary <see cref="AppDomain" /> where they are
        ///         validated. All valid plug-ins are then loaded into the current <see cref="AppDomain" />.
        ///     </para>
        ///     <para>
        ///         Invalid plug-ins are unloaded when the temporary <see cref="AppDomain" /> is unloaded.
        ///     </para>
        /// </remarks>
        /// <param name="directory">The directory in which to search for valid plug-ins.</param>
        /// <param name="extension">The extension of the plug-in assemblies.</param>
        /// <typeparam name="T">
        ///     The implemented interface or the base class derived from
        ///     by the plug-ins.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="CollectionList{T}" /> of all plug-ins that were loaded.
        /// </returns>
        public static CollectionList<T> Load<T>(string directory, string extension)
        {
            return Load<T>(directory, extension, false);
        }

        /// <summary>
        ///     Loads the plug-ins at the specified path with the specified extension into
        ///     the current <see cref="AppDomain" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The plug-ins are first loaded into a temporary <see cref="AppDomain" /> where they are
        ///         validated. All valid plug-ins are then loaded into the current <see cref="AppDomain" />.
        ///     </para>
        ///     <para>
        ///         Invalid plug-ins are unloaded when the temporary <see cref="AppDomain" /> is unloaded.
        ///     </para>
        /// </remarks>
        /// <param name="directory">The directory in which to search for valid plug-ins.</param>
        /// <param name="extension">The extension of the plug-in assemblies.</param>
        /// <typeparam name="T">
        ///     The implemented interface or the base class derived from
        ///     by the plug-ins.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="CollectionList{T}" /> of all plug-ins that were loaded.
        /// </returns>
        /// <param name="includeSubDirectories">
        ///     true if subdirectories should be searched for
        ///     valid assemblies.
        /// </param>
        public static CollectionList<T> Load<T>(string directory, string extension, bool includeSubDirectories)
        {
            CollectionList<T> plugins = new CollectionList<T>();
            List<string> assemblyPaths = LoadValidAssemblies<T>(directory, extension, includeSubDirectories);
            for (int i = 0; i < assemblyPaths.Count; i++)
                try
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyPaths[i]);
                    plugins.AddRange(ExamineAssembly<T>(assembly));
                }
                catch (ReflectionTypeLoadException) { }
                catch (FileLoadException) { }
            return plugins;
        }

        #region Private Methods

        private static IEnumerable<T> ExamineAssembly<T>(_Assembly assembly)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                types = exception.Types;
            }

            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                //Look for public types and ignore abstract classes
                //
                if (type.IsPublic && type.Attributes != TypeAttributes.Abstract && typeof(T).IsAssignableFrom(type))
                    yield return (T) assembly.CreateInstance(type.FullName);
            }
        }

        /// <exception cref="InvalidOperationException">
        ///     Fatal error encountered while attempting
        ///     to load plug-ins.
        /// </exception>
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
        private static List<string> LoadValidAssemblies<T>(string pluginsDirectory, string pluginExtension, bool searchSubDirectories)
        {
            // Create a temporary app domain so we can unload invalid assemblies
            //
            AppDomain domain = AppDomain.CreateDomain(Guid.NewGuid().ToString());
            domain.SetupInformation.ApplicationBase = Environment.CurrentDirectory;

            List<string> validAssemblies;

            try
            {
                // Find the name of this assembly and load it into the domain
                //
                AssemblyName thisAssembly = Assembly.GetExecutingAssembly().GetName(false);
                domain.Load(thisAssembly);

                // Find the name of executing assembly and load it into the domain
                //
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                domain.Load(entryAssembly.GetName(false));

                // Find the names of the assemblies referenced by entryAssembly and load them into the domain
                //
                foreach (AssemblyName referencedAssembly in entryAssembly.GetReferencedAssemblies())
                    domain.Load(referencedAssembly);

                // Create an instance of this PluginLoader type 
                // in the temporary domain
                //
                const BindingFlags binding = BindingFlags.CreateInstance |
                                             BindingFlags.NonPublic |
                                             BindingFlags.Instance;

                ObjectHandle handle = domain.CreateInstanceFrom(thisAssembly.CodeBase,
                    typeof(PluginLoader).ToString(), false,
                    binding, null, null, null, null);

                if (handle == null)
                    throw new InvalidOperationException("Fatal error encountered while attempting to load plug-ins.");

                PluginLoader helper = (PluginLoader) handle.Unwrap();
                validAssemblies = helper.DiscoverPluginAssembliesHelper(typeof(T), pluginsDirectory, pluginExtension, searchSubDirectories);
            }
            finally
            {
                // Unload any unwanted assemblies
                //
                AppDomain.Unload(domain);
            }
            return validAssemblies;
        }

        private static string[] ParsePluginDirectory(string pluginDirectory, string pluginExtension, bool searchSubDirectories)
        {
            string searchPath = Path.Combine(Directory.GetCurrentDirectory(), pluginDirectory);
            if (!Directory.Exists(searchPath))
                return new string[0];

            List<string> filePaths = new List<string>();
            filePaths.AddRange(Directory.GetFileSystemEntries(searchPath, pluginExtension));

            if (searchSubDirectories)
                ParsePluginSubDirectories(filePaths, pluginExtension, searchPath);

            return filePaths.ToArray();
        }

        private static void ParsePluginSubDirectories(List<string> filePaths, string pluginExtension, string searchPath)
        {
            string[] subDirectories = Directory.GetDirectories(searchPath);
            for (int i = 0; i < subDirectories.Length; i++)
                filePaths.AddRange(Directory.GetFileSystemEntries(subDirectories[i], pluginExtension));
        }

        // ReSharper disable MemberCanBeMadeStatic.Local
        // Must not be static in order to marshal across domain boundaries.
        private List<string> DiscoverPluginAssembliesHelper(Type pluginType, string pluginDirectory, string pluginExtension, bool searchSubDirectories)
        {
            List<string> assemblies = new List<string>();
            string[] paths = ParsePluginDirectory(pluginDirectory, pluginExtension, searchSubDirectories);
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                try
                {
                    Assembly assembly = Assembly.LoadFile(path);
                    //Enumerate through the assembly object
                    //

                    Type[] types;
                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException exception)
                    {
                        types = exception.Types;
                    }

                    foreach (Type type in types)
                    {
                        //Look for public types and ignore abstract classes
                        //
                        if (!type.IsPublic || type.Attributes == TypeAttributes.Abstract || !pluginType.IsAssignableFrom(type))
                            continue;

                        if (!assemblies.Contains(path))
                            assemblies.Add(path);
                    }
                }
                catch (ReflectionTypeLoadException) { }
                catch (FileLoadException) { }
            }

            return assemblies;
        }

        #endregion
    }
}