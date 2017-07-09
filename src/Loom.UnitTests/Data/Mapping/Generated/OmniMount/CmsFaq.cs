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
    ///     This is an DataRecord class which wraps the Cms.Faq table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "Faq", "FaqId")]
    public class Faq : DataRecord<Faq>
    {
        private int _faqId;
        private string _name;

        public Faq() { }
        protected Faq(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("FaqId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn FaqId => FetchColumn("FaqId");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}