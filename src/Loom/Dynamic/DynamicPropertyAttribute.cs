#region Using Directives

using System;

#endregion

namespace Loom.Dynamic
{
    public class DynamicPropertyAttribute : Attribute
    {
        public DynamicPropertyAttribute() { }

        public DynamicPropertyAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}