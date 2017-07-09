#region Using Directives

using System;
using System.Security.Principal;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AuthorizeAttribute : FilterAttribute
    {
        public override void Execute(IPortalContext context)
        {
            IPrincipal user = context.HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
                return;

            ControllerErrorResult result = new ControllerErrorResult();
            result.ViewData.Message = "You are not authorized.";
            context.Request.Result = result;
        }
    }
}