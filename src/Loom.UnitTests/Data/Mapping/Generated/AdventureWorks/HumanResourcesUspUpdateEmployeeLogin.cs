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
    ///     This is an StoredProcedure class which wraps the HumanResources.uspUpdateEmployeeLogin procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("HumanResources", "uspUpdateEmployeeLogin")]
    public class UspUpdateEmployeeLogin : StoredProcedure<UspUpdateEmployeeLogin>
    {
        [ActiveProcedureParameter("BusinessEntityID", DbType.Int32, 0, ParameterType.In, false)]
        public int BusinessEntityId { get; set; }

        [ActiveProcedureParameter("OrganizationNode", DbType.String, 892, ParameterType.In, false)]
        public string OrganizationNode { get; set; }

        [ActiveProcedureParameter("LoginID", DbType.String, 256, ParameterType.In, false)]
        public string LoginId { get; set; }

        [ActiveProcedureParameter("JobTitle", DbType.String, 50, ParameterType.In, false)]
        public string JobTitle { get; set; }

        [ActiveProcedureParameter("HireDate", DbType.DateTime, 0, ParameterType.In, false)]
        public DateTime HireDate { get; set; }

        [ActiveProcedureParameter("CurrentFlag", DbType.Boolean, 0, ParameterType.In, false)]
        public bool CurrentFlag { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter BusinessEntityId => CreateParameter("BusinessEntityId", typeof(UspUpdateEmployeeLogin));

            public static ICallableParameter OrganizationNode => CreateParameter("OrganizationNode", typeof(UspUpdateEmployeeLogin));

            public static ICallableParameter LoginId => CreateParameter("LoginId", typeof(UspUpdateEmployeeLogin));

            public static ICallableParameter JobTitle => CreateParameter("JobTitle", typeof(UspUpdateEmployeeLogin));

            public static ICallableParameter HireDate => CreateParameter("HireDate", typeof(UspUpdateEmployeeLogin));

            public static ICallableParameter CurrentFlag => CreateParameter("CurrentFlag", typeof(UspUpdateEmployeeLogin));
        }

        #endregion
    }
}