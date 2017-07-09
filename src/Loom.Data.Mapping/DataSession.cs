#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Serialization;
using Loom.Data.Entities;
using Loom.Data.Mapping.CodeGeneration;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Providers;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;
using Loom.Dynamic;
using Loom.Security;
using IsolationLevel = System.Transactions.IsolationLevel;

#endregion

namespace Loom.Data.Mapping
{
    /// <summary>
    /// </summary>
    [DebuggerDisplay("ConnectionString={ConnectionString}")]
    public class DataSession : DataSessionBase<IActiveDataProvider>
    {
        private const string ObjectName = "DataSession";

        private bool enableAutoVersion;

        protected DataSession(string sessionProviderName = null)
        {
            SessionProvidersCollection providers = Configuration.SessionProviders;

            if (Compare.IsNullOrEmpty(sessionProviderName))
                sessionProviderName = providers.DefaultProvider;

            if (Compare.IsNullOrEmpty(sessionProviderName))
                throw new ConfigurationErrorsException("No sessionProviderName supplied and no default provider is configured.");

            bool found = false;
            int count = providers.Count;
            foreach (SessionProvidersElement sessionProvider in providers)
            {
                if (count == 1)
                    sessionProviderName = sessionProvider.Name;
                else if (sessionProvider.Name != sessionProviderName)
                    continue;

                State = new PersistentSessionConnectionState<IActiveDataProvider>(sessionProvider.Type, sessionProvider.ConnectionStringName);
                found = true;
                break;
            }

            if (!found)
                throw new ConfigurationErrorsException(string.Format("There is no session provider registered with the name {0}.", sessionProviderName));
        }

        protected DataSession(IActiveDataProvider provider, string connectionString)
            : base(provider, connectionString) { }

