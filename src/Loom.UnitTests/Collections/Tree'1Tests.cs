#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class Tree1Tests
    {
        [Test]
        public void Default()
        {
            Tree<string> tree = new Tree<string>("Root");

            tree.AddBranch("Child1");
            Assert.AreEqual(1, tree.Branches.Count);

            Tree<string> child2 = new Tree<string>("Child2");
            tree.AddBranch(child2);
            Assert.AreEqual(2, tree.Branches.Count);
            Assert.AreEqual(tree, child2.Parent);

            tree.Branches.Remove(child2);
            Assert.AreEqual(1, tree.Branches.Count);
            Assert.AreEqual(null, child2.Parent);

            Tree<string> child3 = tree.AddBranch("Child3");
            Assert.AreEqual(2, tree.Branches.Count);
            Assert.AreEqual("Child3", child3.Value);
            Assert.AreEqual(tree, child3.Parent);

            Tree<string> result = tree.BreathFirstSearch(t => t.Value == "Child3");
            Assert.AreEqual("Child3", result.Value);
        }

        [Test]
        public void AddRangeTests()
        {
            Tree<string> tree = new Tree<string>("Root");
            Tree<string>[] children = {new Tree<string>("Child1"), new Tree<string>("Child2"), new Tree<string>("Child3")};
            tree.AddBranches(children);

            Assert.AreEqual(3, tree.Branches.Count);
            Assert.AreEqual(tree.Branches[0].Value, "Child1");
            Assert.AreEqual(tree.Branches[1].Value, "Child2");
            Assert.AreEqual(tree.Branches[2].Value, "Child3");
        }
    }
}