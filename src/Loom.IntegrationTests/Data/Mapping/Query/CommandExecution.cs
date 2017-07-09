#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;
using AdventureWorks;
using AdventureWorks.Person;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class CommandExecutionWithPredicates : ThreadedTestBase
    {
        [Test]
        public void ToArray()
        {
            using (AdventureWorksDataSession session = new AdventureWorksDataSession("adventureWorks"))
            {
                ThreadedRepeat(1, session, (index, asserter, state) =>
                {
                    Person[] contacts = state.Persons.ToArray(Person.Columns.BusinessEntityId > 1000);

                    Assert.IsTrue(contacts.Length > 0);
                    Assert.IsTrue(contacts[0].BusinessEntityId > 1000);
                });
            }
        }

        [Test]
        public void ToCollection()
        {
            using (AdventureWorksDataSession session = new AdventureWorksDataSession("adventureWorks"))
            {
                Collection<Person> contacts = session.Persons.ToCollection(Person.Columns.BusinessEntityId > 1000);

                Assert.IsTrue(contacts.Count > 0);
                Assert.IsTrue(contacts[0].BusinessEntityId > 1000);
            }
        }

        [Test]
        public void ToList()
        {
            using (AdventureWorksDataSession session = new AdventureWorksDataSession("adventureWorks"))
            {
                List<Person> contacts = session.Persons.ToList(Person.Columns.BusinessEntityId > 1000);

                Assert.IsTrue(contacts.Count > 0);
                Assert.IsTrue(contacts[0].BusinessEntityId > 1000);
            }
        }
    }
}