        protected DataSession(IActiveDataProvider provider, DbConnection connection)
            : base(provider, connection) { }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public bool AutoVersion
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);
                return enableAutoVersion;
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);
                enableAutoVersion = value;
            }
        }

        public string DefaultLocale => Configuration.Localization.DefaultLocale;

        public bool UseDefaultLocale { get; set; }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        private ActiveMapConfigurationSection Configuration
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);

                return (ActiveMapConfigurationSection) ConfigurationManager.GetSection("activeMapConfiguration");
            }
        }

        public string[] GetSupportedLocales()
        {
            return Configuration.Localization.SupportedLanguages.Split(';');
        }

        public bool IsLanguageSupported(string locale)
        {
            return Configuration.Localization.SupportedLanguages.Split(';').Contains(locale);
        }

        public EntitySet<T> EntitySet<T>() where T : DataRecord<T>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            return new EntitySet<T>(this);
        }

        public IEntityAdapter CreateEntityAdapter<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            return CreateEntityAdapter(CreateCommand(procedure));
        }

        protected virtual object GetAuditUserIdentifier(QueryColumn table)
        {
            IUserIdentity identity = User as IUserIdentity;
            if (identity == null)
                return User.Name;

            if (table.DbType == DbType.Int32)
                return identity.UserId;
            return User.Name;
        }

        public void Save<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            SavePrivate(record);
        }

        private void SavePrivate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            ITable table = DataRecord<TDataRecord>.Table;

            if (record.IsLoadedFromDatasource && !Equals(table.PrimaryKey.CreatePredicate(record), null))
            {
                Execute(CreateUpdate(record));
            }
            else
            {
                Insert insert = CreateInsert(record);
                Execute(insert);

                object idValue;
                //TODO [Brian,20140601] A better way to handle different identity types.
                switch (insert.Table.IdentityColumn.DbType)
                {
                    case DbType.Int32:
                        idValue = (int) insert.NewId;
                        break;
                    default:
                        idValue = insert.NewId;
                        break;
                }

                record[insert.Table.IdentityColumn] = idValue;
            }

            record.Clean();
        }

        /// <summary>
        ///     Saves this <see cref="DataRecord{TDataRecord}" /> instance to the configured data source.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If this object is new, a new record is inserted into the data source; otherwise the data
        ///         represented by this object is updated.
        ///     </para>
        ///     <para>
        ///         If this object is new and the primary key is an identity column this instance will have
        ///         its primary key updated to reflect the new primary key.
        ///     </para>
        /// </remarks>
        public void Insert<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Insert insert = CreateInsert(record);
            Execute(insert);

            if (!insert.Table.HasIdentityColumn)
                return;

            object idValue;
            //TODO [Brian,20140601] A better way to handle different identity types.
            switch (insert.Table.IdentityColumn.DbType)
            {
                case DbType.Int32:
                    idValue = (int) insert.NewId;
                    break;
                default:
                    idValue = insert.NewId;
                    break;
            }

            record[insert.Table.IdentityColumn] = idValue;
        }

        /// <summary>
        ///     Performs a transactional insert of all items in the <see cref="ICollection{TDataRecord}" />.
        /// </summary>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        public void InsertMany<TDataRecord>(IEnumerable<TDataRecord> items) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Transaction transaction = Transaction.Current;
            InsertMany(items, transaction?.IsolationLevel ?? IsolationLevel.Serializable);
        }

        /// <summary>
        ///     Performs a transactional insert of all items in the <see cref="ICollection{TDataRecord}" />.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="il">
        ///     The <see cref="System.Transactions.IsolationLevel" /> of the transaction.
        /// </param>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void InsertMany<TDataRecord>(IEnumerable<TDataRecord> items, IsolationLevel il) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            try
            {
                ForEach(items, record => Execute(CreateInsert(record)), il);
            }
            catch (Exception ex)
            {
                WriteLog("***********************************************************************************************");
                WriteLog("Exception during InsertMany. Performing transaction rollback.");
                WriteLog(string.Format("Exception Details:{0}{1}", Environment.NewLine, ex));
                WriteLog("***********************************************************************************************");
                throw;
            }
        }

        /// <summary>
        ///     Saves this <see cref="DataRecord{TDataRecord}" /> instance to the configured data source.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If this object is new, a new record is inserted into the data source; otherwise the data
        ///         represented by this object is updated.
        ///     </para>
        ///     <para>
        ///         If this object is new and the primary key is an identity column this instance will have
        ///         its primary key updated to reflect the new primary key.
        ///     </para>
        /// </remarks>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void Update<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            if (!record.IsDirty)
                return;

            Execute(CreateUpdate(record));
        }

        /// <summary>
        ///     Performs a transactional update of all items in the <see cref="ICollection{TDataRecord}" />.
        /// </summary>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        public void UpdateMany<TDataRecord>(IEnumerable<TDataRecord> items) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Transaction transaction = Transaction.Current;
            UpdateMany(items, transaction?.IsolationLevel ?? IsolationLevel.Serializable);
        }

        /// <summary>
        ///     Performs a transactional update of all items in the <see cref="ICollection{TDataRecord}" />.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="il">
        ///     The <see cref="IsolationLevel" /> of the transaction.
        /// </param>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void UpdateMany<TDataRecord>(IEnumerable<TDataRecord> items, IsolationLevel il) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            try
            {
                ForEach(items, record => Execute(CreateUpdate(record)), il);
            }
            catch (Exception ex)
            {
                WriteLog("***********************************************************************************************");
                WriteLog("Exception during UpdateMany. Performing transaction rollback.");
                WriteLog(string.Format("Exception Details:{0}{1}", Environment.NewLine, ex));
                WriteLog("***********************************************************************************************");
                throw;
            }
        }

        /// <summary>
        ///     Deletes from the data source or alternatively marks as deleted the record represented by this
        ///     <see cref="DataRecord{TDataRecord}" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If there is a boolean column named deleted in the table, this method will set its value to true
        ///         instead of deleting the record. If the column does not exist the record is deleted. To force
        ///         a permanent delete use the <see cref="Obliterate" /> method.
        ///     </para>
        ///     <para>
        ///         In order to use this method, the record must have a primary key defined and it must
        ///         be set in this instance. Use the static <see cref="Delete" /> method to delete records
        ///         based on other criteria.
        ///     </para>
        ///     <para>
        ///         After deletion, the primary key of this instance is set to null and it is
        ///         marked as new.
        ///     </para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     No primary key is defined in the table or its
        ///     value is not set on this instance.
        /// </exception>
        public void Delete<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(record, nameof(record));

            if (enableAutoVersion)
                record.SaveVersion();

            DeletePrivate(DataRecord<TDataRecord>.Table.CreatePredicate(record), false);
            record.Clean();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void Delete<TDataRecord>(object primaryKeyValue) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(primaryKeyValue, nameof(primaryKeyValue));

            ITable table = DataRecord<TDataRecord>.Table;

            if (!table.HasPrimaryKey)
                throw new InvalidOperationException("The table " + table.Owner + "." + table.Name + " has no primary key column. Unable to delete a record using this method. Use the overload that allows specifying a ColumnPredicate value.");

            if (table.PrimaryKey.Multiple)
                throw new InvalidOperationException("The table " + table.Owner + "." + table.Name + " has more than one primary key column. Unable to delete a record using this method. Use the overload that allows specifying a ColumnPredicate value.");

            DeletePrivate(table.PrimaryKey[0] == primaryKeyValue, false);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void Delete<TDataRecord>(ColumnPredicate predicate) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(predicate, nameof(predicate));

            DeletePrivate(predicate, false);
        }

        public void Delete<TDataRecord>(Func<dynamic, ColumnPredicate> f) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            dynamic d = new DynamicRecord(DataRecord<TDataRecord>.Table);

            ColumnPredicate p = f(d);

            DeletePrivate(p, false);
        }

        /// <summary>
        ///     Performs a transactional delete of all items in the <see cref="ICollection{TDataRecord}" />.
        /// </summary>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        public void DeleteMany<TDataRecord>(IEnumerable<TDataRecord> items) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(items, nameof(items));

            Transaction transaction = Transaction.Current;
            DeleteMany(items, transaction?.IsolationLevel ?? IsolationLevel.Serializable);
        }

        /// <summary>
        ///     Performs a transactional delete of all items in the <see cref="ICollection{TDataRecord}" />
        ///     with the specified <see cref="IsolationLevel" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If there is a boolean column named 'Deleted' in the table, this method will set its value to true
        ///         for each record instead of deleting the it. If the column does not exist the record is deleted.
        ///     </para>
        ///     <para>
        ///         In order to use this method, each record must have a primary key defined and it must
        ///         be set in the instance. Use the <see cref="Obliterate" /> method to
        ///         delete records based on other criteria.
        ///     </para>
        ///     <para>
        ///         After deletion, the primary key of each item is set to null and it is marked as new.
        ///     </para>
        ///     <para>
        ///         If an exception is raised during the transaction, all changes are rolled back
        ///         and the exception is re-thrown.
        ///     </para>
        /// </remarks>
        /// <param name="items"></param>
        /// <param name="il">
        ///     The <see cref="IsolationLevel" /> of the transaction.
        /// </param>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void DeleteMany<TDataRecord>(IEnumerable<TDataRecord> items, IsolationLevel il) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(items, nameof(items));

            try
            {
                ForEach(items, Delete, il);
            }
            catch (Exception ex)
            {
                WriteLog("***********************************************************************************************");
                WriteLog("Exception during Delete. Performing transaction rollback.");
                WriteLog(string.Format("Exception Details:{0}{1}", Environment.NewLine, ex));
                WriteLog("***********************************************************************************************");
                throw;
            }
        }

        /// <summary>
        ///     Deletes all rows in the table represented by this <see cref="DataRecord{TDataRecord}" /> object or
        ///     alternatively sets the deleted column for each record if the column exists.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use <see cref="ObliterateAll{TDataRecord}" /> to force a delete of all rows.
        ///     </para>
        /// </remarks>
        /// <returns>
        ///     An <see cref="int" /> representing the number of rows deleted.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int DeleteAll<TDataRecord>() where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            return Execute(Mapping.Delete.From(DataRecord<TDataRecord>.Table));
        }

        private void DeletePrivate(ColumnPredicate predicate, bool obliterate)
        {
            // TODO: Make sure first predicate column is always the source table.
            ITable table = predicate.Column.Table;
            AssertNotReadOnly(table);

            Delete delete = Mapping.Delete.From(table);
            delete.WherePredicates.Add(predicate);
            using (DbCommand command = State.Provider.FetchCommand(delete, obliterate))
            {
                Execute(command);
            }
        }

        /// <summary>
        ///     Permanently deletes the record represented by this <see cref="DataRecord{TDataRecord}" /> from
        ///     the data source.
        /// </summary>
        /// <remarks>
        ///     In order to use this method, the record must have a primary key defined and it must
        ///     be set in this instance. Use the static <see cref="Delete" /> method to delete records
        ///     based on other criteria.
        ///     <para>
        ///         After deletion, the primary key of this instance is set to null and it is
        ///         marked as new.
        ///     </para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     No primary key is defined in the table or its
        ///     value is not set in this instance.
        /// </exception>
        public void Obliterate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(record, nameof(record));

            if (enableAutoVersion)
                record.SaveVersion();

            DeletePrivate(DataRecord<TDataRecord>.Table.CreatePredicate(record), false);
            record.Clean();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void Obliterate<TDataRecord>(object primaryKeyValue) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(primaryKeyValue, nameof(primaryKeyValue));

            ITable table = DataRecord<TDataRecord>.Table;

            if (!table.HasPrimaryKey)
                throw new InvalidOperationException("The table " + table.Owner + "." + table.Name + " has no primary key column. Unable to delete a record using this method. Use the overload that allows specifying a ColumnPredicate value.");

            if (table.PrimaryKey.Multiple)
                throw new InvalidOperationException("The table " + table.Owner + "." + table.Name + " has more than one primary key column. Unable to delete a record using this method. Use the overload that allows specifying a ColumnPredicate value.");

            DeletePrivate(table.PrimaryKey[0] == primaryKeyValue, true);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void Obliterate(ColumnPredicate predicate)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(predicate, nameof(predicate));

            DeletePrivate(predicate, true);
        }

        /// <summary>
        ///     Performs a transactional delete of all items in the <see cref="ICollection{TDataRecord}" />.
        /// </summary>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        public void ObliterateMany<TDataRecord>(IEnumerable<TDataRecord> items) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Transaction transaction = Transaction.Current;
            ObliterateMany(items, transaction?.IsolationLevel ?? IsolationLevel.Serializable);
        }

        /// <summary>
        ///     Performs a transactional delete of all items in the <see cref="ICollection{TDataRecord}" />
        ///     with the specified <see cref="IsolationLevel" />.
        /// </summary>
        /// <remarks>
        ///     If an exception is raised during the transaction, all changes are rolled back
        ///     and the exception is re-thrown.
        /// </remarks>
        /// <param name="items"></param>
        /// <param name="il">
        ///     The <see cref="IsolationLevel" /> of the transaction.
        /// </param>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void ObliterateMany<TDataRecord>(IEnumerable<TDataRecord> items, IsolationLevel il) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            try
            {
                ForEach(items, Obliterate, il);
            }
            catch (Exception ex)
            {
                WriteLog("***********************************************************************************************");
                WriteLog("Exception during ObliterateItems. Performing transaction rollback.");
                WriteLog(string.Format("Exception Details:{0}{1}", Environment.NewLine, ex));
                WriteLog("***********************************************************************************************");
                throw;
            }
        }

        /// <summary>
        ///     Deletes all rows in the table represented by this <see cref="DataRecord{TDataRecord}" /> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="int" /> representing the number of rows deleted.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int ObliterateAll<TDataRecord>() where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            return Execute(Mapping.Delete.From(DataRecord<TDataRecord>.Table, true));
        }

        /// <summary>
        ///     Returns the record in the data source that has the specified primary key value.
        /// </summary>
        /// <param name="primaryKeyValue">The value of the primary key.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="primaryKeyValue" /> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">The table does not define a primary key.</exception>
        /// <returns>
        ///     A constructed <see cref="DataRecord{TDataRecord}" /> object containing the record's values.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchByKey<TDataRecord>(object primaryKeyValue)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(primaryKeyValue, nameof(primaryKeyValue));

            ITable table = DataRecord<TDataRecord>.Table;

            if (!table.HasPrimaryKey)
                throw new InvalidOperationException(SR.ExFetchByIdPrimaryKeyNotDefinedInTable(string.Format("{0}.{1}", DataRecord<TDataRecord>.Table.Owner, DataRecord<TDataRecord>.Table.Name)));

            if (table.PrimaryKey.Multiple)
                throw new InvalidOperationException("The table " + table.Owner + "." + table.Name + " has more than one primary key column. Unable to fetch a record using this method. Use a fetch method that allows specifying a ColumnPredicate value.");

            QueryTree queryTree = new QueryTree(DataRecord<TDataRecord>.Table, this);
            queryTree.WherePredicates.Add(DataRecord<TDataRecord>.Table.PrimaryKey[0] == primaryKeyValue);

            return QueryFirstInternal<TDataRecord>(queryTree);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirstByExample<TDataRecord>(TDataRecord record)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(record, nameof(record));

            QueryTree queryTree = new QueryTree(DataRecord<TDataRecord>.Table, this);

            foreach (DynamicProperty<TDataRecord> property in DataEntity<TDataRecord>.Properties)
            {
                IQueryableColumn column = DataRecord<TDataRecord>.Table.Columns.FindColumn(property.AttributeName);
                if (column != null)
                    queryTree.WherePredicates.Add(((QueryColumn) column).IsEqualTo(property.InvokeGetterOn(record)));
            }
            return QueryFirstInternal<TDataRecord>(queryTree);
        }

        /// <summary>
        ///     Returns the first record in the data source.
        /// </summary>
        /// <returns>
        ///     A constructed <see cref="DataRecord{TDataRecord}" /> object containing the record's values.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirst<TDataRecord>()
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            EntitySet<TDataRecord> entitySet = new EntitySet<TDataRecord>(this);
            entitySet.Constrain(Constraint.TopCount(1));
            entitySet.SelectAll();

            return FetchFirst(entitySet);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirst<TDataRecord>(ColumnPredicate columnPredicate)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(columnPredicate, nameof(columnPredicate));

            EntitySet<TDataRecord> entitySet = new EntitySet<TDataRecord>(this);
            entitySet.Constrain(Constraint.TopCount(1));
            entitySet.Where(columnPredicate);
            entitySet.SelectAll();

            return FetchFirst(entitySet);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirst<TDataRecord>(ColumnPredicate columnPredicate, IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(columnPredicate, nameof(columnPredicate));

            EntitySet<TDataRecord> entitySet = new EntitySet<TDataRecord>(this);
            entitySet.Constrain(Constraint.TopCount(1));
            entitySet.Where(columnPredicate);
            entitySet.OrderBy(orderByColumn);
            entitySet.SelectAll();

            return FetchFirst(entitySet);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirst<TDataRecord>(ColumnPredicate columnPredicate, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(columnPredicate, nameof(columnPredicate));

            EntitySet<TDataRecord> entitySet = new EntitySet<TDataRecord>(this);
            entitySet.Constrain(Constraint.TopCount(1));
            entitySet.Where(columnPredicate);
            entitySet.OrderBy(orderByColumn, direction);
            entitySet.SelectAll();

            return FetchFirst(entitySet);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirst<TDataRecord>(EntitySet<TDataRecord> entitySet)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            using (IDataReader reader = ExecuteReaderInternal(entitySet.QueryTree))
            {
                return TranslateToEntity<TDataRecord>(reader);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TDataRecord FetchFirst<TDataRecord>(string commandText, params object[] parameterValues)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(commandText, nameof(commandText));

            using (IDataReader reader = ExecuteReader(commandText, parameterValues))
            {
                return TranslateToEntity<TDataRecord>(reader);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchByExample<TDataRecord>(TDataRecord record)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryByExample(record).ToCollection();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchAll<TDataRecord>()
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryAll<TDataRecord>().ToCollection();
        }

        public Collection<TDataRecord> FetchAll<TDataRecord>(IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryAll<TDataRecord>(orderByColumn, OrderByDirection.Asc).ToCollection();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchAll<TDataRecord>(IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryAll<TDataRecord>(orderByColumn, direction).ToCollection();
        }

        public Collection<TStoredProcedure> FetchAll<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            IEntityAdapter adapter = CreateEntityAdapter(CreateCommand(procedure));
            Collection<TStoredProcedure> items = new Collection<TStoredProcedure>();
            adapter.FillCollection(items);
            return items;
        }

        // TODO: Use expression tree in more places and make a QueryAll<TResult> version.
        public Collection<TResult> FetchAll<TDataRecord, TResult>(Expression<Func<TDataRecord, TResult>> converter)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryAll<TDataRecord>().ToCollection(converter);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchMany<TDataRecord>(ColumnPredicate predicate)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryMany<TDataRecord>(predicate).ToCollection();
        }

        public Collection<TDataRecord> FetchMany<TDataRecord>(ColumnPredicate predicate, IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryMany<TDataRecord>(predicate, orderByColumn, OrderByDirection.Asc).ToCollection();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchMany<TDataRecord>(ColumnPredicate predicate, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryMany<TDataRecord>(predicate, orderByColumn, direction).ToCollection();
        }

        // TODO: Create a Query method for FetchMany<TDataRecord>(string commandText, params object[] parameterValues). 
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchMany<TDataRecord>(string commandText, params object[] parameterValues)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(commandText, nameof(commandText));

            using (IDataReader reader = ExecuteReader(commandText, parameterValues))
            {
                return TranslateToCollection<TDataRecord>(reader);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchTop<TDataRecord>(int count)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNegative(count, nameof(count));

            QueryTree queryTree = new QueryTree(DataRecord<TDataRecord>.Table, this)
            {
                ResultConstraint = Constraint.TopCount(count)
            };

            using (IDataReader reader = ExecuteReaderInternal(queryTree))
            {
                return TranslateToCollection<TDataRecord>(reader);
            }
        }

        public Collection<TDataRecord> FetchTop<TDataRecord>(int count, ColumnPredicate columnPredicate)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return FetchTop<TDataRecord>(count, columnPredicate, null, OrderByDirection.Asc);
        }

        public Collection<TDataRecord> FetchTop<TDataRecord>(int count, IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return FetchTop<TDataRecord>(count, null, orderByColumn, OrderByDirection.Asc);
        }

        public Collection<TDataRecord> FetchTop<TDataRecord>(int count, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return FetchTop<TDataRecord>(count, null, orderByColumn, direction);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public Collection<TDataRecord> FetchTop<TDataRecord>(int count, ColumnPredicate columnPredicate, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNegative(count, nameof(count));

            QueryTree queryTree = new QueryTree(DataRecord<TDataRecord>.Table, this)
            {
                ResultConstraint = Constraint.TopCount(count)
            };

            if (columnPredicate != null)
                queryTree.WherePredicates.Add(columnPredicate);

            if (orderByColumn != null)
                switch (direction)
                {
                    case OrderByDirection.Asc:
                        queryTree.OrderBys.Add(new OrderBy(orderByColumn, OrderByDirection.Asc));
                        break;
                    case OrderByDirection.Desc:
                        queryTree.OrderBys.Add(new OrderBy(orderByColumn, OrderByDirection.Desc));
                        break;
                }

            using (IDataReader reader = ExecuteReaderInternal(queryTree))
            {
                return TranslateToCollection<TDataRecord>(reader);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryByExample<TDataRecord>(TDataRecord record)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            EntitySet<TDataRecord> entitySet = EntitySet<TDataRecord>();
            foreach (DynamicProperty<TDataRecord> property in record.GetUpdatedProperties())
            {
                IQueryableColumn column = DataRecord<TDataRecord>.Table.Columns.FindColumn(property.AttributeName);
                if (column != null)
                    entitySet.Where(((QueryColumn) column).IsEqualTo(property.InvokeGetterOn(record)));
            }
            return entitySet;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryAll<TDataRecord>()
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            return EntitySet<TDataRecord>();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryAll<TDataRecord>(IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryAll<TDataRecord>(orderByColumn, OrderByDirection.Asc);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryAll<TDataRecord>(IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(orderByColumn, nameof(orderByColumn));
            EntitySet<TDataRecord> entitySet = EntitySet<TDataRecord>();

            switch (direction)
            {
                case OrderByDirection.Asc:
                    entitySet.OrderBy(orderByColumn, OrderByDirection.Asc);
                    break;
                case OrderByDirection.Desc:
                    entitySet.OrderBy(orderByColumn, OrderByDirection.Desc);
                    break;
            }
            return entitySet;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryMany<TDataRecord>(ColumnPredicate columnPredicate)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(columnPredicate, nameof(columnPredicate));

            return EntitySet<TDataRecord>().Where(columnPredicate).End();
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryMany<TDataRecord>(ColumnPredicate predicate, IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryMany<TDataRecord>(predicate, orderByColumn, OrderByDirection.Asc);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryMany<TDataRecord>(ColumnPredicate predicate, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(orderByColumn, nameof(orderByColumn));

            EntitySet<TDataRecord> entitySet = EntitySet<TDataRecord>();
            entitySet.Where(predicate);
            switch (direction)
            {
                case OrderByDirection.Asc:
                    return entitySet.OrderBy(orderByColumn, OrderByDirection.Asc);
                case OrderByDirection.Desc:
                    return entitySet.OrderBy(orderByColumn, OrderByDirection.Desc);
            }
            return entitySet;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryTop<TDataRecord>(int count)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNegative(count, nameof(count));

            return EntitySet<TDataRecord>().Top(count);
        }

        public EntitySet<TDataRecord> QueryTop<TDataRecord>(int count, ColumnPredicate columnPredicate)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryTop<TDataRecord>(count, columnPredicate, null, OrderByDirection.Asc);
        }

        public EntitySet<TDataRecord> QueryTop<TDataRecord>(int count, IQueryableColumn orderByColumn)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryTop<TDataRecord>(count, null, orderByColumn, OrderByDirection.Asc);
        }

        public EntitySet<TDataRecord> QueryTop<TDataRecord>(int count, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            return QueryTop<TDataRecord>(count, null, orderByColumn, direction);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public EntitySet<TDataRecord> QueryTop<TDataRecord>(int count, ColumnPredicate columnPredicate, IQueryableColumn orderByColumn, OrderByDirection direction)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNegative(count, nameof(count));

            EntitySet<TDataRecord> entitySet = QueryTop<TDataRecord>(count);
            if (columnPredicate != null)
                entitySet.Where(columnPredicate);

            if (orderByColumn != null)
                switch (direction)
                {
                    case OrderByDirection.Asc:
                        return entitySet.OrderBy(orderByColumn, OrderByDirection.Asc);
                    case OrderByDirection.Desc:
                        return entitySet.OrderBy(orderByColumn, OrderByDirection.Desc);
                }

            return entitySet;
        }

        /// <summary>
        ///     Executes an <see cref="IDataReader" /> over the results of the stored procedure.
        /// </summary>
        /// <returns>
        ///     An <see cref="IDataReader" />.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public IDataReader ExecuteReader<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            using (DbCommand command = CreateCommand(procedure))
            {
                IDataReader reader = ExecuteReader(command);
                UpdateOutParameterPropertyValues(procedure, command);
                return reader;
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal IDataReader ExecuteReader(CodeGenQuery query)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            using (DbCommand command = State.Provider.FetchCommand(query))
            {
                return ExecuteReader(command);
            }
        }

        public static TDataRecord TranslateToEntity<TDataRecord>(IDbCommand command)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Argument.Assert.IsNotNull(command, nameof(command));

            using (IDataReader reader = command.ExecuteReader())
            {
                return TranslateToEntity<TDataRecord>(reader);
            }
        }

        /// <summary>
        ///     Advances the <see cref="IDataReader" /> to the next record and returns a constructed
        ///     <see cref="DataRecord{TDataRecord}" /> representing its data.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="IDataReader" /> to read from.
        /// </param>
        /// <returns>
        ///     A constructed <see cref="DataRecord{TDataRecord}" /> object containing the record's values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        public static TDataRecord TranslateToEntity<TDataRecord>(IDataReader reader)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Argument.Assert.IsNotNull(reader, nameof(reader));

            // Do not create a new object if there was no record returned.
            if (reader.Read())
                return TranslateToEntityPrivate<TDataRecord>(reader);

            return default(TDataRecord);
        }

        public static Collection<TDataRecord> TranslateToCollection<TDataRecord>(IDbCommand command)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Argument.Assert.IsNotNull(command, nameof(command));

            using (IDataReader reader = command.ExecuteReader())
            {
                return TranslateToCollection<TDataRecord>(reader);
            }
        }

        public static Collection<TDataRecord> TranslateToCollection<TDataRecord>(IDataReader reader)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Collection<TDataRecord> collection = new Collection<TDataRecord>();
            while (reader.Read())
                collection.Add(TranslateToEntityPrivate<TDataRecord>(reader));
            return collection;
        }

        private static TDataRecord TranslateToEntityPrivate<TDataRecord>(IDataRecord record)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            TDataRecord entity = DataRecord<TDataRecord>.Instantiate();

            for (int i = 0; i < record.FieldCount; i++)
                entity[record.GetName(i)] = record[i];

            entity.IsLoadedFromDatasource = true;
            return entity;
        }

        internal static void UpdateOutParameterPropertyValues<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure, DbCommand command)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            foreach (ICallableParameter info in StoredProcedure<TStoredProcedure>.Procedure.Parameters)
                if (info.ParameterType == ParameterType.Out || info.ParameterType == ParameterType.InOut)
                    procedure[info.Name] = command.Parameters[info.Name].Value;
        }

        /// <summary>
        ///     Executes the specified <see cref="Insert" /> and returns the number of rows affected.
        /// </summary>
        /// <returns>
        ///     An <see langword="int" /> representing the number of rows affected.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int Execute(Insert insert)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(insert, nameof(insert));
            AssertNotReadOnly(insert.Table);

            if (insert.ColumnCount == 0)
                throw new InvalidOperationException("No column values have been set. Call Insert.Set(TableColumnInfo, object) at least once before executing the command.");

            using (DbCommand command = CreateCommand(insert))
            {
                int affectedRows = Execute(command);
                if (command.Parameters.Contains(NewIdInsertParameterName))
                    insert.NewId = Convert.ToInt32(command.Parameters[NewIdInsertParameterName].Value);

                return affectedRows;
            }
        }

        /// <summary>
        ///     Executes the specified <see cref="Insert" /> and returns the number of rows affected.
        /// </summary>
        /// <returns>
        ///     An <see langword="int" /> representing the number of rows affected.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public async Task<int> ExecuteAsync(Insert insert)
        {
            return await new Task<int>(() => Execute(insert));
        }

        /// <summary>
        ///     Executes the specified <see cref="Update" /> and returns the number of rows affected.
        /// </summary>
        /// <returns>
        ///     An <see langword="int" /> representing the number of rows affected.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int Execute(Update update)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(update, nameof(update));
            AssertNotReadOnly(update.Table);

            if (update.ColumnCount == 0)
                throw new InvalidOperationException("No column values have been set. Call Update.Set(TableColumnInfo, object) at least once before executing the command.");

            using (DbCommand command = State.Provider.FetchCommand(update))
            {
                return Execute(command);
            }
        }

        /// <summary>
        ///     Executes the specified <see cref="Update" /> and returns the number of rows affected.
        /// </summary>
        /// <returns>
        ///     An <see langword="int" /> representing the number of rows affected.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public async Task<int> ExecuteAsync(Update update)
        {
            return await new Task<int>(() => Execute(update));
        }

        /// <summary>
        ///     Executes the specified <see cref="Delete" /> and returns the number of rows affected.
        /// </summary>
        /// <returns>
        ///     An <see langword="int" /> representing the number of rows affected.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int Execute(Delete delete)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(delete, nameof(delete));
            AssertNotReadOnly(delete.Table);

            using (DbCommand command = State.Provider.FetchCommand(delete, delete.Obliterate || delete.Table.HasDeletedColumn))
            {
                return Execute(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int Execute<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(procedure, nameof(procedure));

            using (DbCommand command = CreateCommand(procedure))
            {
                int result = Execute(command);
                UpdateOutParameterPropertyValues(procedure, command);
                return result;
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public object ExecuteScalar<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(procedure, nameof(procedure));

            using (DbCommand command = CreateCommand(procedure))
            {
                object result = ExecuteScalar(command);
                UpdateOutParameterPropertyValues(procedure, command);
                return result;
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DataSet FetchDataSet<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(procedure, nameof(procedure));

            using (DbCommand command = CreateCommand(procedure))
            {
                DataSet ds = FetchDataSet(command);
                UpdateOutParameterPropertyValues(procedure, command);
                return ds;
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DbCommand CreateCommand(Insert insert)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(insert, nameof(insert));

            if (insert.ColumnCount == 0)
                throw new InvalidOperationException("No column values have been set. Call Insert.Set(TableColumnInfo, object) at least once before executing the command.");

            DbCommand dbCommand = State.Provider.FetchCommand(insert);

            WriteLog("CommandText: " + dbCommand.CommandText);
            Diagnostic.WriteDbParameters(dbCommand.Parameters, Logger);
            WriteLog(null);

            return dbCommand;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DbCommand CreateCommand(Update update)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(update, nameof(update));

            if (update.ColumnCount == 0)
                throw new InvalidOperationException("No column values have been set. Call Update.Set(TableColumnInfo, object) at least once before executing the command.");

            DbCommand dbCommand = State.Provider.FetchCommand(update);

            WriteLog("CommandText: " + dbCommand.CommandText);
            Diagnostic.WriteDbParameters(dbCommand.Parameters, Logger);
            WriteLog(null);

            return dbCommand;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DbCommand CreateCommand(Delete delete)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(delete, nameof(delete));

            DbCommand dbCommand = State.Provider.FetchCommand(delete, delete.Obliterate || delete.Table.HasDeletedColumn);

            WriteLog("CommandText: " + dbCommand.CommandText);
            Diagnostic.WriteDbParameters(dbCommand.Parameters, Logger);
            WriteLog(null);

            return dbCommand;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal DbCommand CreateCommand<TStoredProcedure>(StoredProcedure<TStoredProcedure> procedure)
            where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(procedure, nameof(procedure));

            return State.Provider.FetchCommand(procedure);
        }

        public string RetrieveCommandText(Insert insert)
        {
            return CreateCommand(insert).CommandText;
        }

        public string RetrieveCommandText(Update update)
        {
            return CreateCommand(update).CommandText;
        }

        public string RetrieveCommandText(Delete delete)
        {
            return CreateCommand(delete).CommandText;
        }

        private Update CreateUpdate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            AssertNotReadOnly<TDataRecord>();

            if (enableAutoVersion)
                record.SaveVersion();

            ITable table = DataRecord<TDataRecord>.Table;
            if (!table.PrimaryKey.Exists)
                throw new InvalidOperationException(string.Format("There is no primary key defined in the table {0}.{1} Unable to build an update command using this method.", table.Owner, table.Name));

            Update update = Mapping.Update.To(table);

            if (table.HasModifiedOnColumn && table.ModifiedOnColumn.HasDefaultValue)
                record[table.ModifiedOnColumn.Name] = DateTime.Now;
            if (table.HasModifiedByColumn && table.ModifiedByColumn.HasDefaultValue)
                record[table.ModifiedByColumn.Name] = GetAuditUserIdentifier(table.ModifiedByColumn);

            foreach (DynamicProperty<TDataRecord> property in record.GetUpdatedProperties())
            {
                IQueryableColumn column = table.Columns.FindColumn(property.AttributeName);
                if (column == null)
                    throw new InvalidOperationException("Cannot find a column for property '" + property.AttributeName + "'.");

                if ((column.ColumnProperties & ColumnProperties.Identity) != ColumnProperties.Identity &&
                    (column.ColumnProperties & ColumnProperties.PrimaryKey) != ColumnProperties.PrimaryKey &&
                    (column.ColumnProperties & ColumnProperties.Computed) != ColumnProperties.Computed)
                    update.ColumnValues.Add(column, property.InvokeGetterOn(record));
            }

            update.WherePredicates.Add(table.PrimaryKey.CreatePredicate(record));
            return update;
        }

        private Insert CreateInsert<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            AssertNotReadOnly<TDataRecord>();

            if (enableAutoVersion)
                record.SaveVersion();

            if (record.IsLoadedFromDatasource)
                throw new InvalidOperationException("Unable to insert record. The record is not new. It was loaded from a data source.");

            ITable table = DataRecord<TDataRecord>.Table;
            Insert insert = Mapping.Insert.Into(table);

            if (table.HasCreatedOnColumn && !table.CreatedOnColumn.HasDefaultValue)
                record[table.CreatedOnColumn.Name] = DateTime.Now;
            if (table.HasCreatedByColumn && !table.CreatedByColumn.HasDefaultValue)
                record[table.CreatedByColumn.Name] = GetAuditUserIdentifier(table.CreatedByColumn);
            if (table.HasModifiedOnColumn && !table.ModifiedOnColumn.HasDefaultValue)
                record[table.ModifiedOnColumn.Name] = DateTime.Now;
            if (table.HasModifiedByColumn && !table.ModifiedByColumn.HasDefaultValue)
                record[table.ModifiedByColumn.Name] = GetAuditUserIdentifier(table.ModifiedByColumn);

            foreach (DynamicProperty<TDataRecord> property in record.GetUpdatedProperties())
            {
                IQueryableColumn column = table.Columns.FindColumn(property.AttributeName);
                if (column == null)
                    throw new InvalidOperationException("Cannot find a column for property '" + property.AttributeName + "'.");

                if ((column.ColumnProperties & ColumnProperties.Identity) != ColumnProperties.Identity &&
                    (column.ColumnProperties & ColumnProperties.Computed) != ColumnProperties.Computed)
                    insert.ColumnValues.Add(column, property.InvokeGetterOn(record));
            }

            return insert;
        }

        /// <exception cref="InvalidOperationException">
        ///     The <see cref="DataRecord{TDataRecord}" /> is read only.
        /// </exception>
        private static void AssertNotReadOnly<TDataRecord>() where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (DataRecord<TDataRecord>.Table.IsReadOnly)
                throw new InvalidOperationException("The DataRecord is read only. The operation failed.");
        }

        /// <exception cref="InvalidOperationException">
        ///     The <see cref="DataRecord{TDataRecord}" /> is read only.
        /// </exception>
        private static void AssertNotReadOnly(ISchema table)
        {
            if (table.IsReadOnly)
                throw new InvalidOperationException("The table " + table.Name + " is read only. The operation failed.");
        }

        private static void ValidateQuerySchema<TDataRecord>(CommandTree<QueryTree> query)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Argument.Assert.IsNotNull(query, nameof(query));

            if (!SchemaCompare.TablesAreSame(DataRecord<TDataRecord>.Table, query.Table))
                throw new ArgumentException(SR.ExSelectArgumentTableDoesNotMatchRecord);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal string FetchXmlInternal<TDataRecord>(QueryTree queryTree)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            ValidateQuerySchema<TDataRecord>(queryTree);

            using (IDataReader reader = ExecuteReaderInternal(queryTree))
            {
                Collection<TDataRecord> records = TranslateToCollection<TDataRecord>(reader);
                if (records.Count == 1)
                    return records[0].ToXml();

                XmlSerializer serializer = new XmlSerializer(typeof(Collection<TDataRecord>));

                using (Stream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, records);
                    stream.Position = 0;
                    using (StreamReader stmReader = new StreamReader(stream))
                    {
                        return stmReader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        ///     Returns the first record in the data source returned by the specified <see cref="QueryTree" />.
        /// </summary>
        /// <param name="queryTree">
        ///     The <see cref="QueryTree" /> object used to query the data source.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="queryTree" /> is <see langword="null" />.
        /// </exception>
        /// <returns>
        ///     A constructed <see cref="DataRecord{TDataRecord}" /> object containing the record's values.
        /// </returns>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal TDataRecord QueryFirstInternal<TDataRecord>(QueryTree queryTree)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            ValidateQuerySchema<TDataRecord>(queryTree);

            using (IDataReader reader = ExecuteReaderInternal(queryTree))
            {
                return TranslateToEntity<TDataRecord>(reader);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal IEnumerable<TDataRecord> QueryManyInternal<TDataRecord>(QueryTree queryTree)
            where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            ValidateQuerySchema<TDataRecord>(queryTree);

            using (IDataReader reader = ExecuteReaderInternal(queryTree))
            {
                while (reader.Read())
                    yield return TranslateToEntityPrivate<TDataRecord>(reader);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal int FetchCountInternal(QueryTree queryTree)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            object result = ExecuteScalar(CreateCommandInternal(queryTree, ConstraintType.Count));
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);

            return -1;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal IDataReader ExecuteReaderInternal(QueryTree queryTree)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            using (DbCommand command = CreateCommandInternal(queryTree))
            {
                return ExecuteReader(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal object ExecuteScalarInternal(QueryTree queryTree)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(queryTree, nameof(queryTree));

            using (DbCommand command = CreateCommandInternal(queryTree))
            {
                return ExecuteScalar(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal DataSet FetchDataSetInternal(QueryTree queryTree)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(queryTree, nameof(queryTree));

            using (DbCommand command = CreateCommandInternal(queryTree))
            {
                return FetchDataSet(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        internal DbCommand CreateCommandInternal(QueryTree queryTree, ConstraintType constraintType = ConstraintType.None)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(queryTree, nameof(queryTree));

            DbCommand dbCommand = State.Provider.FetchCommand(queryTree, constraintType);

            WriteLog("CommandText: " + dbCommand.CommandText);
            Diagnostic.WriteDbParameters(dbCommand.Parameters, Logger);
            WriteLog(null);

            return dbCommand;
        }

        internal string RetrieveCommandTextInternal(QueryTree queryTree)
        {
            return CreateCommandInternal(queryTree).CommandText;
        }

        #region Nested type: DynamicRecord

        private class DynamicRecord : DynamicObject
        {
            private readonly ITable table;

            public DynamicRecord(ITable table)
            {
                this.table = table;
            }

            /// <summary>
            ///     Provides the implementation for operations that get member values. Classes derived from the
            ///     <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for
            ///     operations such as getting a value for a property.
            /// </summary>
            /// <returns>
            ///     true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the
            ///     language determines the behavior. (In most cases, a run-time exception is thrown.)
            /// </returns>
            /// <param name="binder">
            ///     Provides information about the object that called the dynamic operation. The binder.Name property
            ///     provides the name of the member on which the dynamic operation is performed. For example, for the
            ///     Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived
            ///     from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The
            ///     binder.IgnoreCase property specifies whether the member name is case-sensitive.
            /// </param>
            /// <param name="result">
            ///     The result of the get operation. For example, if the method is called for a property, you can
            ///     assign the property value to <paramref name="result" />.
            /// </param>
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                QueryColumn queryColumn = table.FindColumn(binder.Name);
                if (Equals(queryColumn, null))
                {
                    result = null;
                    return false;
                }

                result = queryColumn;
                return true;
            }
        }

        #endregion
    }
}