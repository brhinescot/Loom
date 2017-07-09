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
    ///     This is an DataRecord class which wraps the HumanResources.vJobCandidateEducation table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "vJobCandidateEducation", ReadOnly = true)]
    public class VJobCandidateEducation : DataRecord<VJobCandidateEducation>
    {
        private string _eduDegree;
        private DateTime? _eduEndDate;
        private string _eduGPA;
        private string _eduGPAScale;
        private string _eduLevel;
        private string _eduLocCity;
        private string _eduLocCountryRegion;
        private string _eduLocState;
        private string _eduMajor;
        private string _eduMinor;

        private string _eduSchool;
        private DateTime? _eduStartDate;
        private int _jobCandidateId;

        public VJobCandidateEducation() { }
        protected VJobCandidateEducation(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.School", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 100)]
        public string EduSchool
        {
            get => _eduSchool;
            set
            {
                if (value == _eduSchool && IsPropertyDirty("Edu.School"))
                    return;

                _eduSchool = value;
                MarkDirty("Edu.School");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.Minor", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 50)]
        public string EduMinor
        {
            get => _eduMinor;
            set
            {
                if (value == _eduMinor && IsPropertyDirty("Edu.Minor"))
                    return;

                _eduMinor = value;
                MarkDirty("Edu.Minor");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.Loc.City", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 100)]
        public string EduLocCity
        {
            get => _eduLocCity;
            set
            {
                if (value == _eduLocCity && IsPropertyDirty("Edu.Loc.City"))
                    return;

                _eduLocCity = value;
                MarkDirty("Edu.Loc.City");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.Level", DbType.String, ColumnProperties.Nullable, Ordinal = 2)]
        public string EduLevel
        {
            get => _eduLevel;
            set
            {
                if (value == _eduLevel && IsPropertyDirty("Edu.Level"))
                    return;

                _eduLevel = value;
                MarkDirty("Edu.Level");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.Degree", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
        public string EduDegree
        {
            get => _eduDegree;
            set
            {
                if (value == _eduDegree && IsPropertyDirty("Edu.Degree"))
                    return;

                _eduDegree = value;
                MarkDirty("Edu.Degree");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.EndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public DateTime? EduEndDate
        {
            get => _eduEndDate;
            set
            {
                if (value == _eduEndDate && IsPropertyDirty("Edu.EndDate"))
                    return;

                _eduEndDate = value;
                MarkDirty("Edu.EndDate");
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
        [ActiveColumn("Edu.Loc.CountryRegion", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 100)]
        public string EduLocCountryRegion
        {
            get => _eduLocCountryRegion;
            set
            {
                if (value == _eduLocCountryRegion && IsPropertyDirty("Edu.Loc.CountryRegion"))
                    return;

                _eduLocCountryRegion = value;
                MarkDirty("Edu.Loc.CountryRegion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.GPA", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 5)]
        public string EduGPA
        {
            get => _eduGPA;
            set
            {
                if (value == _eduGPA && IsPropertyDirty("Edu.GPA"))
                    return;

                _eduGPA = value;
                MarkDirty("Edu.GPA");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.GPAScale", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 5)]
        public string EduGPAScale
        {
            get => _eduGPAScale;
            set
            {
                if (value == _eduGPAScale && IsPropertyDirty("Edu.GPAScale"))
                    return;

                _eduGPAScale = value;
                MarkDirty("Edu.GPAScale");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.Loc.State", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 100)]
        public string EduLocState
        {
            get => _eduLocState;
            set
            {
                if (value == _eduLocState && IsPropertyDirty("Edu.Loc.State"))
                    return;

                _eduLocState = value;
                MarkDirty("Edu.Loc.State");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.StartDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public DateTime? EduStartDate
        {
            get => _eduStartDate;
            set
            {
                if (value == _eduStartDate && IsPropertyDirty("Edu.StartDate"))
                    return;

                _eduStartDate = value;
                MarkDirty("Edu.StartDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Edu.Major", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 50)]
        public string EduMajor
        {
            get => _eduMajor;
            set
            {
                if (value == _eduMajor && IsPropertyDirty("Edu.Major"))
                    return;

                _eduMajor = value;
                MarkDirty("Edu.Major");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn EduSchool => FetchColumn("Edu.School");

            public static QueryColumn EduMinor => FetchColumn("Edu.Minor");

            public static QueryColumn EduLocCity => FetchColumn("Edu.Loc.City");

            public static QueryColumn EduLevel => FetchColumn("Edu.Level");

            public static QueryColumn EduDegree => FetchColumn("Edu.Degree");

            public static QueryColumn EduEndDate => FetchColumn("Edu.EndDate");

            public static QueryColumn JobCandidateId => FetchColumn("JobCandidateID");

            public static QueryColumn EduLocCountryRegion => FetchColumn("Edu.Loc.CountryRegion");

            public static QueryColumn EduGPA => FetchColumn("Edu.GPA");

            public static QueryColumn EduGPAScale => FetchColumn("Edu.GPAScale");

            public static QueryColumn EduLocState => FetchColumn("Edu.Loc.State");

            public static QueryColumn EduStartDate => FetchColumn("Edu.StartDate");

            public static QueryColumn EduMajor => FetchColumn("Edu.Major");
        }

        #endregion
    }
}