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
    ///     This is an DataRecord class which wraps the Production.UnitMeasure table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "UnitMeasure", "UnitMeasureCode", ModifiedOnColumn = "ModifiedDate")]
    public class UnitMeasure : DataRecord<UnitMeasure>
    {
        private DateTime _modifiedDate;
        private string _name;

        private string _unitMeasureCode;

        public UnitMeasure() { }
        protected UnitMeasure(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key.
        /// </summary>
        [ActiveColumn("UnitMeasureCode", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3)]
        public string UnitMeasureCode
        {
            get => _unitMeasureCode;
            set
            {
                if (value == _unitMeasureCode && IsPropertyDirty("UnitMeasureCode"))
                    return;

                _unitMeasureCode = value;
                MarkDirty("UnitMeasureCode");
            }
        }

        /// <summary>
        ///     Unit of measure description.
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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
            public static QueryColumn UnitMeasureCode => FetchColumn("UnitMeasureCode");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}