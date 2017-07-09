#region Using Directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

#endregion

namespace Loom.Web.Portal
{
    public interface IHttpSessionState
    {
        string SessionID { get; }
        int Timeout { get; set; }
        bool IsNewSession { get; }
        SessionStateMode Mode { get; }
        bool IsCookieless { get; }
        HttpCookieMode CookieMode { get; }
        int LCID { get; set; }
        int CodePage { get; set; }
        HttpSessionState Contents { get; }
        HttpStaticObjectsCollection StaticObjects { get; }
        int Count { get; }
        NameObjectCollectionBase.KeysCollection Keys { get; }
        object SyncRoot { get; }
        bool IsReadOnly { get; }
        bool IsSynchronized { get; }
        object this[string name] { get; set; }
        object this[int index] { get; set; }
        void Abandon();
        void Add(string name, object value);
        void Remove(string name);
        void RemoveAt(int index);
        void Clear();
        void RemoveAll();
        IEnumerator GetEnumerator();
        void CopyTo(Array array, int index);
    }
}