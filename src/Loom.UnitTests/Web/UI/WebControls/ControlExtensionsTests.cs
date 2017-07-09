#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NUnit.Framework;

#endregion

namespace Loom.Web.UI.WebControls
{
    [TestFixture]
    public class ControlExtensionsTests
    {
        [TestCase("Child1", true)]
        [TestCase("Child2", true)]
        [TestCase("GrandChild1", true)]
        [TestCase("GrandChild2", true)]
        [TestCase("GrandChildX", false)]
        [TestCase("", false, ExpectedException = typeof(ArgumentException))]
        [TestCase(null, false, ExpectedException = typeof(ArgumentNullException))]
        public void FindControlRecursive(string id, bool shouldFind)
        {
            Control parent = new ControlTree();
            Control control = parent.FindChild(id);
            bool found = control != null;

            Assert.AreEqual(shouldFind, found);
            if (found)
                Assert.AreEqual(id, control.ID);
        }

        [TestCase("Child1", false)]
        [TestCase("Child2", false)]
        [TestCase("GrandChild1", false)]
        [TestCase("GrandChild2", true)]
        [TestCase("GrandChildX", false)]
        [TestCase("", false, ExpectedException = typeof(ArgumentException))]
        [TestCase(null, false, ExpectedException = typeof(ArgumentNullException))]
        public void FindTextBoxRecursive(string id, bool shouldFind)
        {
            Control parent = new ControlTree();
            TextBox control = parent.FindChild<TextBox>(id);
            bool found = control != null;

            Assert.AreEqual(shouldFind, found);
            if (found)
                Assert.AreEqual(id, control.ID);
        }

        #region Nested type: ControlTree

        private class ControlTree : Control
        {
            public ControlTree()
            {
                Initialize();
            }

            private void Initialize()
            {
                ID = "Parent";

                Control child1 = new Control();
                child1.ID = "Child1";

                Control child2 = new Control();
                child2.ID = "Child2";

                Controls.Add(child1);
                Controls.Add(child2);

                Control grandChild1 = new Control();
                grandChild1.ID = "GrandChild1";

                TextBox grandChild2 = new TextBox();
                grandChild2.ID = "GrandChild2";

                child2.Controls.Add(grandChild1);
                child2.Controls.Add(grandChild2);
            }
        }

        #endregion
    }
}