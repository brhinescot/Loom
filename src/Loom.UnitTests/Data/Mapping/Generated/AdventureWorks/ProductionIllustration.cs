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

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.Illustration table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Illustration", "IllustrationID", ModifiedOnColumn = "ModifiedDate")]
    public class Illustration : DataRecord<Illustration>
    {
        private string _diagram;

        private int _illustrationId;
        private DateTime _modifiedDate;

        public Illustration() { }
        protected Illustration(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Illustration records.
        /// </summary>
        [ActiveColumn("IllustrationID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int IllustrationId
        {
            get => _illustrationId;
            set
            {
                if (value == _illustrationId && IsPropertyDirty("IllustrationID"))
                    return;

                _illustrationId = value;
                MarkDirty("IllustrationID");
            }
        }

        /// <summary>
        ///     Illustrations used in manufacturing instructions. Stored as XML.
        /// </summary>
        [ActiveColumn("Diagram", DbType.String, ColumnProperties.Nullable, Ordinal = 2)]
        public string Diagram
        {
            get => _diagram;
            set
            {
                if (value == _diagram && IsPropertyDirty("Diagram"))
                    return;

                _diagram = value;
                MarkDirty("Diagram");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn IllustrationId => FetchColumn("IllustrationID");

            public static QueryColumn Diagram => FetchColumn("Diagram");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}