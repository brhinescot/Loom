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
    ///     This is an DataRecord class which wraps the HumanResources.Department table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "Department", "DepartmentID", ModifiedOnColumn = "ModifiedDate")]
    public class Department : DataRecord<Department>
    {
        private short _departmentId;
        private string _groupName;
        private DateTime _modifiedDate;
        private string _name;

        public Department() { }
        protected Department(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Department records.
        /// </summary>
        [ActiveColumn("DepartmentID", DbType.Int16, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Name of the department.
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
        ///     Name of the group to which the department belongs.
        /// </summary>
        [ActiveColumn("GroupName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string GroupName
        {
            get => _groupName;
            set
            {
                if (value == _groupName && IsPropertyDirty("GroupName"))
                    return;

                _groupName = value;
                MarkDirty("GroupName");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn DepartmentId => FetchColumn("DepartmentID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn GroupName => FetchColumn("GroupName");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}