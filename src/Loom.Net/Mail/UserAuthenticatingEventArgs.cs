#region Using Directives

using System.ComponentModel;
using System.Security;

#endregion

namespace Loom.Net.Mail
{
    /// <summary>
    /// </summary>
    public sealed class UserAuthenticatingEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UserAuthenticatingEventArgs" /> class.
        /// </summary>
        /// <param name="userName">The login name of the user.</param>
        /// <param name="password">The users password.</param>
        /// <param name="mailbox">The mailbox the user is attempting to log in to.</param>
        public UserAuthenticatingEventArgs(string userName, SecureString password, string mailbox)
        {
            UserName = userName;
            Password = password;
            Mailbox = mailbox;
        }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; }

        /// <summary>
        ///     Gets the users password.
        /// </summary>
        /// <value>The password.</value>
        public SecureString Password { get; }

        /// <summary>
        ///     Gets the mailbox the user is attempting to log in to.
        /// </summary>
        /// <value>The name of the mailbox.</value>
        public string Mailbox { get; }
    }
}