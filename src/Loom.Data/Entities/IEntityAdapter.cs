#region Using Directives

using System;
using System.Collections.Generic;
using System.Data;

#endregion

namespace Loom.Data.Entities
{
    public interface IEntityAdapter : IDisposable
    {
        /// <summary>
        ///     Gets a collection that provides the master mapping between a data source record and an entity.
        /// </summary>
        PropertyMappingOptions Options { get; }

        /// <summary>
        /// </summary>
        bool IgnoreCase { get; set; }

        /// <summary>
        ///     Gets or sets a SQL statement or stored procedure used to select records in the data source.
        /// </summary>
        IDbCommand SelectCommand { get; }

        /// <summary>
        ///     Determines the action to take when incoming data does not have a matching property or field.
        /// </summary>
        MissingPropertyMappingAction MissingPropertyMappingAction { get; set; }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Fill<T>(T entity);

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        void FillCollection<T>(ICollection<T> collection) where T : new();

        /// <summary>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="keyField"></param>
        void FillDictionary<TKey, TEntity>(IDictionary<TKey, TEntity> dictionary, string keyField) where TEntity : new();
    }
}