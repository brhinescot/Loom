#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [DebuggerDisplay("Table Count={Tables.Count}, Procedure Count={Procedures.Count}")]
    public sealed class DatabaseSchema
    {
        public DatabaseSchema(TableDefinitionCollection tables, ProcedureDefinitionCollection procedures)
        {
            Argument.Assert.IsNotNull(tables, nameof(tables));
            Argument.Assert.IsNotNull(procedures, nameof(procedures));

            tables.EnsureLocalizableColumns();

            Tables = tables;
            Procedures = procedures;
        }

        public TableDefinitionCollection Tables { get; }

        public ProcedureDefinitionCollection Procedures { get; }
    }
}