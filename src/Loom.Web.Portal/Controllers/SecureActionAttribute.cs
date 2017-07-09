#region Using Directives

using System;

#endregion

namespace Loom.Web.Portal.Controllers
{
    /// <summary>
    ///     Causes a controller action to require a POST request type, application/json content type,
    ///     a JSON object argument, and an anti-forgery token. This class can not be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SecureActionAttribute : Attribute
    {
        public SecureActionAttribute() : this(false) { }

        public SecureActionAttribute(bool allowRouteTokens, string antiForgerySalt = null)
        {
            AllowRouteTokens = allowRouteTokens;
            AntiForgerySalt = antiForgerySalt;
        }

        public bool AllowRouteTokens { get; set; }
        public string AntiForgerySalt { get; set; }
    }
}