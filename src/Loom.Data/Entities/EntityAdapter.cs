#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Entities
{
    public class EntityAdapter : IEntityAdapter
    {
        private const string ClassName = "EntityAdapter";

        private static readonly int IgnoreCaseFlag = BitVector32.CreateMask();
        private static readonly int IsDisposedFlag = BitVector32.CreateMask(IgnoreCaseFlag);

        private CommandBehavior commandBehavior;
        private IDbCommand deleteCommand;
        [NonSerialized] private BitVector32 flags = new BitVector32(0);
        private IDbCommand insertCommand;
        private MissingPropertyMappingAction missingPropertyMappingAction;

        private PropertyMappingOptions options;
        private IDbCommand selectCommand;
        private IDbCommand updateCommand;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityAdapter" />
        ///     class with
        ///     the specified <see cref="IDbCommand" />
        ///     as the <see cref="SelectCommand" />
        ///     property.
        /// </summary>
        /// <param name="selectCommand">
        ///     An <see cref="IDbCommand" />
        ///     that is a SQL
        ///     SELECT statement or stored procedure and is set as the <see cref="SelectCommand" />
        ///     property of the <see cref="EntityAdapter" />
        ///     .
        /// </param>
        public EntityAdapter(IDbCommand selectCommand = null)
        {
            this.selectCommand = selectCommand;
        }

        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        protected internal CommandBehavior CommandBehavior
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return commandBehavior | CommandBehavior.SequentialAccess;
            }
            set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                commandBehavior = value | CommandBehavior.SequentialAccess;
            }
        }

        /// <summary>
        ///     Gets a SQL statement or stored procedure used to select records in the data source.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public IDbCommand DeleteCommand
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return deleteCommand;
            }
            protected set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                deleteCommand = value;
            }
        }

        /// <summary>
        ///     Gets a SQL statement or stored procedure used to select records in the data source.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public IDbCommand InsertCommand
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return insertCommand;
            }
            protected set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                insertCommand = value;
            }
        }

        /// <summary>
        ///     Gets a SQL statement or stored procedure used to select records in the data source.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public IDbCommand UpdateCommand
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return updateCommand;
            }
            protected set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                updateCommand = value;
            }
        }

        #region IEntityAdapter Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>
        ///     2
        /// </filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public void FillCollection<T>(ICollection<T> collection) where T : new()
        {
            if (flags[IsDisposedFlag])
                throw new ObjectDisposedException(ClassName);

            Argument.Assert.IsNotNull(collection, nameof(collection));

            PrepareCommand(selectCommand);
            using (IDataReader reader = selectCommand.ExecuteReader(commandBehavior))
            {
                FillCollectionPrivate(reader, collection);
            }
        }

        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public void Fill<T>(T entity)
        {
            if (flags[IsDisposedFlag])
                throw new ObjectDisposedException(ClassName);

            Argument.Assert.IsNotNull(entity, nameof(entity));

            PrepareCommand(selectCommand);
            using (IDataReader reader = selectCommand.ExecuteReader(commandBehavior))
            {
                FillEntityPrivate(reader, entity);
            }
        }

        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public void FillDictionary<TKey, TEntity>(IDictionary<TKey, TEntity> dictionary, string keyField) where TEntity : new()
        {
            if (flags[IsDisposedFlag])
                throw new ObjectDisposedException(ClassName);

            Argument.Assert.IsNotNull(dictionary, nameof(dictionary));

            PrepareCommand(selectCommand);
            using (IDataReader reader = selectCommand.ExecuteReader(commandBehavior))
            {
                FillDictionaryPrivate(reader, dictionary, keyField);
            }
        }

        /// <summary>
        ///     Gets a collection that provides the master mapping between a data source record and an entity.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public PropertyMappingOptions Options
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return options ?? (options = new PropertyMappingOptions());
            }
        }

        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public bool IgnoreCase
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return flags[IgnoreCaseFlag];
            }
            set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                flags[IgnoreCaseFlag] = value;
            }
        }

        /// <summary>
        ///     Determines the action to take when incoming data does not have a matching property or field.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public MissingPropertyMappingAction MissingPropertyMappingAction
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return missingPropertyMappingAction;
            }
            set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                missingPropertyMappingAction = value;
            }
        }

        /// <summary>
        ///     Gets a SQL statement or stored procedure used to select records in the data source.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public IDbCommand SelectCommand
        {
            get
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                return selectCommand;
            }
            protected set
            {
                if (flags[IsDisposedFlag])
                    throw new ObjectDisposedException(ClassName);

                selectCommand = value;
            }
        }

        #endregion

        protected static void FormatCommand(IDbCommand command, string commandText, params object[] parameters)
        {
            command.AddParameterizedCommandText(commandText, parameters);
        }

        public Collection<T> CreateCollection<T>() where T : new()
        {
            Collection<T> collection = new Collection<T>();
            FillCollection(collection);
            return collection;
        }

        /// <exception cref="ObjectDisposedException">
        ///     This instance has been disposed.
        /// </exception>
        public T Create<T>() where T : new()
        {
            if (flags[IsDisposedFlag])
                throw new ObjectDisposedException(ClassName);

            PrepareCommand(selectCommand);
            using (IDataReader reader = selectCommand.ExecuteReader(commandBehavior))
            {
                return Create<T>(reader);
            }
        }

        public void FillCollection<T>(ICollection<T> collection, IDataReader reader) where T : new()
        {
            FillCollectionPrivate(reader, collection);
        }

        public Collection<T> CreateCollection<T>(IDataReader reader) where T : new()
        {
            Collection<T> collection = new Collection<T>();
            FillCollectionPrivate(reader, collection);
            return collection;
        }

        public void Fill<T>(T entity, IDataReader reader)
        {
            FillEntityPrivate(reader, entity);
        }

        public T Create<T>(IDataReader reader) where T : new()
        {
            T entity = new T();
            FillEntityPrivate(reader, entity);
            return entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (flags[IsDisposedFlag])
                return;

            selectCommand = null;
            flags[IsDisposedFlag] = true;
        }

        private static void FillEntityPrivate<T>(T entity, IDataRecord reader, IList<CachedSetter<T>> setters)
        {
            for (int i = 0; i < setters.Count; i++)
            {
                CachedSetter<T> cachedSetter = setters[i];
                object value = reader[cachedSetter.ReaderOrdinal];
                cachedSetter.Setter(entity, value == DBNull.Value ? null : value);
            }
        }

        /// <param name="command">
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="SelectCommand" />
        ///     property has
        ///     not been initialized.
        /// </exception>
        private static void PrepareCommand(IDbCommand command)
        {
            if (command == null)
                throw new InvalidOperationException("The command has not been initialized.");

            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
        }

        /// <exception cref="InvalidOperationException">
        ///     A column has no corresponding public property
        ///     setter or mapping entry.
        /// </exception>
        private List<CachedSetter<T>> CreateCachedSetters<T>(IDataRecord record)
        {
            bool hasMappings = options != null && options.Mappings != null && options.Mappings.Count > 0;
            List<CachedSetter<T>> setters = new List<CachedSetter<T>>();

            for (int i = 0; i < record.FieldCount; i++)
            {
                string propertyName = null;
                string columnName = record.GetName(i);
                bool columnMapped = false;

                if (hasMappings)
                    columnMapped = (propertyName = options.Mappings.Find(columnName)) != null;

                if (!columnMapped)
                    propertyName = columnName;

                PropertySetter<T, object> setter;
                if (DynamicType<T>.TryCreatePropertySetterAsObject(propertyName, out setter, flags[IgnoreCaseFlag]))
                    setters.Add(new CachedSetter<T>(setter, i));
                else if (columnMapped || missingPropertyMappingAction == MissingPropertyMappingAction.Error)
                    throw new InvalidOperationException("The column '" + columnName + "' has no corresponding public property setter or mapping entry.");
            }
            return setters;
        }

        private void FillEntityPrivate<T>(IDataReader reader, T entity)
        {
            if (!reader.Read())
                return;

            FillEntityPrivate(entity, reader, CreateCachedSetters<T>(reader));
        }

        private void FillCollectionPrivate<T>(IDataReader reader, ICollection<T> collection) where T : new()
        {
            if (!reader.Read())
                return;

            List<CachedSetter<T>> setters = CreateCachedSetters<T>(reader);

            T entity = new T();
            FillEntityPrivate(entity, reader, setters);
            collection.Add(entity);

            while (reader.Read())
            {
                entity = new T();
                FillEntityPrivate(entity, reader, setters);
                collection.Add(entity);
            }
        }

        private void FillDictionaryPrivate<TKey, TEntity>(IDataReader reader, IDictionary<TKey, TEntity> dictionary, string keyField) where TEntity : new()
        {
            if (!reader.Read())
                return;

            List<CachedSetter<TEntity>> setters = CreateCachedSetters<TEntity>(reader);

            TEntity entity = new TEntity();
            FillEntityPrivate(entity, reader, setters);
            dictionary.Add((TKey) reader[keyField], entity);

            while (reader.Read())
            {
                entity = new TEntity();
                FillEntityPrivate(entity, reader, setters);
                dictionary.Add((TKey) reader[keyField], entity);
            }
        }

        #region Nested type: CachedSetter

        private sealed class CachedSetter<T>
        {
            public CachedSetter(PropertySetter<T, object> setter, int readerOrdinal)
            {
                ReaderOrdinal = readerOrdinal;
                Setter = setter;
            }

            public int ReaderOrdinal { get; }

            public PropertySetter<T, object> Setter { get; }
        }

        #endregion
    }
}