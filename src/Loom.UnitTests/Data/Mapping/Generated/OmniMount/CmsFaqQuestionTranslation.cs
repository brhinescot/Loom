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
    ///     This is an DataRecord class which wraps the Cms.FaqQuestionTranslation table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "FaqQuestionTranslation", "Locale")]
    public class FaqQuestionTranslation : DataRecord<FaqQuestionTranslation>
    {
        private string _answer;

        private int _faqQuestionId;
        private string _locale;
        private string _question;

        public FaqQuestionTranslation() { }
        protected FaqQuestionTranslation(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("FaqQuestionId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("FaqQuestionId", typeof(FaqQuestion), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int FaqQuestionId
        {
            get => _faqQuestionId;
            set
            {
                if (value == _faqQuestionId && IsPropertyDirty("FaqQuestionId"))
                    return;

                _faqQuestionId = value;
                MarkDirty("FaqQuestionId");
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
        [ActiveColumn("Question", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 400)]
        public string Question
        {
            get => _question;
            set
            {
                if (value == _question && IsPropertyDirty("Question"))
                    return;

                _question = value;
                MarkDirty("Question");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Answer", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 1073741823)]
        public string Answer
        {
            get => _answer;
            set
            {
                if (value == _answer && IsPropertyDirty("Answer"))
                    return;

                _answer = value;
                MarkDirty("Answer");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn FaqQuestionId => FetchColumn("FaqQuestionId");

            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn Question => FetchColumn("Question");

            public static QueryColumn Answer => FetchColumn("Answer");
        }

        #endregion
    }
}