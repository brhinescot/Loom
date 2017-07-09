#region Using Directives

using System.Collections.ObjectModel;
using System.Diagnostics;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class SearchTests
    {
        [TestCase("grandchild3")]
        [TestCase("child3")]
        [TestCase("greatgrandchild2")]
        [TestCase("child4")]
        [TestCase("grandchild3")]
        [TestCase("grandchild5")]
        public void ContainerClassCall(string searchId)
        {
            MockItem parent = GetParentWithChildren();

            MockItem result = MockRecursiveSearch.Find(parent, searchId);
            Assert.IsNotNull(result);
            Assert.AreEqual(searchId, result.Id);

            result = MockRecursiveSearch.Find(parent, searchId);
            Assert.IsNotNull(result);
            Assert.AreEqual(searchId, result.Id);
        }

        [TestCase("grandchild3")]
        [TestCase("child3")]
        [TestCase("greatgrandchild2")]
        [TestCase("child4")]
        [TestCase("grandchild3")]
        [TestCase("grandchild5")]
        public void DirectStaticCall(string searchId)
        {
            MockItem parent = GetParentWithChildren();

            MockItem result = Search.BreadthFirst(parent, p => p.Id == searchId, c => c.Items);
            Assert.IsNotNull(result);
            Assert.AreEqual(searchId, result.Id);

            result = Search.DepthFirst(parent, p => p.Id == searchId, c => c.Items);
            Assert.IsNotNull(result);
            Assert.AreEqual(searchId, result.Id);
        }

        private static MockItem GetParentWithChildren()
        {
            MockItem parent = new MockItem {Id = "parent"};

            parent.Items.Add(new MockItem {Id = "child1"});
            parent.Items.Add(new MockItem {Id = "child2"});
            MockItem child3 = new MockItem {Id = "child3"};
            parent.Items.Add(child3);
            parent.Items.Add(new MockItem {Id = "child4"});

            child3.Items.Add(new MockItem {Id = "grandchild1"});
            MockItem grandChild2 = new MockItem {Id = "grandchild2"};
            child3.Items.Add(grandChild2);
            child3.Items.Add(new MockItem {Id = "grandchild3"});
            child3.Items.Add(new MockItem {Id = "grandchild4"});
            child3.Items.Add(new MockItem {Id = "grandchild5"});

            grandChild2.Items.Add(new MockItem {Id = "greatgrandchild1"});
            grandChild2.Items.Add(new MockItem {Id = "greatgrandchild2"});
            grandChild2.Items.Add(new MockItem {Id = "greatgrandchild3"});
            grandChild2.Items.Add(new MockItem {Id = "greatgrandchild4"});

            return parent;
        }

        #region Nested type: MockItem

        [DebuggerDisplay("{Id}")]
        public class MockItem
        {
            public Collection<MockItem> Items { get; } = new Collection<MockItem>();

            public string Id { get; set; }
        }

        #endregion

        #region Nested type: MockRecursiveSearch

        public static class MockRecursiveSearch
        {
            public static MockItem Find(MockItem parent, string childId)
            {
                return Search.BreadthFirst(parent, p => p.Id == childId, c => c.Items);
            }
        }

        #endregion
    }
}