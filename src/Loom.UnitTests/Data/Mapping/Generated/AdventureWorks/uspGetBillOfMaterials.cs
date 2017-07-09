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
    ///     This is an StoredProcedure class which wraps the dbo.uspGetBillOfMaterials procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "uspGetBillOfMaterials")]
    public class UspGetBillOfMaterials : StoredProcedure<UspGetBillOfMaterials>
    {
        [ActiveProcedureParameter("StartProductID", DbType.Int32, 0, ParameterType.In, false)]
        public int StartProductId { get; set; }

        [ActiveProcedureParameter("CheckDate", DbType.DateTime, 0, ParameterType.In, false)]
        public DateTime CheckDate { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter StartProductId => CreateParameter("StartProductId", typeof(UspGetBillOfMaterials));

            public static ICallableParameter CheckDate => CreateParameter("CheckDate", typeof(UspGetBillOfMaterials));
        }

        #endregion
    }
}