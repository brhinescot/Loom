#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class CircularQueue1Tests
    {
        [Test]
        public void ItemCount()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();
            Assert.AreEqual(0, circularQueue.Count);

            circularQueue.Enqueue("First");
            circularQueue.Enqueue("Second");
            circularQueue.Enqueue("Third");
            circularQueue.Enqueue("Fourth");
            Assert.AreEqual(4, circularQueue.Count);

            circularQueue.Enqueue("Fifth");
            circularQueue.Enqueue("Sixth");
            Assert.AreEqual(6, circularQueue.Count);
        }

        [Test]
        public void NextMethod()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();

            circularQueue.Enqueue("First");
            circularQueue.Enqueue("Second");
            circularQueue.Enqueue("Third");
            circularQueue.Enqueue("Fourth");

            Assert.AreEqual("First", circularQueue.Next());
            Assert.AreEqual("Second", circularQueue.Next());
            Assert.AreEqual("Third", circularQueue.Next());
            Assert.AreEqual("Fourth", circularQueue.Next());

            Assert.AreEqual("First", circularQueue.Next());
            Assert.AreEqual("Second", circularQueue.Next());
            Assert.AreEqual("Third", circularQueue.Next());
            Assert.AreEqual("Fourth", circularQueue.Next());
        }

        [Test]
        public void NextMethodWithAdd()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();

            circularQueue.Enqueue("First");
            circularQueue.Enqueue("Second");
            circularQueue.Enqueue("Third");
            circularQueue.Enqueue("Fourth");

            Assert.AreEqual("First", circularQueue.Next());
            Assert.AreEqual("Second", circularQueue.Next());
            Assert.AreEqual("Third", circularQueue.Next());
            Assert.AreEqual("Fourth", circularQueue.Next());

            Assert.AreEqual("First", circularQueue.Next());
            Assert.AreEqual("Second", circularQueue.Next());
            Assert.AreEqual("Third", circularQueue.Next());

            // These should be added just after "Third" since it was the last one retrieved
            // and is now at the end of the queue. The new order from this point is 
            // Fourth, First, Second, Third, Fifth, Sixth, Seventh, Eight
            circularQueue.Enqueue("Fifth");
            circularQueue.Enqueue("Sixth");
            circularQueue.Enqueue("Seventh");
            circularQueue.Enqueue("Eight");

            // The four new items are now at the end of the queue behing "Third". The next
            // item is still "Fourth". The original items are still in the previous order 
            // until "Three" is retrieved. Then the new items are accessed.
            Assert.AreEqual("Fourth", circularQueue.Next());
            Assert.AreEqual("First", circularQueue.Next());
            Assert.AreEqual("Second", circularQueue.Next());
            Assert.AreEqual("Third", circularQueue.Next());

            Assert.AreEqual("Fifth", circularQueue.Next());
            Assert.AreEqual("Sixth", circularQueue.Next());
            Assert.AreEqual("Seventh", circularQueue.Next());
            Assert.AreEqual("Eight", circularQueue.Next());
        }

        [Test]
        public void PeekMethod()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();

            circularQueue.Enqueue("First");
            circularQueue.Enqueue("Second");
            circularQueue.Enqueue("Third");
            circularQueue.Enqueue("Fourth");

            Assert.AreEqual("First", circularQueue.Peek());
            Assert.AreEqual("First", circularQueue.Peek());
            Assert.AreEqual("First", circularQueue.Next());

            Assert.AreEqual("Second", circularQueue.Peek());
            Assert.AreEqual("Second", circularQueue.Peek());
            Assert.AreEqual("Second", circularQueue.Next());

            Assert.AreEqual("Third", circularQueue.Peek());
            Assert.AreEqual("Third", circularQueue.Peek());
            Assert.AreEqual("Third", circularQueue.Next());

            Assert.AreEqual("Fourth", circularQueue.Peek());
            Assert.AreEqual("Fourth", circularQueue.Peek());
            Assert.AreEqual("Fourth", circularQueue.Next());

            Assert.AreEqual("First", circularQueue.Peek());
            Assert.AreEqual("First", circularQueue.Peek());
            Assert.AreEqual("First", circularQueue.Next());
        }

        [Test]
        public void ItemEnumeration()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();

            circularQueue.Enqueue("First");
            circularQueue.Enqueue("Second");
            circularQueue.Enqueue("Third");
            circularQueue.Enqueue("Fourth");

            int i = 0;
            string[] arr = new string[4];
            foreach (string s in circularQueue)
            {
                arr[i] = s;
                i++;
            }

            Assert.AreEqual(4, i);
            Assert.AreEqual("First", arr[0]);
            Assert.AreEqual("Second", arr[1]);
            Assert.AreEqual("Third", arr[2]);
            Assert.AreEqual("Fourth", arr[3]);
        }

        [Test]
        public void CopyToMethod()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>(new[] {"First", "Second", "Third"});
            string[] arr = new string[circularQueue.Count];
            circularQueue.CopyTo(arr, 0);

            Assert.IsNotNull(arr);
            Assert.AreEqual(3, arr.Length);
            Assert.AreEqual("First", arr[0]);
            Assert.AreEqual("Second", arr[1]);
            Assert.AreEqual("Third", arr[2]);
        }

        [Test]
        public void CopyToMethodOnReadonly()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>(new[] {"First", "Second", "Third"}, true);
            string[] arr = new string[circularQueue.Count];
            circularQueue.CopyTo(arr, 0);

            Assert.IsNotNull(arr);
            Assert.AreEqual(3, arr.Length);
            Assert.AreEqual("First", arr[0]);
            Assert.AreEqual("Second", arr[1]);
            Assert.AreEqual("Third", arr[2]);
        }

        [Test]
        public void ToArrayMethod()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>(new[] {"First", "Second", "Third"});
            string[] arr = circularQueue.ToArray();

            Assert.IsNotNull(arr);
            Assert.AreEqual(3, arr.Length);
            Assert.AreEqual("First", arr[0]);
            Assert.AreEqual("Second", arr[1]);
            Assert.AreEqual("Third", arr[2]);
        }

        [Test]
        public void ToArrayMethodOnReadonly()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>(new[] {"First", "Second", "Third"}, true);
            string[] arr = circularQueue.ToArray();

            Assert.IsNotNull(arr);
            Assert.AreEqual(3, arr.Length);
            Assert.AreEqual("First", arr[0]);
            Assert.AreEqual("Second", arr[1]);
            Assert.AreEqual("Third", arr[2]);
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyException))]
        public void AddItemToReadonly()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>(new[] {"First", "Second", "Third"}, true);
            circularQueue.Enqueue("First");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekOnEmpty()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();
            circularQueue.Peek();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NextOnEmpty()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();
            circularQueue.Next();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveOnEmpty()
        {
            CircularQueue<string> circularQueue = new CircularQueue<string>();
            circularQueue.Remove();
        }
    }
}