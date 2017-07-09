#region Using Directives

using AdventureWorks;
using AdventureWorks.Person;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class ExpressionConversionTests
    {
        [Test]
        public void Test()
        {
            using (AdventureWorksDataSession session = new AdventureWorksDataSession("adventureWorks"))
            {
                Person[] contacts = session.Persons.ToArray(Person.Columns.BusinessEntityId.IsBetween(1000, 1020));
                var converted = session.Persons.ToArray(Person.Columns.BusinessEntityId.IsBetween(1000, 1020), c => new {Name = c.FirstName + " " + c.LastName});

                Assert.IsTrue(converted.Length > 0);
                Assert.IsTrue(converted.Length <= 20);
                Assert.IsNotNull(converted[0].Name);
                Assert.AreEqual(contacts[0].FirstName + " " + contacts[0].LastName, converted[0].Name);
            }
        }
    }
}