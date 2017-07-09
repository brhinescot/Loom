#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Loom.Data.Mapping;
using Loom.Diagnostics;
using NUnit.Framework;

#endregion

namespace Loom.Data.Diff
{
    [TestFixture]
    public class CollectionDiffTests : ThreadedTestBase
    {
        [Test]
        public void GenerateNotNull()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);

            Assert.IsNotNull(diffs);
        }

        [Test]
        public void OneDifference()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            expected.Add(new Item(2, "Scotty"));
            actual.Add(new Item(2, "Bones"));

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);

            Assert.AreEqual(1, diffs.Count);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);
            Assert.AreEqual("Scotty", entries[0].BaselineValue);
            Assert.AreEqual("Bones", entries[0].NewValue);
        }

        /// <performance>
        ///     Execution Times:
        ///     2000 Items
        ///     List - 639 ms
        ///     Dictionary - 366 ms
        ///     Introspection - 125 ms
        ///     Introspection (generic struct Key) - 96 ms
        ///     Introspection (simple struct Key) - 56 ms
        ///     Introspection (no formatting) - 46 ms
        ///     Introspection (int key and dynamic GetHashCode) - 31 ms
        ///     Introspection (size initialized dictionary) - 31 ms
        ///     Introspection (ignore key properties) - 0 ms
        ///     20000 Items
        ///     List - 4041 ms
        ///     Dictionary - 3715 ms
        ///     Introspection - 791 ms
        ///     Introspection (generic struct Key) - 637 ms
        ///     Introspection (simple struct Key) - 622 ms
        ///     Introspection (no formatting) - 562 ms
        ///     Introspection (int key and dynamic GetHashCode) - 406 ms
        ///     Introspection (size initialized dictionary) - 359 ms
        ///     Introspection (ignore key properties) - 62 ms
        ///     200000 Items
        ///     List - 37446 ms
        ///     Dictionary - 35261 ms
        ///     Introspection - 8390 ms
        ///     Introspection (generic struct Key) - 7149 ms
        ///     Introspection (simple struct Key) - 6523 ms
        ///     Introspection (no formatting) - 5906 ms
        ///     Introspection (int key and dynamic GetHashCode) - 4015 ms
        ///     Introspection (size initialized dictionary) - 3796 ms
        ///     Introspection (ignore key properties) - 1062 ms
        /// </performance>
        [Test]
        [Repeat(20)]
        public void OneDifferencePerformance()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();
            const int count = 20000;

            for (int i = 0; i < count; i++)
            {
                expected.Add(new Item(i, "Scotty", i, DateTime.Now.Date));
                actual.Add(new Item(i, "Bones", i, DateTime.Now.Date));
            }

            CodeTimer timer = CodeTimer.Start();

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Day", "Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);

            CodeTimer.WriteMilliseconds(timer);

            Assert.AreEqual(count, diffs.Count);
        }

        /// <summary>
        ///     Time:
        ///     1800 items in 824.166ms.
        ///     2000 items in 977.477ms.
        /// </summary>
        [Test]
        [Repeat(1)]
        public void TwoDifferencesPerformance()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            for (int i = 0; i < 18000; i++)
            {
                expected.Add(new Item(i, "Scotty", i, DateTime.Now.AddHours(i).Date));
                actual.Add(new Item(i, "Bones", i + 1, DateTime.Now.AddHours(i).Date));
            }

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Day", "Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);

            Assert.AreEqual(18000, diffs.Count);
        }

        [Test]
        public void TwoDifferencesInSameItem()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            expected.Add(new Item(2, "Scotty", 50));
            actual.Add(new Item(2, "Bones", 55));

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);

            Assert.AreEqual(1, diffs.Count);
            Assert.AreEqual(2, entries.Count);
            Assert.AreEqual("Scotty", entries[0].BaselineValue);
            Assert.AreEqual("Bones", entries[0].NewValue);
            Assert.AreEqual("50", entries[1].BaselineValue);
            Assert.AreEqual("55", entries[1].NewValue);
        }

        [Test]
        public void OneDifferenceInTwoItems()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            expected.Add(new Item(2, "Scotty", 50));
            actual.Add(new Item(2, "Scotty", 55));

            expected.Add(new Item(3, "Kirk", 45));
            actual.Add(new Item(3, "Kirk", 48));

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);

            Assert.AreEqual(2, diffs.Count);

            Assert.AreEqual("50", entries[0].BaselineValue);
            Assert.AreEqual("55", entries[0].NewValue);

            Assert.AreEqual("45", entries[1].BaselineValue);
            Assert.AreEqual("48", entries[1].NewValue);
        }

        [Test]
        public void MultiLevelKey()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            expected.Add(new Item(2, "Scotty", 50));
            actual.Add(new Item(2, "Scotty", 55));

            expected.Add(new Item(2, "Bones", 58));
            actual.Add(new Item(2, "Bones", 60));

            expected.Add(new Item(3, "Kirk", 45));
            actual.Add(new Item(3, "Kirk", 48));

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Id", "Name");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);

            Assert.AreEqual(3, diffs.Count);

            Assert.AreEqual("50", entries[0].BaselineValue);
            Assert.AreEqual("55", entries[0].NewValue);

            Assert.AreEqual("58", entries[1].BaselineValue);
            Assert.AreEqual("60", entries[1].NewValue);

            Assert.AreEqual("45", entries[2].BaselineValue);
            Assert.AreEqual("48", entries[2].NewValue);
        }

        [Test]
        public void OneDifferenceFriendlyFormat()
        {
            ItemCollection expected = new ItemCollection();
            ItemCollection actual = new ItemCollection();

            expected.Add(new Item(2, "Scotty", 35));
            actual.Add(new Item(2, "Bones", 35));

            DiffCollectionGenerator<Item> diffCollectionGenerator = new DiffCollectionGenerator<Item>("Id");
            DiffCollection<Item> diffs = diffCollectionGenerator.GenerateDiffs(expected, actual);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);

            Assert.AreEqual(1, diffs.Count);
            Assert.AreEqual("Scotty", entries[0].BaselineValue);
            Assert.AreEqual("Bones", entries[0].NewValue);
        }

        [Test]
        public void FlightInfoDiffOnlyOutputTest()
        {
            Collection<FlightInfo> nonActualFlightInfo = MockFlightData.FetchNonActualFlightInfo();
            nonActualFlightInfo.Add(new FlightInfo(2265, DateTime.Parse("06/24/07"), "DEN", DateTime.Parse("06/24/07 12:22"), "ORD", DateTime.Parse("06/24/07 15:32"), "PILOT"));

            Collection<FlightInfo> actualFlightInfo = MockFlightData.FetchActualFlightInfo();
            actualFlightInfo.Add(new FlightInfo(1265, DateTime.Parse("06/25/07"), "DEN", DateTime.Parse("06/25/07 12:22"), "ORD", DateTime.Parse("06/25/07 15:32"), "INF"));

            DiffCollectionGenerator<FlightInfo> diffCollectionGenerator = new DiffCollectionGenerator<FlightInfo>("Flight", "Date", "DepartureCity", "ArrivalCity");
            diffCollectionGenerator.MissingName = "OPT ONLY";
            diffCollectionGenerator.AddedName = "SBS ONLY";
            DiffCollection<FlightInfo> diffs = diffCollectionGenerator.GenerateDiffs(actualFlightInfo, nonActualFlightInfo);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);

            Assert.AreEqual(7, diffs.Count);
            Assert.AreEqual(8, entries.Count);
            Assert.AreEqual(DiffType.Changed, entries[0].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[1].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[2].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[3].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[4].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[5].DiffType);
            Assert.AreEqual(DiffType.Missing, entries[6].DiffType);
            Assert.AreEqual(DiffType.Added, entries[7].DiffType);
        }

        [Test]
        public void FlightInfoMissingFromWorkingCollection()
        {
            Collection<FlightInfo> nonActualFlightInfo = MockFlightData.FetchNonActualFlightInfo();
            nonActualFlightInfo.Add(new FlightInfo(2265, DateTime.Parse("06/24/07"), "DEN", DateTime.Parse("06/24/07 12:22"), "ORD", DateTime.Parse("06/24/07 15:32"), "PILOT"));
            Collection<FlightInfo> actualFlightInfo = MockFlightData.FetchActualFlightInfo();

            DiffCollectionGenerator<FlightInfo> diffCollectionGenerator = new DiffCollectionGenerator<FlightInfo>("Flight", "Date", "DepartureCity", "ArrivalCity");
            diffCollectionGenerator.MissingName = "OPT ONLY";
            diffCollectionGenerator.AddedName = "SBS ONLY";
            DiffCollection<FlightInfo> diffs = diffCollectionGenerator.GenerateDiffs(actualFlightInfo, nonActualFlightInfo);
            List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);

            Assert.AreEqual(6, diffs.Count);
            Assert.AreEqual(7, entries.Count);
            Assert.AreEqual(DiffType.Changed, entries[0].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[1].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[2].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[3].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[4].DiffType);
            Assert.AreEqual(DiffType.Changed, entries[5].DiffType);
            Assert.AreEqual(DiffType.Added, entries[6].DiffType);
        }

        [Test]
        public void ThreadedFlightInfoDiff()
        {
            ThreadedRepeat(100, (index, threadAsserter) =>
            {
                Collection<FlightInfo> nonActualFlightInfo = MockFlightData.FetchNonActualFlightInfo();
                Collection<FlightInfo> actualFlightInfo = MockFlightData.FetchActualFlightInfo();

                DiffCollectionGenerator<FlightInfo> diffCollectionGenerator = new DiffCollectionGenerator<FlightInfo>("Flight", "Date", "DepartureCity", "ArrivalCity");
                DiffCollection<FlightInfo> diffs = diffCollectionGenerator.GenerateDiffs(actualFlightInfo, nonActualFlightInfo);
                List<DiffEntry> entries = new List<DiffEntry>(diffs.Entries);
                int diffCount = diffs.Count;
                int entriesCount = entries.Count;

                threadAsserter.Assert(() => Assert.AreEqual(5, diffCount));
                threadAsserter.Assert(() => Assert.AreEqual(6, entriesCount));

                Thread.Sleep(100);

                diffs = diffCollectionGenerator.GenerateDiffs(actualFlightInfo, nonActualFlightInfo);
                int diffCount2 = diffs.Count;
                int entriesCount2 = entries.Count;

                threadAsserter.Assert(() => Assert.AreEqual(5, diffCount2));
                threadAsserter.Assert(() => Assert.AreEqual(6, entriesCount2));
            });
        }
    }

    public static class FlightInfoFormatter
    {
        private const string FlightInfoActArrDiffFormat = "${Flight:0000}\t${Date:MMddyy}\t\t${DepartureCity}\t${DepartureTime:HHmm}\t${ArrivalCity}\t[${ArrivalTime:HHmm}]";
        private const string FlightInfoActDepDiffFormat = "${Flight:0000}\t${Date:MMddyy}\t\t${DepartureCity}\t[${DepartureTime:HHmm}]\t${ArrivalCity}\t${ArrivalTime:HHmm}";
        private const string FlightInfoFormat = "${Flight:0000}\t${Date:MMddyy}\t\t${DepartureCity}\t${DepartureTime:HHmm}\t${ArrivalCity}\t${ArrivalTime:HHmm}";

        public static string Console(FlightInfo item, DiffEntry entry)
        {
            string format;
            switch (entry.Name)
            {
                case "ACT ARR":
                    format = FlightInfoActArrDiffFormat;
                    break;
                case "ACT DEP":
                    format = FlightInfoActDepDiffFormat;
                    break;
                default:
                    format = FlightInfoFormat;
                    break;
            }

            return string.Concat(FormattableObject.ToString(item, format, null), string.Format("\t{0}\t\t{1}\t{2}", entry.Name, entry.BaselineValue, item.Source));
        }

        public static string ItemFormater(Item item, DiffEntry entry)
        {
            return string.Concat(FormattableObject.ToString(item, "${Id}\t${Name}\t${Age}", null), string.Format("\t{0}\t{1}", entry.Name, entry.BaselineValue));
        }
    }

    internal static class MockFlightData
    {
        public static Collection<FlightInfo> FetchNonActualFlightInfo()
        {
            Collection<FlightInfo> collection = new Collection<FlightInfo>();

            collection.Add(new FlightInfo(2354, DateTime.Parse("06/23/07"), "PHX", DateTime.Parse("06/23/07 12:22"), "DEN", DateTime.Parse("06/23/07 15:32"), "PILOT"));
            collection.Add(new FlightInfo(5698, DateTime.Parse("06/23/07"), "ABQ", DateTime.Parse("06/23/07 06:52"), "MOB", DateTime.Parse("06/23/07 09:45"), "PILOT")); // ACT ARR
            collection.Add(new FlightInfo(1485, DateTime.Parse("06/22/07"), "ORD", DateTime.Parse("06/23/07 18:56"), "LAX", DateTime.Parse("06/23/07 21:02"), "PILOT"));
            collection.Add(new FlightInfo(6975, DateTime.Parse("06/22/07"), "PHL", DateTime.Parse("06/23/07 14:36"), "WAS", DateTime.Parse("06/23/07 18:45"), "PILOT")); // ACT DEP, ACT ARR
            collection.Add(new FlightInfo(5673, DateTime.Parse("06/15/07"), "MSY", DateTime.Parse("06/15/07 06:32"), "ORD", DateTime.Parse("06/15/07 12:45"), "PILOT"));
            collection.Add(new FlightInfo(0235, DateTime.Parse("06/15/07"), "MDY", DateTime.Parse("06/15/07 08:23"), "MSY", DateTime.Parse("06/15/07 13:35"), "PILOT")); // ACT ARR
            collection.Add(new FlightInfo(0775, DateTime.Parse("06/14/07"), "PHX", DateTime.Parse("06/14/07 12:25"), "ORD", DateTime.Parse("06/14/07 17:21"), "PILOT")); // ACT DEP
            collection.Add(new FlightInfo(0689, DateTime.Parse("06/13/07"), "ORD", DateTime.Parse("06/13/07 05:12"), "DEN", DateTime.Parse("06/13/07 09:25"), "PILOT"));
            collection.Add(new FlightInfo(4520, DateTime.Parse("06/12/07"), "PHX", DateTime.Parse("06/12/07 12:41"), "DEN", DateTime.Parse("06/12/07 17:43"), "PILOT")); // ACT ARR
            collection.Add(new FlightInfo(9874, DateTime.Parse("06/12/07"), "ORD", DateTime.Parse("06/12/07 07:36"), "MOB", DateTime.Parse("06/12/07 10:42"), "PILOT"));
            collection.Add(new FlightInfo(1420, DateTime.Parse("06/06/07"), "MOB", DateTime.Parse("06/06/07 09:54"), "PHX", DateTime.Parse("06/06/07 08:56"), "PILOT"));

            return collection;
        }

        public static Collection<FlightInfo> FetchActualFlightInfo()
        {
            Collection<FlightInfo> collection = new Collection<FlightInfo>();
            collection.Add(new FlightInfo(2354, DateTime.Parse("06/23/07"), "PHX", DateTime.Parse("06/23/07 12:22"), "DEN", DateTime.Parse("06/23/07 15:32"), "INF"));
            collection.Add(new FlightInfo(5698, DateTime.Parse("06/23/07"), "ABQ", DateTime.Parse("06/23/07 06:52"), "MOB", DateTime.Parse("06/23/07 09:55"), "INF")); // ACT ARR
            collection.Add(new FlightInfo(1485, DateTime.Parse("06/22/07"), "ORD", DateTime.Parse("06/23/07 18:56"), "LAX", DateTime.Parse("06/23/07 21:02"), "INF"));
            collection.Add(new FlightInfo(6975, DateTime.Parse("06/22/07"), "PHL", DateTime.Parse("06/23/07 14:21"), "WAS", DateTime.Parse("06/23/07 18:23"), "INF")); // ACT DEP, ACT ARR
            collection.Add(new FlightInfo(5673, DateTime.Parse("06/15/07"), "MSY", DateTime.Parse("06/15/07 06:32"), "ORD", DateTime.Parse("06/15/07 12:45"), "INF"));
            collection.Add(new FlightInfo(0235, DateTime.Parse("06/15/07"), "MDY", DateTime.Parse("06/15/07 08:23"), "MSY", DateTime.Parse("06/15/07 13:25"), "INF")); // ACT ARR
            collection.Add(new FlightInfo(0775, DateTime.Parse("06/14/07"), "PHX", DateTime.Parse("06/14/07 12:28"), "ORD", DateTime.Parse("06/14/07 17:21"), "INF")); // ACT DEP
            collection.Add(new FlightInfo(0689, DateTime.Parse("06/13/07"), "ORD", DateTime.Parse("06/13/07 05:12"), "DEN", DateTime.Parse("06/13/07 09:25"), "INF"));
            collection.Add(new FlightInfo(4520, DateTime.Parse("06/12/07"), "PHX", DateTime.Parse("06/12/07 12:41"), "DEN", DateTime.Parse("06/12/07 17:55"), "INF")); // ACT ARR
            collection.Add(new FlightInfo(9874, DateTime.Parse("06/12/07"), "ORD", DateTime.Parse("06/12/07 07:36"), "MOB", DateTime.Parse("06/12/07 10:42"), "INF"));
            collection.Add(new FlightInfo(1420, DateTime.Parse("06/06/07"), "MOB", DateTime.Parse("06/06/07 09:54"), "PHX", DateTime.Parse("06/06/07 08:56"), "INF"));

            return collection;
        }
    }

    public class FlightInfo
    {
        public FlightInfo(int flight, DateTime date, string departureCity, DateTime departureTime, string arrivalCity, DateTime arrivalTime, string source)
        {
            Flight = flight;
            Source = source;
            Date = date;
            DepartureCity = departureCity;
            ArrivalCity = arrivalCity;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        [DiffVisible(FriendlyName = "FLIGHT")]
        public int Flight { get; }

        [DiffVisible(FriendlyName = "FLIGHT DATE", Format = "MMddyy")]
        public DateTime Date { get; }

        [DiffVisible(FriendlyName = "DEP CITY")]
        public string DepartureCity { get; }

        [DiffVisible(FriendlyName = "ACT DEP", Format = "HHmm")]
        public DateTime DepartureTime { get; }

        [DiffVisible(FriendlyName = "ARR CITY")]
        public string ArrivalCity { get; }

        [DiffVisible(FriendlyName = "ACT ARR", Format = "HHmm")]
        public DateTime ArrivalTime { get; }

        [DiffVisible(false)]
        public string Source { get; }
    }

    public class Item
    {
        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Item(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public Item(int id, string name, int age, DateTime day)
        {
            Age = age;
            Id = id;
            Name = name;
            Day = day;
        }

        public int Id { get; }

        public string Name { get; }

        public int Age { get; }

        public DateTime Day { get; }
    }

    public class ItemCollection : Collection<Item> { }
}