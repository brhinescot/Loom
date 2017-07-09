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
    ///     This is an DataRecord class which wraps the HumanResources.JobCandidate table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "JobCandidate", "JobCandidateID", ModifiedOnColumn = "ModifiedDate")]
    public class JobCandidate : DataRecord<JobCandidate>
    {
        private int? _businessEntityId;

        private int _jobCandidateId;
        private DateTime _modifiedDate;
        private string _resume;

        public JobCandidate() { }
        protected JobCandidate(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for JobCandidate records.
        /// </summary>
        [ActiveColumn("JobCandidateID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Employee), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        ///     Résumé in XML format.
        /// </summary>
        [ActiveColumn("Resume", DbType.String, ColumnProperties.Nullable, Ordinal = 3)]
        public string Resume
        {
            get => _resume;
            set
            {
                if (value == _resume && IsPropertyDirty("Resume"))
                    return;

                _resume = value;
                MarkDirty("Resume");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                if (value == _modifiedDate && IsPropertyDirty("ModifiedDate"))
                    return;

                _modifiedDate = value;
                MarkDirty("ModifiedDate");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn JobCandidateId => FetchColumn("JobCandidateID");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn Resume => FetchColumn("Resume");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}