#region Using Directives

using System.Collections;
using NUnit.Framework;

#endregion

namespace Loom.Security
{
    [TestFixture]
    public class ValidationProviderTests
    {
        [Test]
        public void IsEmailPass()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("brian@thegridmaster.com");
            testValues.Add("brian@thegridmaster.net");
            testValues.Add("brian@thegridmaster.org");
            testValues.Add("brian@thegridmaster.cc");
            testValues.Add("brian@thegridmaster.us");
            testValues.Add("brian.scott@thegridmaster.com");
            testValues.Add("brian.scott@design.DevInterop.com");
            testValues.Add("brian_scott@design.DevInterop.com");
            testValues.Add("brian.scott@design_DevInterop.com");

            foreach (string email in testValues)
                Assert.IsTrue(ValidationProvider.IsEmail(email), email + " failed test.");
        }

        [Test]
        public void IsEmailFail()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("brianthegridmaster.com");
            testValues.Add("brian@thegridmaster");
            testValues.Add("brian#thegridmaster.org");
            testValues.Add("@thegridmaster.cc");
            testValues.Add("thegridmaster.us");
            testValues.Add("brian.scott@thegridmaster.agargager");
            testValues.Add("brian.scott@.com");
            testValues.Add("brian_scott@com");
            testValues.Add("DevInterop.com");

            foreach (string email in testValues)
                Assert.IsFalse(ValidationProvider.IsEmail(email), email + " failed test.");
        }

        [Test]
        public void IsNotMalicious()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("Brian Scott");
            testValues.Add("brian@thegridmaster.com");
            testValues.Add("[b]hello[/b]");

            foreach (string value in testValues)
                Assert.IsTrue(ValidationProvider.IsNotMalicious(value), value + " failed test.");
        }

        [Test]
        public void IsMalicious()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("alert(\"hi\");");
            testValues.Add("<script>malicious</script>");
            testValues.Add("<script>--SELECT</script>");
            testValues.Add("<script>''</script>");
            testValues.Add("<div>idjf</div>");
            testValues.Add("<span>idjf</span>");

            foreach (string script in testValues)
                Assert.IsFalse(ValidationProvider.IsNotMalicious(script), script + " failed test.");
        }

        [Test]
        public void IsPhoneNumber()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("777-777-7777");
            testValues.Add("(777)777-7777");
            testValues.Add("(777) 777-7777");
            testValues.Add("777-777-7777 x345");
            testValues.Add("(777)777-7777 x345");
            testValues.Add("(777) 777-7777 x345");
            testValues.Add("777-777-7777 X345");
            testValues.Add("(777)777-7777 X345");
            testValues.Add("(777) 777-7777 X345");
            testValues.Add("777-777-7777 X3459");
            testValues.Add("(777)777-7777 X3459");
            testValues.Add("(777) 777-7777 X3459");

            foreach (string value in testValues)
                Assert.IsTrue(ValidationProvider.IsPhoneNumber(value), value + " failed test.");
        }

        [Test]
        public void IsNotNumber()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("7777777777");
            testValues.Add("555777-7777");
            testValues.Add("555-7777777");

            foreach (string value in testValues)
                Assert.IsFalse(ValidationProvider.IsPhoneNumber(value), value + " failed test.");
        }

        [Test]
        public void IsMoney()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("235.00");
            testValues.Add("235");
            testValues.Add("$235.00");
            testValues.Add("$235");

            foreach (string value in testValues)
                Assert.IsTrue(ValidationProvider.IsMoney(value), value + " failed test.");
        }

        [Test]
        public void IsSocialSecurity()
        {
            ArrayList testValues = new ArrayList();
            testValues.Add("421-31-8900");
            testValues.Add("421318900");

            foreach (string value in testValues)
                Assert.IsTrue(ValidationProvider.IsSocialSecurityNumber(value), value + " failed test.");
        }

        [Test]
        public void IsNotSocialSecurity()
        {
            ArrayList testValues = new ArrayList();

            // 666 not valid
            testValues.Add("666-31-8900");
            testValues.Add("666318900");

            // 588 state code not valid
            testValues.Add("588-31-8900");

            // 69[1-9] state code not valid
            testValues.Add("691-31-8900");
            testValues.Add("692-31-8900");
            testValues.Add("693-31-8900");
            testValues.Add("694-31-8900");
            testValues.Add("695-31-8900");
            testValues.Add("696-31-8900");
            testValues.Add("697-31-8900");
            testValues.Add("698-31-8900");
            testValues.Add("699-31-8900");

            // All zeros in a position not valid
            testValues.Add("000-31-8900");
            testValues.Add("421-00-8900");
            testValues.Add("421-31-0000");

            foreach (string value in testValues)
                Assert.IsFalse(ValidationProvider.IsSocialSecurityNumber(value), value + " failed test.");
        }
    }
}