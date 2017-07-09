#region Using Directives

using System;

#endregion

namespace Loom.Web.Portal.Routing
{
    public abstract class SectionBase
    {
        private Route route;

        public virtual Route DefaultRoute => route ?? (route = new Route(Name, (string.Compare(Name, "home", StringComparison.OrdinalIgnoreCase) == 0 ? null : "/" + Name) + "/{controller}/{action,0,1}/{arguments,*}/", null, null, Name));

        public virtual string Name { get; internal set; }

        public virtual string TenantName { get; internal set; }

        public virtual void OnRegister(SectionContext context) { }
    }
}