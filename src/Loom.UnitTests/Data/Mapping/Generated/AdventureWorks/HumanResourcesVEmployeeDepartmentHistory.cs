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
    ///     This is an DataRecord class which wraps the HumanResources.vEmployeeDepartmentHistory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "vEmployeeDepartmentHistory", ReadOnly = true)]
    public class VEmployeeDepartmentHistory : DataRecord<VEmployeeDepartmentHistory>
    {
        private int _businessEntityId;
        private string _department;
        private string _endDate;
        private string _firstName;

        private string _groupName;
        private string _lastName;
        private string _middleName;
        private string _shift;
        private string _startDate;
        private string _suffix;
        private string _title;

        public VEmployeeDepartmentHistory() { }
        protected VEmployeeDepartmentHistory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("GroupName", DbType.String, ColumnProperties.None, Ordinal = 9, MaxLength = 50)]
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
        /// </summary>
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName && IsPropertyDirty("FirstName"))
                    return;

                _firstName = value;
                MarkDirty("FirstName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Suffix", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 10)]
        public string Suffix
        {
            get => _suffix;
            set
            {
                if (value == _suffix && IsPropertyDirty("Suffix"))
                    return;

                _suffix = value;
                MarkDirty("Suffix");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Department", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 50)]
        public string Department
        {
            get => _department;
            set
            {
                if (value == _department && IsPropertyDirty("Department"))
                    return;

                _department = value;
                MarkDirty("Department");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EndDate", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("LastName", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName && IsPropertyDirty("LastName"))
                    return;

                _lastName = value;
                MarkDirty("LastName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Title", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 8)]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title && IsPropertyDirty("Title"))
                    return;

                _title = value;
                MarkDirty("Title");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.None, Ordinal = 1, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("MiddleName", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 50)]
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (value == _middleName && IsPropertyDirty("MiddleName"))
                    return;

                _middleName = value;
                MarkDirty("MiddleName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StartDate", DbType.String, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("Shift", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 50)]
        public string Shift
        {
            get => _shift;
            set
            {
                if (value == _shift && IsPropertyDirty("Shift"))
                    return;

                _shift = value;
                MarkDirty("Shift");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn GroupName => FetchColumn("GroupName");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn Suffix => FetchColumn("Suffix");

            public static QueryColumn Department => FetchColumn("Department");

            public static QueryColumn EndDate => FetchColumn("EndDate");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn MiddleName => FetchColumn("MiddleName");

            public static QueryColumn StartDate => FetchColumn("StartDate");

            public static QueryColumn Shift => FetchColumn("Shift");
        }

        #endregion
    }
}