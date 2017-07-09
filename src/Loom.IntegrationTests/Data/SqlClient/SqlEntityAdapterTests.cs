#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using Loom.Data.Entities;
using NUnit.Framework;

#endregion

namespace Loom.Data.SqlClient
{
    [TestFixture]
    public class SqlEntityAdapterAdapterTests
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["adventureWorks"].ConnectionString;

        [Test]
        public void BasicFill()
        {
            List<TestCustomer> customers = new List<TestCustomer>();

            const string commandText = "SELECT TOP 10 CustomerID as CustomerId, AccountNumber, TerritoryID as TerritoryId FROM Sales.Customer";
            using (IEntityAdapter adapter = new SqlEntityAdapter(connectionString, commandText))
            {
                adapter.FillCollection(customers);
            }

            Assert.AreEqual(10, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerId);
            Assert.IsTrue(customers[0].Rowguid == Guid.Empty);
        }

        [Test]
        public void BasicFillTwice()
        {
            List<TestCustomer> customers = new List<TestCustomer>();

            const string commandText = "SELECT TOP 10 CustomerID as CustomerId, AccountNumber, TerritoryID as TerritoryId FROM Sales.Customer";
            using (IEntityAdapter adapter = new SqlEntityAdapter(connectionString, commandText))
            {
                adapter.FillCollection(customers);

                customers = new List<TestCustomer>();
                adapter.FillCollection(customers);
            }
        }

        [TestCase(true)]
        [TestCase(false, ExpectedException = typeof(InvalidOperationException))]
        public void FillTestCaseDifferences(bool ignoreCase)
        {
            List<TestCustomer> customers = new List<TestCustomer>();

            const string commandText = "SELECT TOP 10 CustomerID as CustomerId, AccountNumber, TerritoryID as TerritoryId, rowguid FROM Sales.Customer";
            using (IEntityAdapter adapter = new SqlEntityAdapter(connectionString, commandText))
            {
                adapter.IgnoreCase = ignoreCase;
                adapter.FillCollection(customers);
            }

            Assert.AreEqual(10, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerId);
            Assert.IsTrue(customers[0].Rowguid != Guid.Empty);
        }

        [Test]
        public void FillWithMappings()
        {
            List<TestCustomer> customers = new List<TestCustomer>();

            const string commandText = "SELECT TOP 10 CustomerID AS Customer, AccountNumber, TerritoryID as TerritoryId FROM Sales.Customer";
            using (IEntityAdapter adapter = new SqlEntityAdapter(connectionString, commandText))
            {
                adapter.Options.Mappings.Add("Customer", "CustomerId");
                adapter.FillCollection(customers);
            }

            Assert.AreEqual(10, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerId);
            Assert.IsTrue(customers[0].Rowguid == Guid.Empty);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FillWithoutMappingsErrorOnSurplusColumn()
        {
            List<TestCustomer> customers = new List<TestCustomer>();

            const string commandText = "SELECT TOP 10 CustomerID AS Customer, AccountNumber, TerritoryID as TerritoryId FROM Sales.Customer";
            using (IEntityAdapter adapter = new SqlEntityAdapter(connectionString, commandText))
            {
                adapter.FillCollection(customers);
            }
        }

        [Test]
        public void FillWithoutMappingsIgnoreSurplusColumn()
        {
            List<TestCustomer> customers = new List<TestCustomer>();

            const string commandText = "SELECT TOP 10 CustomerID AS Customer, AccountNumber, TerritoryID as TerritoryId FROM Sales.Customer";
            using (IEntityAdapter adapter = new SqlEntityAdapter(connectionString, commandText))
            {
                adapter.MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore;
                adapter.FillCollection(customers);
            }

            Assert.AreEqual(10, customers.Count);
            Assert.AreEqual(0, customers[0].CustomerId);
            Assert.IsTrue(customers[0].Rowguid == Guid.Empty);
        }
    }
}