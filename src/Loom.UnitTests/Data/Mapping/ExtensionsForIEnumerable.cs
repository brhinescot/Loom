#region Using Directives

using System.Collections.ObjectModel;
using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping
{
    [TestFixture]
    public class ExtensionsForIEnumerable
    {
        private static Collection<Customer> GetCustomers()
        {
            Collection<Customer> customers = new Collection<Customer>();
            customers.Add(new Customer {CustomerId = 100, AccountNumber = "UF767SD65456ASU"});
            customers.Add(new Customer {CustomerId = 200, AccountNumber = "RA767FD99999ASU"});
            customers.Add(new Customer {CustomerId = 300, AccountNumber = "RA767FF65456TSU"});
            customers.Add(new Customer {CustomerId = 400, AccountNumber = "UF767FF65456TSU"});
            return customers;
        }

        [Test]
        public void FindWithEqual()
        {
            Collection<Customer> customers = GetCustomers();

            Customer customer = customers.Find(Customer.Columns.CustomerId == 300);
            Assert.IsNotNull(customer);
            Assert.AreEqual(300, customer.CustomerId);

            customer = customers.Find(Customer.Columns.CustomerId == 100);
            Assert.IsNotNull(customer);
            Assert.AreEqual(100, customer.CustomerId);

            customer = customers.Find(Customer.Columns.CustomerId == 999);
            Assert.IsNull(customer);
        }

        [Test]
        public void FindWithNotEqual()
        {
            Collection<Customer> customers = GetCustomers();

            Customer customer = customers.Find(Customer.Columns.CustomerId != 300);
            Assert.IsNotNull(customer);
            Assert.AreEqual(100, customer.CustomerId);

            customer = customers.Find(Customer.Columns.CustomerId != 100);
            Assert.IsNotNull(customer);
            Assert.AreEqual(200, customer.CustomerId);

            customer = customers.Find(Customer.Columns.CustomerId != 999);
            Assert.IsNotNull(customer);
            Assert.AreEqual(100, customer.CustomerId);
        }

        [Test]
        public void FindWithContains()
        {
            Collection<Customer> customers = GetCustomers();

            Customer customer = customers.Find(Customer.Columns.AccountNumber.Contains("FF"));
            Assert.IsNotNull(customer);
            Assert.AreEqual(300, customer.CustomerId);

            customer = customers.Find(Customer.Columns.AccountNumber.Contains(99999));
            Assert.IsNotNull(customer);
            Assert.AreEqual(200, customer.CustomerId);
        }
    }
}