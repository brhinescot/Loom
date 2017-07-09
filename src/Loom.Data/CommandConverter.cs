#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using Loom.Collections;
using Loom.Data.Entities;

#endregion

namespace Loom.Data
{
    public class CommandConverter
    {
        private readonly ICommandFactory commandFactory;

        public CommandConverter(ICommandFactory factory)
        {
            commandFactory = factory;
        }

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

        public Dictionary<TKey, TEntity> ToDictionary<TKey, TEntity>(string keyColumn) where TEntity : new()
        {
            Dictionary<TKey, TEntity> dictionary = new Dictionary<TKey, TEntity>();
            FillDictionary(dictionary, null, keyColumn);
            return dictionary;
        }

        public SortedDictionary<TKey, TEntity> ToSortedDictionary<TKey, TEntity>(string keyColumn) where TEntity : new()
        {
            SortedDictionary<TKey, TEntity> dictionary = new SortedDictionary<TKey, TEntity>();
            FillDictionary(dictionary, null, keyColumn);
            return dictionary;
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

        private void FillCollection<T>(ICollection<T> collection, PropertyMappings mappings) where T : new()
        {
            using (DbCommand command = commandFactory.CreateCommand())
            {
                IEntityAdapter adapter = new EntityAdapter(command) {MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore};

                if (mappings != null && mappings.Count > 0)
                    adapter.Options.Mappings.Add(mappings);
                adapter.FillCollection(collection);
            }
        }

        private void FillDictionary<TKey, TEntity>(IDictionary<TKey, TEntity> dictionary, PropertyMappings mappings, string keyField) where TEntity : new()
        {
            using (DbCommand command = commandFactory.CreateCommand())
            {
                IEntityAdapter adapter = new EntityAdapter(command) {MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore};

                if (mappings != null && mappings.Count > 0)
                    adapter.Options.Mappings.Add(mappings);
                adapter.FillDictionary(dictionary, keyField);
            }
        }
    }
}