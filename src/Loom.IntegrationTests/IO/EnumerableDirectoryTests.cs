#region Using Directives

using System.Diagnostics;
using NUnit.Framework;

#endregion

namespace Loom.IO
{
    [TestFixture]
    public class EnumerableDirectoryTests
    {
        [Test]
        public void TEST()
        {
            foreach (string s in EnumerableDirectory.FindFiles(@"G:\Archive\TestMp3s\*.mp3"))
                Debug.WriteLine(s);
        }
    }
}