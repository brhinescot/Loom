#region Using Directives

using System;
using System.Threading;
using NUnit.Framework;

#endregion

namespace Loom.Threading
{
    [TestFixture]
    public class ProcessQueueTest
    {
        [Test]
        public void ConstructWithProcessor()
        {
            Processor processor = new Processor();
            ProcessQueue<Item> queue = new ProcessQueue<Item>(processor.Process);

            Item item1 = new Item("Scotty", 1, DateTime.Now);
            Item item2 = new Item("Bones", 2, DateTime.Now);
            Item item3 = new Item("Spock", 3, DateTime.Now);
            Item item4 = new Item("Kirk", 4, DateTime.Now);

            queue.Enqueue(item1);
            queue.Enqueue(item2);
            queue.Enqueue(item3);
            queue.Enqueue(item4);

            Assert.AreEqual(4, queue.Count);
        }

        [Test]
        public void ConstructAndSetProcessor()
        {
            ProcessQueue<Item> queue = new ProcessQueue<Item>();
            Processor processor = new Processor();
            queue.RegisterProcessor(processor.Process);

            Item item1 = new Item("Scotty", 1, DateTime.Now);
            Item item2 = new Item("Bones", 2, DateTime.Now);
            Item item3 = new Item("Spock", 3, DateTime.Now);
            Item item4 = new Item("Kirk", 4, DateTime.Now);

            queue.Enqueue(item1);
            queue.Enqueue(item2);
            queue.Enqueue(item3);
            queue.Enqueue(item4);

            Assert.AreEqual(4, queue.Count);
        }

        [Test]
        public void Test2()
        {
            ProcessQueue<Item> queue = new ProcessQueue<Item>();
            Processor processor = new Processor();
            queue.RegisterProcessor(processor.Process);

            Item item1 = new Item("Scotty", 1, DateTime.Now);
            Item item2 = new Item("Bones", 2, DateTime.Now);
            Item item3 = new Item("Spock", 3, DateTime.Now);
            Item item4 = new Item("Kirk", 4, DateTime.Now);

            queue.Enqueue(item1);
            queue.Enqueue(item2);
            queue.Enqueue(item3);
            queue.Enqueue(item4);

            Assert.IsFalse(queue.Running);
            queue.Start();
            queue.Stop();

            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(4, processor.ProcessCount);
        }

        [Test]
        public void Test3()
        {
            ProcessQueue<Item> queue = new ProcessQueue<Item>();
            Processor processor = new Processor();
            queue.RegisterProcessor(processor.Process);

            Item item1 = new Item("Scotty", 1, DateTime.Now);
            Item item2 = new Item("Bones", 2, DateTime.Now);
            Item item3 = new Item("Spock", 3, DateTime.Now);
            Item item4 = new Item("Kirk", 4, DateTime.Now);

            queue.Enqueue(item1);
            queue.Enqueue(item2);

            queue.Start();
            queue.Stop();

            queue.Enqueue(item2);
            queue.Enqueue(item3);
            queue.Enqueue(item4);

            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(2, processor.ProcessCount);

            queue.Start();
            queue.Stop();

            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(5, processor.ProcessCount);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResetProcessorWhileRunning()
        {
            ProcessQueue<Item> queue = new ProcessQueue<Item>();

            Processor processor = new Processor();
            processor.WaitTime = 1000;
            queue.RegisterProcessor(processor.Process);

            Item item1 = new Item("Scotty", 1, DateTime.Now);
            Item item2 = new Item("Bones", 2, DateTime.Now);

            queue.Enqueue(item1);
            queue.Enqueue(item2);

            queue.Start();
            queue.RegisterProcessor(processor.Process);
        }

        [Test]
        public void ResetProcessorWhileStopped()
        {
            ProcessQueue<Item> queue = new ProcessQueue<Item>();

            Processor processor = new Processor();
            processor.WaitTime = 1000;
            queue.RegisterProcessor(processor.Process);

            Item item1 = new Item("Scotty", 1, DateTime.Now);
            Item item2 = new Item("Bones", 2, DateTime.Now);

            queue.Enqueue(item1);
            queue.Enqueue(item2);

            queue.Start();
            queue.Stop(true);
            queue.RegisterProcessor(processor.Process);
            queue.Start();
            queue.Stop();
            Assert.AreEqual(0, queue.Count);
        }
    }

    #region Mock Test Objects

    internal class Processor
    {
        public int ProcessCount;
        public int WaitTime;

        public void Process(Item obj)
        {
            ProcessCount++;

            if (WaitTime > 0)
                Thread.Sleep(WaitTime);
        }
    }

    internal class Item
    {
        public Item(string name, int id, DateTime birthday)
        {
            Name = name;
            Id = id;
            Birthday = birthday;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime Birthday { get; set; }
    }

    #endregion
}