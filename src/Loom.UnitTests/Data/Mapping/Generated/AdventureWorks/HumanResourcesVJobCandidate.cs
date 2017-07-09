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
    ///     This is an DataRecord class which wraps the HumanResources.vJobCandidate table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "vJobCandidate", ReadOnly = true, ModifiedOnColumn = "ModifiedDate")]
    public class VJobCandidate : DataRecord<VJobCandidate>
    {
        private string _addrLocCity;
        private string _addrLocCountryRegion;
        private string _addrLocState;
        private string _addrPostalCode;
        private string _addrType;

        private int? _businessEntityId;
        private string _eMail;
        private int _jobCandidateId;
        private DateTime _modifiedDate;
        private string _nameFirst;
        private string _nameLast;
        private string _nameMiddle;
        private string _namePrefix;
        private string _nameSuffix;
        private string _skills;
        private string _webSite;

        public VJobCandidate() { }
        protected VJobCandidate(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("Skills", DbType.String, ColumnProperties.Nullable, Ordinal = 8)]
        public string Skills
        {
            get => _skills;
            set
            {
                if (value == _skills && IsPropertyDirty("Skills"))
                    return;

                _skills = value;
                MarkDirty("Skills");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name.Middle", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 30)]
        public string NameMiddle
        {
            get => _nameMiddle;
            set
            {
                if (value == _nameMiddle && IsPropertyDirty("Name.Middle"))
                    return;

                _nameMiddle = value;
                MarkDirty("Name.Middle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Addr.Loc.State", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 100)]
        public string AddrLocState
        {
            get => _addrLocState;
            set
            {
                if (value == _addrLocState && IsPropertyDirty("Addr.Loc.State"))
                    return;

                _addrLocState = value;
                MarkDirty("Addr.Loc.State");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name.Suffix", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 30)]
        public string NameSuffix
        {
            get => _nameSuffix;
            set
            {
                if (value == _nameSuffix && IsPropertyDirty("Name.Suffix"))
                    return;

                _nameSuffix = value;
                MarkDirty("Name.Suffix");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name.First", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 30)]
        public string NameFirst
        {
            get => _nameFirst;
            set
            {
                if (value == _nameFirst && IsPropertyDirty("Name.First"))
                    return;

                _nameFirst = value;
                MarkDirty("Name.First");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Addr.PostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 20)]
        public string AddrPostalCode
        {
            get => _addrPostalCode;
            set
            {
                if (value == _addrPostalCode && IsPropertyDirty("Addr.PostalCode"))
                    return;

                _addrPostalCode = value;
                MarkDirty("Addr.PostalCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Addr.Loc.CountryRegion", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 100)]
        public string AddrLocCountryRegion
        {
            get => _addrLocCountryRegion;
            set
            {
                if (value == _addrLocCountryRegion && IsPropertyDirty("Addr.Loc.CountryRegion"))
                    return;

                _addrLocCountryRegion = value;
                MarkDirty("Addr.Loc.CountryRegion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 16, MaxLength = 0)]
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
        [ActiveColumn("WebSite", DbType.String, ColumnProperties.Nullable, Ordinal = 15)]
        public string WebSite
        {
            get => _webSite;
            set
            {
                if (value == _webSite && IsPropertyDirty("WebSite"))
                    return;

                _webSite = value;
                MarkDirty("WebSite");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Addr.Type", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 30)]
        public string AddrType
        {
            get => _addrType;
            set
            {
                if (value == _addrType && IsPropertyDirty("Addr.Type"))
                    return;

                _addrType = value;
                MarkDirty("Addr.Type");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Addr.Loc.City", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 100)]
        public string AddrLocCity
        {
            get => _addrLocCity;
            set
            {
                if (value == _addrLocCity && IsPropertyDirty("Addr.Loc.City"))
                    return;

                _addrLocCity = value;
                MarkDirty("Addr.Loc.City");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name.Last", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 30)]
        public string NameLast
        {
            get => _nameLast;
            set
            {
                if (value == _nameLast && IsPropertyDirty("Name.Last"))
                    return;

                _nameLast = value;
                MarkDirty("Name.Last");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name.Prefix", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 30)]
        public string NamePrefix
        {
            get => _namePrefix;
            set
            {
                if (value == _namePrefix && IsPropertyDirty("Name.Prefix"))
                    return;

                _namePrefix = value;
                MarkDirty("Name.Prefix");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EMail", DbType.String, ColumnProperties.Nullable, Ordinal = 14)]
        public string EMail
        {
            get => _eMail;
            set
            {
                if (value == _eMail && IsPropertyDirty("EMail"))
                    return;

                _eMail = value;
                MarkDirty("EMail");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn Skills => FetchColumn("Skills");

            public static QueryColumn NameMiddle => FetchColumn("Name.Middle");

            public static QueryColumn AddrLocState => FetchColumn("Addr.Loc.State");

            public static QueryColumn NameSuffix => FetchColumn("Name.Suffix");

            public static QueryColumn NameFirst => FetchColumn("Name.First");

            public static QueryColumn AddrPostalCode => FetchColumn("Addr.PostalCode");

            public static QueryColumn AddrLocCountryRegion => FetchColumn("Addr.Loc.CountryRegion");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");

            public static QueryColumn JobCandidateId => FetchColumn("JobCandidateID");

            public static QueryColumn WebSite => FetchColumn("WebSite");

            public static QueryColumn AddrType => FetchColumn("Addr.Type");

            public static QueryColumn AddrLocCity => FetchColumn("Addr.Loc.City");

            public static QueryColumn NameLast => FetchColumn("Name.Last");

            public static QueryColumn NamePrefix => FetchColumn("Name.Prefix");

            public static QueryColumn EMail => FetchColumn("EMail");
        }

        #endregion
    }
}