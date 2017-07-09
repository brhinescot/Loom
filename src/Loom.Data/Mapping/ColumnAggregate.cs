#region File Header

// *************************************************************************
// Copyright © 2008 Colossus Interactive, LLC
// All Rights Reserved
//    
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.colossusinteractive.com
// licensing@colossusinteractive.com
//  
// *************************************************************************

#endregion

#region Using Directives

using System.Data;
using System.Diagnostics;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    [DebuggerDisplay("{Table.Owner,nq}.{Table.Name,nq}.{Name,nq}, ColumnProperties={ColumnProperties}, ForeignKeyColumn={ForeignKeyColumn == null ? \"None\" : ForeignKeyColumn.Table.Owner + \".\" + ForeignKeyColumn.Table.Name + \".\" + ForeignKeyColumn.Name, nq}")]
    public abstract class ColumnAggregate : QueryColumn
    {
        #region Instance Fields

        protected readonly IQueryableColumn Column;

        #endregion

        #region Property Accessors

        public override string Alias
        {
            get { return Column.Alias; }
        }

        public override ColumnProperties ColumnProperties
        {
            get { return Column.ColumnProperties; }
        }

        public override DbType DbType
        {
            get { return Column.DbType; }
        }

        public override int MaxLength
        {
            get { return Column.MaxLength; }
        }

        public override string DefaultValue
        {
            get { return Column.DefaultValue; }
        }

        public override IQueryableColumn ForeignKeyColumn
        {
            get { return Column.ForeignKeyColumn; }
        }

        public override IQueryableColumn LocalizedColumn
        {
            get { return Column.LocalizedColumn; }
        }

        public override IQueryableColumn LocalizeFallbackColumn
        {
            get { return Column.LocalizeFallbackColumn; }
            set { Column.LocalizeFallbackColumn = value; }
        }

        public override string Name
        {
            get { return Column.Name; }
        }

        public override TableData Table
        {
            get { return Column.Table; }
            set { Column.Table = value; }
        }

        #endregion

        #region .ctor

        protected ColumnAggregate(IQueryableColumn column)
        {
            Column = column;
        }

        #endregion

        public override IQueryableColumn As(string columnAlias)
        {
            return Column.As(columnAlias);
        }
    }
}
