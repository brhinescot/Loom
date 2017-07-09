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
    ///     This is an DataRecord class which wraps the HumanResources.Employee table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "Employee", "BusinessEntityID", ModifiedOnColumn = "ModifiedDate")]
    public class Employee : DataRecord<Employee>
    {
        private string _birthDate;

        private int _businessEntityId;
        private bool _currentFlag;
        private string _gender;
        private string _hireDate;
        private string _jobTitle;
        private string _loginId;
        private string _maritalStatus;
        private DateTime _modifiedDate;
        private string _nationalIdNumber;
        private short? _organizationLevel;
        private string _organizationNode;
        private Guid _rowguid;
        private bool _salariedFlag;
        private short _sickLeaveHours;
        private short _vacationHours;

        public Employee() { }
        protected Employee(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Person.Person), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Unique national identification number such as a social security number.
        /// </summary>
        [ActiveColumn("NationalIDNumber", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 15)]
        public string NationalIdNumber
        {
            get => _nationalIdNumber;
            set
            {
                if (value == _nationalIdNumber && IsPropertyDirty("NationalIDNumber"))
                    return;

                _nationalIdNumber = value;
                MarkDirty("NationalIDNumber");
            }
        }

        /// <summary>
        ///     Network login.
        /// </summary>
        [ActiveColumn("LoginID", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 256)]
        public string LoginId
        {
            get => _loginId;
            set
            {
                if (value == _loginId && IsPropertyDirty("LoginID"))
                    return;

                _loginId = value;
                MarkDirty("LoginID");
            }
        }

        /// <summary>
        ///     Where the employee is located in corporate hierarchy.
        /// </summary>
        [ActiveColumn("OrganizationNode", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 892)]
        public string OrganizationNode
        {
            get => _organizationNode;
            set
            {
                if (value == _organizationNode && IsPropertyDirty("OrganizationNode"))
                    return;

                _organizationNode = value;
                MarkDirty("OrganizationNode");
            }
        }

        /// <summary>
        ///     The depth of the employee in the corporate hierarchy.
        /// </summary>
        [ActiveColumn("OrganizationLevel", DbType.Int16, ColumnProperties.Computed | ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public short? OrganizationLevel
        {
            get => _organizationLevel;
            set
            {
                if (value == _organizationLevel && IsPropertyDirty("OrganizationLevel"))
                    return;

                _organizationLevel = value;
                MarkDirty("OrganizationLevel");
            }
        }

        /// <summary>
        ///     Work title such as Buyer or Sales Representative.
        /// </summary>
        [ActiveColumn("JobTitle", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 50)]
        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                if (value == _jobTitle && IsPropertyDirty("JobTitle"))
                    return;

                _jobTitle = value;
                MarkDirty("JobTitle");
            }
        }

        /// <summary>
        ///     Date of birth.
        /// </summary>
        [ActiveColumn("BirthDate", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
        public string BirthDate
        {
            get => _birthDate;
            set
            {
                if (value == _birthDate && IsPropertyDirty("BirthDate"))
                    return;

                _birthDate = value;
                MarkDirty("BirthDate");
            }
        }

        /// <summary>
        ///     M = Married, S = Single
        /// </summary>
        [ActiveColumn("MaritalStatus", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 1)]
        public string MaritalStatus
        {
            get => _maritalStatus;
            set
            {
                if (value == _maritalStatus && IsPropertyDirty("MaritalStatus"))
                    return;

                _maritalStatus = value;
                MarkDirty("MaritalStatus");
            }
        }

        /// <summary>
        ///     M = Male, F = Female
        /// </summary>
        [ActiveColumn("Gender", DbType.String, ColumnProperties.None, Ordinal = 9, MaxLength = 1)]
        public string Gender
        {
            get => _gender;
            set
            {
                if (value == _gender && IsPropertyDirty("Gender"))
                    return;

                _gender = value;
                MarkDirty("Gender");
            }
        }

        /// <summary>
        ///     Employee hired on this date.
        /// </summary>
        [ActiveColumn("HireDate", DbType.String, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
        public string HireDate
        {
            get => _hireDate;
            set
            {
                if (value == _hireDate && IsPropertyDirty("HireDate"))
                    return;

                _hireDate = value;
                MarkDirty("HireDate");
            }
        }

        /// <summary>
        ///     Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective
        ///     bargaining.
        /// </summary>
        [ActiveColumn("SalariedFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "((1))")]
        public bool SalariedFlag
        {
            get => _salariedFlag;
            set
            {
                if (value == _salariedFlag && IsPropertyDirty("SalariedFlag"))
                    return;

                _salariedFlag = value;
                MarkDirty("SalariedFlag");
            }
        }

        /// <summary>
        ///     Number of available vacation hours.
        /// </summary>
        [ActiveColumn("VacationHours", DbType.Int16, ColumnProperties.None, Ordinal = 12, MaxLength = 0, DefaultValue = "((0))")]
        public short VacationHours
        {
            get => _vacationHours;
            set
            {
                if (value == _vacationHours && IsPropertyDirty("VacationHours"))
                    return;

                _vacationHours = value;
                MarkDirty("VacationHours");
            }
        }

        /// <summary>
        ///     Number of available sick leave hours.
        /// </summary>
        [ActiveColumn("SickLeaveHours", DbType.Int16, ColumnProperties.None, Ordinal = 13, MaxLength = 0, DefaultValue = "((0))")]
        public short SickLeaveHours
        {
            get => _sickLeaveHours;
            set
            {
                if (value == _sickLeaveHours && IsPropertyDirty("SickLeaveHours"))
                    return;

                _sickLeaveHours = value;
                MarkDirty("SickLeaveHours");
            }
        }

        /// <summary>
        ///     0 = Inactive, 1 = Active
        /// </summary>
        [ActiveColumn("CurrentFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 14, MaxLength = 0, DefaultValue = "((1))")]
        public bool CurrentFlag
        {
            get => _currentFlag;
            set
            {
                if (value == _currentFlag && IsPropertyDirty("CurrentFlag"))
                    return;

                _currentFlag = value;
                MarkDirty("CurrentFlag");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 15, MaxLength = 0, DefaultValue = "(newid())")]
        public Guid Rowguid
        {
            get => _rowguid;
            set
            {
                if (value == _rowguid && IsPropertyDirty("rowguid"))
                    return;

                _rowguid = value;
                MarkDirty("rowguid");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 16, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn NationalIdNumber => FetchColumn("NationalIDNumber");

            public static QueryColumn LoginId => FetchColumn("LoginID");

            public static QueryColumn OrganizationNode => FetchColumn("OrganizationNode");

            public static QueryColumn OrganizationLevel => FetchColumn("OrganizationLevel");

            public static QueryColumn JobTitle => FetchColumn("JobTitle");

            public static QueryColumn BirthDate => FetchColumn("BirthDate");

            public static QueryColumn MaritalStatus => FetchColumn("MaritalStatus");

            public static QueryColumn Gender => FetchColumn("Gender");

            public static QueryColumn HireDate => FetchColumn("HireDate");

            public static QueryColumn SalariedFlag => FetchColumn("SalariedFlag");

            public static QueryColumn VacationHours => FetchColumn("VacationHours");

            public static QueryColumn SickLeaveHours => FetchColumn("SickLeaveHours");

            public static QueryColumn CurrentFlag => FetchColumn("CurrentFlag");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}