#region Using Directives

using System;
using System.Web;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.Controllers
{
    /// <summary>
    ///     Represents a custom <see cref="Attribute" /> used to mark an <see cref="IController" />
    ///     action as requiring an anti-forgery token.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use this <see cref="Attribute" /> in conjunction with an <see cref="AntiForgeryToken" /> control.
    ///         The control should be placed within the form that posts it's values to the decorated <see cref="IController" />
    ///         action.
    ///     </para>
    ///     <para>
    ///         <see cref="IController" /> actions decorated with this attribute will throw an <see cref="HttpException" /> if
    ///         the form token does
    ///         not exist, if the cookie token does not exist, or if the encrypted values of the form token and the cookie
    ///         token
    ///         do not match.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AntiForgeryAttribute : Attribute, IControllerFilter
    {
        public AntiForgeryAttribute() { }

        public AntiForgeryAttribute(string salt)
        {
            Salt = salt;
        }

        public string Salt { get; set; }

        #region IControllerFilter Members

        public void OnProcessAction(IPortalContext context) { }

        #endregion
    }
}