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
    ///     This is an StoredProcedure class which wraps the dbo.uspLogError procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "uspLogError")]
    public class UspLogError : StoredProcedure<UspLogError>
    {
        [ActiveProcedureParameter("ErrorLogID", DbType.Int32, 0, ParameterType.InOut, false)]
        public int ErrorLogId { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter ErrorLogId => CreateParameter("ErrorLogId", typeof(UspLogError));
        }

        #endregion
    }
}