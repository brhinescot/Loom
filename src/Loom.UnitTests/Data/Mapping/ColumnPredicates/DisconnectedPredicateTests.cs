#region Using Directives

using System;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class DisconnectedPredicateTests
    {
        [TestCase('d')]
        [TestCase(101)]
        public void GreaterThanChar(object value)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > 'c';
            Assert.IsTrue(predicate.Evaluate(value));
        }

        [TestCase('c')]
        [TestCase('b')]
        [TestCase(1)]
        public void NotGreaterThanChar(object value)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > 'c';
            Assert.IsFalse(predicate.Evaluate(value));
        }

        [TestCase(100, 100)]
        [TestCase("A", "A")]
        [TestCase("", "")]
        [TestCase('d', 'd')]
        [TestCase('d', 100)]
        [TestCase(100, 'd')]
        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(null, null)]
        [TestCase(100d, 100)]
        [TestCase(100, 100d)]
        [TestCase(100f, 100d)]
        public void EqualsTrue(object columnValue, object testValue)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId == columnValue;
            Assert.IsTrue(predicate.Evaluate(testValue));
        }

        [TestCase(100, 200)]
        [TestCase(100d, 200)]
        [TestCase(100, 200f)]
        [TestCase(true, false)]
        [TestCase("A", "a")]
        [TestCase("A", "B")]
        [TestCase("A", "")]
        [TestCase(100, null)]
        [TestCase('d', null)]
        [TestCase("d", null)]
        [TestCase(null, 100)]
        [TestCase(null, 'd')]
        [TestCase(null, "d")]
        public void EqualsFalse(object columnValue, object testValue)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId == columnValue;
            Assert.IsFalse(predicate.Evaluate(testValue));
        }

        [Test]
        public void IntColumnGreater()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > 100;
            Assert.IsFalse(predicate.Evaluate(100));
            Assert.IsTrue(predicate.Evaluate(101));
            Assert.IsTrue(predicate.Evaluate(200));
            Assert.IsFalse(predicate.Evaluate(-200));
            Assert.IsFalse(predicate.Evaluate(0));
            Assert.IsFalse(predicate.Evaluate(0d));
            Assert.IsFalse(predicate.Evaluate(0f));
        }

        [Test]
        public void IntColumnGreaterOrEqual()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId >= 100d;
            Assert.IsFalse(predicate.Evaluate(99));
            Assert.IsTrue(predicate.Evaluate(100));
            Assert.IsTrue(predicate.Evaluate(101));
            Assert.IsFalse(predicate.Evaluate(-200));
            Assert.IsFalse(predicate.Evaluate(0d));
            Assert.IsTrue(predicate.Evaluate(84583685682775675675645657356745676345f));
            Assert.IsTrue(predicate.Evaluate(845836856827756756756456573567456763131231231231245233245673657356736733567455674567456345d));
        }

        [Test]
        public void IsBetween()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsBetween(100, 200);
            Assert.IsTrue(predicate.Evaluate(100));
            Assert.IsTrue(predicate.Evaluate(150));
            Assert.IsTrue(predicate.Evaluate(200));
            Assert.IsFalse(predicate.Evaluate(50));
            Assert.IsFalse(predicate.Evaluate(99));
            Assert.IsFalse(predicate.Evaluate(201));
            Assert.IsFalse(predicate.Evaluate(250));
            Assert.IsFalse(predicate.Evaluate(-100));
        }

        [Test]
        public void IsFalseMethod()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsFalse();
            Assert.IsFalse(predicate.Evaluate(true));
            Assert.IsTrue(predicate.Evaluate(false));
            Assert.IsFalse(predicate.Evaluate("awr"));
        }

        [Test]
        public void IsFalseOperator()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId == false;
            Assert.IsFalse(predicate.Evaluate(true));
            Assert.IsTrue(predicate.Evaluate(false));
            Assert.IsFalse(predicate.Evaluate("awr"));
        }

        [Test]
        public void IsTrueMethod()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsTrue();
            Assert.IsFalse(predicate.Evaluate(false));
            Assert.IsTrue(predicate.Evaluate(true));
        }

        [Test]
        public void IsTrueOperator()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId == true;
            Assert.IsFalse(predicate.Evaluate(false));
            Assert.IsTrue(predicate.Evaluate(true));
        }

        [Test]
        public void StringColumnGreater()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > "CDE";
            Assert.IsTrue(predicate.Evaluate("DDD"));
            Assert.IsTrue(predicate.Evaluate("CDF"));
            Assert.IsTrue(predicate.Evaluate("CEE"));
            Assert.IsFalse(predicate.Evaluate("AAA"));
            Assert.IsFalse(predicate.Evaluate("CDA"));
            Assert.IsFalse(predicate.Evaluate("CDE"));
        }

        [Test]
        public void IsEqualToAnyMethod()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, 4, 6);
            Assert.IsFalse(predicate.Evaluate(1));
            Assert.IsTrue(predicate.Evaluate(2));
            Assert.IsFalse(predicate.Evaluate(3));
            Assert.IsTrue(predicate.Evaluate(4));
            Assert.IsFalse(predicate.Evaluate(5));
            Assert.IsTrue(predicate.Evaluate(6));
            Assert.IsFalse(predicate.Evaluate(7));
        }

        [Test]
        public void IsEqualToNoneMethod()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(2, 4, 6);
            Assert.IsTrue(predicate.Evaluate(1));
            Assert.IsFalse(predicate.Evaluate(2));
            Assert.IsTrue(predicate.Evaluate(3));
            Assert.IsFalse(predicate.Evaluate(4));
            Assert.IsTrue(predicate.Evaluate(5));
            Assert.IsFalse(predicate.Evaluate(6));
            Assert.IsTrue(predicate.Evaluate(7));
        }

        [Test]
        public void EqualOrOperator()
        {
            ColumnPredicate predicate = (Customer.Columns.CustomerId == 'c') | (Customer.Columns.AccountNumber == 'd');
            Assert.IsTrue(predicate.Evaluate('c'));
            Assert.IsFalse(predicate.Evaluate('b'));
            Assert.IsTrue(predicate.Evaluate('d'));
        }

        [Test]
        public void EvaluateCharGreaterThanInt()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > 100;
            Assert.IsFalse(predicate.Evaluate('c'));
            Assert.IsFalse(predicate.Evaluate('C'));
            Assert.IsTrue(predicate.Evaluate('h'));
        }

        [Test]
        public void EvaluateNumericStringGreaterThanInt()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > 100;
            Assert.IsFalse(predicate.Evaluate("99"));
            Assert.IsTrue(predicate.Evaluate("101"));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void EvaluateAlphaStringGreaterThanOrEqualIntFail()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId >= 100;
            predicate.Evaluate("c");
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void EvaluateAlphaStringLessThanOrEqualIntFail()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId <= 100;
            predicate.Evaluate("c");
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void EvaluateAlphaStringLessThanIntFail()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId < 100;
            predicate.Evaluate("c");
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void EvaluateAlphaStringGreaterThanIntFail()
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > 100;
            predicate.Evaluate("c");
        }
    }
}