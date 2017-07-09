#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.Routing
{
    public sealed class RouteTokens : ICollection
    {
        private readonly Dictionary<string, List<string>> tokens = new Dictionary<string, List<string>>();

        public string this[string key] => tokens.ContainsKey(key) ? string.Join("/", tokens[key].ToArray()) : null;

        #region ICollection Members

        public int Count => tokens.Count;

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection) tokens).CopyTo(array, index);
        }

        object ICollection.SyncRoot => ((ICollection) tokens).SyncRoot;

        bool ICollection.IsSynchronized => ((ICollection) tokens).IsSynchronized;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection) tokens).GetEnumerator();
        }

        #endregion

        public void Add(string key, string value)
        {
            if (tokens.ContainsKey(key))
                tokens[key].Add(value);
            else
                tokens.Add(key, new List<string> {value});
        }

        public void Merge(RouteTokens newTokens)
        {
            foreach (KeyValuePair<string, List<string>> t in newTokens.tokens)
                tokens.Add(t.Key, t.Value);
        }

        internal bool Contains(string key)
        {
            return tokens.ContainsKey(key);
        }

        public bool IsMultiToken(string key)
        {
            return tokens.ContainsKey(key) && tokens[key].Count > 1;
        }

        [NotNull]
        public string[] GetMultiToken(string key)
        {
            return tokens.ContainsKey(key) ? tokens[key].ToArray() : new string[0];
        }

        internal IDictionary ToDictonary()
        {
            return tokens;
        }
    }
}