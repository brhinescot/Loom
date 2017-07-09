#region Using Directives

using System;
using System.Data;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace Pubs
{
    /// <summary>
    ///     This is an StoredProcedure class which wraps the dbo.reptq3 procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "reptq3")]
    public class Reptq3 : StoredProcedure<Reptq3>
    {
        [ActiveProcedureParameter("lolimit", DbType.Currency, 0, ParameterType.In, false)]
        public decimal Lolimit { get; set; }

        [ActiveProcedureParameter("hilimit", DbType.Currency, 0, ParameterType.In, false)]
        public decimal Hilimit { get; set; }

        [ActiveProcedureParameter("type", DbType.String, 12, ParameterType.In, false)]
        public string Type { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter Lolimit => CreateParameter("Lolimit", typeof(Reptq3));

            public static ICallableParameter Hilimit => CreateParameter("Hilimit", typeof(Reptq3));

            public static ICallableParameter Type => CreateParameter("Type", typeof(Reptq3));
        }

        #endregion
    }
}