#region Using Directives

using AdventureWorks.Person;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateEqualTests
    {
        [TestCase("A", false)]
        [TestCase("B", true)]
        [TestCase("C", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase(1, false)]
        public void EqualsString(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId == "B";
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo("B");

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [Test]
        public void EqualsCustomValueType()
        {
            string digits = "(454) 987-9898";
            PhoneNumber phoneNumber = PhoneNumber.Parse(digits);

            string wrongDigits = "(858) 987-9898";
            PhoneNumber wrongPhoneNumber = PhoneNumber.Parse(wrongDigits);

            ColumnPredicate predicate1 = PersonPhone.Columns.PhoneNumber == phoneNumber;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo(phoneNumber);

            Assert.IsTrue(predicate1.Evaluate(digits));
            Assert.IsTrue(predicate1.Evaluate(phoneNumber));
            Assert.IsTrue(predicate2.Evaluate(digits));
            Assert.IsTrue(predicate2.Evaluate(phoneNumber));

            Assert.IsFalse(predicate1.Evaluate(wrongDigits));
            Assert.IsFalse(predicate1.Evaluate(wrongPhoneNumber));
            Assert.IsFalse(predicate2.Evaluate(wrongDigits));
            Assert.IsFalse(predicate2.Evaluate(wrongPhoneNumber));
        }

        [TestCase("1", false)]
        [TestCase("2", true)]
        [TestCase("3", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void EqualsStringEvalNumeric(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId == "2";
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo("2");

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(1f, false)]
        [TestCase(2f, true)]
        [TestCase(3f, false)]
        [TestCase(1d, false)]
        [TestCase(2d, true)]
        [TestCase(3d, false)]
        public void EqualsInt(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId == 2;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo(2);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(1.2, false)]
        [TestCase(2.0, true)]
        [TestCase(3.4, false)]
        [TestCase(1.2f, false)]
        [TestCase(2.0f, true)]
        [TestCase(3.4f, false)]
        public void EqualsDouble(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId == 2.0;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo(2.0);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(1.2f, false)]
        [TestCase(2.0f, true)]
        [TestCase(3.4f, false)]
        [TestCase(1.2, false)]
        [TestCase(2.0, true)]
        [TestCase(3.4, false)]
        public void EqualsFloat(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId == 2.0f;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo(2.0f);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(1.2f, false)]
        [TestCase(2.0f, true)]
        [TestCase(3.4f, false)]
        [TestCase(1.2, false)]
        [TestCase(2.0, true)]
        [TestCase(3.4, false)]
        public void EqualsDecimal(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId == 2.0m;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsEqualTo(2.0m);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }
    }
}