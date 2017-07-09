#region Using Directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

#endregion

namespace Loom.Web.Portal.Http
{
    public class WrappedHttpCookieCollection : NameObjectCollectionBase, IHttpCookieCollection, ICollection
    {
        private readonly HttpCookieCollection collection;

        public WrappedHttpCookieCollection(HttpCookieCollection collection)
        {
            this.collection = collection;
        }

        #region ICollection Members

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <returns>
        ///     An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        object ICollection.SyncRoot => ((ICollection) collection).SyncRoot;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized
        ///     (thread safe).
        /// </summary>
        /// <returns>
        ///     true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise,
        ///     false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        bool ICollection.IsSynchronized => ((ICollection) collection).IsSynchronized;

        #endregion

        #region IHttpCookieCollection Members

        /// <summary>
        ///     Adds the specified cookie to the cookie collection.
        /// </summary>
        /// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> to add to the collection. </param>
        public void Add(HttpCookie cookie)
        {
            collection.Add(cookie);
        }

        /// <summary>
        ///     Copies members of the cookie collection to an <see cref="T:System.Array" /> beginning at the specified index of the
        ///     array.
        /// </summary>
        /// <param name="dest">The destination <see cref="T:System.Array" />. </param>
        /// <param name="index">The index of the destination array where copying starts. </param>
        public void CopyTo(Array dest, int index)
        {
            collection.CopyTo(dest, index);
        }

        /// <summary>
        ///     Updates the value of an existing cookie in a cookie collection.
        /// </summary>
        /// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> object to update. </param>
        public void Set(HttpCookie cookie)
        {
            collection.Set(cookie);
        }

        /// <summary>
        ///     Removes the cookie with the specified name from the collection.
        /// </summary>
        /// <param name="name">The name of the cookie to remove from the collection. </param>
        public void Remove(string name)
        {
            collection.Remove(name);
        }

        /// <summary>
        ///     Clears all cookies from the cookie collection.
        /// </summary>
        public void Clear()
        {
            collection.Clear();
        }

        /// <summary>
        ///     Returns the cookie with the specified name from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name" />.
        /// </returns>
        /// <param name="name">The name of the cookie to retrieve from the collection. </param>
        public HttpCookie Get(string name)
        {
            return collection.Get(name);
        }

        /// <summary>
        ///     Returns the <see cref="T:System.Web.HttpCookie" /> item with the specified index from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the cookie to return from the collection. </param>
        public HttpCookie Get(int index)
        {
            return collection.Get(index);
        }

        /// <summary>
        ///     Returns the key (name) of the cookie at the specified numerical index.
        /// </summary>
        /// <returns>
        ///     The name of the cookie specified by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the key to retrieve from the collection. </param>
        public string GetKey(int index)
        {
            return collection.GetKey(index);
        }

        /// <summary>
        ///     Gets the cookie with the specified name from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name." />
        /// </returns>
        /// <param name="name">Name of cookie to retrieve. </param>
        public HttpCookie this[string name] => collection[name];

        /// <summary>
        ///     Gets the cookie with the specified numerical index from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the cookie to retrieve from the collection. </param>
        public HttpCookie this[int index] => collection[index];

        /// <summary>
        ///     Gets a string array containing all the keys (cookie names) in the cookie collection.
        /// </summary>
        /// <returns>
        ///     An array of cookie names.
        /// </returns>
        public string[] AllKeys => collection.AllKeys;

        #endregion
    }
}