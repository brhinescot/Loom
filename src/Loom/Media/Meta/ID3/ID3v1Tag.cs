#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace Loom.Media.Meta.ID3
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct ID3v1Tag
    {
        #region Private Member Fields

        private fixed byte tag [3];
        private fixed byte title [30];
        private fixed byte artist [30];
        private fixed byte album [30];
        private fixed byte year [4];
        private fixed byte comment [30];
        private fixed byte genre [1];

        #endregion

        #region Property Accessors

        public string Tag
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->tag);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->tag, value, 3);
                }
            }
        }

        public string Title
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->title);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->title, value, 30);
                }
            }
        }

        public string Artist
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->artist);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->artist, value, 30);
                }
            }
        }

        public string Album
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->album);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->album, value, 30);
                }
            }
        }

        public string Year
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->year);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->year, value, 4);
                }
            }
        }

        public string Comment
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->comment);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->comment, value, 30);
                }
            }
        }

        public string Genre
        {
            get
            {
                fixed (ID3v1Tag* t = &this)
                {
                    return GetString(t->genre);
                }
            }
            set
            {
                fixed (ID3v1Tag* t = &this)
                {
                    FillArray(t->genre, value, 1);
                }
            }
        }

        #endregion

        #region Private Helpers

        private static void FillArray(byte* array, string value, int maxLength)
        {
            if (Compare.IsNullOrEmpty(value))
                return;

            byte[] bytes = Encoding.ASCII.GetBytes(value);
            for (int i = 0; i < maxLength; i++)
            {
                if (i < bytes.Length)
                {
                    array[i] = bytes[i];
                    continue;
                }

                array[i] = 0;
            }

//          var bytes = Encoding.ASCII.GetBytes(value);
//          var sbytes = new sbyte[bytes.Length];
//          Buffer.BlockCopy(bytes, 0, sbytes, 0, bytes.Length); 
//          fixed (SByte* arrayPointer = sbytes)
//          {
//              
//          }
        }

        private static string GetString(byte* buffer)
        {
            if ((IntPtr) buffer == IntPtr.Zero)
                return null;

            StringBuilder s = new StringBuilder();

            for (int i = 0;; i++)
                try
                {
                    if (buffer[i] == '\0')
                        break;

                    s.Append((char) buffer[i]);
                }
                catch (AccessViolationException e)
                {
                    throw new ArgumentException("Data in buffer not null terminated or bad pointer", e);
                }

            return s.ToString();
        }

        #endregion
    }
}