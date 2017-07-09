#region Using Directives

using System;
using Loom.Mocks;
using Loom.Threading;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class ExceptionFormatterTests
    {
        [Test]
        public void Test1()
        {
            try
            {
                Exception e = new LockTimeoutException("This is a test exception");
                e.Data.Add("Platform", "Bing");
                MockExceptionThrower mock = new MockExceptionThrower();
                mock.ExceptionToThrow = e;
                mock.Depth = 20;
                mock.Throw();
            }
            catch (Exception ex)
            {
                ExceptionFormatter formater = new ExceptionFormatter("Test Application", "Exception Report Header", "Location: Home Office", "Version: 3.4");
                formater.WriteExceptionHash = true;
                formater.WriteLoadedAssemblies = true;
                string message = formater.Generate(ex);
                Assert.IsNotNull(message);
            }
        }
    }
}