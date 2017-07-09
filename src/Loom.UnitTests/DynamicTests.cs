#region Using Directives

using System;
using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class DynamicTests
    {
        [Test]
        public void Test()
        {
            dynamic d = GetClass();

            Assert.AreEqual(d.Name, "Zach");
        }

        [Test]
        public void Test2()
        {
            dynamic d = new ExpandoObject();
            d.Name = "Brian";
            d.Age = 36;
            d.Birthday = DateTime.Parse("06/26/1973");

            Assert.AreEqual(d.Name, "Brian");
            Assert.AreEqual(d.Age, 36);
            Assert.AreEqual(d.Birthday, DateTime.Parse("06/26/1973"));

            d.Name = "Zach";
            Assert.AreEqual(d.Name, "Zach");
        }

        [Test]
        public void RouteDynamicTests()
        {
            dynamic t = new TesterDynamic();
            t.Page = "default.aspx";

            int i = 0;

            string route = GetRoute(t);
            string nullRoute = GetRoute(i);

            Assert.AreEqual("default.aspx", route);
            Assert.IsNull(nullRoute);
        }

        [Test]
        public void PropertyValueTest()
        {
            dynamic d = new TesterDynamic();
            d.Property = "Property";

            string actual = GetPropertyValue(d.Property);

            Assert.AreEqual("Dynamic: Property", actual);
        }

        private object GetClass()
        {
            return new NormalClass {Name = "Zach"};
        }

        private string GetRoute(dynamic values)
        {
            TesterDynamic t = values as TesterDynamic;
            if (t == null)
                return null;

            return t.GetValues()["Page"].ToString();
        }

        private string GetPropertyValue(dynamic prop)
        {
            return "Dynamic: " + prop;
        }
    }

    public class NormalClass
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class TesterDynamic : DynamicObject
    {
        private readonly Dictionary<string, object> lookup = new Dictionary<string, object>();

        public Dictionary<string, object> GetValues()
        {
            return lookup;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            lookup[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!lookup.ContainsKey(binder.Name))
                throw new ArgumentException("Error getting member value. The instance of DynamicObject does not contain a member named \"" + binder.Name + "\".");

            result = lookup[binder.Name];
            return true;
        }
    }
}