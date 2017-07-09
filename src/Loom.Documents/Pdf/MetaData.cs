#region Using Directives

using System.Collections;
using System.IO;
using iTextSharp.text.xml.xmp;

#endregion

namespace Loom.Documents.Pdf
{
    public class MetaData
    {
        public MetaData()
        {
            Info = new Hashtable();
        }

        public string Author
        {
            get => (string) Info["Author"];
            set => Add("Author", value);
        }

        public string Creator
        {
            get => (string) Info["Creator"];
            set => Add("Creator", value);
        }

        protected Hashtable Info { get; }

        public string Keywords
        {
            get => (string) Info["Keywords"];
            set => Add("Keywords", value);
        }

        public string Producer
        {
            get => (string) Info["Producer"];
            set => Add("Producer", value);
        }

        public string Subject
        {
            get => (string) Info["Subject"];
            set => Add("Subject", value);
        }

        public string Title
        {
            get => (string) Info["Title"];
            set => Add("Title", value);
        }

        public void Add(string name, string value)
        {
            Argument.Assert.IsNotNull(name, nameof(name));

            if (value == null)
            {
                Info.Remove(name);
                return;
            }

            if (!Info.ContainsKey(name))
                Info.Add(name, value);
            else
                Info[name] = value;
        }

        public void Remove(string name)
        {
            Argument.Assert.IsNotNull(name, nameof(name));

            Info.Remove(name);
        }

        public Hashtable ToHashtable()
        {
            return Info;
        }

        public byte[] ToByteArray()
        {
            MemoryStream ms = new MemoryStream();
            XmpWriter xmp = new XmpWriter(ms, Info);
            xmp.Close();
            return ms.ToArray();
        }
    }
}