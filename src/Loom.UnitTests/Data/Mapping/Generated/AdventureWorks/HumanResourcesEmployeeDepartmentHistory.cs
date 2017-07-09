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

namespace AdventureWorks.HumanResources
{
    /// <summary>
    ///     This is an DataRecord class which wraps the HumanResources.EmployeeDepartmentHistory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "EmployeeDepartmentHistory", "StartDate", ModifiedOnColumn = "ModifiedDate")]
    public class EmployeeDepartmentHistory : DataRecord<EmployeeDepartmentHistory>
    {
        private int _businessEntityId;
        private short _departmentId;
        private string _endDate;
        private DateTime _modifiedDate;
        private short _shiftId;
        private string _startDate;

        public EmployeeDepartmentHistory() { }
        protected EmployeeDepartmentHistory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Employee identification number. Foreign key to Employee.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Employee), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        ///     Department in which the employee worked including currently. Foreign key to Department.DepartmentID.
        /// </summary>
        [ActiveColumn("DepartmentID", DbType.Int16, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("DepartmentID", typeof(Department), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int16)]
        public short DepartmentId
        {
            get => _departmentId;
            set
            {
                if (value == _departmentId && IsPropertyDirty("DepartmentID"))
                    return;

                _departmentId = value;
                MarkDirty("DepartmentID");
            }
        }

        /// <summary>
        ///     Identifies which 8-hour shift the employee works. Foreign key to Shift.Shift.ID.
        /// </summary>
        [ActiveColumn("ShiftID", DbType.Int16, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ShiftID", typeof(Shift), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int16)]
        public short ShiftId
        {
            get => _shiftId;
            set
            {
                if (value == _shiftId && IsPropertyDirty("ShiftID"))
                    return;

                _shiftId = value;
                MarkDirty("ShiftID");
            }
        }

        /// <summary>
        ///     Date the employee started work in the department.
        /// </summary>
        [ActiveColumn("StartDate", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 4, MaxLength = 0)]
        public string StartDate
        {
            get => _startDate;
            set
            {
                if (value == _startDate && IsPropertyDirty("StartDate"))
                    return;

                _startDate = value;
                MarkDirty("StartDate");
            }
        }

        /// <summary>
        ///     Date the employee left the department. NULL = Current department.
        /// </summary>
        [ActiveColumn("EndDate", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public string EndDate
        {
            get => _endDate;
            set
            {
                if (value == _endDate && IsPropertyDirty("EndDate"))
                    return;

                _endDate = value;
                MarkDirty("EndDate");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn DepartmentId => FetchColumn("DepartmentID");

            public static QueryColumn ShiftId => FetchColumn("ShiftID");

            public static QueryColumn StartDate => FetchColumn("StartDate");

            public static QueryColumn EndDate => FetchColumn("EndDate");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}