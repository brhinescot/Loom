#region Using Directives

using Loom.Data.Mapping.CodeGeneration;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.CodeGen
{
    [TestFixture]
    public class CodeFormatToCamelTests
    {
        [TestCase("Hello there buddy", "helloThereBuddy")]
        [TestCase("Hello there buddy Lee", "helloThereBuddyLee")]
        [TestCase("hello there buddy", "helloThereBuddy")]
        public void ToCamelUpperLowerMix(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("Hello there buddy l", "helloThereBuddyL")]
        [TestCase("Hello there buddy L", "helloThereBuddyL")]
        [TestCase("Hello there a buddy L", "helloThereABuddyL")]
        [TestCase("a buddy Lee is a buddy n deed", "aBuddyLeeIsABuddyNDeed")]
        public void ToCamelSingleLetters(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("hello", "hello")]
        [TestCase("hello there buddy", "helloThereBuddy")]
        [TestCase("hello there buddy lee", "helloThereBuddyLee")]
        public void ToCamelAllLower(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("Television  Advertisement", "televisionAdvertisement")]
        [TestCase("Television   Advertisement", "televisionAdvertisement")]
        [TestCase("Television    Advertisement    Material", "televisionAdvertisementMaterial")]
        [TestCase("Television    Advertisement    material", "televisionAdvertisementMaterial")]
        [TestCase("Television    advertisement    Material", "televisionAdvertisementMaterial")]
        public void ToCamelWithMultipleSpaces(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("hello there/buddy", "helloThereBuddy")]
        [TestCase("hello-there buddy lee", "helloThereBuddyLee")]
        [TestCase("hello there *buddy lee", "helloThereBuddyLee")]
        [TestCase("hello there **buddy lee**", "helloThereBuddyLee")]
        [TestCase("hello there \"buddy lee\"", "helloThereBuddyLee")]
        public void ToCamelWithSpecialCharacters(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("hello there number 1 buddy", "helloThereNumber1Buddy")]
        [TestCase("hello there buddy1", "helloThereBuddy1")]
        public void ToCamelWithNumbers(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("2002", "n2002")]
        [TestCase("2002 4th Quarter", "n20024thQuarter")]
        [TestCase("2002 Sales", "n2002Sales")]
        public void ToCamelStartsWithNumbers(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input));
        }

        [TestCase("FK_Customer", "customerId")]
        [TestCase("FKCustomer", "customerId")]
        [TestCase("fk_Customer", "customerId")]
        [TestCase("fkCustomer", "customerId")]
        public void ToCamelRemoveKeyPrefix(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input, CodeFormatOptions.RemoveFKPrefix));
        }

        [TestCase("tblCustomer", "customer")]
        [TestCase("tbl_Customer", "customer")]
        [TestCase("Tbl_Customer", "customer")]
        [TestCase("TBLCustomer", "customer")]
        public void ToCamelRemoveTblPrefix(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input, CodeFormatOptions.RemoveTblPrefix));
        }

        [TestCase("Baha'i", "bahai")]
        [TestCase("Regional Bahá'í Council", "regionalBaháíCouncil")]
        [TestCase("Huqúq'u'lláh Board", "huqúqulláhBoard")]
        [TestCase("Chuuk (truk)", "chuukTruk")]
        public void ToCamelSpecialPuncuation(string input, string expected)
        {
            Assert.AreEqual(expected, CodeFormat.ToCamelCase(input, CodeFormatOptions.RemoveTblPrefix));
        }
    }
}