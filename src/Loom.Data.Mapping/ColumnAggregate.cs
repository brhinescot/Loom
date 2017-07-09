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
        protected readonly IQueryableColumn Column;

        protected ColumnAggregate(IQueryableColumn column)
        {
            Column = column;
        }

        public override string Alias => Column.Alias;

        public override ColumnProperties ColumnProperties => Column.ColumnProperties;

        public override DbType DbType => Column.DbType;

        public override int MaxLength => Column.MaxLength;

        public override string DefaultValue => Column.DefaultValue;

        public override IQueryableColumn ForeignKeyColumn => Column.ForeignKeyColumn;

        public override IQueryableColumn LocalizedColumn => Column.LocalizedColumn;

        public override IQueryableColumn LocalizeFallbackColumn
        {
            get => Column.LocalizeFallbackColumn;
            set => Column.LocalizeFallbackColumn = value;
        }

        public override string Name => Column.Name;

        public override ITable Table
        {
            get => Column.Table;
            set => Column.Table = value;
        }

        public override IQueryableColumn As(string columnAlias)
        {
            return Column.As(columnAlias);
        }
    }
}