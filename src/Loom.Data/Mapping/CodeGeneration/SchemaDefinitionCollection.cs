using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Loom.Collections;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Schema;

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable, DebuggerDisplay("Count={Count}")]
    public abstract class SchemaDefinitionCollection<T> : IWriteEnumerable<T> where T : ISchema
    {
        #region Abstract Members

        protected abstract IEnumerable<ISchema> GetExcludedItems();
        protected abstract IEnumerable<string> GetSchemaExcludes();
        protected abstract IEnumerable<string> GetPrefixExcludes();
        protected abstract IEnumerable<string> GetSuffixExcludes();
        protected abstract IEnumerable<string> GetSchemaIncludes();
        protected abstract IEnumerable<string> GetPrefixIncludes();
        protected abstract IEnumerable<string> GetSuffixIncludes();
        protected abstract bool ExplicitInclude { get; }

        #endregion

        #region Member Fields

        private readonly ActiveMapCodeGenConfigurationSection configuration;
        private readonly Dictionary<string, T> innerList = new Dictionary<string, T>();

        #endregion

        #region Property Accessors

        /// <summary>
        /// Gets the number of items in this list.
        /// </summary>
        public int Count
        {
            get { return innerList.Count; }
        }

        protected ActiveMapCodeGenConfigurationSection Configuration
        {
            get { return configuration; }
        }

        protected Dictionary<string, T> InnerList
        {
            get { return innerList; }
        }

        #endregion

        protected SchemaDefinitionCollection(ActiveMapCodeGenConfigurationSection configuration)
        {
            this.configuration = configuration;
        }

        public virtual bool Add(T item)
        {
            Argument.Assert.IsNotNull(item, "item");

            if (IsGlobalExclude(item))
                return false;
            if (IsGlobalInclude(item))
            {
                AddTableToList(item);
                return true;
            }

            foreach (ISchema schema in GetExcludedItems())
                if (schema.Name == item.Name && schema.Owner == item.Owner)
                    return false;

            if (!ExplicitInclude)
            {
                AddTableToList(item);
                return true;
            }
            return false;
        }

        private bool IsGlobalExclude(ISchema table)
        {
            foreach (string schema in GetSchemaExcludes())
                if (Compare.AreSameOrdinalIgnoreCase(schema, table.Owner))
                    return true;

            foreach (string prefix in GetPrefixExcludes())
                if (table.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return true;

            foreach (string suffix in GetSuffixExcludes())
                if (table.Name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        private bool IsGlobalInclude(ISchema table)
        {
            foreach (string schema in GetSchemaIncludes())
                if (Compare.AreSameOrdinalIgnoreCase(schema, table.Owner))
                    return true;

            foreach (string prefix in GetPrefixIncludes())
                if (table.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return true;

            foreach (string suffix in GetSuffixIncludes())
                if (table.Name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        private void AddTableToList(T item)
        {
            string key = item.Owner + item.Name;
            if (innerList.ContainsKey(key))
                innerList[key] = item;
            else
                innerList.Add(key, item);
        }

        #region IEnumerable<T> Implementation

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="IEnumerator{T}"></see> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        #endregion
    }
}
