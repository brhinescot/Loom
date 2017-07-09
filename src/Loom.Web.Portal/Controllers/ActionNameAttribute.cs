#region Using Directives

using System;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ActionNameAttribute : Attribute
    {
        public ActionNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}