#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ActiveProcedureAttribute : Attribute
    {
        public ActiveProcedureAttribute(string owner, string name)
        {
            Name = name;
            Owner = owner;
        }

        public string Name { get; }

        public string Owner { get; }
    }
}