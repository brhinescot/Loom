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

namespace OmniMount.Cms
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Cms.Article table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "Article", "ArticleId", CreatedOnColumn = "CreatedOn", CreatedByColumn = "CreatedBy", DeletedColumn = "Deleted")]
    public class Article : DataRecord<Article>
    {
        private short _applicationId;

        private int _articleId;
        private int _articleTypeId;
        private string _body;
        private string _createdBy;
        private DateTime _createdOn;
        private bool _deleted;
        private int _ordinal;
        private DateTime _publishDate;
        private DateTime _removeDate;
        private string _subject;
        private string _summary;

        public Article() { }
        protected Article(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ArticleId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ArticleId
        {
            get => _articleId;
            set
            {
                if (value == _articleId && IsPropertyDirty("ArticleId"))
                    return;

                _articleId = value;
                MarkDirty("ArticleId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Subject", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 200)]
        public string Subject
        {
            get => _subject;
            set
            {
                if (value == _subject && IsPropertyDirty("Subject"))
                    return;

                _subject = value;
                MarkDirty("Subject");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Body", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 2147483647)]
        public string Body
        {
            get => _body;
            set
            {
                if (value == _body && IsPropertyDirty("Body"))
                    return;

                _body = value;
                MarkDirty("Body");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ArticleTypeId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("ArticleTypeId", typeof(ArticleType), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ArticleTypeId
        {
            get => _articleTypeId;
            set
            {
                if (value == _articleTypeId && IsPropertyDirty("ArticleTypeId"))
                    return;

                _articleTypeId = value;
                MarkDirty("ArticleTypeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PublishDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime PublishDate
        {
            get => _publishDate;
            set
            {
                if (value == _publishDate && IsPropertyDirty("PublishDate"))
                    return;

                _publishDate = value;
                MarkDirty("PublishDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RemoveDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "('12/31/9999')")]
        public DateTime RemoveDate
        {
            get => _removeDate;
            set
            {
                if (value == _removeDate && IsPropertyDirty("RemoveDate"))
                    return;

                _removeDate = value;
                MarkDirty("RemoveDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("CreatedBy", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 30)]
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
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value == _deleted && IsPropertyDirty("Deleted"))
                    return;

                _deleted = value;
                MarkDirty("Deleted");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Summary", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 200)]
        public string Summary
        {
            get => _summary;
            set
            {
                if (value == _summary && IsPropertyDirty("Summary"))
                    return;

                _summary = value;
                MarkDirty("Summary");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "((1))")]
        public int Ordinal
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
        [ActiveColumn("ApplicationId", DbType.Int16, ColumnProperties.None, Ordinal = 12, MaxLength = 0, DefaultValue = "((1))")]
        public short ApplicationId
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
            public static QueryColumn ArticleId => FetchColumn("ArticleId");

            public static QueryColumn Subject => FetchColumn("Subject");

            public static QueryColumn Body => FetchColumn("Body");

            public static QueryColumn ArticleTypeId => FetchColumn("ArticleTypeId");

            public static QueryColumn PublishDate => FetchColumn("PublishDate");

            public static QueryColumn RemoveDate => FetchColumn("RemoveDate");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn CreatedBy => FetchColumn("CreatedBy");

            public static QueryColumn Deleted => FetchColumn("Deleted");

            public static QueryColumn Summary => FetchColumn("Summary");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}