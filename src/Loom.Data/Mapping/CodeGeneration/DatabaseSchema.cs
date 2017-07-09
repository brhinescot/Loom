#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

using System.Diagnostics;

namespace Loom.Data.Mapping.CodeGeneration
{
    [DebuggerDisplay("Table Count={Tables.Count}, Procedure Count={Procedures.Count}")]
    public sealed class DatabaseSchema
    {
        private readonly TableDefinitionCollection tables;
        private readonly ProcedureDefinitionCollection procedures;

        public TableDefinitionCollection Tables
        {
            get { return tables; }
        }

        public ProcedureDefinitionCollection Procedures
        {
            get { return procedures; }
        }

        public DatabaseSchema(TableDefinitionCollection tables, ProcedureDefinitionCollection procedures)
        {
            Argument.Assert.IsNotNull(tables, Argument.Names.table);
            Argument.Assert.IsNotNull(procedures, Argument.Names.procedures);

            tables.EnsureLocalizableColumns();

            this.tables = tables;
            this.procedures = procedures;
        }
    }
}
