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

namespace Northwind
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.EmployeeTerritories table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "EmployeeTerritories", "TerritoryID")]
    public class EmployeeTerritories : DataRecord<EmployeeTerritories>
    {
        private int _employeeId;
        private string _territoryId;

        public EmployeeTerritories() { }
        protected EmployeeTerritories(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmployeeID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("EmployeeID", typeof(Employees), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                if (value == _employeeId)
                    return;

                _employeeId = value;
                MarkDirty("EmployeeID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 20)]
        [ForeignColumn("TerritoryID", typeof(Territories), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 20, DbType = DbType.String)]
        public string TerritoryId
        {
            get => _territoryId;
            set
            {
                if (value == _territoryId)
                    return;

                _territoryId = value;
                MarkDirty("TerritoryID");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn EmployeeId => FetchColumn("EmployeeID");

            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");
        }

        #endregion
    }
}