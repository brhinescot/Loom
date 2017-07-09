#region Using Directives

using System;

#endregion

namespace Loom.Data
{
    /// <summary>
    ///     Represents values to ignore when building conditional queries.
    /// </summary>
    /// <example>
    ///     The following example demonstrates usage of the <see cref="Ignore" /> enumeration.
    ///     <code>
    /// public IDataReader RetrieveData(string departmentName, DataSession session)
    /// {
    ///     session.Department
    ///         .Where(Department.Columns.DepartmentID == 1)
    ///         .And(Department.Columns.Name).IsEqualTo(departmentName, Ignore.NullOrEmpty)
    ///         .End()
    ///         .ExecuteReader();
    /// }
    /// </code>
    ///     <para>
    ///         If departmentName is null or an empty string, the following query will
    ///         be generated for SQL Server:
    ///     </para>
    ///     <code>
    ///     SELECT * FROM [HumanResources].[Department] _t0
    ///     WHERE _t0.[DepartmentID] = @p0;
    /// </code>
    ///     <para>
    ///         If departmentName is NOT null, the following query will be generated
    ///         for SQL Server:
    ///     </para>
    ///     <code>
    ///     SELECT * FROM [HumanResources].[Department] _t0
    ///     WHERE _t0.[DepartmentID] = @p0
    ///     AND _t0.[Name] = @p1; 
    /// </code>
    /// </example>
    [Flags]
    public enum Ignore
    {
        /// <summary>
        ///     Ignore no values.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Ignore null values in conditional queries.
        /// </summary>
        Null = 1,

        /// <summary>
        ///     Ignore empty <see cref="string" /> objects in conditional queries.
        /// </summary>
        Empty = 2,

        /// <summary>
        ///     Ignore numeric values that equal zero in conditional queries.
        /// </summary>
        Zero = 4,

        /// <summary>
        ///     Ignore <see cref="DateTime" /> objects that are equal to the default value
        ///     <see cref="DateTime.MinValue" /> in conditional queries.
        /// </summary>
        MinDate = 8,

        /// <summary>
        ///     Ignore null values and empty <see cref="string" /> objects in conditional queries.
        /// </summary>
        NullOrEmpty = Null | Empty,

        /// <summary>
        ///     Ignore null values and numeric values that equal zero in conditional queries.
        /// </summary>
        NullOrZero = Null | Zero,

        /// <summary>
        ///     Ignore null values and <see cref="DateTime" /> objects that are equal to the default value
        ///     <see cref="DateTime.MinValue" /> in conditional queries
        /// </summary>
        NullOrMinDate = Null | MinDate
    }
}