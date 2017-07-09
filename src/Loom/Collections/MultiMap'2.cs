#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    public class MultiMap<TKey, TValue> : IDictionary<TKey, IList<TValue>>
    {
        private readonly Dictionary<TKey, IList<TValue>> map = new Dictionary<TKey, IList<TValue>>();

        #region IDictionary<TKey,IList<TValue>> Members

        public IList<TValue> this[TKey key]
        {
            get => map[key];
            set => map[key] = value;
        }

        public IEnumerator<KeyValuePair<TKey, IList<TValue>>> GetEnumerator()
        {
            return map.GetEnumerator();
        }

        public void Clear()
        {
            map.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return map.ContainsKey(key);
        }

        public void Add(TKey key, IList<TValue> values)
        {
            foreach (TValue value in values)
                AddSingleMap(key, value);
        }

        public bool Remove(TKey key)
        {
            return RemoveSingleMap(key);
        }

        public bool TryGetValue(TKey key, out IList<TValue> value)
        {
            return map.TryGetValue(key, out value);
        }

        public int Count => map.Count;

        public ICollection<TKey> Keys => map.Keys;

        public ICollection<IList<TValue>> Values => map.Values;

        void ICollection<KeyValuePair<TKey, IList<TValue>>>.Add(KeyValuePair<TKey, IList<TValue>> item)
        {
            foreach (TValue value in item.Value)
                AddSingleMap(item.Key, value);
        }

        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Remove(KeyValuePair<TKey, IList<TValue>> item)
        {
            return ((ICollection<KeyValuePair<TKey, IList<TValue>>>) map).Remove(item);
        }

        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Contains(KeyValuePair<TKey, IList<TValue>> item)
        {
            return ((ICollection<KeyValuePair<TKey, IList<TValue>>>) map).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, IList<TValue>>>.CopyTo(KeyValuePair<TKey, IList<TValue>>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, IList<TValue>>>) map).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.IsReadOnly => ((ICollection<KeyValuePair<TKey, IList<TValue>>>) map).IsReadOnly;

        #endregion

        public void Add(TKey key, TValue value)
        {
            AddSingleMap(key, value);
        }

        private void AddSingleMap(TKey key, TValue value)
        {
            if (map.ContainsKey(key))
            {
                IList<TValue> values = map[key];
                values.Add(value);
            }
            else
            {
                if (Equals(value, default(TValue)))
                {
                    map.Add(key, new List<TValue>());
                }
                else
                {
                    IList<TValue> values = new List<TValue>();
                    values.Add(value);

                    map.Add(key, values);
                }
            }
        }

        private bool RemoveSingleMap(TKey key)
        {
            if (map.ContainsKey(key))
                return map.Remove(key);

            throw new ArgumentOutOfRangeException("key", key, "The key does not exist in the map.");
        }
    }
}