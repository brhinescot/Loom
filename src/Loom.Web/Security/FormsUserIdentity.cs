#region Using Directives

using System.Web.Security;
using Loom.Security;

#endregion

namespace Loom.Web.Security
{
    public class FormsUserIdentity : FormsIdentity, IUserIdentity
    {
        public FormsUserIdentity(FormsAuthenticationTicket ticket, int userId) : base(ticket)
        {
            UserId = userId;
        }

        #region IUserIdentity Members

        public int UserId { get; }

        #endregion
    }
}