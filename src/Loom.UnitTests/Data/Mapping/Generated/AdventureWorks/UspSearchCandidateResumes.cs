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
    ///     This is an StoredProcedure class which wraps the dbo.uspSearchCandidateResumes procedure.
    /// </summary>
    [Serializable]
    [ActiveProcedure("dbo", "uspSearchCandidateResumes")]
    public class UspSearchCandidateResumes : StoredProcedure<UspSearchCandidateResumes>
    {
        [ActiveProcedureParameter("searchString", DbType.String, 1000, ParameterType.In, false)]
        public string SearchString { get; set; }

        [ActiveProcedureParameter("useInflectional", DbType.Boolean, 0, ParameterType.In, false)]
        public bool UseInflectional { get; set; }

        [ActiveProcedureParameter("useThesaurus", DbType.Boolean, 0, ParameterType.In, false)]
        public bool UseThesaurus { get; set; }

        [ActiveProcedureParameter("language", DbType.Int32, 0, ParameterType.In, false)]
        public int Language { get; set; }

        #region Nested type: Parameters

        public struct Parameters
        {
            public static ICallableParameter SearchString => CreateParameter("SearchString", typeof(UspSearchCandidateResumes));

            public static ICallableParameter UseInflectional => CreateParameter("UseInflectional", typeof(UspSearchCandidateResumes));

            public static ICallableParameter UseThesaurus => CreateParameter("UseThesaurus", typeof(UspSearchCandidateResumes));

            public static ICallableParameter Language => CreateParameter("Language", typeof(UspSearchCandidateResumes));
        }

        #endregion
    }
}