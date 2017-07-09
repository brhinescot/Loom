#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class ConsoleSpinnerTests
    {
        [Test]
        public void SetMessagesInConstructor()
        {
            ConsoleSpinner spinner = new ConsoleSpinner("Processing", "Complete");
            Assert.AreEqual("Processing", spinner.ProcessingMessage);
            Assert.AreEqual("Complete", spinner.CompletedMessage);
        }

        [Test]
        public void SetMessagesInConstructorAndProperties()
        {
            ConsoleSpinner spinner = new ConsoleSpinner("Processing", "Complete");
            Assert.AreEqual("Processing", spinner.ProcessingMessage);
            Assert.AreEqual("Complete", spinner.CompletedMessage);

            spinner.CompletedMessage = "New Message";
            spinner.ProcessingMessage = "New Message";
            Assert.AreEqual("New Message", spinner.ProcessingMessage);
            Assert.AreEqual("New Message", spinner.CompletedMessage);
        }

        [Test]
        public void StarteWithDifferentMessage()
        {
            ConsoleSpinner spinner = new ConsoleSpinner("Processing", "Complete");
            Assert.AreEqual("Processing", spinner.ProcessingMessage);
            Assert.AreEqual("Complete", spinner.CompletedMessage);

            spinner.Start("Other Message");
            Assert.AreEqual("Processing", spinner.ProcessingMessage);
            Assert.AreEqual("Complete", spinner.CompletedMessage);

            spinner.Stop("Other Message");
            Assert.AreEqual("Processing", spinner.ProcessingMessage);
            Assert.AreEqual("Complete", spinner.CompletedMessage);
        }

        [Test]
        public void IsSpinning()
        {
            ConsoleSpinner spinner = new ConsoleSpinner("Processing", "Complete");

            spinner.Start();
            Assert.IsTrue(spinner.IsSpinning);
            spinner.Stop();
            Assert.IsFalse(spinner.IsSpinning);
            spinner.Start();
            Assert.IsTrue(spinner.IsSpinning);
            spinner.Stop();
            Assert.IsFalse(spinner.IsSpinning);
        }
    }
}