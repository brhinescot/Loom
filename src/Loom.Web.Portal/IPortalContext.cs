#region Using Directives

using System;
using Loom.Web.Portal.Configuration;

#endregion

namespace Loom.Web.Portal
{
    public interface IPortalContext
    {
        IHttpContext HttpContext { get; }
        ScriptSetting JQuery { get; }
        IPortalRequest Request { get; }
        IPortalResponse Response { get; }
        bool SetupMode { get; }
        DateTime LastModified { get; }
        PortalSettingsSection Config { get; }
        ScriptSetting Modernizer { get; }

        VirtualResourceData GetVirtualResourceData(string name);
    }
}