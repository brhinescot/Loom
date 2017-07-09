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

namespace OmniMount.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.Award table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Award", "AwardId")]
    public class Award : DataRecord<Award>
    {
        private int _awardId;
        private string _description;
        private string _imageUrl;
        private bool? _isLive;
        private string _name;
        private int? _year;

        public Award() { }
        protected Award(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("AwardId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int AwardId
        {
            get => _awardId;
            set
            {
                if (value == _awardId && IsPropertyDirty("AwardId"))
                    return;

                _awardId = value;
                MarkDirty("AwardId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 80)]
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
        [ActiveColumn("ImageUrl", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 200)]
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
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 700)]
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
        [ActiveColumn("IsLive", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public bool? IsLive
        {
            get => _isLive;
            set
            {
                if (value == _isLive && IsPropertyDirty("IsLive"))
                    return;

                _isLive = value;
                MarkDirty("IsLive");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Year", DbType.Int32, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public int? Year
        {
            get => _year;
            set
            {
                if (value == _year && IsPropertyDirty("Year"))
                    return;

                _year = value;
                MarkDirty("Year");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn AwardId => FetchColumn("AwardId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ImageUrl => FetchColumn("ImageUrl");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn IsLive => FetchColumn("IsLive");

            public static QueryColumn Year => FetchColumn("Year");
        }

        #endregion
    }
}