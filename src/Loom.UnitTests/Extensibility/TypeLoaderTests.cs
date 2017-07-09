#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Extensibility
{
    [TestFixture]
    public class TypeLoaderTests
    {
        [Test]
        public void Test()
        {
            BaseClass baseClass = TypeLoader.CreateInstance<BaseClass>("DerivedClass");
            DerivedClass derivedClass = baseClass as DerivedClass;

            Assert.IsNotNull(baseClass);
            Assert.IsNotNull(derivedClass);
        }

        [Test]
        public void Test2()
        {
            Type type = TypeLoader.GetDerivedType("DerivedClass", typeof(BaseClass));

            Assert.IsNotNull(type);
        }

        #region Nested type: BaseClass

        public class BaseClass { }

        #endregion

        #region Nested type: DerivedClass

        public class DerivedClass : BaseClass { }

        #endregion
    }
}