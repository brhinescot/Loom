#region Using Directives

using System;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    ///     LogField Attrubute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class LogFieldAttribute : Attribute
    {
        internal LogFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; }

        public bool Required { get; set; }
    }
}