#region Using Directives

using System;
using AdventureWorks.HumanResources;
using AdventureWorks.Person;
using AdventureWorks.Sales;
using NUnit.Framework;
using OmniMount;
using OmniMount.Production;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    [Ignore("Manual joining has issues")]
    public class ManualJoinTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void ManualJoinReverseTableOrder()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[CustomerID] " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            const string expected2 = "SELECT * FROM [Sales].[Customer] _t0, [HumanResources].[Employee] _t1 " +
                                     "WHERE _t0.[CustomerID] = _t1.[BusinessEntityID] " +
                                     "AND (_t1.[HireDate] = @_p0 " +
                                     "AND _t0.[CustomerID] = @_p1)";

            DateTime now = DateTime.Now;

            EntitySet<Employee> query = Session.Employees;
            query.Join<Customer>(Employee.Columns.BusinessEntityId, Customer.Columns.CustomerId);
            query.Where((Employee.Columns.HireDate == now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamsSame(query, now, 1);

            EntitySet<Customer> query2 = Session.Customers;
            query2.Join<Employee>(Customer.Columns.CustomerId, Employee.Columns.BusinessEntityId);
            query2.Where((Employee.Columns.HireDate == now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected2, query2);
            AssertParamsSame(query2, now, 1);
        }

        [Test]
        public void ManualPredicateJoinReverseTableOrder()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[CustomerID] " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            const string expected2 = "SELECT * FROM [Sales].[Customer] _t0, [HumanResources].[Employee] _t1 " +
                                     "WHERE _t0.[CustomerID] = _t1.[BusinessEntityID] " +
                                     "AND (_t1.[HireDate] = @_p0 " +
                                     "AND _t0.[CustomerID] = @_p1)";

            DateTime now = DateTime.Now;

            EntitySet<Employee> query = Session.Employees;
            query.Where(Employee.Columns.BusinessEntityId == Customer.Columns.CustomerId);
            query.Where((Employee.Columns.HireDate == now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamsSame(query, now, 1);

            EntitySet<Customer> query2 = Session.Customers;
            query2.Where(Customer.Columns.CustomerId == Employee.Columns.BusinessEntityId);
            query2.Where((Employee.Columns.HireDate == now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected2, query2);
            AssertParamsSame(query2, now, 1);
        }

        [Test]
        public void ManualJoinMultipleWhereColumnPredicates()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[CustomerID] " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;
            query.Join<Customer>(Employee.Columns.BusinessEntityId, Customer.Columns.CustomerId);
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test]
        public void InlinePredicateJoin()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[CustomerID] " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;
            query.Where(Employee.Columns.BusinessEntityId == Customer.Columns.CustomerId);
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test]
        public void InlinePredicateMethodJoin()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] <> _t1.[CustomerID] " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;
            query.Where(Employee.Columns.BusinessEntityId.IsNotEqualTo(Customer.Columns.CustomerId));
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test]
        public void InlineTwoPredicateJoins()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE (_t0.[BusinessEntityID] = _t1.[CustomerID] OR _t0.[BusinessEntityID] = _t1.[CustomerID]) " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;
            query.Where((Employee.Columns.BusinessEntityId == Customer.Columns.CustomerId) | (Employee.Columns.BusinessEntityId == Customer.Columns.CustomerId));
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test]
        public void ManualPredicateJoin()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE (_t0.[BusinessEntityID] > _t1.[CustomerID] " +
                                    "OR _t0.[BusinessEntityID] < _t1.[CustomerID]) " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;

            JoinPredicate joinPredicate1 = new JoinPredicate(Employee.Columns.BusinessEntityId, Comparison.Greater, Customer.Columns.CustomerId);
            JoinPredicate joinPredicate2 = Employee.Columns.BusinessEntityId < Customer.Columns.CustomerId;

            query.Join<Customer>(joinPredicate1 | joinPredicate2);
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test]
        public void ManualNestedPredicateJoin()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE (_t0.[BusinessEntityID] > _t1.[CustomerID] " +
                                    "OR _t0.[BusinessEntityID] < _t1.[CustomerID] " +
                                    "AND _t0.[BusinessEntityID] = _t1.[CustomerID]) " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;

            JoinPredicate joinPredicate1 = new JoinPredicate(Employee.Columns.BusinessEntityId, Comparison.Greater, Customer.Columns.CustomerId);
            JoinPredicate joinPredicate2 = new JoinPredicate(Employee.Columns.BusinessEntityId, Comparison.Less, Customer.Columns.CustomerId);
            JoinPredicate joinPredicate3 = Employee.Columns.BusinessEntityId == Customer.Columns.CustomerId;

            query.Join<Customer>(joinPredicate1 | (joinPredicate2 & joinPredicate3));
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test]
        public void ManualJoinInQuery()
        {
            const string expected = "SELECT * FROM [HumanResources].[Employee] _t0, [Sales].[Customer] _t1 " +
                                    "WHERE _t0.[BusinessEntityID] = _t1.[CustomerID] " +
                                    "AND (_t0.[HireDate] = @_p0 " +
                                    "AND _t1.[CustomerID] = @_p1)";

            EntitySet<Employee> query = Session.Employees;
            query.Join<Customer>(Employee.Columns.BusinessEntityId, Customer.Columns.CustomerId);
            query.Where((Employee.Columns.HireDate == DateTime.Now) & (Customer.Columns.CustomerId == 1));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 2);
        }

        [Test] //], Ignore("Self referential table joins are not yet supported.")]
        public void ManualJoinSameTable()
        {
            const string expected = "SELECT * FROM [Production].[Category] _t0, [Production].[Category] _t1 WHERE _t0.[ParentId] = _t1.[CategoryId] AND _t1.[ApplicationId] = 1 ";

            using (ProductDataSession session = new ProductDataSession())
            {
                EntitySet<Category> entitySet = session.Categorys;
                entitySet.Join<Category>(Category.Columns.ParentId == Category.Columns.CategoryId);
                entitySet.Where(Category.Columns.ApplicationId == 1);

                AssertCommandTextSame(expected, entitySet);
                AssertParamCountSame(entitySet, 2);
            }
        }

        [Test]
        public void CrossReferenceJoin()
        {
            const string expected = "SELECT _t0.[City] " +
                                    "FROM [HumanResources].[Employee] _t1, [Person].[Address] _t0, [Person].[BusinessEntityAddress] _t2 " +
                                    "WHERE _t2.[BusinessEntityID] = _t1.[BusinessEntityID] " +
                                    "AND _t2.[AddressID] = _t0.[AddressID]";

            EntitySet<Employee> query = Session.Employees;
            query.Join<BusinessEntityAddress>(BusinessEntityAddress.Columns.BusinessEntityId == Employee.Columns.BusinessEntityId);
            query.Join<Address>(BusinessEntityAddress.Columns.AddressId == Address.Columns.AddressId);
            query.Select(Address.Columns.City);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }
    }
}