#region Using Directives

using System;
using AdventureWorks.HumanResources;
using AdventureWorks.Person;
using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class AutoJoinTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void AutoJoinMultipleWheres()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Person].[Person] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[BusinessEntityID] " +
                                    "AND _t0.[HireDate] = @_p0 " +
                                    "AND _t1.[FirstName] = @_p1";
            const string name = "John";
            DateTime hireDate = DateTime.Now;

            EntitySet<Employee> query = Session.Employees;
            query.Where(Employee.Columns.HireDate == hireDate);
            query.Where(Person.Columns.FirstName == name);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
            AssertParamsSame(query, hireDate, name);
        }

        [Test]
        public void AutoJoinNull()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Person].[Person] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[BusinessEntityID] " +
                                    "AND (_t1.[FirstName] = @_p0 " +
                                    "OR _t1.[FirstName] IS NULL)";

            EntitySet<Employee> query = Session.Employees;
            query.Where((Person.Columns.FirstName == "John") | (Person.Columns.FirstName == null));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 1);
        }

        [Test]
        public void AutoJoinMultipleWhereColumnPredicates()
        {
            const string expected = "SELECT _t0.[FirstName], _t1.[BusinessEntityID] " +
                                    "FROM [HumanResources].[Employee] _t1, [Person].[Person] _t0 " +
                                    "WHERE (_t1.[BusinessEntityID] = _t0.[BusinessEntityID] " +
                                    "OR _t0.[BusinessEntityID] IS NULL)";

            EntitySet<Employee> query = Session.Employees.Select(Person.Columns.FirstName, Employee.Columns.BusinessEntityId)
                .Where((Employee.Columns.BusinessEntityId == Person.Columns.BusinessEntityId) | (Person.Columns.BusinessEntityId == null)).End();

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TwoDuplicatePredicateJoinFail()
        {
            EntitySet<Employee> query = Session.Employees;

            JoinPredicate joinPredicate1 = new JoinPredicate(Employee.Columns.BusinessEntityId, Comparison.Greater, Customer.Columns.CustomerId);

            // Expected: Cannot add joinPredicate1 twice.
            query.Join<Customer>(joinPredicate1 | joinPredicate1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ThreeDuplicatePredicateJoinFail()
        {
            EntitySet<Employee> query = Session.Employees;

            JoinPredicate joinPredicate1 = new JoinPredicate(Employee.Columns.BusinessEntityId, Comparison.Greater, Customer.Columns.CustomerId);
            JoinPredicate joinPredicate2 = new JoinPredicate(Employee.Columns.BusinessEntityId, Comparison.Less, Customer.Columns.CustomerId);

            // Expected: Cannot add joinPredicate2 twice.
            query.Join<Customer>((joinPredicate1 | joinPredicate2) & joinPredicate2);
        }

        [Test]
        public void AutoJoinMultipleSelectColumns()
        {
            const string expected = "SELECT _t0.[HireDate], _t1.[FirstName] " +
                                    "FROM [HumanResources].[Employee] _t0, [Person].[Person] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[BusinessEntityID]";

            EntitySet<Employee> query = Session.Employees.Join<Person>();
            query.Select(Employee.Columns.HireDate, Person.Columns.FirstName);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }
    }
}