#region Using Directives

using System;
using System.Collections;
using System.Resources;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     Required simple IResourceReader implementation. A ResourceReader
    ///     is little more than an Enumeration interface that allows
    ///     parsing through the Resources in a Resource Set which
    ///     is passed in the constructor.
    /// </summary>
    internal sealed class DbSimpleResourceReader : IResourceReader
    {
        private readonly IDictionary resources;

        public DbSimpleResourceReader(IDictionary resources)
        {
            this.resources = resources;
        }

        #region IResourceReader Members

        IDictionaryEnumerator IResourceReader.GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        void IResourceReader.Close() { }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        void IDisposable.Dispose() { }

        #endregion
    }
}