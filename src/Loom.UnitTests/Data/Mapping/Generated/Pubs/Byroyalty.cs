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
    ///     This is an StoredProcedure class which wraps the dbo.byroyalty procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "byroyalty")]
    public class Byroyalty : StoredProcedure<Byroyalty>
    {
        [ActiveProcedureParameter("percentage", DbType.Int32, 0, ParameterType.In, false)]
        public int Percentage { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter Percentage => CreateParameter("Percentage", typeof(Byroyalty));
        }

        #endregion
    }
}