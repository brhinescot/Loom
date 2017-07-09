#region Using Directives

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class EnumDescriptionAttributeTests
    {
        #region Pet enum

        public enum Pet
        {
            // Attribute has a description. It will be returned.
            [EnumDescription("My Dog")] Dog,

            // Attribute has a description. It will be returned.
            [EnumDescription("My Fish")] Fish,

            // No attribute. A proper case version of the enum will be returned (Cat Fish).
            CatFish,

            // Resource is valid and has a default description. The resource value will be returned (ResourceDog From Embedded Resource).
            [EnumDescription("Resource Dog Default", ResourceName = "ResourceDog_Desc", ResourceAssemblyName = "Loom.UnitTests", ResourceBaseName = "Loom.Properties.Resources")] ResourceDog,

            // Resource is not valid and has a default description. The default description will be returned (Resource Fish Default).
            [EnumDescription("Resource Fish Default", ResourceName = "ResourceFish_Desc", ResourceAssemblyName = "Loom.UnitTests", ResourceBaseName = "Loom.Properties.Resources")] ResourceFish,

            // Resource is not valid and does not have a default description. A proper case version of the enum will be returned (Resource Cat Fish).
            [EnumDescription(ResourceName = "ResourceFish_Desc", ResourceAssemblyName = "Loom.UnitTests", ResourceBaseName = "Loom.Properties.Resources")] ResourceCatFish
        }

        #endregion

        [TestCase(Pet.Dog, "My Dog")]
        [TestCase(Pet.Fish, "My Fish")]
        [TestCase(Pet.CatFish, "Cat Fish")]
        [TestCase(Pet.ResourceDog, "ResourceDog From Embedded Resource")]
        [TestCase(Pet.ResourceFish, "Resource Fish Default")]
        [TestCase(Pet.ResourceCatFish, "Resource Cat Fish")]
        public void FromDescription(Pet pet, string expected)
        {
            string actual = EnumDescriptionAttribute.ToString(pet);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEnumData()
        {
            Dictionary<string, object> dictionary = EnumDescriptionAttribute.GetEnumData(typeof(Pet));

            Assert.AreEqual(6, dictionary.Count);

            Assert.IsNotNull(dictionary["My Dog"]);
            Assert.AreEqual(0, dictionary["My Dog"]);

            Assert.IsNotNull(dictionary["My Fish"]);
            Assert.AreEqual(1, dictionary["My Fish"]);

            Assert.IsNotNull(dictionary["Cat Fish"]);
            Assert.AreEqual(2, dictionary["Cat Fish"]);

            Assert.IsNotNull(dictionary["ResourceDog From Embedded Resource"]);
            Assert.AreEqual(3, dictionary["ResourceDog From Embedded Resource"]);

            Assert.IsNotNull(dictionary["Resource Fish Default"]);
            Assert.AreEqual(4, dictionary["Resource Fish Default"]);

            Assert.IsNotNull(dictionary["Resource Cat Fish"]);
            Assert.AreEqual(5, dictionary["Resource Cat Fish"]);
        }
    }
}