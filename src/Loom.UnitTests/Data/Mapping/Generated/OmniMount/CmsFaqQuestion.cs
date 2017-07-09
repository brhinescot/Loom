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
    ///     This is an DataRecord class which wraps the Cms.FaqQuestion table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "FaqQuestion", "FaqQuestionId", CreatedOnColumn = "CreatedOn", CreatedByColumn = "CreatedBy", ModifiedOnColumn = "ModifiedOn", ModifiedByColumn = "ModifiedBy", DeletedColumn = "Deleted")]
    public class FaqQuestion : DataRecord<FaqQuestion>
    {
        private string _answer;
        private string _createdBy;
        private DateTime _createdOn;
        private bool _deleted;
        private string _deletedBy;
        private DateTime? _deletedOn;
        private int _faqId;

        private int _faqQuestionId;
        private string _modifiedBy;
        private DateTime _modifiedOn;
        private string _question;

        public FaqQuestion() { }
        protected FaqQuestion(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("FaqQuestionId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("FaqId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("FaqId", typeof(Faq), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int FaqId
        {
            get => _faqId;
            set
            {
                if (value == _faqId && IsPropertyDirty("FaqId"))
                    return;

                _faqId = value;
                MarkDirty("FaqId");
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
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        public DateTime? DeletedOn
        {
            get => _deletedOn;
            set
            {
                if (value == _deletedOn && IsPropertyDirty("DeletedOn"))
                    return;

                _deletedOn = value;
                MarkDirty("DeletedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DeletedBy", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 30)]
        public string DeletedBy
        {
            get => _deletedBy;
            set
            {
                if (value == _deletedBy && IsPropertyDirty("DeletedBy"))
                    return;

                _deletedBy = value;
                MarkDirty("DeletedBy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "((0))")]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn FaqQuestionId => FetchColumn("FaqQuestionId");

            public static QueryColumn FaqId => FetchColumn("FaqId");

            public static QueryColumn Question => FetchColumn("Question");

            public static QueryColumn Answer => FetchColumn("Answer");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn CreatedBy => FetchColumn("CreatedBy");

            public static QueryColumn ModifiedOn => FetchColumn("ModifiedOn");

            public static QueryColumn ModifiedBy => FetchColumn("ModifiedBy");

            public static QueryColumn DeletedOn => FetchColumn("DeletedOn");

            public static QueryColumn DeletedBy => FetchColumn("DeletedBy");

            public static QueryColumn Deleted => FetchColumn("Deleted");
        }

        #endregion
    }
}