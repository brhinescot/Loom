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
using OmniMount.Portal;

#endregion

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.Company table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "Company", "CompanyId")]
    public class Company : DataRecord<Company>
    {
        private string _apiEmail;
        private string _apiKey;
        private int _applicationId;

        private int _companyId;
        private int _companyTypeId;
        private string _mountFinderName;
        private string _name;
        private int? _userId;
        private string _website;

        public Company() { }
        protected Company(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int CompanyId
        {
            get => _companyId;
            set
            {
                if (value == _companyId && IsPropertyDirty("CompanyId"))
                    return;

                _companyId = value;
                MarkDirty("CompanyId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 100)]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name && IsPropertyDirty("Name"))
                    return;

                _name = value;
                MarkDirty("Name");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Website", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 200)]
        public string Website
        {
            get => _website;
            set
            {
                if (value == _website && IsPropertyDirty("Website"))
                    return;

                _website = value;
                MarkDirty("Website");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyTypeId", DbType.Int32, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public int CompanyTypeId
        {
            get => _companyTypeId;
            set
            {
                if (value == _companyTypeId && IsPropertyDirty("CompanyTypeId"))
                    return;

                _companyTypeId = value;
                MarkDirty("CompanyTypeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApiKey", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 90)]
        public string ApiKey
        {
            get => _apiKey;
            set
            {
                if (value == _apiKey && IsPropertyDirty("ApiKey"))
                    return;

                _apiKey = value;
                MarkDirty("ApiKey");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UserId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? UserId
        {
            get => _userId;
            set
            {
                if (value == _userId && IsPropertyDirty("UserId"))
                    return;

                _userId = value;
                MarkDirty("UserId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApiEmail", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 150)]
        public string ApiEmail
        {
            get => _apiEmail;
            set
            {
                if (value == _apiEmail && IsPropertyDirty("ApiEmail"))
                    return;

                _apiEmail = value;
                MarkDirty("ApiEmail");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 8, MaxLength = 0, DefaultValue = "((1))")]
        [ForeignColumn("ApplicationId", typeof(Application), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                if (value == _applicationId && IsPropertyDirty("ApplicationId"))
                    return;

                _applicationId = value;
                MarkDirty("ApplicationId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MountFinderName", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 20)]
        public string MountFinderName
        {
            get => _mountFinderName;
            set
            {
                if (value == _mountFinderName && IsPropertyDirty("MountFinderName"))
                    return;

                _mountFinderName = value;
                MarkDirty("MountFinderName");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CompanyId => FetchColumn("CompanyId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Website => FetchColumn("Website");

            public static QueryColumn CompanyTypeId => FetchColumn("CompanyTypeId");

            public static QueryColumn ApiKey => FetchColumn("ApiKey");

            public static QueryColumn UserId => FetchColumn("UserId");

            public static QueryColumn ApiEmail => FetchColumn("ApiEmail");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");

            public static QueryColumn MountFinderName => FetchColumn("MountFinderName");
        }

        #endregion
    }
}