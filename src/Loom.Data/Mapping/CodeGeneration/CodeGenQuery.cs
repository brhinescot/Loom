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

namespace Loom.Data.Mapping.CodeGeneration
{
    public sealed class CodeGenQuery
    {
        private readonly TableDefinition table;

        #region Internal Property Accessors

        public TableDefinition Table
        {
            get { return table; }
        }

        #endregion

        #region .ctor

        private CodeGenQuery(TableDefinition table)
        {
            this.table = table;
        }

        #endregion

        #region Factory Methods

        ///<summary>
        ///</summary>
        ///<param name="table"></param>
        ///<returns></returns>
        public static CodeGenQuery From(TableDefinition table)
        {
            Argument.Assert.IsNotNull(table, Argument.Names.table);

            return new CodeGenQuery(table);
        }

        #endregion
    }
}
