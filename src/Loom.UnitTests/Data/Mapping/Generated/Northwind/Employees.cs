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
    ///     This is an DataRecord class which wraps the dbo.Employees table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Employees", "EmployeeID")]
    public class Employees : DataRecord<Employees>
    {
        private string _address;
        private DateTime? _birthDate;
        private string _city;
        private string _country;

        private int _employeeId;
        private string _extension;
        private string _firstName;
        private DateTime? _hireDate;
        private string _homePhone;
        private string _lastName;
        private string _notes;
        private byte[] _photo;
        private string _photoPath;
        private string _postalCode;
        private string _region;
        private int? _reportsTo;
        private string _title;
        private string _titleOfCourtesy;

        public Employees() { }
        protected Employees(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmployeeID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("LastName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 20)]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName)
                    return;

                _lastName = value;
                MarkDirty("LastName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 10)]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName)
                    return;

                _firstName = value;
                MarkDirty("FirstName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Title", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 30)]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                    return;

                _title = value;
                MarkDirty("Title");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TitleOfCourtesy", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 25)]
        public string TitleOfCourtesy
        {
            get => _titleOfCourtesy;
            set
            {
                if (value == _titleOfCourtesy)
                    return;

                _titleOfCourtesy = value;
                MarkDirty("TitleOfCourtesy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BirthDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (value == _birthDate)
                    return;

                _birthDate = value;
                MarkDirty("BirthDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HireDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public DateTime? HireDate
        {
            get => _hireDate;
            set
            {
                if (value == _hireDate)
                    return;

                _hireDate = value;
                MarkDirty("HireDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Address", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 60)]
        public string Address
        {
            get => _address;
            set
            {
                if (value == _address)
                    return;

                _address = value;
                MarkDirty("Address");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 15)]
        public string City
        {
            get => _city;
            set
            {
                if (value == _city)
                    return;

                _city = value;
                MarkDirty("City");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Region", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 15)]
        public string Region
        {
            get => _region;
            set
            {
                if (value == _region)
                    return;

                _region = value;
                MarkDirty("Region");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 10)]
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (value == _postalCode)
                    return;

                _postalCode = value;
                MarkDirty("PostalCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Country", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 15)]
        public string Country
        {
            get => _country;
            set
            {
                if (value == _country)
                    return;

                _country = value;
                MarkDirty("Country");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HomePhone", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 24)]
        public string HomePhone
        {
            get => _homePhone;
            set
            {
                if (value == _homePhone)
                    return;

                _homePhone = value;
                MarkDirty("HomePhone");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Extension", DbType.String, ColumnProperties.Nullable, Ordinal = 14, MaxLength = 4)]
        public string Extension
        {
            get => _extension;
            set
            {
                if (value == _extension)
                    return;

                _extension = value;
                MarkDirty("Extension");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Photo", DbType.Binary, ColumnProperties.Nullable, Ordinal = 15, MaxLength = 2147483647)]
        public byte[] Photo
        {
            get => _photo;
            set
            {
                if (value == _photo)
                    return;

                _photo = value;
                MarkDirty("Photo");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Notes", DbType.String, ColumnProperties.Nullable, Ordinal = 16, MaxLength = 1073741823)]
        public string Notes
        {
            get => _notes;
            set
            {
                if (value == _notes)
                    return;

                _notes = value;
                MarkDirty("Notes");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ReportsTo", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 17, MaxLength = 0)]
        [ForeignColumn("EmployeeID", typeof(Employees), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ReportsTo
        {
            get => _reportsTo;
            set
            {
                if (value == _reportsTo)
                    return;

                _reportsTo = value;
                MarkDirty("ReportsTo");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PhotoPath", DbType.String, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 255)]
        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                if (value == _photoPath)
                    return;

                _photoPath = value;
                MarkDirty("PhotoPath");
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

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn TitleOfCourtesy => FetchColumn("TitleOfCourtesy");

            public static QueryColumn BirthDate => FetchColumn("BirthDate");

            public static QueryColumn HireDate => FetchColumn("HireDate");

            public static QueryColumn Address => FetchColumn("Address");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn Region => FetchColumn("Region");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn Country => FetchColumn("Country");

            public static QueryColumn HomePhone => FetchColumn("HomePhone");

            public static QueryColumn Extension => FetchColumn("Extension");

            public static QueryColumn Photo => FetchColumn("Photo");

            public static QueryColumn Notes => FetchColumn("Notes");

            public static QueryColumn ReportsTo => FetchColumn("ReportsTo");

            public static QueryColumn PhotoPath => FetchColumn("PhotoPath");
        }

        #endregion
    }
}