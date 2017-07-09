#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal delegate ActionResult ActionExecuter(IController target, List<object> values);
}