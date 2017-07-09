#region Using Directives

using System;
using System.Collections.Generic;
using System.Transactions;
using Loom.Cryptography;
using Loom.Security;
using Loom.Web.Portal.Data;
using Loom.Web.Portal.Data.Person;
using Loom.Web.Portal.Data.Portal;
using Loom.Web.Portal.Data.Security;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal sealed class SetupController : ResourceViewController
    {
        private const string AdminSimpleTemplate = "Loom.Web.Portal.Resources.Templates.Admin.Simple.aspx";

        public ActionResult Index()
        {
            using (PortalDataSession session = new PortalDataSession("portal"))
            {
                int loginCount = GetLoginCount(session);
                return loginCount == 0 ? Redirect((SetupController c) => c.ShowAddAdmin()) : Redirect((SetupController c) => c.ShowLogin(null));
            }
        }

        [SecureAction(AntiForgerySalt = "sfgasdfgsaergsadfhgsdfh")]
        public ActionResult DoLogin(string username, string password)
        {
            if (Compare.IsAnyNullOrEmpty(username, password))
                return Message(MessageResultType.Error, "Login Error", "A username and password is required.");

            Login login;
            using (PortalDataSession session = new PortalDataSession())
            {
                List<Login> logins = session.Logins
                    .Join<User>(Login.Columns.LoginId == User.Columns.LoginId)
                    .SelectAll().Select(User.Columns.UserId.As("LoginId"))
                    .Where(User.Columns.UserName == username)
                    .And(User.Columns.Deleted == false).End()
                    .ToList();

                if (Compare.IsNullOrEmpty(logins))
                    return Message(MessageResultType.Error, "Login Error", "The username or password is incorrect.");

                login = logins[0];
            }

            if (!HashProvider.SHA512.Compare(password, login.Password, 12))
                return Message(MessageResultType.Error, "Login Error", "The username or password is incorrect.");

            Response.SetLoginCookie(username, login.LoginId);
            return AjaxRedirect((SetupController c) => c.ShowDashboard());
        }

        [Authorize]
        [ActionName("Dashboard")]
        public ActionResult ShowDashboard()
        {
            ActionResult result = GetTemplateResource(AdminSimpleTemplate);
            AddDashboardModules(result as IViewResult);
            return result;
        }

        [ActionName("Login")]
        public ActionResult ShowLogin(string user)
        {
            ActionResult result = GetTemplateResource(AdminSimpleTemplate);
            AddLoginModules(result as IViewResult);
            ViewData.Username = user;
            return result;
        }

        [ActionName("AddAdministrator")]
        public ActionResult ShowAddAdmin()
        {
            ActionResult result = GetTemplateResource(AdminSimpleTemplate);
            AddAccountSetupModules(result as IViewResult);
            return result;
        }

        [SecureAction(AntiForgerySalt = "sfgasdfgsaergsadfhgsdfh")]
        public ActionResult InsertAdmin([HtmlEncode] string userName, string password, string passwordConfirm, string emailAddress)
        {
            if (string.IsNullOrEmpty(password))
                return Message(MessageResultType.Error, "Error Creating Account", "A password is required.");

            if (password != passwordConfirm)
                return Message(MessageResultType.Error, "Error Creating Account", "The passwords do not match.");

            using (PortalDataSession session = new PortalDataSession("portal"))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    session.User = new UserIdentity("System", 1);

                    int loginCount = GetLoginCount(session);
                    if (loginCount > 0)
                        return Message(MessageResultType.Error, "Error Creating Account", "An administrator account has already been created");

                    try
                    {
                        EntityBase entity = new EntityBase();
                        entity.DisplayName = "Administrator";
                        entity.TenantId = 1;
                        session.Insert(entity);

                        Contact contact = new Contact();
                        contact.Email = emailAddress;
                        contact.FirstName = "System";
                        contact.LastName = "Administrator";
                        contact.EntityId = entity.EntityId;
                        session.Insert(contact);

                        Login login = new Login();
                        login.Password = HashProvider.SHA512.GenerateString(password, "XXXXXXXXXXXX");
                        login.ResetAnswer = string.Empty;
                        login.ResetQuestion = string.Empty;
                        login.GroupMask = 1;
                        session.Insert(login);

                        User user = new User();
                        user.UserName = userName;
                        user.LoginId = login.LoginId;
                        user.ContactId = contact.ContactId;
                        session.Insert(user);
                    }
                    catch (Exception)
                    {
                        return Message(MessageResultType.Error, "An Error Was Encountered", "Unable to save.");
                    }

                    scope.Complete();
                }

                return AjaxRedirect((SetupController c) => c.ShowLogin(userName));
            }
        }

        public ActionResult RedirectClient()
        {
            return AjaxRedirect("~/Template.aspx");
        }

        private static int GetLoginCount(PortalDataSession session)
        {
            return session.Users.Where(User.Columns.UserName != "system").End().Count;
        }

        private void AddAccountSetupModules(IViewResult result)
        {
            if (result == null)
                return;

            Response.Tiles.Add(new TileDefinition("pageHeader", "~/moduleresource/Admin/Logo.ascx"));
            Response.Tiles.Add(new TileDefinition("middlePart", "~/moduleresource/Admin/AddUser.ascx"));
        }

        private void AddLoginModules(IViewResult result)
        {
            if (result == null)
                return;

            Response.Tiles.Add(new TileDefinition("pageHeader", "~/moduleresource/Admin/Logo.ascx"));
            Response.Tiles.Add(new TileDefinition("middlePart", "~/moduleresource/Admin/Login.ascx"));
        }

        private void AddDashboardModules(IViewResult result)
        {
            if (result == null)
                return;

            Response.Tiles.Add(new TileDefinition("pageHeader", "~/moduleresource/Admin/Logo.ascx"));
        }
    }
}