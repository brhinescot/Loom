#region Using Directives

using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class WhereTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void WhereOneColumnEqual()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] = @_p0";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber == "1234");

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234");
        }

        [Test]
        public void WhereOneColumnGreater()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] > @_p0";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber > "1234");

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234");
        }

        [Test]
        public void WhereTwoColumnsEqual()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] = @_p0 "
                                    + "AND _t0.[CustomerID] = @_p1";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber == "1234");
            entitySet.Where(Customer.Columns.CustomerId == "9999");

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsEqualWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] = @_p0 " +
                                    "AND _t0.[CustomerID] = @_p1)";

            EntitySet<Customer> entitySet = Session.Customers
                .Where((Customer.Columns.AccountNumber == "1234") & (Customer.Columns.CustomerId == "9999")).End();

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsEqualWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] = @_p0 " +
                                    "OR _t0.[CustomerID] = @_p1)";

            EntitySet<Customer> entitySet = Session.Customers
                .Where((Customer.Columns.AccountNumber == "1234") | (Customer.Columns.CustomerId == "9999")).End();

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsNotEqualWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] <> @_p0 " +
                                    "AND _t0.[CustomerID] <> @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber != "1234") & (Customer.Columns.CustomerId != "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsNotEqualWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] <> @_p0 " +
                                    "OR _t0.[CustomerID] <> @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber != "1234") | (Customer.Columns.CustomerId != "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsGreaterWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] > @_p0 " +
                                    "AND _t0.[CustomerID] > @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber > "1234") & (Customer.Columns.CustomerId > "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsGreaterWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] > @_p0 " +
                                    "OR _t0.[CustomerID] > @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber > "1234") | (Customer.Columns.CustomerId > "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsGreaterOrEqualWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] >= @_p0 " +
                                    "AND _t0.[CustomerID] >= @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber >= "1234") & (Customer.Columns.CustomerId >= "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsGreaterOrEqualWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] >= @_p0 " +
                                    "OR _t0.[CustomerID] >= @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber >= "1234") | (Customer.Columns.CustomerId >= "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsLessWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] < @_p0 " +
                                    "AND _t0.[CustomerID] < @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber < "1234") & (Customer.Columns.CustomerId < "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsLessWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] < @_p0 " +
                                    "OR _t0.[CustomerID] < @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber < "1234") | (Customer.Columns.CustomerId < "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsLessOrEqualWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] <= @_p0 " +
                                    "AND _t0.[CustomerID] <= @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber <= "1234") & (Customer.Columns.CustomerId <= "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsLessOrEqualWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] <= @_p0 " +
                                    "OR _t0.[CustomerID] <= @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where((Customer.Columns.AccountNumber <= "1234") | (Customer.Columns.CustomerId <= "9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234", "9999");
        }

        [Test]
        public void WhereTwoColumnsDoesNotContainWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] NOT LIKE @_p0 " +
                                    "AND _t0.[CustomerID] NOT LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.DoesNotContain("1234") & Customer.Columns.CustomerId.DoesNotContain("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234%", "%9999%");
        }

        [Test]
        public void WhereTwoColumnsDoesNotContainWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] NOT LIKE @_p0 " +
                                    "OR _t0.[CustomerID] NOT LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.DoesNotContain("1234") | Customer.Columns.CustomerId.DoesNotContain("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234%", "%9999%");
        }

        [Test]
        public void WhereTwoColumnsStartsWithWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] LIKE @_p0 " +
                                    "AND _t0.[CustomerID] LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.StartsWith("1234") & Customer.Columns.CustomerId.StartsWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234%", "9999%");
        }

        [Test]
        public void WhereTwoColumnsStartsWithWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] LIKE @_p0 " +
                                    "OR _t0.[CustomerID] LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.StartsWith("1234") | Customer.Columns.CustomerId.StartsWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234%", "9999%");
        }

        [Test]
        public void WhereTwoColumnsDoesNotStartWithWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] NOT LIKE @_p0 " +
                                    "AND _t0.[CustomerID] NOT LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.DoesNotStartWith("1234") & Customer.Columns.CustomerId.DoesNotStartWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234%", "9999%");
        }

        [Test]
        public void WhereTwoColumnsDoesNotStartWithWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] NOT LIKE @_p0 " +
                                    "OR _t0.[CustomerID] NOT LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.DoesNotStartWith("1234") | Customer.Columns.CustomerId.DoesNotStartWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234%", "9999%");
        }

        [Test]
        public void WhereTwoColumnsEndsWithWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] LIKE @_p0 " +
                                    "AND _t0.[CustomerID] LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.EndsWith("1234") & Customer.Columns.CustomerId.EndsWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234", "%9999");
        }

        [Test]
        public void WhereTwoColumnsEndsWithWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] LIKE @_p0 " +
                                    "OR _t0.[CustomerID] LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.EndsWith("1234") | Customer.Columns.CustomerId.EndsWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234", "%9999");
        }

        [Test]
        public void WhereTwoColumnsDoesNotEndWithWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] NOT LIKE @_p0 " +
                                    "AND _t0.[CustomerID] NOT LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.DoesNotEndWith("1234") & Customer.Columns.CustomerId.DoesNotEndWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234", "%9999");
        }

        [Test]
        public void WhereTwoColumnsDoesNotEndWithWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] NOT LIKE @_p0 " +
                                    "OR _t0.[CustomerID] NOT LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.DoesNotEndWith("1234") | Customer.Columns.CustomerId.DoesNotEndWith("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234", "%9999");
        }

        [Test]
        public void WhereTwoColumnsContainsWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] LIKE @_p0 " +
                                    "AND _t0.[CustomerID] LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.Contains("1234") & Customer.Columns.CustomerId.Contains("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234%", "%9999%");
        }

        [Test]
        public void WhereTwoColumnsContainsWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] LIKE @_p0 " +
                                    "OR _t0.[CustomerID] LIKE @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.Contains("1234") | Customer.Columns.CustomerId.Contains("9999"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "%1234%", "%9999%");
        }

        [Test]
        public void WhereTwoColumnsBetweenWithAnd()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] BETWEEN @_p0 AND @_p1 " +
                                    "AND _t0.[CustomerID] BETWEEN @_p2 AND @_p3)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.IsBetween("1234a", "1234b") & Customer.Columns.CustomerId.IsBetween("9999a", "9999b"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234a", "1234b", "9999a", "9999b");
        }

        [Test]
        public void WhereTwoColumnsBetweenWithOr()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] BETWEEN @_p0 AND @_p1 " +
                                    "OR _t0.[CustomerID] BETWEEN @_p2 AND @_p3)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.IsBetween("1234a", "1234b") | Customer.Columns.CustomerId.IsBetween("9999a", "9999b"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234a", "1234b", "9999a", "9999b");
        }

        [Test]
        public void WhereColumnIsEqualToAny()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE (_t0.[AccountNumber] = @_p0 OR _t0.[AccountNumber] = @_p1)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.AccountNumber.IsEqualToAny("1234a", "1234b"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234a", "1234b");
        }

        [Test]
        public void WhereColumnIsEqualToAnyColumnEquals()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "WHERE _t0.[PersonID] = @_p0 AND (_t0.[AccountNumber] = @_p1 OR _t0.[AccountNumber] = @_p2)";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Where(Customer.Columns.PersonId == "Pro")
                .And(Customer.Columns.AccountNumber.IsEqualToAny("1234a", "1234b"));

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "Pro", "1234a", "1234b");
        }

        [Test]
        public void WhereTwoColumnsAndTwoColumns()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE (_t0.[AccountNumber] = @_p0 OR _t0.[CustomerID] = @_p1) "
                                    + "AND (_t0.[AccountNumber] = @_p2 OR _t0.[CustomerID] = @_p3)";

            EntitySet<Customer> query = Session.Customers;
            query.Where((Customer.Columns.AccountNumber == "1234") | (Customer.Columns.CustomerId == "9999"))
                .And((Customer.Columns.AccountNumber == "1234") | (Customer.Columns.CustomerId == "9999"));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 4);
        }

        [Test]
        public void WhereTwoColumnsOrTwoColumns()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE (_t0.[AccountNumber] <> @_p0 AND _t0.[CustomerID] <> @_p1) "
                                    + "OR (_t0.[AccountNumber] = @_p2 AND _t0.[CustomerID] = @_p3)";

            EntitySet<Customer> query = Session.Customers;
            query.Where((Customer.Columns.AccountNumber != "1234") & (Customer.Columns.CustomerId != "9999"))
                .Or((Customer.Columns.AccountNumber == "1234") & (Customer.Columns.CustomerId == "9999"));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 4);
        }
    }
}