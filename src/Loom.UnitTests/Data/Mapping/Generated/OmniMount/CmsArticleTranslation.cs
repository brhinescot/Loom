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
    ///     This is an DataRecord class which wraps the Cms.ArticleTranslation table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "ArticleTranslation", "Locale")]
    public class ArticleTranslation : DataRecord<ArticleTranslation>
    {
        private int _articleId;
        private string _body;
        private string _locale;
        private string _subject;

        public ArticleTranslation() { }
        protected ArticleTranslation(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        [ActiveColumn("Locale", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 5)]
        public string Locale
        {
            get => _locale;
            set
            {
                if (value == _locale && IsPropertyDirty("Locale"))
                    return;

                _locale = value;
                MarkDirty("Locale");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Subject", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 100)]
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
        [ActiveColumn("Body", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 1073741823)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ArticleId => FetchColumn("ArticleId");

            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn Subject => FetchColumn("Subject");

            public static QueryColumn Body => FetchColumn("Body");
        }

        #endregion
    }
}