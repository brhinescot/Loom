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
    ///     This is an StoredProcedure class which wraps the HumanResources.uspUpdateEmployeePersonalInfo procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("HumanResources", "uspUpdateEmployeePersonalInfo")]
    public class UspUpdateEmployeePersonalInfo : StoredProcedure<UspUpdateEmployeePersonalInfo>
    {
        [ActiveProcedureParameter("BusinessEntityID", DbType.Int32, 0, ParameterType.In, false)]
        public int BusinessEntityId { get; set; }

        [ActiveProcedureParameter("NationalIDNumber", DbType.String, 15, ParameterType.In, false)]
        public string NationalIdNumber { get; set; }

        [ActiveProcedureParameter("BirthDate", DbType.DateTime, 0, ParameterType.In, false)]
        public DateTime BirthDate { get; set; }

        [ActiveProcedureParameter("MaritalStatus", DbType.String, 1, ParameterType.In, false)]
        public string MaritalStatus { get; set; }

        [ActiveProcedureParameter("Gender", DbType.String, 1, ParameterType.In, false)]
        public string Gender { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter BusinessEntityId => CreateParameter("BusinessEntityId", typeof(UspUpdateEmployeePersonalInfo));

            public static ICallableParameter NationalIdNumber => CreateParameter("NationalIdNumber", typeof(UspUpdateEmployeePersonalInfo));

            public static ICallableParameter BirthDate => CreateParameter("BirthDate", typeof(UspUpdateEmployeePersonalInfo));

            public static ICallableParameter MaritalStatus => CreateParameter("MaritalStatus", typeof(UspUpdateEmployeePersonalInfo));

            public static ICallableParameter Gender => CreateParameter("Gender", typeof(UspUpdateEmployeePersonalInfo));
        }

        #endregion
    }
}