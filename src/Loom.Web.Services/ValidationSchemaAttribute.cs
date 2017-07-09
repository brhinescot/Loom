#region Using Directives

using System;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Specifies (at the class-level) the exact location of a
    ///     schema file (relative to vroot)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ValidationSchemaAttribute : Attribute
    {
        /// <summary>
        ///     File path to a schema file
        /// </summary>
        private readonly string location;

        /// <summary>
        ///     constructs a validation schema for a specific relative path
        /// </summary>
        /// <param name="location"></param>
        public ValidationSchemaAttribute(string location)
        {
            this.location = location;
        }

        /// <summary>
        ///     used to access the location member
        /// </summary>
        public string Location => location;
    }
}