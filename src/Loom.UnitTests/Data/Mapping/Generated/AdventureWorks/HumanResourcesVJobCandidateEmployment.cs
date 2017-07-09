#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.HumanResources
{
    /// <summary>
    ///     This is an DataRecord class which wraps the HumanResources.vJobCandidateEmployment table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "vJobCandidateEmployment", ReadOnly = true)]
    public class VJobCandidateEmployment : DataRecord<VJobCandidateEmployment>
    {
        private DateTime? _empEndDate;
        private string _empFunctionCategory;
        private string _empIndustryCategory;
        private string _empJobTitle;
        private string _empLocCity;
        private string _empLocCountryRegion;
        private string _empLocState;
        private string _empOrgName;

        private string _empResponsibility;
        private DateTime? _empStartDate;
        private int _jobCandidateId;

        public VJobCandidateEmployment() { }
        protected VJobCandidateEmployment(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.Responsibility", DbType.String, ColumnProperties.Nullable, Ordinal = 6)]
        public string EmpResponsibility
        {
            get => _empResponsibility;
            set
            {
                if (value == _empResponsibility && IsPropertyDirty("Emp.Responsibility"))
                    return;

                _empResponsibility = value;
                MarkDirty("Emp.Responsibility");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.EndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public DateTime? EmpEndDate
        {
            get => _empEndDate;
            set
            {
                if (value == _empEndDate && IsPropertyDirty("Emp.EndDate"))
                    return;

                _empEndDate = value;
                MarkDirty("Emp.EndDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.IndustryCategory", DbType.String, ColumnProperties.Nullable, Ordinal = 8)]
        public string EmpIndustryCategory
        {
            get => _empIndustryCategory;
            set
            {
                if (value == _empIndustryCategory && IsPropertyDirty("Emp.IndustryCategory"))
                    return;

                _empIndustryCategory = value;
                MarkDirty("Emp.IndustryCategory");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.Loc.CountryRegion", DbType.String, ColumnProperties.Nullable, Ordinal = 9)]
        public string EmpLocCountryRegion
        {
            get => _empLocCountryRegion;
            set
            {
                if (value == _empLocCountryRegion && IsPropertyDirty("Emp.Loc.CountryRegion"))
                    return;

                _empLocCountryRegion = value;
                MarkDirty("Emp.Loc.CountryRegion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.Loc.City", DbType.String, ColumnProperties.Nullable, Ordinal = 11)]
        public string EmpLocCity
        {
            get => _empLocCity;
            set
            {
                if (value == _empLocCity && IsPropertyDirty("Emp.Loc.City"))
                    return;

                _empLocCity = value;
                MarkDirty("Emp.Loc.City");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("JobCandidateID", DbType.Int32, ColumnProperties.Identity, Ordinal = 1, MaxLength = 0)]
        public int JobCandidateId
        {
            get => _jobCandidateId;
            set
            {
                if (value == _jobCandidateId && IsPropertyDirty("JobCandidateID"))
                    return;

                _jobCandidateId = value;
                MarkDirty("JobCandidateID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.JobTitle", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 100)]
        public string EmpJobTitle
        {
            get => _empJobTitle;
            set
            {
                if (value == _empJobTitle && IsPropertyDirty("Emp.JobTitle"))
                    return;

                _empJobTitle = value;
                MarkDirty("Emp.JobTitle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.StartDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public DateTime? EmpStartDate
        {
            get => _empStartDate;
            set
            {
                if (value == _empStartDate && IsPropertyDirty("Emp.StartDate"))
                    return;

                _empStartDate = value;
                MarkDirty("Emp.StartDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.FunctionCategory", DbType.String, ColumnProperties.Nullable, Ordinal = 7)]
        public string EmpFunctionCategory
        {
            get => _empFunctionCategory;
            set
            {
                if (value == _empFunctionCategory && IsPropertyDirty("Emp.FunctionCategory"))
                    return;

                _empFunctionCategory = value;
                MarkDirty("Emp.FunctionCategory");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.OrgName", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 100)]
        public string EmpOrgName
        {
            get => _empOrgName;
            set
            {
                if (value == _empOrgName && IsPropertyDirty("Emp.OrgName"))
                    return;

                _empOrgName = value;
                MarkDirty("Emp.OrgName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Emp.Loc.State", DbType.String, ColumnProperties.Nullable, Ordinal = 10)]
        public string EmpLocState
        {
            get => _empLocState;
            set
            {
                if (value == _empLocState && IsPropertyDirty("Emp.Loc.State"))
                    return;

                _empLocState = value;
                MarkDirty("Emp.Loc.State");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn EmpResponsibility => FetchColumn("Emp.Responsibility");

            public static QueryColumn EmpEndDate => FetchColumn("Emp.EndDate");

            public static QueryColumn EmpIndustryCategory => FetchColumn("Emp.IndustryCategory");

            public static QueryColumn EmpLocCountryRegion => FetchColumn("Emp.Loc.CountryRegion");

            public static QueryColumn EmpLocCity => FetchColumn("Emp.Loc.City");

            public static QueryColumn JobCandidateId => FetchColumn("JobCandidateID");

            public static QueryColumn EmpJobTitle => FetchColumn("Emp.JobTitle");

            public static QueryColumn EmpStartDate => FetchColumn("Emp.StartDate");

            public static QueryColumn EmpFunctionCategory => FetchColumn("Emp.FunctionCategory");

            public static QueryColumn EmpOrgName => FetchColumn("Emp.OrgName");

            public static QueryColumn EmpLocState => FetchColumn("Emp.Loc.State");
        }

        #endregion
    }
}