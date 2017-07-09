#region Using Directives

using System;
using System.Reflection;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [TestFixture]
    public class AtrributeTest
    {
        [Test]
        public void FromExternalDecoratedClassTypeOf()
        {
            ActiveTableAttribute attribute = (ActiveTableAttribute) Attribute.GetCustomAttribute(typeof(Derived), typeof(ActiveTableAttribute));
            Assert.IsNotNull(attribute);
            Assert.AreEqual("Sales", attribute.Owner);
            Assert.AreEqual("Customer", attribute.Name);
            Assert.AreEqual("CustomerID", attribute.KeyColumn);
            Assert.AreEqual("ModifiedDate", attribute.ModifiedOnColumn);
        }

        [Test]
        public void FromExternalBaseClassTypeOf()
        {
            // Using typeof() on a base class does not retrieve the metadata for a derived class.
            // typeof() only works on the actual decorated class. The GetType() instance method must be used
            // in all other instances as it is the only way the CLR knows the class hierarchy.
            ActiveTableAttribute attribute = (ActiveTableAttribute) Attribute.GetCustomAttribute(typeof(BaseClass), typeof(ActiveTableAttribute));
            Assert.IsNull(attribute);
        }

        [Test]
        public void FromExternalBaseClassGetType()
        {
            BaseClass baseClass = new Derived();
            ActiveTableAttribute attribute = (ActiveTableAttribute) Attribute.GetCustomAttribute(baseClass.GetType(), typeof(ActiveTableAttribute));
            Assert.IsNotNull(attribute);
            Assert.AreEqual("Sales", attribute.Owner);
            Assert.AreEqual("Customer", attribute.Name);
            Assert.AreEqual("CustomerID", attribute.KeyColumn);
            Assert.AreEqual("ModifiedDate", attribute.ModifiedOnColumn);
        }

        [Test]
        public void FromInternalDerivedClass()
        {
            // This works because we are working on an instance that the CLR knows the class hierarchy of.
            Derived derived = new Derived();
            ActiveTableAttribute attribute = derived.GetAttribute();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("Sales", attribute.Owner);
            Assert.AreEqual("Customer", attribute.Name);
            Assert.AreEqual("CustomerID", attribute.KeyColumn);
            Assert.AreEqual("ModifiedDate", attribute.ModifiedOnColumn);
        }

        [Test]
        public void FromInternalDerivedClassBaseReference()
        {
            // This works because we are working on an instance that the CLR knows the class hierarchy of.
            BaseClass baseClass = new Derived();
            ActiveTableAttribute attribute = baseClass.GetAttribute();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("Sales", attribute.Owner);
            Assert.AreEqual("Customer", attribute.Name);
            Assert.AreEqual("CustomerID", attribute.KeyColumn);
            Assert.AreEqual("ModifiedDate", attribute.ModifiedOnColumn);
        }

        [Test]
        public void FromExternalDecoratedClassGetAllPropertyAttributes()
        {
            Type type = typeof(Derived);
            PropertyInfo[] pinfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            ForeignColumnAttribute attr = (ForeignColumnAttribute) Attribute.GetCustomAttribute(pinfo[0], typeof(ForeignColumnAttribute));

            Assert.AreEqual(1, pinfo.Length);
            Assert.IsNotNull(attr);
            Assert.AreEqual("Name", attr.Name);
        }

        #region Nested type: BaseClass

        public abstract class BaseClass
        {
            public ActiveTableAttribute GetAttribute()
            {
                ActiveTableAttribute attribute = (ActiveTableAttribute) Attribute.GetCustomAttribute(GetType(), typeof(ActiveTableAttribute));
                return attribute;
            }
        }

        #endregion

        #region Nested type: Derived

        [ActiveTable("Sales", "Customer", "CustomerID", ModifiedOnColumn = "ModifiedDate")]
        public class Derived : BaseClass
        {
            [ForeignColumn("Name", typeof(BaseClass))]
            public string Name => null;
        }

        #endregion
    }
}