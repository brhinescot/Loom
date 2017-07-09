#region Using Directives

using Loom.Data.Mapping.CodeGeneration;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.CodeGen
{
    [TestFixture]
    public class ColumnFormatoPascalTests
    {
        [Test]
        public void ToPascalUpperLowerMix()
        {
            Assert.AreEqual("HelloThereBuddy", CodeFormat.ToPascalCase("Hello there buddy"));
            Assert.AreEqual("HelloThereBuddyLee", CodeFormat.ToPascalCase("Hello there buddy Lee"));
            Assert.AreEqual("HelloThereBuddy", CodeFormat.ToPascalCase("hello there buddy"));
        }

        [Test]
        public void ToPascalSingleLetters()
        {
            Assert.AreEqual("HelloThereBuddyL", CodeFormat.ToPascalCase("Hello there buddy l"));
            Assert.AreEqual("HelloThereBuddyL", CodeFormat.ToPascalCase("Hello there buddy L"));
            Assert.AreEqual("HelloThereABuddyL", CodeFormat.ToPascalCase("Hello there a buddy L"));
            Assert.AreEqual("ABuddyLeeIsABuddyNDeed", CodeFormat.ToPascalCase("a buddy Lee is a buddy n deed"));
        }

        [Test]
        public void ToPascalAllLower()
        {
            Assert.AreEqual("HelloThereBuddy", CodeFormat.ToPascalCase("hello there buddy"));
            Assert.AreEqual("HelloThereBuddyLee", CodeFormat.ToPascalCase("hello there buddy lee"));
        }

        [Test]
        public void ToPascalWithMultipleSpaces()
        {
            Assert.AreEqual("TelevisionAdvertisement", CodeFormat.ToPascalCase("Television  Advertisement"));
            Assert.AreEqual("TelevisionAdvertisement", CodeFormat.ToPascalCase("Television   Advertisement"));
            Assert.AreEqual("TelevisionAdvertisementMaterial", CodeFormat.ToPascalCase("Television    Advertisement    Material"));
            Assert.AreEqual("TelevisionAdvertisementMaterial", CodeFormat.ToPascalCase("Television    Advertisement    material"));
            Assert.AreEqual("TelevisionAdvertisementMaterial", CodeFormat.ToPascalCase("Television    advertisement    Material"));
        }

        [Test]
        public void ToPascalWithSpecialCharacters()
        {
            Assert.AreEqual("HelloThereBuddy", CodeFormat.ToPascalCase("hello there/buddy"));
            Assert.AreEqual("HelloThereBuddyLee", CodeFormat.ToPascalCase("hello-there buddy lee"));
        }

        [Test]
        public void ToPascalWithNumbers()
        {
            Assert.AreEqual("HelloThereNumber1Buddy", CodeFormat.ToPascalCase("hello there number 1 buddy"));
            Assert.AreEqual("HelloThereBuddy1", CodeFormat.ToPascalCase("hello there buddy1"));
        }

        [Test]
        public void ToPascalStartsWithNumbers()
        {
            Assert.AreEqual("N2002", CodeFormat.ToPascalCase("2002"));
            Assert.AreEqual("N2002Sales", CodeFormat.ToPascalCase("2002 Sales"));
        }

        [Test]
        public void ToPascalSpecialPuncuation()
        {
            Assert.AreEqual("Bahai", CodeFormat.ToPascalCase("Baha'i"));
            Assert.AreEqual("RegionalBaháíCouncil", CodeFormat.ToPascalCase("Regional Bahá'í Council"));
            Assert.AreEqual("HuqúqulláhBoard", CodeFormat.ToPascalCase("Huqúq'u'lláh Board"));
            Assert.AreEqual("ChuukTruk", CodeFormat.ToPascalCase("Chuuk (truk)"));
        }
    }
}