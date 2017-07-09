#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using NUnit.Framework;

#endregion

// ReSharper disable ObjectCreationAsStatement
namespace Loom.Security
{
    [TestFixture]
    public class CryptoRandomTests
    {
        [Test]
        public void FormatCardNumber()
        {
            CryptoRandom generator = new CryptoRandom("{G:2}{N:2}-{G:4}-{G:3}-{G:4}");
            for (int i = 0; i < 100; i++)
            {
                string generated = generator.Generate();
                Assert.AreEqual(generated.ToUpper(), generated);
                Console.Out.WriteLine("result = {0}", generated);
                AssertValidLength(18, generated.Length);
            }
        }

        [Test]
        [Ignore("Speed")]
        public void NoDuplicatesIn10000()
        {
            StringDictionary dict = new StringDictionary();

            int dupes = 0;
            Parallel.For(0, 10000, i =>
            {
                CryptoRandom generator = new CryptoRandom("{D:MM}{A:9}-{G:5}");
                string number = generator.Generate();
                if (!dict.ContainsKey(number))
                    dict.Add(number, null);
                else
                    dupes++;
            });

            Assert.AreEqual(0, dupes, "{0} duplicates found in generated text. Should be 0", dupes);
        }

        [Test]
        public void FormatGeneral6()
        {
            CryptoRandom generator = new CryptoRandom("{Gg:6}");
            for (int i = 0; i < 100; i++)
            {
                string generated = generator.Generate();
                int length = generated.Length;
                AssertValidLength(6, length);
                Console.Out.WriteLine("result = {0}", generated);
            }
        }

        [Test]
        public void FormatGeneral6Upper()
        {
            CryptoRandom generator = new CryptoRandom("{G:6}");
            for (int i = 0; i < 100; i++)
            {
                string generated = generator.Generate();
                Assert.AreEqual(generated.ToUpper(), generated);
                AssertValidLength(6, generated.Length);
                Console.Out.WriteLine("result = {0}", generated);
            }
        }

        [Test]
        public void FormatGeneral6Lower()
        {
            CryptoRandom generator = new CryptoRandom("{g:6}");
            for (int i = 0; i < 100; i++)
            {
                string generated = generator.Generate();
                Assert.AreEqual(generated.ToLower(), generated);
                AssertValidLength(6, generated.Length);
                Console.Out.WriteLine("result = {0}", generated);
            }
        }

        [Test]
        public void FormatSpecial6()
        {
            CryptoRandom generator = new CryptoRandom("{S:12}");
            for (int i = 0; i < 100; i++)
            {
                string result = generator.Generate();
                int length = result.Length;
                AssertValidLength(12, length);
                Console.Out.WriteLine("result = {0}", result);
            }
        }

        [Test]
        public void FormatAlpha2General6()
        {
            CryptoRandom generator = new CryptoRandom("{A:2}-{G:6}");
            for (int i = 0; i < 100; i++)
            {
                string text = generator.Generate();
                AssertValidLength(9, text.Length);
                Assert.AreEqual(2, text.IndexOf('-'));
                Console.Out.WriteLine("result = {0}", text);
            }
        }

        [Test]
        public void FormatNumeric6()
        {
            CryptoRandom generator = new CryptoRandom("{N:6}");
            for (int i = 0; i < 100; i++)
            {
                string text = generator.Generate();
                AssertValidLength(6, text.Length);
                Assert.IsTrue(ValidationProvider.IsInteger(text));
                Console.Out.WriteLine("result = {0}", text);
            }
        }

        [Test]
        public void GenarateNumberStaticNoDupes()
        {
            Dictionary<long, object> dict = new Dictionary<long, object>();
            int dupes = 0;

            for (int i = 0; i < 10000; i++)
            {
                long number = CryptoRandom.NextNumber();
                Console.Out.WriteLine("number = {0}", number);
                if (!dict.ContainsKey(number))
                    dict.Add(number, null);
                else
                    dupes++;
                Console.Out.WriteLine("result = {0}", number);
            }
            Assert.AreEqual(0, dupes, "{0} duplicates found in generated numbers. Should be 0", dupes);
        }

        [Test]
        public void NoFormatInFormatString()
        {
            CryptoRandom generator = new CryptoRandom("NoFormat");
            Assert.AreEqual("NoFormat", generator.Generate(), "The generated text should be the same as the string passed to the format constructor.");
        }

        [Test]
        public void EmptyConstructorDefaultLength()
        {
            CryptoRandom generator = new CryptoRandom();
            int length = generator.Generate().Length;
            AssertValidLength(CryptoRandom.DefaultLength, length);
        }

        [Test]
        public void LengthConstructor5()
        {
            CryptoRandom generator = new CryptoRandom(5);
            int length = generator.Generate().Length;
            AssertValidLength(5, length);
        }

        [Test]
        public void LengthConstructor0()
        {
            CryptoRandom generator = new CryptoRandom(0);
            int length = generator.Generate().Length;
            AssertValidLength(0, length);
        }

        [Test]
        public void LengthConstructor10()
        {
            CryptoRandom generator = new CryptoRandom(10);
            int length = generator.Generate().Length;
            AssertValidLength(10, length);
        }

        [Test]
        public void LengthConstructor1000()
        {
            CryptoRandom generator = new CryptoRandom(1000);
            int length = generator.Generate().Length;
            AssertValidLength(1000, length);
        }

        [Test]
        public void FormatDate()
        {
            CryptoRandom generator = new CryptoRandom("{D:MMdd}");
            for (int i = 0; i < 100; i++)
            {
                string text = generator.Generate();
                AssertValidLength(4, text.Length);
                Assert.AreEqual(DateTime.Now.ToString("MMdd"), text);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFormatTest()
        {
            new CryptoRandom(null);
        }

        private static void AssertValidLength(int expected, int actual)
        {
            Assert.AreEqual(expected, actual, GetInvalidLengthMessage(expected, actual));
        }

        private static string GetInvalidLengthMessage(int expected, int actual)
        {
            return string.Format("The generated string should have a length of {0}, was {1}.", expected, actual);
        }
    }
}