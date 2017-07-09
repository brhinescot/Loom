#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Loom.Annotations;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping
{
    public abstract class CommandBase<TDataRecord> : ICommandFactory, IEnumerable<TDataRecord> where TDataRecord : DataRecord<TDataRecord>, new()
    {
        protected CommandBase(DataSession session, ITable table)
        {
            Session = session;
            QueryTree = new QueryTree(table, session);
        }

        protected DataSession Session { get; set; }
        protected internal QueryTree QueryTree { get; set; }

        public int Count => Session.FetchCountInternal(QueryTree);

        #region ICommandFactory Members

        public virtual DbCommand CreateCommand()
        {
            return Session.CreateCommandInternal(QueryTree);
        }

        #endregion

        #region IEnumerable<TDataRecord> Members

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TDataRecord> GetEnumerator()
        {
            return Session.QueryManyInternal<TDataRecord>(QueryTree).GetEnumerator();
        }

        #endregion

        protected virtual DataSet FetchDataSet()
        {
            return Session.FetchDataSetInternal(QueryTree);
        }

        protected virtual IDataReader FetchReader()
        {
            return Session.ExecuteReaderInternal(QueryTree);
        }

        protected virtual object FetchScalar()
        {
            return Session.ExecuteScalarInternal(QueryTree);
        }

        protected TDataRecord FetchFirst()
        {
            return Session.QueryFirstInternal<TDataRecord>(QueryTree);
        }

        protected TResult FetchFirst<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            return GetConverterEnumerator(converter).First();
        }

        public List<TDataRecord> ToList()
        {
            return Session.QueryManyInternal<TDataRecord>(QueryTree).ToList();
        }

        protected List<TResult> ToList<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            return GetConverterEnumerator(converter).ToList();
        }

        public Collection<TDataRecord> ToCollection()
        {
            Collection<TDataRecord> collection = new Collection<TDataRecord>();
            foreach (TDataRecord t in this)
                collection.Add(t);
            return collection;
        }

        public Collection<TResult> ToCollection<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            Collection<TResult> collection = new Collection<TResult>();
            foreach (TResult t in GetConverterEnumerator(converter))
                collection.Add(t);
            return collection;
        }

        public void Each(Action<TDataRecord> action)
        {
            foreach (TDataRecord t in this)
                action(t);
        }

        [NotNull]
        public Dictionary<TKey, TDataRecord> ToDictionary<TKey>(IQueryableColumn keyColumn)
        {
            DynamicProperty<TDataRecord> keyProperty = DataEntity<TDataRecord>.Properties[keyColumn.Name];

            return this.ToDictionary(item => (TKey) keyProperty.InvokeGetterOn(item));
        }

        [NotNull]
        public TDataRecord[] ToArray()
        {
            return ToList().ToArray();
        }

        [NotNull]
        public TResult[] ToArray<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            return ToList(converter).ToArray();
        }

        protected string ToXml()
        {
            return Session.FetchXmlInternal<TDataRecord>(QueryTree);
        }

        private IEnumerable<TResult> GetConverterEnumerator<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            NewExpression newExpression = converter.Body as NewExpression;
            if (newExpression == null)
                throw new ArgumentException("Must be an expression of type 'NewExpression'.");

            List<string> sourceNames = new List<string>();
            Func<TDataRecord, TResult> func = converter.Compile();

            foreach (Expression argument in newExpression.Arguments)
            {
                MemberExpression memberExpression = argument as MemberExpression;
                if (memberExpression == null)
                    continue;

                sourceNames.Add(memberExpression.Member.Name);
            }

            IQueryableColumn[] columns = new IQueryableColumn[sourceNames.Count];
            for (int i = 0; i < sourceNames.Count; i++)
                columns[i] = DataRecord<TDataRecord>.Table.FindColumn(sourceNames[i]);

            QueryTree.Columns.AddRange(columns);

            return Session.QueryManyInternal<TDataRecord>(QueryTree).Select(func);
        }

        [NotNull]
        public CommandConverter Convert()
        {
            return new CommandConverter(this);
        }
    }
}