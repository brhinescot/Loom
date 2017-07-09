#region Using Directives

using System;
using System.Diagnostics;
using System.IO;
using Loom.Diagnostics;
using Loom.IO;
using NUnit.Framework;

#endregion

namespace Loom.Media.Meta.ID3
{
    [TestFixture]
    public class Id3TagEditorTests
    {
        private const string Mp3Path = @"Media\Meta\ID3\blind_willie.mp3";

        [Test]
        public void ReadTag()
        {
            using (Id3TagEditor editor = new Id3TagEditor(Mp3Path, FileAccess.Read))
            {
                Id3V11Tag tag = editor.Read();
                Assert.AreEqual("Rob Towns", tag.Artist);
                Assert.AreEqual("Blind Willie's Big Album", tag.Album);
                Assert.AreEqual(1, tag.Track);
                Assert.AreEqual("Blind Willie", tag.Title);
                Assert.AreEqual(40, tag.Genre);

                Console.WriteLine("File Opened");
                Console.WriteLine("Tag = " + tag.Tag);
                Console.WriteLine("Title = " + tag.Title);
                Console.WriteLine("Artist = " + tag.Artist);
                Console.WriteLine("Album = " + tag.Album);
                Console.WriteLine("Year = " + tag.Year);
                Console.WriteLine("Comment = " + tag.Comment);
                Console.WriteLine("Track = " + tag.Track);
                Console.WriteLine("Genre = " + tag.Genre);
                Console.WriteLine();
            }
        }

        [Test]
        public void PerformanceReads()
        {
            CodeTimer timer = CodeTimer.Start();
            for (int i = 0; i < 100000; i++)
                using (Id3TagEditor editor = new Id3TagEditor(Mp3Path, FileAccess.Read))
                {
                    Id3V11Tag tag = editor.Read();

                    Assert.AreEqual("Rob Towns", tag.Artist);
                    Assert.AreEqual("Blind Willie's Big Album", tag.Album);
                    Assert.AreEqual(1, tag.Track);
                    Assert.AreEqual("Blind Willie", tag.Title);
                    Assert.AreEqual(40, tag.Genre);
                }
            CodeTimer.WriteMilliseconds(timer);
        }

        [Test]
        public void ReadWriteTag()
        {
            using (Id3TagEditor editor = new Id3TagEditor(Mp3Path))
            {
                Id3V11Tag tag = editor.Read();
                Assert.AreEqual("Blind Willie", tag.Title);

                tag.Title = "Big Willie";
                tag.Album = "Blind Willie's Big Album";
                tag.Comment = "Best Willie album ever.";
                tag.Track = 1;
                tag.Genre = 40;
                editor.Write(tag);

                tag = editor.Read();
                Assert.AreEqual("Big Willie", tag.Title);
                Assert.AreEqual("Blind Willie's Big Album", tag.Album);
                Assert.AreEqual("Best Willie album ever.", tag.Comment);
                Assert.AreEqual(1, tag.Track);
                Assert.AreEqual(40, tag.Genre);

                tag.Title = "Blind Willie";
                editor.Write(tag);

                tag = editor.Read();
                Assert.AreEqual("Blind Willie", tag.Title);

                Console.WriteLine(tag.Title);
            }
        }

        [Test]
        public void ReadDirectory()
        {
            foreach (string path in EnumerableDirectory.FindFiles(@"G:\Archive\TestMp3s\*.*"))
//            foreach (string path in EnumerableDirectory.FindFiles(@"Z:\Music\Subscription - [NoDRM]\Pink Floyd\A Momentary Lapse Of Reason\"))
            {
                Id3TagEditor editor = null;
                try
                {
                    editor = new Id3TagEditor(path, FileAccess.ReadWrite);
                    Id3V11Tag tag = editor.Read();
                    if (tag == Id3V11Tag.Empty)
                    {
                        Debug.WriteLine("[No Tag] " + path);
                        continue;
                    }

                    Debug.WriteLine(tag.ToString());
//                    Debug.WriteLineIf(tag.Artist == "UB40", tag.ToString());
                }
                catch (IOException) { }
                finally
                {
                    if (editor != null)
                        editor.Dispose();
                }
            }
        }
    }
}