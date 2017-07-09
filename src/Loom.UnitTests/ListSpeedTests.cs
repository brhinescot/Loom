#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    [Ignore("Speed Tests: Run Tests Manually")]
    public class ListSpeedTests
    {
        [Test]
        public void Test()
        {
            // Warm up;
            Hashtable();
            GenericDictionary();
            GenericList();

            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            double hash = Hashtable();
            double dict = GenericDictionary();
            double list = GenericList();

            Console.WriteLine("Dictionary = " + dict);
            Console.WriteLine("Hashtable = " + hash);
            Console.WriteLine("List = " + list);
        }

        public double GenericDictionary()
        {
            Dictionary<int, object> test = new Dictionary<int, object>();
            for (int i = 0; i < 200000; i++)
                test.Add(i, null);

            DateTime start = DateTime.Now;
            for (int i = 5000; i < 20000; i++)
            {
                object x = test[i];
            }
            DateTime stop = DateTime.Now;
            TimeSpan t = stop - start;
            return t.TotalMilliseconds;
        }

        public double Hashtable()
        {
            Hashtable test = new Hashtable();
            for (int i = 0; i < 200000; i++)
                test.Add(i, null);

            DateTime start = DateTime.Now;
            for (int i = 5000; i < 20000; i++)
            {
                object x = test[i];
            }
            DateTime stop = DateTime.Now;
            TimeSpan t = stop - start;
            return t.TotalMilliseconds;
        }

        public double GenericList()
        {
            List<int> test = new List<int>();
            for (int i = 0; i < 200000; i++)
                test.Add(i);

            DateTime start = DateTime.Now;
            for (int i = 5000; i < 20000; i++)
            {
                int x = test.IndexOf(i);
            }
            DateTime stop = DateTime.Now;
            TimeSpan t = stop - start;
            return t.TotalMilliseconds;
        }
    }
}