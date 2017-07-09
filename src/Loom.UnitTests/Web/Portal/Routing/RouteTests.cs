#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Web.Portal.Routing
{
    [TestFixture]
    public class RouteTests
    {
        [Test]
        public void ConstructorInitialized()
        {
            Route route = new Route("ConstructorInitialized", "/setup/{action}/{constraint}");

            Assert.IsTrue(route.IsMatch("/setup/list/all"));
            Assert.IsFalse(route.IsMatch("/admin/list/all"));

            RouteTokens tokens = route.GetTokens("/setup/list/all");
            Assert.IsNotNull(tokens);
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual("list", tokens["action"]);
            Assert.AreEqual("all", tokens["constraint"]);
        }

        [Test]
        public void ConstructorInitializedMultiToken()
        {
            Route route = new Route("ConstructorInitialized2", "/setup/{action,2}/{constraint}");

            Assert.IsTrue(route.IsMatch("/setup/users/list/all"));
            Assert.IsFalse(route.IsMatch("/admin/users/list/all"));

            RouteTokens tokens = route.GetTokens("/setup/users/list/all");
            Assert.IsNotNull(tokens);
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual("users/list", tokens["action"]);
            Assert.AreEqual("all", tokens["constraint"]);

            Assert.IsTrue(tokens.IsMultiToken("action"));
            string[] multiToken = tokens.GetMultiToken("action");
            Assert.AreEqual(2, multiToken.Length);
            Assert.AreEqual("users", multiToken[0]);
            Assert.AreEqual("list", multiToken[1]);
        }

        [Test]
        public void PropertyInitialized()
        {
            Route route = new Route(null, "/setup/{action}/{constraint}");

            Assert.IsTrue(route.IsMatch("/setup/list/all"));
            Assert.IsFalse(route.IsMatch("/admin/list/all"));
        }

        [Test]
        public void PropertyInitialized2()
        {
            Route route = new Route(null, "/setup/{action,2}/{constraint}");

            Assert.IsTrue(route.IsMatch("/setup/users/list/all"));
            Assert.IsFalse(route.IsMatch("/admin/users/list/all"));
        }
    }
}