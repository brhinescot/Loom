#region Using Directives

using System.Security.Principal;

#endregion

namespace Loom.Security
{
    public class UserIdentity : GenericIdentity, IUserIdentity
    {
        public UserIdentity(string name, int userId) : base(name)
        {
            UserId = userId;
        }

        public UserIdentity(string name, int userId, string type) : base(name, type)
        {
            UserId = userId;
        }

        #region IUserIdentity Members

        public int UserId { get; set; }

        #endregion
    }
}