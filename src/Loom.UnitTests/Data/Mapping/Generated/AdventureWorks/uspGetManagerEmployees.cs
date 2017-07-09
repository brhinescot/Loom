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
    ///     This is an StoredProcedure class which wraps the dbo.uspGetManagerEmployees procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "uspGetManagerEmployees")]
    public class UspGetManagerEmployees : StoredProcedure<UspGetManagerEmployees>
    {
        [ActiveProcedureParameter("BusinessEntityID", DbType.Int32, 0, ParameterType.In, false)]
        public int BusinessEntityId { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter BusinessEntityId => CreateParameter("BusinessEntityId", typeof(UspGetManagerEmployees));
        }

        #endregion
    }
}