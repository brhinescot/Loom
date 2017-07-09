#region Using Directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Web;

#endregion

namespace Loom.Web.Portal
{
    public interface IHttpCookieCollection
    {
        /// <summary>
        ///     Gets a string array containing all the keys (cookie names) in the cookie collection.
        /// </summary>
        /// <returns>
        ///     An array of cookie names.
        /// </returns>
        string[] AllKeys { get; }

        /// <summary>
        ///     Gets the number of key/value pairs contained in the
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </summary>
        /// <returns>
        ///     The number of key/value pairs contained in the
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </returns>
        int Count { get; }

        /// <summary>
        ///     Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that
        ///     contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains
        ///     all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </returns>
        NameObjectCollectionBase.KeysCollection Keys { get; }

        /// <summary>
        ///     Gets the cookie with the specified name from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name." />
        /// </returns>
        /// <param name="name">Name of cookie to retrieve. </param>
        HttpCookie this[string name] { get; }

        /// <summary>
        ///     Gets the cookie with the specified numerical index from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the cookie to retrieve from the collection. </param>
        HttpCookie this[int index] { get; }

        /// <summary>
        ///     Adds the specified cookie to the cookie collection.
        /// </summary>
        /// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> to add to the collection. </param>
        void Add(HttpCookie cookie);

        /// <summary>
        ///     Copies members of the cookie collection to an <see cref="T:System.Array" /> beginning at the specified index of the
        ///     array.
        /// </summary>
        /// <param name="dest">The destination <see cref="T:System.Array" />. </param>
        /// <param name="index">The index of the destination array where copying starts. </param>
        void CopyTo(Array dest, int index);

        /// <summary>
        ///     Updates the value of an existing cookie in a cookie collection.
        /// </summary>
        /// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> object to update. </param>
        void Set(HttpCookie cookie);

        /// <summary>
        ///     Removes the cookie with the specified name from the collection.
        /// </summary>
        /// <param name="name">The name of the cookie to remove from the collection. </param>
        void Remove(string name);

        /// <summary>
        ///     Clears all cookies from the cookie collection.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Returns the cookie with the specified name from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name" />.
        /// </returns>
        /// <param name="name">The name of the cookie to retrieve from the collection. </param>
        HttpCookie Get(string name);

        /// <summary>
        ///     Returns the <see cref="T:System.Web.HttpCookie" /> item with the specified index from the cookie collection.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the cookie to return from the collection. </param>
        HttpCookie Get(int index);

        /// <summary>
        ///     Returns the key (name) of the cookie at the specified numerical index.
        /// </summary>
        /// <returns>
        ///     The name of the cookie specified by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the key to retrieve from the collection. </param>
        string GetKey(int index);

        /// <summary>
        ///     Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to
        ///     serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </summary>
        /// <param name="info">
        ///     A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the
        ///     information required to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />
        ///     instance.
        /// </param>
        /// <param name="context">
        ///     A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source
        ///     and destination of the serialized stream associated with the
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="info" /> is null.</exception>
        void GetObjectData(SerializationInfo info, StreamingContext context);

        /// <summary>
        ///     Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization
        ///     event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">The source of the deserialization event.</param>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     The
        ///     <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is invalid.
        /// </exception>
        void OnDeserialization(object sender);

        /// <summary>
        ///     Returns an enumerator that iterates through the
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> for the
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </returns>
        IEnumerator GetEnumerator();
    }
}