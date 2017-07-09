#region Using Directives

using System;
using System.Threading;

#endregion

namespace Loom
{
    /// <summary>
    ///     Represents a class for displaying an animated spinner during long operations in a
    ///     console application.
    /// </summary>
    public class ConsoleSpinner
    {
        private static readonly string[] Frames = {"|", "/", "-", "\\"};
        private ConsoleColor defaultColor;
        private Thread workerThread;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleSpinner" /> class.
        /// </summary>
        public ConsoleSpinner() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleSpinner" /> class.
        /// </summary>
        /// <param name="speed">The speed of the animation in milliseconds per frame.</param>
        /// <param name="defaultProcessingMessage">The processing message.</param>
        /// <param name="defaultCompletedMessage">The completed message.</param>
        public ConsoleSpinner(int speed, string defaultProcessingMessage = null, string defaultCompletedMessage = null)
        {
            ProcessingMessage = defaultProcessingMessage;
            CompletedMessage = defaultCompletedMessage;
            Speed = speed;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleSpinner" /> class.
        /// </summary>
        /// <param name="defaultProcessingMessage">The processing message.</param>
        /// <param name="defaultCompletedMessage">The completed message.</param>
        public ConsoleSpinner(string defaultProcessingMessage, string defaultCompletedMessage) : this(150, defaultProcessingMessage, defaultCompletedMessage) { }

        /// <summary>
        ///     Gets or sets the color of the <see cref="CompletedMessage" />.
        /// </summary>
        public ConsoleColor CompleteColor { get; set; }

        /// <summary>
        ///     Gets or sets the message displayed when the <see cref="ConsoleSpinner" /> is stopped.
        /// </summary>
        /// <value>
        ///     A <see langword="String" /> representing the message to display.
        /// </value>
        public string CompletedMessage { get; set; }

        /// <summary>
        ///     Gets or sets the color of the error message.
        /// </summary>
        public ConsoleColor ErrorColor { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is spinning.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is spinning; otherwise, <c>false</c>.
        /// </value>
        public bool IsSpinning { get; private set; }

        /// <summary>
        ///     Gets or sets the message displayed when the <see cref="ConsoleSpinner" /> is started.
        /// </summary>
        /// <value>
        ///     A <see langword="String" /> representing the message to display.
        /// </value>
        public string ProcessingMessage { get; set; }

        /// <summary>
        ///     Gets or sets the speed in milliseconds between each frame of animation in the
        ///     <see cref="ConsoleSpinner" />.
        /// </summary>
        /// <value>The speed in milliseconds.</value>
        public int Speed { get; set; }

        /// <summary>
        ///     Gets or sets the color of the spinner animation.
        /// </summary>
        public ConsoleColor SpinnerColor { get; set; }

        /// <summary>
        ///     Starts the <see cref="ConsoleSpinner" /> and displays the <see cref="ProcessingMessage" />
        ///     if set.
        /// </summary>
        public void Start()
        {
            Start(ProcessingMessage);
        }

        /// <summary>
        ///     Starts the <see cref="ConsoleSpinner" /> and displays the message represented by the
        ///     <paramref name="message" /> parameter.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void Start(string message)
        {
            if (IsSpinning)
                return;

            Console.Write("{0}{1}", message, message == null ? string.Empty : string.Empty.PadRight(2));
            IsSpinning = true;
            defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = SpinnerColor;
            workerThread = new Thread(Spin);
            workerThread.Start();
        }

        /// <summary>
        ///     Stops the <see cref="ConsoleSpinner" /> and displays the <see cref="CompletedMessage" />
        ///     if set.
        /// </summary>
        public void Stop()
        {
            Stop(CompletedMessage);
        }

        /// <summary>
        ///     Stops the <see cref="ConsoleSpinner" /> and displays the message represented by the
        ///     <paramref name="message" /> parameter.
        /// </summary>
        /// <param name="message">The message to display on the console.</param>
        public void Stop(string message)
        {
            if (!IsSpinning)
                return;

            IsSpinning = false;
            workerThread.Join();
            Console.ForegroundColor = CompleteColor;
            Console.WriteLine("\b{0}", message);
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        ///     Stops the <see cref="ConsoleSpinner" /> and displays the error message represented by the
        ///     <paramref name="message" /> parameter.
        /// </summary>
        /// <param name="message">The message to display on the console.</param>
        public void Error(string message)
        {
            ConsoleColor cached = CompleteColor;
            CompleteColor = ErrorColor;
            Stop(message);
            CompleteColor = cached;
        }

        private void Spin()
        {
            int counter = 0;
            while (IsSpinning)
            {
                Console.Write("\b{0}", Frames[++counter >= Frames.Length ? counter = 0 : counter]);
                Thread.Sleep(Speed);
            }
        }
    }
}