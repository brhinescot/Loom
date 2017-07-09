#region Using Directives

using System;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class FilterAttribute : Attribute, IPortalFilter
    {
        #region IPortalFilter Members

        public int Order { get; set; }
        public abstract void Execute(IPortalContext context);

        #endregion
    }
}