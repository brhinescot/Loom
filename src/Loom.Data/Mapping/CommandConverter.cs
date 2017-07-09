#region File Header

//  *************************************************************************
//  Copyright © 2008 Colossus Interactive, LLC
//  All Rights Reserved
//   
//  Unauthorized reproduction or distribution in source or compiled
//  form is strictly prohibited.
//   
//  http://www.colossusinteractive.com
//  licensing@colossusinteractive.com
//   
//  *************************************************************************

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using Loom.Collections;
using Loom.Data.Entities;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public class CommandConverter
    {
        #region Member Fields

        private readonly LiteralQuery commandBase;

        #endregion

        #region .ctors

        internal CommandConverter(LiteralQuery commandBase)
        {
            this.commandBase = commandBase;
        }

        #endregion

        public List<T> ToList<T>() where T : new()
        {
            return ToList<T>(null);
        }

        public List<T> ToList<T>(PropertyMappings mappings) where T : new()
        {
            List<T> collection = new List<T>();
            FillCollection(collection, mappings);
            return collection;
        }

        public Collection<T> ToCollection<T>() where T : new()
        {
            return ToCollection<T>(null);
        }

        public Collection<T> ToCollection<T>(PropertyMappings mappings) where T : new()
        {
            Collection<T> collection = new Collection<T>();
            FillCollection(collection, mappings);
            return collection;
        }

        public Dictionary<TKey, TEntity> ToDictionary<TKey, TEntity>(IQueryableColumn keyColumn) where TEntity : new()
        {
            return ToDictionary<TKey, TEntity>(keyColumn.Name);
        }

        public Dictionary<TKey, TEntity> ToDictionary<TKey, TEntity>(string keyColumn) where TEntity : new()
        {
            Dictionary<TKey, TEntity> dictionary = new Dictionary<TKey, TEntity>();
            FillDictionary(dictionary, null, keyColumn);
            return dictionary;
        }
        
        public SortedDictionary<TKey, TEntity> ToSortedDictionary<TKey, TEntity>(IQueryableColumn keyColumn) where TEntity : new()
        {
            return ToSortedDictionary<TKey, TEntity>(keyColumn.Name);
        }

        public SortedDictionary<TKey, TEntity> ToSortedDictionary<TKey, TEntity>(string keyColumn) where TEntity : new()
        {
            SortedDictionary<TKey, TEntity> dictionary = new SortedDictionary<TKey, TEntity>();
            FillDictionary(dictionary, null, keyColumn);
            return dictionary;
        }

        public OrderedDictionary<TKey, TEntity> ToOrderedDictionary<TKey, TEntity>(IQueryableColumn keyColumn) where TEntity : new()
        {
            return ToOrderedDictionary<TKey, TEntity>(keyColumn.Name);
        }

        public OrderedDictionary<TKey, TEntity> ToOrderedDictionary<TKey, TEntity>(string keyColumn) where TEntity : new()
        {
            OrderedDictionary<TKey, TEntity> dictionary = new OrderedDictionary<TKey, TEntity>();
            FillDictionary(dictionary, null, keyColumn);
            return dictionary;
        }

        public T[] ToArray<T>() where T : new()
        {
            return ToList<T>().ToArray();
        }

        #region Private Methods

        private void FillCollection<T>(ICollection<T> collection, PropertyMappings mappings) where T : new()
        {
            DbCommand command = commandBase.CreateCommand();
            commandBase.Session.EnlistConnection(command);

            IEntityAdapter adapter = new EntityAdapter(command) {MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore};

            if (mappings != null && mappings.Count > 0)
                adapter.Options.Mappings.Add(mappings);
            adapter.FillCollection(collection);
        }

        private void FillDictionary<TKey, TEntity>(IDictionary<TKey, TEntity> dictionary, PropertyMappings mappings, string keyField) where TEntity : new()
        {
            DbCommand command = commandBase.CreateCommand();
            commandBase.Session.EnlistConnection(command);

            IEntityAdapter adapter = new EntityAdapter(command) { MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore };

            if (mappings != null && mappings.Count > 0)
                adapter.Options.Mappings.Add(mappings);
            adapter.FillDictionary(dictionary, keyField);
        }

        #endregion
    }
}