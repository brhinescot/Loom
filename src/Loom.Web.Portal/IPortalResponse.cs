#region Using Directives

using System;
using System.Collections.ObjectModel;
using Loom.Annotations;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal
{
    public interface IPortalResponse
    {
        Uri RedirectLocation { get; set; }
        bool IsRedirected { get; set; }
        Collection<TileDefinition> Tiles { get; }
        void AjaxRedirect(string url, bool endResponse = false);

        [NotNull]
        string GetCallbackUrl();

        void WriteJson(object o);
        void WriteJson(string json);
        void WriteJson(StringCreator jsonStringCreator);
        void SetLoginCookie(string userName, int userId, string userRole = null);
    }
}