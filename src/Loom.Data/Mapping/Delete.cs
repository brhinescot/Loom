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

#region Using Directives

using System;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    /// <summary>
    /// Represents a class for deleting records in a datasource.
    /// </summary>
    /// <example>
    /// The following examples demonstrate how to use the <see cref="Delete"/> class.
    /// <code>
    /// Delete.From(Employee.Table).Execute();
    /// 
    /// Delete.From(Employee.Table).Where(Employee.Columns.EmployeeID).EqualTo(1).End().Execute();
    /// 
    /// Delete employeeDelete = Delete.From(Employee.Table).
    ///     Where(Employee.Columns.EmployeeID).IsEqualTo(1).
    ///         Or(Employee.Columns.Title).IsNotEqualTo("Manager").
    ///     End();
    /// 
    /// employeeDelete.Execute();
    /// </code>
    /// </example>
    public sealed class Delete : CommandTree<Delete>
    {
        private bool obliterate;

        public bool Obliterate
        {
            get { return obliterate; }
            set { obliterate = value; }
        }

        #region .ctor and Factory Method

        private Delete(TableData table) : base(table) { }

        /// <summary>
        /// Indicates a table in the datasource to delete records from.
        /// </summary>
        /// <param name="table">An <see cref="ISchema"/> instance representing the table from which records will be deleted.</param>
        /// <returns>A new <see cref="Delete"/> instance for performing the delete operations.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="table"/> is null (Nothing in VisualBasic).</exception>
        public static Delete From(TableData table)
        {
            Argument.Assert.IsNotNull(table, Argument.Names.table);

            return From(table, false);
        }

        /// <summary>
        /// Indicates a table in the datasource to delete records from.
        /// </summary>
        /// <param name="table">An <see cref="ISchema"/> instance representing the table from which records will be deleted.</param>
        /// <returns>A new <see cref="Delete"/> instance for performing the delete operations.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="table"/> is null (Nothing in VisualBasic).</exception>
        /// <param name="obliterateRecord"></param>
        public static Delete From(TableData table, bool obliterateRecord)
        {
            Argument.Assert.IsNotNull(table, Argument.Names.table);

            return new Delete(table) { obliterate = obliterateRecord };
        }

        #endregion
    }
}
