#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class AutoStringDictionaryTests
    {
        private const string input = "name=john&id=3277373&state=assigned&mode=admin&action=delete";
        private const string flatHyperlinkList = "More Info=moreinfo.aspx&Contact Us=contactus.aspx&Apply Now=carrers/apply.aspx";

        [Test]
        public void BasicCreate()
        {
            AutoStringDictionary dictionary = AutoStringDictionary.Parse(input, '&', '=');

            Assert.AreEqual(5, dictionary.Count);
            Assert.AreEqual(dictionary["name"], "john");
            Assert.AreEqual(dictionary["id"], "3277373");
            Assert.AreEqual(dictionary["state"], "assigned");
            Assert.AreEqual(dictionary["mode"], "admin");
            Assert.AreEqual(dictionary["action"], "delete");
        }

        [Test]
        public void BasicCreate2()
        {
            AutoStringDictionary dictionary = AutoStringDictionary.Parse(flatHyperlinkList, '&', '=');

            Assert.AreEqual(3, dictionary.Count);
            Assert.AreEqual(dictionary["More Info"], "moreinfo.aspx");
            Assert.AreEqual(dictionary["Contact Us"], "contactus.aspx");
            Assert.AreEqual(dictionary["Apply Now"], "carrers/apply.aspx");
        }

        [Test]
        public void Properties()
        {
            AutoStringDictionary dictionary = new AutoStringDictionary('&', '=');
            dictionary.PairSeperator = '&';
            dictionary.KeyValueSeperator = '=';

            Assert.AreEqual('&', dictionary.PairSeperator);
            Assert.AreEqual('=', dictionary.KeyValueSeperator);
        }

        [Test]
        public void ItemOrder()
        {
            AutoStringDictionary dictionary = new AutoStringDictionary();
            dictionary.KeyValueSeperator = '=';
            dictionary.PairSeperator = '&';

            dictionary.Add("name", "john");
            dictionary.Add("id", "3277373");
            dictionary.Add("state", "assigned");
            dictionary.Add("mode", "admin");
            dictionary.Add("action", "delete");

            Assert.AreEqual(5, dictionary.Count);
            Assert.AreEqual(dictionary["name"], "john");
            Assert.AreEqual(dictionary["id"], "3277373");
            Assert.AreEqual(dictionary["state"], "assigned");
            Assert.AreEqual(dictionary["mode"], "admin");
            Assert.AreEqual(dictionary["action"], "delete");

            Assert.AreEqual(input, dictionary.ToString());
        }

        [Test]
        public void RoundTripCreate()
        {
            AutoStringDictionary expected = AutoStringDictionary.Parse(input, '&', '=');
            AutoStringDictionary actual = AutoStringDictionary.Parse(expected.ToString(), '&', '=');

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected["name"], actual["name"]);
            Assert.AreEqual(expected["id"], actual["id"]);
            Assert.AreEqual(expected["state"], actual["state"]);
            Assert.AreEqual(expected["mode"], actual["mode"]);
            Assert.AreEqual(expected["action"], actual["action"]);
        }
    }
}