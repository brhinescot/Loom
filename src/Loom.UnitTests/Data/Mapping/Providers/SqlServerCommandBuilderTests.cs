#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Providers
{
    /// <summary>
    ///     These test do not represent everyday expected usage. Normaly a <see cref="CommandBase{T}" /> would be used
    ///     to generate the lists and predicates the <see cref="CommandBuilder" /> uses when writing sql text.
    /// </summary>
    [TestFixture]
    public class SqlServerCommandBuilderTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void Basic()
        {
            const string expected =
                "SELECT _t0.[CustomerID], _t0.[AccountNumber], _t1.[Name] " +
                "FROM [Sales].[Customer] _t0, [Sales].[SalesTerritory] _t1 " +
                "WHERE _t0.[TerritoryID] = _t1.[TerritoryID] " +
                "AND _t0.[CustomerID] = @_p0 " +
                "AND (_t0.[CustomerID] = @_p1 OR _t0.[TerritoryID] = @_p2) " +
                "OR (_t0.[CustomerID] > @_p3 OR _t1.[Name] LIKE @_p4)";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add Select Columns
            builder.AppendSelect(Customer.Columns.CustomerId, Customer.Columns.AccountNumber, SalesTerritory.Columns.Name);

            // Add Join Columns
            builder.AppendWhere(Customer.Columns.TerritoryId == SalesTerritory.Columns.TerritoryId);

            // Add Where Columns
            builder.AppendWhere(Customer.Columns.CustomerId == 1);
            builder.AppendWhere((Customer.Columns.CustomerId == 2) | (Customer.Columns.TerritoryId == 5));
            ColumnPredicate predicate = (Customer.Columns.CustomerId > 2) | SalesTerritory.Columns.Name.Contains("X");
            predicate.OrToPreviousGroup = true;
            builder.AppendWhere(predicate);

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 5);
        }

        [Test]
        public void Basic2()
        {
            const string expected =
                "SELECT * " +
                "FROM [Sales].[Customer] _t0, [Sales].[SalesTerritory] _t1 " +
                "WHERE _t0.[TerritoryID] = _t1.[TerritoryID] " +
                "AND _t0.[CustomerID] = @_p0 " +
                "AND (_t0.[CustomerID] = @_p1 OR _t0.[TerritoryID] = @_p2) " +
                "OR (_t0.[CustomerID] > @_p3 OR _t1.[Name] LIKE @_p4)";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add tables
            builder.AppendFrom(Customer.Table);
            builder.AppendFrom(SalesTerritory.Table);

            // Add Join Columns
            builder.AppendWhere((Customer.Columns.TerritoryId == SalesTerritory.Columns.TerritoryId).AsJoin());

            // Add Where Columns
            builder.AppendWhere(Customer.Columns.CustomerId == 1);
            builder.AppendWhere((Customer.Columns.CustomerId == 2) | (Customer.Columns.TerritoryId == 5));
            ColumnPredicate predicate = (Customer.Columns.CustomerId > 2) | SalesTerritory.Columns.Name.Contains("X");
            predicate.OrToPreviousGroup = true;
            builder.AppendWhere(predicate);

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 5);
        }

        [Test]
        public void GroupByTest()
        {
            const string expected =
                "SELECT * " +
                "FROM [Sales].[Customer] _t0, [Sales].[SalesTerritory] _t1 " +
                "WHERE _t0.[TerritoryID] = _t1.[TerritoryID] " +
                "AND _t0.[CustomerID] = @_p0 " +
                "AND (_t0.[CustomerID] = @_p1 OR _t0.[TerritoryID] = @_p2) " +
                "OR (_t0.[CustomerID] > @_p3 OR _t1.[Name] LIKE @_p4) " +
                "GROUP BY _t1.[TerritoryID]";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add tables
            builder.AppendFrom(Customer.Table);
            builder.AppendFrom(SalesTerritory.Table);

            // Add Join Columns
            builder.AppendWhere((Customer.Columns.TerritoryId == SalesTerritory.Columns.TerritoryId).AsJoin());

            // Add Where Columns
            builder.AppendWhere(Customer.Columns.CustomerId == 1);
            builder.AppendWhere((Customer.Columns.CustomerId == 2) | (Customer.Columns.TerritoryId == 5));
            ColumnPredicate predicate = (Customer.Columns.CustomerId > 2) | SalesTerritory.Columns.Name.Contains("X");
            predicate.OrToPreviousGroup = true;
            builder.AppendWhere(predicate);

            builder.AppendGroupBy(SalesTerritory.Columns.TerritoryId);

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 5);
        }

        [Test]
        public void OrderByTest()
        {
            const string expected =
                "SELECT * " +
                "FROM [Sales].[Customer] _t0, [Sales].[SalesTerritory] _t1 " +
                "WHERE _t0.[TerritoryID] = _t1.[TerritoryID] " +
                "AND _t0.[CustomerID] = @_p0 " +
                "AND (_t0.[CustomerID] = @_p1 OR _t0.[TerritoryID] = @_p2) " +
                "OR (_t0.[CustomerID] > @_p3 OR _t1.[Name] LIKE @_p4) " +
                "ORDER BY _t1.[TerritoryID] DESC";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add tables
            builder.AppendFrom(Customer.Table);
            builder.AppendFrom(SalesTerritory.Table);

            // Add Join Columns
            builder.AppendWhere((Customer.Columns.TerritoryId == SalesTerritory.Columns.TerritoryId).AsJoin());

            // Add Where Columns
            builder.AppendWhere(Customer.Columns.CustomerId == 1);
            builder.AppendWhere((Customer.Columns.CustomerId == 2) | (Customer.Columns.TerritoryId == 5));
            ColumnPredicate predicate = (Customer.Columns.CustomerId > 2) | SalesTerritory.Columns.Name.Contains("X");
            predicate.OrToPreviousGroup = true;
            builder.AppendWhere(predicate);

            builder.AppendOrderBy(SalesTerritory.Columns.TerritoryId, OrderByDirection.Desc);

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 5);
        }

        [Test]
        public void OrderByAndGroupByTest()
        {
            const string expected =
                "SELECT * " +
                "FROM [Sales].[Customer] _t0, [Sales].[SalesTerritory] _t1 " +
                "WHERE _t0.[TerritoryID] = _t1.[TerritoryID] " +
                "AND _t0.[CustomerID] = @_p0 " +
                "AND (_t0.[CustomerID] = @_p1 OR _t0.[TerritoryID] = @_p2) " +
                "OR (_t0.[CustomerID] > @_p3 OR _t1.[Name] LIKE @_p4) " +
                "GROUP BY _t1.[TerritoryID] " +
                "ORDER BY _t1.[TerritoryID] DESC";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add tables
            builder.AppendFrom(Customer.Table);
            builder.AppendFrom(SalesTerritory.Table);

            // Add Join Columns
            builder.AppendWhere((Customer.Columns.TerritoryId == SalesTerritory.Columns.TerritoryId).AsJoin());

            // Add Where Columns
            builder.AppendWhere(Customer.Columns.CustomerId == 1);
            builder.AppendWhere((Customer.Columns.CustomerId == 2) | (Customer.Columns.TerritoryId == 5));
            ColumnPredicate predicate = (Customer.Columns.CustomerId > 2) | SalesTerritory.Columns.Name.Contains("X");
            predicate.OrToPreviousGroup = true;
            builder.AppendWhere(predicate);

            builder.AppendGroupBy(SalesTerritory.Columns.TerritoryId);
            builder.AppendOrderBy(SalesTerritory.Columns.TerritoryId, OrderByDirection.Desc);

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 5);
        }

        [Test]
        public void SingleWhereColumn()
        {
            const string expected =
                "SELECT * " +
                "FROM [Sales].[Customer] _t0 " +
                "WHERE _t0.[CustomerID] = @_p0";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add tables
            builder.AppendFrom(Customer.Table);

            // Add Where Columns
            builder.AppendWhere(Customer.Columns.CustomerId == 1);

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 1);
        }

        [Test]
        public void Paging()
        {
            const string expected =
                "SELECT * FROM (" +
                "SELECT TOP (@_pi + @_ps) ROW_NUMBER() " +
                "OVER ( ORDER BY _t0.[CustomerID] ASC) AS _prr, * " +
                "FROM [Sales].[Customer] _t0" +
                ") AS _prt " +
                "WHERE (_prr BETWEEN @_pi + 1 AND (@_pi  + @_ps))";

            SqlServerCommandBuilder builder = new SqlServerCommandBuilder(Session);
            // Add tables
            builder.AppendFrom(Customer.Table);
            builder.Constrain(Constraint.Page(0, 25));

            AssertCommandTextSame(expected, builder);
            AssertParamCountSame(builder, 2);
        }
    }
}