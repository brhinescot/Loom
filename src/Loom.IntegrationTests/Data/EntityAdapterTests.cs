#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Loom.Data.Entities;
using NUnit.Framework;

#endregion

namespace Loom.Data
{
    [TestFixture]
    public class EntityAdapterTests
    {
        [Test]
        [TestCase(MissingPropertyMappingAction.Ignore)]
        [TestCase(MissingPropertyMappingAction.Error, ExpectedException = typeof(InvalidOperationException))]
        public void CreateCollection(MissingPropertyMappingAction action)
        {
            const string expected = "SELECT * FROM Production.ProductVariant WHERE ProductVariantId > @_p0";
            EntityAdapter adapter;
            using (SqlCommand command = GetCommand("SELECT * FROM Production.ProductVariant WHERE ProductVariantId > {0}", 100))
            {
                Assert.AreEqual(expected, command.CommandText);
                Assert.AreEqual(1, command.Parameters.Count);
                Assert.AreEqual(100, command.Parameters[0].Value);

                adapter = GetAdapter(command, action);
            }

            Collection<Product> products = adapter.CreateCollection<Product>();

            Assert.IsTrue(products.Count > 1);
        }

        [Test]
        [TestCase(MissingPropertyMappingAction.Ignore)]
        [TestCase(MissingPropertyMappingAction.Error, ExpectedException = typeof(InvalidOperationException))]
        public void FillCollection(MissingPropertyMappingAction action)
        {
            const string expected = "SELECT * FROM Production.ProductVariant WHERE ProductVariantId > @_p0";
            IEntityAdapter adapter;
            using (SqlCommand command = GetCommand("SELECT * FROM Production.ProductVariant WHERE ProductVariantId > {0}", 100))
            {
                Assert.AreEqual(expected, command.CommandText);
                Assert.AreEqual(1, command.Parameters.Count);
                Assert.AreEqual(100, command.Parameters[0].Value);

                adapter = GetAdapter(command, action);
            }
            List<Product> products = new List<Product>();
            adapter.FillCollection(products);

            Assert.IsTrue(products.Count > 1);
        }

        private static EntityAdapter GetAdapter(IDbCommand command, MissingPropertyMappingAction action = MissingPropertyMappingAction.Ignore)
        {
            EntityAdapter adapter = new EntityAdapter(command);
            adapter.MissingPropertyMappingAction = action;
            return adapter;
        }

        private static SqlCommand GetCommand(string commandText, params object[] parameterValues)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Omni"].ConnectionString)
            };

            command.AddParameterizedCommandText(commandText, parameterValues);
            return command;
        }
    }

    public class Product
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}