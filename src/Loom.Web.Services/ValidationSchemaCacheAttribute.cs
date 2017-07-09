#region Using Directives

using System;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Specifies (at the class-level) the location of a directory
    ///     containing schema files (relative to vroot)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ValidationSchemaCacheAttribute : Attribute
    {
        /// <summary>
        ///     path to directory that holds schemas/wsdl files
        /// </summary>
        private string relativeDirectory;

        /// <summary>
        ///     COnstructs schema cache for specific relative directory
        /// </summary>
        /// <param name="relativeDirectory"></param>
        public ValidationSchemaCacheAttribute(string relativeDirectory)
        {
            this.relativeDirectory = relativeDirectory;
        }

        /// <summary>
        ///     access path to relative directory
        /// </summary>
        public string RelativeDirectory
        {
            get => relativeDirectory;
            set => relativeDirectory = value;
        }
    }
}