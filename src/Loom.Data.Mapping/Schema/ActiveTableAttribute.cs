#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ActiveTableAttribute : Attribute
    {
        public ActiveTableAttribute(string owner, string name) : this(owner, name, null) { }

        public ActiveTableAttribute(string owner, string name, string keyColumn)
        {
            Name = string.Intern(name);
            Owner = string.Intern(owner);
            KeyColumn = keyColumn;
        }

        public string CreatedByColumn { get; set; }
        public string CreatedOnColumn { get; set; }
        public string Datasource { get; set; }

        public string DeletedByColumn { get; set; }
        public string DeletedColumn { get; set; }
        public string KeyColumn { get; }

        public string ModifiedByColumn { get; set; }
        public string ModifiedOnColumn { get; set; }
        public string Name { get; }

        public string Owner { get; }

        public bool ReadOnly { get; set; }
    }
}