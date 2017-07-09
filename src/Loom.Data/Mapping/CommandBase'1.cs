#region License Information

// ******************************************************************
// Devinterop Framework 
// 
// Copyright © 2004, 2008 by Brian Scott (DevInterop)
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.devinterop.com
// http://blogs.geekdojo.net/brian
//  
// ******************************************************************

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Linq;
using Loom.Annotations;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping
{
    public abstract class CommandBase<TDataRecord> : LiteralQuery where TDataRecord : DataRecord<TDataRecord>, new()
    {
        #region .ctors

        protected CommandBase(DataSession session, TableData table)
        {
            Session = session;
            QueryTree = new QueryTree(table, session);
        }

        #endregion

        protected internal QueryTree QueryTree { get; internal set; }

        public override DbCommand CreateCommand()
        {
            return Session.CreateCommandInternal(QueryTree);
        }

        #region Command Execution

        public override DataSet FetchDataSet()
        {
            return Session.FetchDataSetInternal(QueryTree);
        }

        public override IDataReader FetchReader()
        {
            return Session.ExecuteReaderInternal(QueryTree);
        }

        public override object FetchScalar()
        {
            return Session.ExecuteScalarInternal(QueryTree);
        }

        public TDataRecord FetchFirst()
        {
            return Session.QueryFirstInternal<TDataRecord>(QueryTree);
        }

        public TResult FetchFirst<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            return GetConverterEnumerator(converter).First();
        }

        public List<TDataRecord> ToList()
        {
            return new List<TDataRecord>(Session.QueryManyInternal<TDataRecord>(QueryTree));
        }

        public List<TResult> ToList<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            return new List<TResult>(GetConverterEnumerator(converter));
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

            Dictionary<TKey, TDataRecord> dictionary = new Dictionary<TKey, TDataRecord>();
            foreach (TDataRecord item in this)
                dictionary.Add((TKey) keyProperty.InvokeGetter(item), item);

            return dictionary;
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

        public string ToXml()
        {
            return Session.FetchXmlInternal<TDataRecord>(QueryTree);
        }

        public int Count
        {
            get { return Session.FetchCountInternal(QueryTree); }
        }

        private IEnumerable<TResult> GetConverterEnumerator<TResult>(Expression<Func<TDataRecord, TResult>> converter)
        {
            var newExpression = converter.Body as NewExpression;
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

            foreach (TDataRecord item in Session.QueryManyInternal<TDataRecord>(QueryTree))
                yield return func(item);
        }

        #endregion

        #region IEnumerable<T> Implementation

        public IEnumerator<TDataRecord> GetEnumerator()
        {
            foreach (TDataRecord item in Session.QueryManyInternal<TDataRecord>(QueryTree))
                yield return item;
        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

        #endregion
    }
}
