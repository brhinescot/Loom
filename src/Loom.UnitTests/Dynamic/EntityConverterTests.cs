#region Using Directives

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class EntityConverterTests
    {
        [Test]
        public void Convert()
        {
            List<Customer> customers = GetCustomers();

            EntityConverter<Customer, Contact> converter = new EntityConverter<Customer, Contact>();
            IList<Contact> contacts = converter.Convert(customers);

            Assert.AreEqual(100, contacts.Count);
            Assert.AreEqual(contacts[0].Name, "TestCustomer1");
            Assert.AreEqual(contacts[0].Address, "TestAddress1");
        }

        [Test]
        public void ConvertWithMapping()
        {
            List<Customer> customers = GetCustomers();

            EntityConverter<Customer, Contact> converter = new EntityConverter<Customer, Contact>();
            converter.AddMapping("Telephone", "Phone");
            IList<Contact> contacts = converter.Convert(customers);

            Assert.AreEqual(100, contacts.Count);
            Assert.AreEqual("TestCustomer1", contacts[0].Name);
            Assert.AreEqual("TestAddress1", contacts[0].Address);
            Assert.AreEqual("555-1212", contacts[0].Phone);
        }

        [Test]
        public void ConvertWithMappingAction()
        {
            List<Customer> customers = GetCustomers();

            EntityConverter<Customer, Contact> converter = new EntityConverter<Customer, Contact>((cu, co) => co.Phone = cu.Telephone);
            IList<Contact> contacts = converter.Convert(customers);

            Assert.AreEqual(100, contacts.Count);
            Assert.AreEqual("TestCustomer1", contacts[0].Name);
            Assert.AreEqual("TestAddress1", contacts[0].Address);
            Assert.AreEqual("555-1212", contacts[0].Phone);
        }

        [Test]
        public void ConvertWithMappingCollection()
        {
            List<Customer> customers = GetCustomers();

            PropertyMappings collection = new PropertyMappings();
            collection.Add("Telephone", "Phone");

            EntityConverter<Customer, Contact> converter = new EntityConverter<Customer, Contact>(collection);
            IList<Contact> contacts = converter.Convert(customers);

            Assert.AreEqual(100, contacts.Count);
            Assert.AreEqual("TestCustomer1", contacts[0].Name);
            Assert.AreEqual("TestAddress1", contacts[0].Address);
            Assert.AreEqual("555-1212", contacts[0].Phone);
        }

        [Test]
        public void MergeWith()
        {
            Customer customer = new Customer {Address = "123 Main Street.", City = "Seattle", State = "WA", Name = "Jack"};
            Contact contact = new Contact {Name = "Jackson", Phone = "555-1212"};

            EntityConverter<Customer, Contact> converter = new EntityConverter<Customer, Contact>();
            converter.Merge(customer, contact);

            Assert.AreEqual(customer.Address, contact.Address);
        }

        [Test]
        public void ConvertWithNullableIntToNotNullableIntProperty()
        {
            CustomerNullableInt customer = new CustomerNullableInt
            {
                Address = "Address1",
                City = "City1",
                Name = "Name1",
                State = "State1",
                Telephone = "Telephone1",
                TerritoryId = 0,
                ZipCode = 12345
            };

            EntityConverter<CustomerNullableInt, ContactNotNullableInt> converter = new EntityConverter<CustomerNullableInt, ContactNotNullableInt>((cu, co) => co.Phone = cu.Telephone);
            ContactNotNullableInt contact = converter.Convert(customer);

            Assert.IsNotNull(contact);
        }

        [Test]
        public void ConvertWithNullableIntToNullableIntProperty()
        {
            CustomerNullableInt customer = new CustomerNullableInt
            {
                Address = "Address1",
                City = "City1",
                Name = "Name1",
                State = "State1",
                Telephone = "Telephone1",
                TerritoryId = 0,
                ZipCode = 12345
            };

            EntityConverter<CustomerNullableInt, ContactNullableInt> converter = new EntityConverter<CustomerNullableInt, ContactNullableInt>((cu, co) => co.Phone = cu.Telephone);
            ContactNullableInt contact = converter.Convert(customer);

            Assert.IsNotNull(contact);
        }

        [Test]
        public void ConvertWithDefaultValueForToEntityProperty()
        {
            CustomerNullableInt customer = new CustomerNullableInt
            {
                Address = "Address1",
                City = "City1",
                Name = "Name1",
                State = null,
                Telephone = "555-1212",
                TerritoryId = 0,
                ZipCode = 12345
            };

            EntityConverter<CustomerNullableInt, ContactNullableInt> converter = new EntityConverter<CustomerNullableInt, ContactNullableInt>((cu, co) => co.Phone = cu.Telephone);
            ContactNullableInt contact = converter.Convert(customer);

            Assert.IsNotNull(contact);
            Assert.AreEqual("WA", contact.State);
        }

//        [Test]
//        public void ConvertWithLamda()
//        {
//            Customer customer = GetCustomers()[0];
//
//            Contact contact = customer.Convert(x => new Contact {Phone = x.Telephone, Address = x.Address, Name = x.Name});
//
//            Assert.AreEqual(contact.Name, "TestCustomer1");
//            Assert.AreEqual(contact.Address, "TestAddress1");
//            Assert.AreEqual(contact.Phone, "555-1212");
//
//            var customerToContact = customer.CreateConverter(x => new Contact { Phone = x.Telephone, Address = x.Address, Name = x.Name });
//            Contact contact2 = customerToContact(customer);
//
//            Assert.AreEqual(contact2.Name, "TestCustomer1");
//            Assert.AreEqual(contact2.Address, "TestAddress1");
//            Assert.AreEqual(contact2.Phone, "555-1212");
//
//            Func<Customer, Contact> act = x => new Contact { Phone = x.Telephone, Address = x.Address, Name = x.Name };
//            act(new Customer());
//        }

//        [Test]
//        public void Convert()
//        {
//            const string source = "HELLO";
//            const string normalized = "hello";
//
//            var translater1 = Funcify((string x) => new { Original = x, Normalized = x.ToLower() });
//            var translater2 = source.CreateConverter(x => new { Original = x, Normalized = x.ToLower() });
//            
//            var item1 = translater1(source);
//            var item2 = translater2(source);
//            var item3 = source.Convert(translater2);
//            var item4 = source.Convert(x => new {Original = x, Normalized = x.ToLower()});
//
//            Assert.AreEqual(source, item1.Original);
//            Assert.AreEqual(source, item2.Original);
//            Assert.AreEqual(source, item3.Original);
//            Assert.AreEqual(source, item4.Original);
//
//            Assert.AreEqual(normalized, item1.Normalized);
//            Assert.AreEqual(normalized, item2.Normalized);
//            Assert.AreEqual(normalized, item3.Normalized);
//            Assert.AreEqual(normalized, item4.Normalized);
//        }

        private static Func<T, TResult> Funcify<T, TResult>(Func<T, TResult> f)
        {
            return f;
        }

        private static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Name = "TestCustomer1", Address = "TestAddress1", TerritoryId = 1, Telephone = "555-1212"},
                new Customer {Name = "TestCustomer2", Address = null, TerritoryId = 2, Telephone = "555-2222"},
                new Customer {Name = "TestCustomer3", Address = "TestAddress3", TerritoryId = 3, Telephone = "555-3232"},
                new Customer {Name = "TestCustomer4", Address = "TestAddress4", TerritoryId = 4, Telephone = "555-4242"},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0},
                new Customer {Name = "TestCustomer", Address = "TestAddress", TerritoryId = 0}
            };
        }

        #region Nested type: Contact

        internal class Contact
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
            /// </summary>
            public Contact()
            {
                State = "WA";
            }

            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int ZipCode { get; set; }
            public string Phone { get; set; }
            public DateTime LastContactDate { get; set; }
        }

        #endregion

        #region Nested type: ContactNotNullableInt

        internal class ContactNotNullableInt : Contact
        {
            public int Id { get; set; }
        }

        #endregion

        #region Nested type: ContactNullableInt

        internal class ContactNullableInt : Contact
        {
            public int? Id { get; set; }
        }

        #endregion

        #region Nested type: Customer

        internal class Customer
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int ZipCode { get; set; }
            public string Telephone { get; set; }
            public int TerritoryId { get; set; }
        }

        #endregion

        #region Nested type: CustomerNullableInt

        internal class CustomerNullableInt : Customer
        {
            public int? Id { get; set; }
        }

        #endregion
    }
}