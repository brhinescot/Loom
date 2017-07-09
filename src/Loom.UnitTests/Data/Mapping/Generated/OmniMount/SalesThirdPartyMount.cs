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

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.ThirdPartyMount table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "ThirdPartyMount", "ThirdPartyMountId")]
    public class ThirdPartyMount : DataRecord<ThirdPartyMount>
    {
        private int _companyId;
        private string _custom1;
        private string _custom2;
        private string _custom3;
        private string _description;
        private string _imageUrl;
        private string _manufacturer;
        private string _model;
        private string _navigateUrl;
        private int? _ordinal;

        private int _thirdPartyMountId;

        public ThirdPartyMount() { }
        protected ThirdPartyMount(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ThirdPartyMountId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ThirdPartyMountId
        {
            get => _thirdPartyMountId;
            set
            {
                if (value == _thirdPartyMountId && IsPropertyDirty("ThirdPartyMountId"))
                    return;

                _thirdPartyMountId = value;
                MarkDirty("ThirdPartyMountId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("CompanyId", typeof(Company), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        [ActiveColumn("Manufacturer", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (value == _manufacturer && IsPropertyDirty("Manufacturer"))
                    return;

                _manufacturer = value;
                MarkDirty("Manufacturer");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Model", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
        public string Model
        {
            get => _model;
            set
            {
                if (value == _model && IsPropertyDirty("Model"))
                    return;

                _model = value;
                MarkDirty("Model");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 4000)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description && IsPropertyDirty("Description"))
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ImageUrl", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 200)]
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value == _imageUrl && IsPropertyDirty("ImageUrl"))
                    return;

                _imageUrl = value;
                MarkDirty("ImageUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NavigateUrl", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 200)]
        public string NavigateUrl
        {
            get => _navigateUrl;
            set
            {
                if (value == _navigateUrl && IsPropertyDirty("NavigateUrl"))
                    return;

                _navigateUrl = value;
                MarkDirty("NavigateUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
        public int? Ordinal
        {
            get => _ordinal;
            set
            {
                if (value == _ordinal && IsPropertyDirty("Ordinal"))
                    return;

                _ordinal = value;
                MarkDirty("Ordinal");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Custom1", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 200)]
        public string Custom1
        {
            get => _custom1;
            set
            {
                if (value == _custom1 && IsPropertyDirty("Custom1"))
                    return;

                _custom1 = value;
                MarkDirty("Custom1");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Custom2", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 200)]
        public string Custom2
        {
            get => _custom2;
            set
            {
                if (value == _custom2 && IsPropertyDirty("Custom2"))
                    return;

                _custom2 = value;
                MarkDirty("Custom2");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Custom3", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 200)]
        public string Custom3
        {
            get => _custom3;
            set
            {
                if (value == _custom3 && IsPropertyDirty("Custom3"))
                    return;

                _custom3 = value;
                MarkDirty("Custom3");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ThirdPartyMountId => FetchColumn("ThirdPartyMountId");

            public static QueryColumn CompanyId => FetchColumn("CompanyId");

            public static QueryColumn Manufacturer => FetchColumn("Manufacturer");

            public static QueryColumn Model => FetchColumn("Model");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn ImageUrl => FetchColumn("ImageUrl");

            public static QueryColumn NavigateUrl => FetchColumn("NavigateUrl");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn Custom1 => FetchColumn("Custom1");

            public static QueryColumn Custom2 => FetchColumn("Custom2");

            public static QueryColumn Custom3 => FetchColumn("Custom3");
        }

        #endregion
    }
}