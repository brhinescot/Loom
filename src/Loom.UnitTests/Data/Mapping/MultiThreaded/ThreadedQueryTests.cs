#region Using Directives

using System;
using System.Data.Common;
using AdventureWorks;
using AdventureWorks.HumanResources;
using AdventureWorks.Person;
using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.MultiThreaded
{
    [TestFixture]
    public class ThreadedQueryTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void AutoJoinReverseTableOrderThreaded()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Person].[Person] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[BusinessEntityID] " +
                                    "AND _t0.[HireDate] = @_p0 " +
                                    "AND _t1.[FirstName] = @_p1";

            const string expected2 = "SELECT * FROM [Person].[Person] _t0, [HumanResources].[Employee] _t1 " +
                                     "WHERE _t0.[BusinessEntityID] = _t1.[BusinessEntityID] " +
                                     "AND _t1.[HireDate] = @_p0 " +
                                     "AND _t0.[FirstName] = @_p1";

            const string expected3 = "SELECT * FROM [HumanResources].[Employee] _t0 " +
                                     "WHERE _t0.[HireDate] = @_p0";

            ThreadedRepeat(500, (index, threadAsserter) =>
            {
                AdventureWorksDataSession session = new AdventureWorksDataSession("adventureWorks");

                EntitySet<Employee> query = session.Employees;
                query.Where(Employee.Columns.HireDate == DateTime.Now);
                query.Where(Person.Columns.FirstName == "John");
                DbCommand command = query.CreateCommand();

                EntitySet<Person> query2 = session.Persons;
                query2.Where(Employee.Columns.HireDate == DateTime.Now);
                query2.Where(Person.Columns.FirstName == "John");
                DbCommand command2 = query2.CreateCommand();

                EntitySet<Employee> query3 = session.Employees;
                query3.Where(Employee.Columns.HireDate == DateTime.Now);
                DbCommand command3 = query3.CreateCommand();

                threadAsserter.RegisterDependency(session);

                threadAsserter.Assert(() => AssertCommandTextSame(expected, command));
                threadAsserter.Assert(() => AssertParamCountSame(command, 2));

                threadAsserter.Assert(() => AssertCommandTextSame(expected2, command2));
                threadAsserter.Assert(() => AssertParamCountSame(command2, 2));

                threadAsserter.Assert(() => AssertCommandTextSame(expected3, command3));
                threadAsserter.Assert(() => AssertParamCountSame(command3, 1));
            });
        }

        [Test]
        public void SelectRandomThreaded()
        {
            const string expected = "SELECT TOP 3 * FROM [Sales].[Customer] " +
                                    "ORDER BY NEWID()";

            ThreadedRepeat(500, (index, threadAsserter) =>
            {
                EntitySet<Customer> entitySet = Session.Customers;
                entitySet.Constrain(Constraint.Random(3));

                threadAsserter.Assert(() => AssertCommandTextSame(expected, entitySet));
                threadAsserter.Assert(() => AssertParamCountSame(entitySet, 0));
            });
        }
    }
}