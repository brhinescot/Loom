#region Using Directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

#endregion

namespace Loom
{
    /// <summary>
    ///     Represents a class exposing various console utilities.
    /// </summary>
    public static class ConsoleUtil
    {
        /// <summary>
        ///     Prints the "Press any key to continue..." message to the console and waits for a key press.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method blocks until the user types a character in the console window.
        ///         This method will return as soon as a character is available without waiting
        ///         for a carriage return.
        ///     </para>
        ///     <para>
        ///         The character typed is not echoed to standard output.
        ///     </para>
        /// </remarks>
        /// <returns>
        ///     An <see cref="int" /> representing the character read.
        /// </returns>
        public static int PressAnyKeyToContinue()
        {
            return PressAnyKeyToContinuePrivate();
        }

        /// <summary>
        ///     Starts the application at the specified <paramref name="path" /> with console output capture.
        /// </summary>
        /// <param name="path">The path to the application.</param>
        /// <returns>
        ///     A <see cref="string" /> containing the console output from  the application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> can not <see langword="null" />
        ///     (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     File
        ///     <paramref name="path" /> can not be found.
        /// </exception>
        public static string StartWithOutputCapture(string path)
        {
            Argument.Assert.FileExists(path);

            return StartWithOutputCapturePrivate(path);
        }

        /// <summary>
        ///     Writes a banner that includes the calling assembly's name, version and copyright information to
        ///     the console.
        /// </summary>
        public static void WriteAssemblyBanner()
        {
            WriteAssemblyBannerPrivate();
        }

        private static int PressAnyKeyToContinuePrivate()
        {
            Console.WriteLine("Press any key to continue...");
            return NativeMethods._getch();
        }

        private static string StartWithOutputCapturePrivate(string path)
        {
            using (Process consoleApp = new Process())
            {
                consoleApp.StartInfo.UseShellExecute = false;
                consoleApp.StartInfo.RedirectStandardOutput = true;
                consoleApp.StartInfo.FileName = path;
                consoleApp.Start();
                consoleApp.WaitForExit();

                return consoleApp.StandardOutput.ReadToEnd();
            }
        }

        private static void WriteAssemblyBannerPrivate()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            AssemblyName name = assembly.GetName();
            Console.Out.WriteLine("{0} (v{1})", name.Name, name.Version);

            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            foreach (AssemblyCopyrightAttribute attribute in attributes)
                Console.Out.WriteLine(attribute.Copyright);
        }
    }
}