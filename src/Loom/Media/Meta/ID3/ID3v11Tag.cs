#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Loom.Media.Meta.ID3
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct Id3V11Tag : IEquatable<Id3V11Tag>
    {
        public static Id3V11Tag Empty;

        #region Private Member Fields

        private fixed byte tag [3];
        private fixed byte title [30];
        private fixed byte artist [30];
        private fixed byte album [30];
        private fixed byte year [4];
        private fixed byte comment [29];
        private fixed byte track [1];
        private fixed byte genre [1];

        #endregion

        #region Property Accessors

        public string Tag
        {
            get
            {
                fixed (byte* p = tag)
                {
                    return PointerUtils.ArrayToString(p, 3);
                }
            }
            set
            {
                fixed (byte* p = tag)
                {
                    PointerUtils.FillArray(p, value, 3);
                }
            }
        }

        public string Title
        {
            get
            {
                fixed (byte* p = title)
                {
                    return PointerUtils.ArrayToString(p, 30);
                }
            }
            set
            {
                fixed (byte* p = title)
                {
                    PointerUtils.FillArray(p, value, 30);
                }
            }
        }

        public string Artist
        {
            get
            {
                fixed (byte* p = artist)
                {
                    return PointerUtils.ArrayToString(p, 30);
                }
            }
            set
            {
                fixed (byte* p = artist)
                {
                    PointerUtils.FillArray(p, value, 30);
                }
            }
        }

        public string Album
        {
            get
            {
                fixed (byte* p = album)
                {
                    return PointerUtils.ArrayToString(p, 30);
                }
            }
            set
            {
                fixed (byte* p = album)
                {
                    PointerUtils.FillArray(p, value, 30);
                }
            }
        }

        public string Year
        {
            get
            {
                fixed (byte* p = year)
                {
                    return PointerUtils.ArrayToString(p, 4);
                }
            }
            set
            {
                fixed (byte* p = year)
                {
                    PointerUtils.FillArray(p, value, 4);
                }
            }
        }

        public string Comment
        {
            get
            {
                fixed (byte* p = comment)
                {
                    return PointerUtils.ArrayToString(p, 29);
                }
            }
            set
            {
                fixed (byte* p = comment)
                {
                    PointerUtils.FillArray(p, value, 29);
                }
            }
        }

        public int Track
        {
            get
            {
                fixed (byte* p = track)
                {
                    return p[0];
                }
            }
            set
            {
                if (value > byte.MaxValue || value < byte.MinValue)
                    throw new ArgumentOutOfRangeException("value", "The Track must be between " + byte.MinValue + " and " + byte.MaxValue + ".");

                fixed (byte* p = track)
                {
                    p[0] = (byte) value;
                }
            }
        }

        public int Genre
        {
            get
            {
                fixed (byte* p = genre)
                {
                    return p[0];
                }
            }
            set
            {
                if (value > 79 || value < 0)
                    throw new ArgumentOutOfRangeException("value", "The Track must be between 0 and 79.");

                fixed (byte* p = genre)
                {
                    p[0] = (byte) value;
                }
            }
        }

        #endregion

        #region Equality Operators

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An object to compare with this object.</param>
        public bool Equals(Id3V11Tag obj)
        {
            return Equals(obj.Tag, Tag) &&
                   Equals(obj.Title, Title) &&
                   Equals(obj.Artist, Artist) &&
                   Equals(obj.Album, Album) &&
                   Equals(obj.Year, Year) &&
                   Equals(obj.Comment, Comment) &&
                   Equals(obj.Track, Track) &&
                   Equals(obj.Genre, Genre);
        }

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Id3V11Tag))
                return false;

            return Equals((Id3V11Tag) obj);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = Tag.GetHashCode();
                result = (result * 397) ^ Title.GetHashCode();
                result = (result * 397) ^ Artist.GetHashCode();
                result = (result * 397) ^ Album.GetHashCode();
                result = (result * 397) ^ Year.GetHashCode();
                result = (result * 397) ^ Comment.GetHashCode();
                result = (result * 397) ^ Track.GetHashCode();
                result = (result * 397) ^ Genre.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(Id3V11Tag left, Id3V11Tag right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id3V11Tag left, Id3V11Tag right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Overrides of ValueType

        public override string ToString()
        {
            return Artist + " - " + Title;
        }

        #endregion
    }
}