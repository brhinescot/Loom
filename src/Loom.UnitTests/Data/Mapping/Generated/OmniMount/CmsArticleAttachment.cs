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
using OmniMount.Production;

#endregion

namespace OmniMount.Cms
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Cms.ArticleAttachment table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "ArticleAttachment", "AttachmentId")]
    public class ArticleAttachment : DataRecord<ArticleAttachment>
    {
        private int _articleId;
        private int _attachmentId;

        public ArticleAttachment() { }
        protected ArticleAttachment(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ArticleId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ArticleId", typeof(Article), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        [ActiveColumn("AttachmentId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("AttachmentId", typeof(Attachment), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int AttachmentId
        {
            get => _attachmentId;
            set
            {
                if (value == _attachmentId && IsPropertyDirty("AttachmentId"))
                    return;

                _attachmentId = value;
                MarkDirty("AttachmentId");
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

            public static QueryColumn AttachmentId => FetchColumn("AttachmentId");
        }

        #endregion
    }
}