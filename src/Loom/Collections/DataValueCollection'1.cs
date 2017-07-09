#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

#endregion

namespace Loom.Collections
{
    //TODO [Brian,20140606] This class is not used, only inherited.
    [Serializable]
    public class DataValueCollection<T> : IEnumerable<KeyValuePair<T, object>>
    {
        private readonly Dictionary<T, object> lookup = new Dictionary<T, object>();
        private Stack<Dictionary<T, object>> history;

        /// <summary>
        ///     Gets the number of items in the collection.
        /// </summary>
        /// <value>The count.</value>
        public int Count => lookup.Count;

        internal int HistorySteps => history == null ? 0 : history.Count;

        #region IEnumerable<KeyValuePair<T,object>> Members

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Collections.Generic.IEnumerator{T}"></see> that can be used to
        ///     iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public IEnumerator<KeyValuePair<T, object>> GetEnumerator()
        {
            return lookup.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to
        ///     iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return lookup.GetEnumerator();
        }

        #endregion

        protected static void ValidateKey(T key)
        {
            Argument.Assert.IsNotNull(key, nameof(key));
        }

        public bool IsSet(T key)
        {
            ValidateKey(key);
            return lookup.ContainsKey(key);
        }

        public void Set(T key, object value)
        {
            ValidateKey(key);

            if (Convert.IsDBNull(value))
                value = null;

            if (lookup.ContainsKey(key))
                lookup[key] = value;
            else
                lookup.Add(key, value);

            OnValueSet(key, value);
        }

        public bool Remove(T key)
        {
            ValidateKey(key);
            return lookup.Remove(key);
        }

        public void SetBooleanAsChar(T key, object value)
        {
            ValidateKey(key);

            if (value == null || Convert.IsDBNull(value))
            {
                Set(key, null);
                return;
            }

            char charValue = Convert.ToBoolean(value) ? 'Y' : 'N';
            if (lookup.ContainsKey(key))
                lookup[key] = charValue;
            else
                lookup.Add(key, charValue);
        }

        protected object Retrieve(T key)
        {
            ValidateKey(key);
            object value;
            return !lookup.TryGetValue(key, out value) ? null : value;
        }

        protected virtual void OnValueSet(T key, object value) { }

        public string RetrieveString(T key)
        {
            object result = Retrieve(key);
            return result == null ? null : Convert.ToString(result);
        }

        public char RetrieveAnsiStringFixedLength(T key)
        {
            return Convert.ToChar(Retrieve(key));
        }

        public int RetrieveInt32(T key)
        {
            return Convert.ToInt32(Retrieve(key));
        }

        public long RetrieveInt64(T key)
        {
            return Convert.ToInt64(Retrieve(key));
        }

        public short RetrieveInt16(T key)
        {
            return Convert.ToInt16(Retrieve(key));
        }

        public decimal RetrieveDecimal(T key)
        {
            return Convert.ToDecimal(Retrieve(key));
        }

        public decimal RetrieveCurrency(T key)
        {
            return Convert.ToDecimal(Retrieve(key));
        }

        public float RetrieveSingle(T key)
        {
            return Convert.ToSingle(Retrieve(key));
        }

        public byte RetrieveByte(T key)
        {
            return Convert.ToByte(Retrieve(key));
        }

        public double RetrieveDouble(T key)
        {
            return Convert.ToDouble(Retrieve(key));
        }

        public bool RetrieveBoolean(T key)
        {
            object result = Retrieve(key);
            return result != null && Convert.ToBoolean(result);
        }

        public bool RetrieveCharAsBoolean(T key)
        {
            object result = Retrieve(key);
            return result != null && (char) result == 'Y';
        }

        public TimeSpan RetrieveTimeSpan(T key)
        {
            object result = Retrieve(key);
            if (result == null)
                return TimeSpan.Zero;

            TimeSpan output;
            return !TimeSpan.TryParse(result.ToString(), out output) ? TimeSpan.Zero : output;
        }

        public DateTime RetrieveDateTime(T key)
        {
            object result = Retrieve(key);
            if (result == null)
                return DateTime.MinValue;

            DateTime output;
            return !DateTime.TryParse(result.ToString(), out output) ? DateTime.MinValue : output;
        }

        public byte[] RetrieveBinary(T key)
        {
            object result = Retrieve(key);
            return result == null ? new byte[0] : (byte[]) result;
        }

        public Guid RetrieveGuid(T key)
        {
            object result = Retrieve(key);
            return result == null ? Guid.Empty : new Guid(result.ToString());
        }

        public int? RetrieveNullableInt32(T key)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToInt32(obj);
        }

        public long? RetrieveNullableInt64(T key)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToInt64(obj);
        }

        public short? RetrieveNullableInt16(T key)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToInt16(obj);
        }

        public decimal? RetrieveNullableDecimal(T key)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToDecimal(obj);
        }

        public decimal? RetrieveNullableCurrency(T key)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToDecimal(obj);
        }

        public float? RetrieveNullableSingle(T key)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToSingle(obj);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public byte? RetrieveNullableByte(T key, CultureInfo cultureInfo = null)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToByte(obj, cultureInfo ?? CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public double? RetrieveNullableDouble(T key, CultureInfo cultureInfo = null)
        {
            object obj = Retrieve(key);
            if (obj == null)
                return null;

            return Convert.ToDouble(obj, cultureInfo ?? CultureInfo.InvariantCulture);
        }

        public bool? RetrieveNullableBoolean(T key, CultureInfo cultureInfo = null)
        {
            object result = Retrieve(key);
            if (result == null)
                return null;

            return Convert.ToBoolean(result, cultureInfo ?? CultureInfo.InvariantCulture);
        }

        public DateTime? RetrieveNullableDateTime(T key)
        {
            object result = Retrieve(key);
            if (result == null)
                return null;

            DateTime output;
            return !DateTime.TryParse(result.ToString(), out output) ? DateTime.MinValue : output;
        }

        public TimeSpan? RetrieveNullableTimeSpan(T key)
        {
            object result = Retrieve(key);
            if (result == null)
                return null;

            TimeSpan output;
            return !TimeSpan.TryParse(result.ToString(), out output) ? TimeSpan.Zero : output;
        }

        public Guid? RetrieveNullableGuid(T key)
        {
            object result = Retrieve(key);
            if (result == null)
                return null;

            return new Guid(result.ToString());
        }

        internal void ClearHistory()
        {
            if (history == null)
                return;

            history.Clear();
            history = null;
        }

        internal int Version()
        {
            if (history == null)
                history = new Stack<Dictionary<T, object>>();

            Dictionary<T, object> version = lookup.ToDictionary(pair => pair.Key, pair => pair.Value);

            history.Push(version);
            return history.Count;
        }

        internal bool Undo(int steps)
        {
            if (history == null)
                return false;

            if (history.Count < steps || steps < 0)
                return false;

            for (int i = 0; i < steps - 1; i++)
                history.Pop();

            Dictionary<T, object> version = history.Pop();
            lookup.Clear();
            foreach (KeyValuePair<T, object> pair in version)
                lookup.Add(pair.Key, pair.Value);

            return true;
        }
    }
}