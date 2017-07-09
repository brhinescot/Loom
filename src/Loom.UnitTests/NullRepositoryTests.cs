#region Using Directives

using System;
using NUnit.Framework;

#endregion

// ReSharper disable ExpressionIsAlwaysNull

namespace Loom
{
    [TestFixture]
    public class NullRepositoryTests
    {
        [Test]
        public void NullObject()
        {
            NullRepository.RegisterValue(new Customer {Name = "Unknown"});

            Customer customer = new Customer {Name = "Brian"};
            Customer nullCustomer = null;

            Customer repositoryCustomer = NullRepository.Retrieve(customer);
            Assert.AreEqual(customer.Name, repositoryCustomer.Name);

            repositoryCustomer = NullRepository.Retrieve(nullCustomer);
            Assert.AreEqual("Unknown", repositoryCustomer.Name);
        }

        [Test]
        public void Test2()
        {
            NullRepository.RegisterConversionValue<DateTime>(0000);
            NullRepository.RegisterConversionValue<DateTime>("Unknown");

            DateTime? hasValue = new DateTime(2001, 1, 1);
            DateTime? noValue = null;

            string hasValueActual = hasValue.ConvertOrDefault(x => x.ToString("MM/dd/yyyy"));
            string noValueConvertedToString = noValue.ConvertOrDefault(x => x.ToString("MM/dd/yyyy"));
            int noValueConvertedToInt = noValue.ConvertOrDefault(x => x.Year);

            string dateTimeDefault = NullRepository.RetrieveConversion<DateTime, string>();

            Assert.AreEqual("01/01/2001", hasValueActual);
            Assert.AreEqual(0000, noValueConvertedToInt);
            Assert.AreEqual("Unknown", noValueConvertedToString);
            Assert.AreEqual("Unknown", dateTimeDefault);
        }

        #region Nested type: Customer

        private class Customer
        {
            public string Name { get; set; }
        }

        #endregion
    }
}