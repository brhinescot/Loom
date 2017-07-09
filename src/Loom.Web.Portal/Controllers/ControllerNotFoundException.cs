#region Using Directives

using System;
using System.Diagnostics;
using System.IO;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [DebuggerStepThrough]
    [Serializable]
    public sealed class ControllerNotFoundException : FileNotFoundException
    {
        public ControllerNotFoundException(string controllerName) : base("Unable to find a controller named \"" + controllerName + "\".")
        {
            ControllerName = controllerName;
        }

        public string ControllerName { get; set; }
    }
}