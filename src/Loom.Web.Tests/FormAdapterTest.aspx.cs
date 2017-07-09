#region Using Directives

using System;
using System.Web.UI;
using Loom.Web.UI;

#endregion

namespace Loom.Web.Tests
{
    public partial class FormAdapterTest : Page
    {
        private FormAdapter<TestUser> adapter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            adapter = new FormAdapter<TestUser>(form1);
            adapter.AddMapping("Id", "UserId");

            adapter.FillForm(new TestUser {Email = "test@test.com", Id = 12345, Name = "Test Name", Percent = 123.456f});
        }

        protected void Button1Click(object sender, EventArgs e)
        {
            TestUser emptyUser = new TestUser();
            adapter.FillEntity(emptyUser);

            WritePropertyValues(emptyUser);
        }

        private void WritePropertyValues(TestUser user)
        {
            Response.Write("Property Values:");
            Response.Write("<br />&nbsp;&nbsp;TestUser.Id = ");
            Response.Write(user.Id);
            Response.Write("<br />&nbsp;&nbsp;TestUser.Name = ");
            Response.Write(user.Name);
            Response.Write("<br />&nbsp;&nbsp;TestUser.Email = ");
            Response.Write(user.Email);
            Response.Write("<br />&nbsp;&nbsp;TestUser.Percent = ");
            Response.Write(user.Percent);
            Response.Write("<br />");
            Response.Write("<br />");
        }

        #region Nested type: TestUser

        internal class TestUser
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public int Id { get; set; }
            public float Percent { get; set; }
        }

        #endregion
    }
}