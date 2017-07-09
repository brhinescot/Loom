#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Media.Meta
{
    public class MetaAttributeCollection : ICollection<MetaAttribute>
    {
        private readonly Dictionary<string, MetaAttribute> inner = new Dictionary<string, MetaAttribute>(StringComparer.CurrentCultureIgnoreCase);

        public string this[string name]
        {
            get
            {
                if (inner.ContainsKey(name))
                    return inner[name].Value;
                return null;
            }
        }

        #region ICollection<MetaAttribute> Members

        public IEnumerator<MetaAttribute> GetEnumerator()
        {
            return inner.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(MetaAttribute item)
        {
            inner.Add(item.Name, item);
        }

        public void Clear()
        {
            inner.Clear();
        }

        public bool Contains(MetaAttribute item)
        {
            return inner.ContainsKey(item.Name);
        }

        public void CopyTo(MetaAttribute[] array, int arrayIndex)
        {
            inner.Values.CopyTo(array, arrayIndex);
        }

        public bool Remove(MetaAttribute item)
        {
            return inner.Remove(item.Name);
        }

        public int Count => inner.Count;

        public bool IsReadOnly => false;

        #endregion

        public string GetValue(string name)
        {
            return this[name];
        }
    }
}