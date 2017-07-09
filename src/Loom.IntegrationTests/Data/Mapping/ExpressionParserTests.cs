#region Using Directives

using AdventureWorks;
using AdventureWorks.Person;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping
{
    [TestFixture]
    public class ExpressionParserTests
    {
        [Test]
        public void Test()
        {
            using (AdventureWorksDataSession session = new AdventureWorksDataSession())
            {
                var results = session.FetchAll((Person c) => new {UserFirstName = c.FirstName, UserLastName = c.LastName});
                Assert.IsNotNull(results);
            }
        }
    }
}