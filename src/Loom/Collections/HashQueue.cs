#region Using Directives

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Loom.Collections
{
    public class HashQueue<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public IEqualityComparer<TKey> Comparer => dictionary.Comparer;

        public bool IsSynchronized => ((ICollection) dictionary).IsSynchronized;

        public Dictionary<TKey, TValue>.KeyCollection Keys => dictionary.Keys;

        public Dictionary<TKey, TValue>.ValueCollection Values => dictionary.Values;

        #region IDictionary<TKey,TValue> Members

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable) dictionary).GetEnumerator();
        }

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) dictionary).Add(item);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) dictionary).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) dictionary).Remove(item);
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values => dictionary.Values;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => dictionary.Keys;

        public bool IsReadOnly => false;

        public int Count => dictionary.Count;

        #endregion

        public bool ContainsValue(TValue value)
        {
            return dictionary.ContainsValue(value);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            dictionary.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            dictionary.OnDeserialization(sender);
        }

//        private Queue<T> queue = new Queue<T>();
    }
}