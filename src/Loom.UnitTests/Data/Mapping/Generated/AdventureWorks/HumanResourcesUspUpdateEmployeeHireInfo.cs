#region Using Directives

using System;
using System.Data;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.HumanResources
{
    /// <summary>
    ///     This is an StoredProcedure class which wraps the HumanResources.uspUpdateEmployeeHireInfo procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("HumanResources", "uspUpdateEmployeeHireInfo")]
    public class UspUpdateEmployeeHireInfo : StoredProcedure<UspUpdateEmployeeHireInfo>
    {
        [ActiveProcedureParameter("BusinessEntityID", DbType.Int32, 0, ParameterType.In, false)]
        public int BusinessEntityId { get; set; }

        [ActiveProcedureParameter("JobTitle", DbType.String, 50, ParameterType.In, false)]
        public string JobTitle { get; set; }

        [ActiveProcedureParameter("HireDate", DbType.DateTime, 0, ParameterType.In, false)]
        public DateTime HireDate { get; set; }

        [ActiveProcedureParameter("RateChangeDate", DbType.DateTime, 0, ParameterType.In, false)]
        public DateTime RateChangeDate { get; set; }

        [ActiveProcedureParameter("Rate", DbType.Currency, 0, ParameterType.In, false)]
        public decimal Rate { get; set; }

        [ActiveProcedureParameter("PayFrequency", DbType.Int16, 0, ParameterType.In, false)]
        public short PayFrequency { get; set; }

        [ActiveProcedureParameter("CurrentFlag", DbType.Boolean, 0, ParameterType.In, false)]
        public bool CurrentFlag { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter BusinessEntityId => CreateParameter("BusinessEntityId", typeof(UspUpdateEmployeeHireInfo));

            public static ICallableParameter JobTitle => CreateParameter("JobTitle", typeof(UspUpdateEmployeeHireInfo));

            public static ICallableParameter HireDate => CreateParameter("HireDate", typeof(UspUpdateEmployeeHireInfo));

            public static ICallableParameter RateChangeDate => CreateParameter("RateChangeDate", typeof(UspUpdateEmployeeHireInfo));

            public static ICallableParameter Rate => CreateParameter("Rate", typeof(UspUpdateEmployeeHireInfo));

            public static ICallableParameter PayFrequency => CreateParameter("PayFrequency", typeof(UspUpdateEmployeeHireInfo));

            public static ICallableParameter CurrentFlag => CreateParameter("CurrentFlag", typeof(UspUpdateEmployeeHireInfo));
        }

        #endregion
    }
}