#region Using Directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

#endregion

namespace Loom.Web.Portal.Http
{
    public sealed class WrappedHttpSessionState : IHttpSessionState
    {
        private readonly HttpSessionState sessionState;

        public WrappedHttpSessionState(HttpSessionState sessionState)
        {
            this.sessionState = sessionState;
        }

        #region IHttpSessionState Members

        public void Abandon()
        {
            sessionState.Abandon();
        }

        public void Add(string name, object value)
        {
            sessionState.Add(name, value);
        }

        public void Remove(string name)
        {
            sessionState.Remove(name);
        }

        public void RemoveAt(int index)
        {
            sessionState.RemoveAt(index);
        }

        public void Clear()
        {
            sessionState.Clear();
        }

        public void RemoveAll()
        {
            sessionState.RemoveAll();
        }

        public IEnumerator GetEnumerator()
        {
            return sessionState.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            sessionState.CopyTo(array, index);
        }

        public string SessionID => sessionState.SessionID;

        public int Timeout
        {
            get => sessionState.Timeout;
            set => sessionState.Timeout = value;
        }

        public bool IsNewSession => sessionState.IsNewSession;

        public SessionStateMode Mode => sessionState.Mode;

        public bool IsCookieless => sessionState.IsCookieless;

        public HttpCookieMode CookieMode => sessionState.CookieMode;

        public int LCID
        {
            get => sessionState.LCID;
            set => sessionState.LCID = value;
        }

        public int CodePage
        {
            get => sessionState.CodePage;
            set => sessionState.CodePage = value;
        }

        public HttpSessionState Contents => sessionState.Contents;

        public HttpStaticObjectsCollection StaticObjects => sessionState.StaticObjects;

        public object this[string name]
        {
            get => sessionState[name];
            set => sessionState[name] = value;
        }

        public object this[int index]
        {
            get => sessionState[index];
            set => sessionState[index] = value;
        }

        public int Count => sessionState.Count;

        public NameObjectCollectionBase.KeysCollection Keys => sessionState.Keys;

        public object SyncRoot => sessionState.SyncRoot;

        public bool IsReadOnly => sessionState.IsReadOnly;

        public bool IsSynchronized => sessionState.IsSynchronized;

        #endregion
    }
}