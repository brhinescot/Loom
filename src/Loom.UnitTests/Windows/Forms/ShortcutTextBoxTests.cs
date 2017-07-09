#region Using Directives

using System.Windows.Forms;
using NUnit.Framework;

#endregion

namespace Loom.Windows.Forms
{
    [TestFixture]
    public class ShortcutTextBoxTests
    {
        [Test]
        public void SetKeyDataNoModifiers()
        {
            ShortcutTextBox box = new ShortcutTextBox();
            box.KeyData = Keys.X;

            Assert.AreEqual(Keys.X, box.KeyCode);
            Assert.AreEqual(Keys.X, box.KeyData);
            Assert.AreEqual(Keys.None, box.Modifiers);
        }

        [TestCase(Keys.Alt)]
        [TestCase(Keys.Alt | Keys.Shift)]
        [TestCase(Keys.Alt | Keys.Shift | Keys.Control)]
        public void SetKeyDataAddModifiers(Keys modifiers)
        {
            ShortcutTextBox box = new ShortcutTextBox();
            box.KeyData = Keys.X;
            box.Modifiers = modifiers;

            Assert.AreEqual(Keys.X, box.KeyCode);
            Assert.AreEqual(Keys.X | modifiers, box.KeyData);
            Assert.AreEqual(modifiers, box.Modifiers);
        }

        [TestCase(Keys.X, "X")]
        [TestCase(Keys.X | Keys.Alt, "Alt + X")]
        [TestCase(Keys.X | Keys.Alt | Keys.Shift, "Shift + Alt + X")]
        [TestCase(Keys.X | Keys.Alt | Keys.Control | Keys.Shift, "Ctrl + Shift + Alt + X")]
        public void GetText(Keys keyData, string expected)
        {
            ShortcutTextBox box = new ShortcutTextBox();
            box.KeyData = keyData;
            string actual = box.Text;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(Keys.None, false)]
        [TestCase(Keys.Alt, false)]
        [TestCase(Keys.Shift, false)]
        [TestCase(Keys.Shift | Keys.Control, false)]
        [TestCase(Keys.Control, false)]
        [TestCase(Keys.X, true)]
        [TestCase(Keys.X | Keys.Alt, true)]
        [TestCase(Keys.X | Keys.Shift, true)]
        [TestCase(Keys.X | Keys.Control, true)]
        public void HasShortcut(Keys keyData, bool expected)
        {
            ShortcutTextBox box = new ShortcutTextBox();
            box.KeyData = keyData;

            Assert.AreEqual(expected, box.HasShortcut);
        }
    }
}