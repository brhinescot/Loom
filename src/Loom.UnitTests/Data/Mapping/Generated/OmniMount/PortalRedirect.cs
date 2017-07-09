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

namespace OmniMount.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.Redirect table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Redirect", "RedirectId", CreatedOnColumn = "CreatedOn", CreatedByColumn = "CreatedBy", ModifiedOnColumn = "ModifiedOn", ModifiedByColumn = "ModifiedBy")]
    public class Redirect : DataRecord<Redirect>
    {
        private int _applicationId;
        private string _createdBy;
        private DateTime _createdOn;
        private string _fromUrl;
        private string _modifiedBy;
        private DateTime _modifiedOn;
        private string _note;
        private bool _permanent;

        private int _redirectId;
        private string _toUrl;

        public Redirect() { }
        protected Redirect(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RedirectId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int RedirectId
        {
            get => _redirectId;
            set
            {
                if (value == _redirectId && IsPropertyDirty("RedirectId"))
                    return;

                _redirectId = value;
                MarkDirty("RedirectId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FromUrl", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 400)]
        public string FromUrl
        {
            get => _fromUrl;
            set
            {
                if (value == _fromUrl && IsPropertyDirty("FromUrl"))
                    return;

                _fromUrl = value;
                MarkDirty("FromUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ToUrl", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 400)]
        public string ToUrl
        {
            get => _toUrl;
            set
            {
                if (value == _toUrl && IsPropertyDirty("ToUrl"))
                    return;

                _toUrl = value;
                MarkDirty("ToUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Permanent", DbType.Boolean, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public bool Permanent
        {
            get => _permanent;
            set
            {
                if (value == _permanent && IsPropertyDirty("Permanent"))
                    return;

                _permanent = value;
                MarkDirty("Permanent");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime CreatedOn
        {
            get => _createdOn;
            set
            {
                if (value == _createdOn && IsPropertyDirty("CreatedOn"))
                    return;

                _createdOn = value;
                MarkDirty("CreatedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedBy", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 30)]
        public string CreatedBy
        {
            get => _createdBy;
            set
            {
                if (value == _createdBy && IsPropertyDirty("CreatedBy"))
                    return;

                _createdBy = value;
                MarkDirty("CreatedBy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedOn
        {
            get => _modifiedOn;
            set
            {
                if (value == _modifiedOn && IsPropertyDirty("ModifiedOn"))
                    return;

                _modifiedOn = value;
                MarkDirty("ModifiedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedBy", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 30)]
        public string ModifiedBy
        {
            get => _modifiedBy;
            set
            {
                if (value == _modifiedBy && IsPropertyDirty("ModifiedBy"))
                    return;

                _modifiedBy = value;
                MarkDirty("ModifiedBy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Note", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 400)]
        public string Note
        {
            get => _note;
            set
            {
                if (value == _note && IsPropertyDirty("Note"))
                    return;

                _note = value;
                MarkDirty("Note");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 10, MaxLength = 0, DefaultValue = "((1))")]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn RedirectId => FetchColumn("RedirectId");

            public static QueryColumn FromUrl => FetchColumn("FromUrl");

            public static QueryColumn ToUrl => FetchColumn("ToUrl");

            public static QueryColumn Permanent => FetchColumn("Permanent");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn CreatedBy => FetchColumn("CreatedBy");

            public static QueryColumn ModifiedOn => FetchColumn("ModifiedOn");

            public static QueryColumn ModifiedBy => FetchColumn("ModifiedBy");

            public static QueryColumn Note => FetchColumn("Note");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}