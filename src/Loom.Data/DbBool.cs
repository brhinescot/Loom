#region Using Directives

using System;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

#endregion

namespace Loom.Data
{
    /// <summary>Represents Null, False, and True</summary>
    [Serializable]
    [StructLayout(LayoutKind.Auto)]
    public struct DBBool : IEquatable<DBBool>, INullable
    {
        /// <summary>
        ///     A DbBool containing 'Null'.
        /// </summary>
        /// <remarks>One of three possible DBBool values.</remarks>
        public static readonly DBBool Null = new DBBool(0);

        /// <summary>A DBBool containing 'False'.</summary>
        /// <remarks>One of three possible DBBool values.</remarks>
        public static readonly DBBool False = new DBBool(-1);

        /// <summary>A DBBool containing 'True'.</summary>
        /// <remarks>One of three possible DBBool values.</remarks>
        public static readonly DBBool True = new DBBool(1);

        /// <summary>
        ///     Private field that stores –1, 0, 1 for False, Null,
        ///     True.
        /// </summary>
        private readonly sbyte value;

        /// <summary>
        ///     Private instance constructor. The value parameter must
        ///     be –1, 0, or 1.
        /// </summary>
        private DBBool(int value)
        {
            this.value = (sbyte) value;
        }

        public bool Value => value > 0;

        public bool HasValue => value != 0;

        #region INullable Members

        /// <summary>
        ///     Indicates whether a structure is null. This property is read-only.
        /// </summary>
        /// <returns>
        ///     <see cref="T:System.Data.SqlTypes.SqlBoolean"></see>true if the value of this object is null. Otherwise, false.
        /// </returns>
        public bool IsNull => HasValue;

        #endregion

        /// <summary>
        ///     Implicit conversion from bool to DBBool. Maps true to
        ///     DBBool.True and false to DBBool.False.
        /// </summary>
        /// <param name="x">a DBBool</param>
        public static implicit operator DBBool(bool x)
        {
            return x ? True : False;
        }

        /// <summary>Explicit conversion from DBBool to bool.</summary>
        /// <exception cref="InvalidOperationException">
        ///     The given DBBool is
        ///     Null
        /// </exception>
        /// <param name="x">a DBBool</param>
        /// <returns>true or false</returns>
        public static explicit operator bool(DBBool x)
        {
            if (x.value == 0)
                throw new InvalidCastException("Can not convert DBBool.Null to type bool.");

            return x.value > 0;
        }

        #region Operator Overloads

        /// <summary>Equality operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <param name="y">a DBBool</param>
        /// <returns>
        ///     Returns Null if either operand is Null, otherwise
        ///     returns True or False.
        /// </returns>
        public static DBBool operator ==(DBBool x, DBBool y)
        {
            if (x.value == 0 || y.value == 0) return Null;
            return x.value == y.value ? True : False;
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <param name="y">a DBBool</param>
        /// <returns>
        ///     Returns Null if either operand is Null, otherwise
        ///     returns True or False.
        /// </returns>
        public static DBBool operator !=(DBBool x, DBBool y)
        {
            if (x.value == 0 || y.value == 0) return Null;
            return x.value != y.value ? True : False;
        }

        /// <summary>Logical negation operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <returns>
        ///     Returns True if the operand is False, Null if the
        ///     operand is Null, or False if the operand is True.
        /// </returns>
        public static DBBool operator !(DBBool x)
        {
            return LogicalNot(x);
        }

        /// <summary>Logical negation operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <returns>
        ///     Returns True if the operand is False, Null if the
        ///     operand is Null, or False if the operand is True.
        /// </returns>
        public static DBBool LogicalNot(DBBool x)
        {
            return new DBBool(-x.value);
        }

        /// <summary>Logical AND operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <param name="y">a DBBool</param>
        /// <returns>
        ///     Returns False if either operand is False, otherwise
        ///     Null if either operand is Null, otherwise True.
        /// </returns>
        public static DBBool operator &(DBBool x, DBBool y)
        {
            return BitwiseAnd(x, y);
        }

        /// <summary>Logical AND operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <param name="y">a DBBool</param>
        /// <returns>
        ///     Returns False if either operand is False, otherwise
        ///     Null if either operand is Null, otherwise True.
        /// </returns>
        public static DBBool BitwiseAnd(DBBool x, DBBool y)
        {
            return new DBBool(x.value < y.value ? x.value : y.value);
        }

        /// <summary>Logical OR operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <param name="y">a DBBool</param>
        /// <returns>
        ///     Returns True if either operand is True, otherwise
        ///     Null if either operand is Null, otherwise False.
        /// </returns>
        public static DBBool operator |(DBBool x, DBBool y)
        {
            return BitwiseOr(x, y);
        }

        /// <summary>Logical OR operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <param name="y">a DBBool</param>
        /// <returns>
        ///     Returns True if either operand is True, otherwise
        ///     Null if either operand is Null, otherwise False.
        /// </returns>
        public static DBBool BitwiseOr(DBBool x, DBBool y)
        {
            return new DBBool(x.value > y.value ? x.value : y.value);
        }

        /// <summary>Definitely true operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <returns>Returns true if the operand is True, false otherwise.</returns>
        public static bool operator true(DBBool x)
        {
            return x.value > 0;
        }

        /// <summary>Definitely false operator.</summary>
        /// <param name="x">a DBBool</param>
        /// <returns>Returns true if the operand is False, false otherwise.</returns>
        public static bool operator false(DBBool x)
        {
            return x.value < 0;
        }

        #endregion

        /// <summary>
        ///     Returns a string representation of the current
        ///     Object.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Object has not been
        ///     initialized.
        /// </exception>
        /// <returns>
        ///     A string containing DBBool.False, DBBool.Null, or
        ///     DBBool.True
        /// </returns>
        public override string ToString()
        {
            switch (value)
            {
                case -1:
                    return "DBBool.False";
                case 0:
                    return "DBBool.Null";
                case 1:
                    return "DBBool.True";
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>Determines whether two <see cref="DBBool" /> instances are equal.</summary>
        /// <param name="dbBool">The <see cref="DBBool" /> to check.</param>
        /// <returns>True if the two DBBools are equal.</returns>
        public bool Equals(DBBool dbBool)
        {
            return value == dbBool.value;
        }

        /// <summary>Determines whether two <see cref="DBBool" /> instances are equal.</summary>
        /// <param name="obj">The <see cref="object" /> to check.</param>
        /// <returns>True if the two DBBools are equal.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DBBool))
                return false;

            return Equals((DBBool) obj);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type, suitable
        ///     for use in hashing algorithms and data structures like a
        ///     hash table.
        /// </summary>
        /// <returns>A hash code for the current DBBool.</returns>
        public override int GetHashCode()
        {
            return value;
        }
    }
}