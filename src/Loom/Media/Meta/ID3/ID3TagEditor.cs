#region Using Directives

using System;
using System.IO;
using Loom.IO;

#endregion

namespace Loom.Media.Meta.ID3
{
    public class Id3TagEditor : IDisposable
    {
        private const string TagIdentifier = "TAG";

        private readonly BinaryFileStream<Id3V11Tag> stream;

        public Id3TagEditor(string path, FileAccess access)
        {
            stream = new BinaryFileStream<Id3V11Tag>(path, FileMode.Open, access);
        }

        public Id3TagEditor(string path)
        {
            stream = new BinaryFileStream<Id3V11Tag>(path);
        }

        #region IDisposable Members

        public void Dispose()
        {
            stream.Dispose();
        }

        #endregion

        public Id3V11Tag Read()
        {
            stream.Seek(-128, SeekOrigin.End);

            Id3V11Tag tag = new Id3V11Tag();
            unsafe
            {
                stream.Read(&tag);
            }

            return tag.Tag != TagIdentifier ? Id3V11Tag.Empty : tag;
        }

        public void Write(Id3V11Tag tag)
        {
            stream.Seek(-128, SeekOrigin.End);
            unsafe
            {
                stream.Write(&tag);
            }
        }
    }
}