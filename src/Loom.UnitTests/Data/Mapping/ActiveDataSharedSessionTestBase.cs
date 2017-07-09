#region Using Directives

using System.Data.Common;
using AdventureWorks;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping
{
    public class ActiveDataSharedSessionTestBase : ThreadedTestBase
    {
        protected AdventureWorksDataSession Session { get; private set; }

        // BUG: Possible: Investigate connection error when Session setup and tear down occurs per test. Could point to need to better handle disposed sessions.
        [TestFixtureSetUp]
        public void Initialize()
        {
            Session = new AdventureWorksDataSession();
        }

        [TestFixtureTearDown]
        public void CleanUp()
        {
            Session.Dispose();
        }

        protected static void AssertCommandTextSame<T>(string expected, EntitySet<T> actual) where T : DataRecord<T>, new()
        {
            Util.AssertSqlSame(expected, actual.CreateCommand());
        }

        protected void AssertCommandTextSame(string expected, DbCommand actual)
        {
            Util.AssertSqlSame(expected, actual);
        }

        protected static void AssertCommandTextSame(string expected, CommandBuilder actual)
        {
            Util.AssertSqlSame(expected, actual.ToSelectCommand());
        }

        protected static void AssertParamCountSame<T>(EntitySet<T> entitySet, int count) where T : DataRecord<T>, new()
        {
            DbCommand command = entitySet.CreateCommand();
            AssertParamCountSame(command, count);
        }

        protected static void AssertParamCountSame(CommandBuilder actual, int count)
        {
            DbCommand command = actual.ToSelectCommand();
            AssertParamCountSame(command, count);
        }

        protected static void AssertParamCountSame(DbCommand command, int count)
        {
            Assert.AreEqual(count, command.Parameters.Count, "The parameter count is not the expected value.");
        }

        protected static void AssertParamsSame<T>(EntitySet<T> entitySet, params object[] values) where T : DataRecord<T>, new()
        {
            AssertParamsSame(entitySet.CreateCommand(), values);
        }

        protected static void AssertParamsSame(CommandBuilder actual, params object[] values)
        {
            AssertParamsSame(actual.ToSelectCommand(), values);
        }

        protected static void AssertParamsSame(DbCommand command, params object[] values)
        {
            AssertParamCountSame(command, values.Length);
            for (int i = 0; i < values.Length; i++)
                Assert.AreEqual(values[i], command.Parameters[i].Value, string.Format("The parameter at index {0} is not the expected value.", i));
        }
    }
}