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
    ///     This is an StoredProcedure class which wraps the dbo.uspGetWhereUsedProductID procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "uspGetWhereUsedProductID")]
    public class UspGetWhereUsedProductId : StoredProcedure<UspGetWhereUsedProductId>
    {
        [ActiveProcedureParameter("StartProductID", DbType.Int32, 0, ParameterType.In, false)]
        public int StartProductId { get; set; }

        [ActiveProcedureParameter("CheckDate", DbType.DateTime, 0, ParameterType.In, false)]
        public DateTime CheckDate { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter StartProductId => CreateParameter("StartProductId", typeof(UspGetWhereUsedProductId));

            public static ICallableParameter CheckDate => CreateParameter("CheckDate", typeof(UspGetWhereUsedProductId));
        }

        #endregion
    }
}