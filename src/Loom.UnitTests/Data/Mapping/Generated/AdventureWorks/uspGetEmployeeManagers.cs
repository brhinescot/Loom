#region Using Directives

using System;
using System.Data;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks
{
    /// <summary>
    ///     This is an StoredProcedure class which wraps the dbo.uspGetEmployeeManagers procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "uspGetEmployeeManagers")]
    public class UspGetEmployeeManagers : StoredProcedure<UspGetEmployeeManagers>
    {
        [ActiveProcedureParameter("BusinessEntityID", DbType.Int32, 0, ParameterType.In, false)]
        public int BusinessEntityId { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter BusinessEntityId => CreateParameter("BusinessEntityId", typeof(UspGetEmployeeManagers));
        }

        #endregion
    }
}