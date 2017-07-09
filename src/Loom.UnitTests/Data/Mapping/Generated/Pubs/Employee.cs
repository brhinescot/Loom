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

namespace Pubs
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.employee table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "employee", "emp_id")]
    public class Employee : DataRecord<Employee>
    {
        private string _emp_Id;
        private string _fname;
        private DateTime _hire_Date;
        private short _job_Id;
        private short? _job_Lvl;
        private string _lname;
        private char _minit;
        private string _pub_Id;

        public Employee() { }
        protected Employee(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("emp_id", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 9)]
        public string Emp_id
        {
            get => _emp_Id;
            set
            {
                if (value == _emp_Id)
                    return;

                _emp_Id = value;
                MarkDirty("emp_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("fname", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 20)]
        public string Fname
        {
            get => _fname;
            set
            {
                if (value == _fname)
                    return;

                _fname = value;
                MarkDirty("fname");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("minit", DbType.AnsiStringFixedLength, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 1)]
        public char Minit
        {
            get => _minit;
            set
            {
                if (value == _minit)
                    return;

                _minit = value;
                MarkDirty("minit");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("lname", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 30)]
        public string Lname
        {
            get => _lname;
            set
            {
                if (value == _lname)
                    return;

                _lname = value;
                MarkDirty("lname");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("job_id", DbType.Int16, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0, DefaultValue = "((1))")]
        [ForeignColumn("job_id", typeof(Jobs), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int16)]
        public short Job_id
        {
            get => _job_Id;
            set
            {
                if (value == _job_Id)
                    return;

                _job_Id = value;
                MarkDirty("job_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("job_lvl", DbType.Int16, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0, DefaultValue = "((10))")]
        public short? Job_lvl
        {
            get => _job_Lvl;
            set
            {
                if (value == _job_Lvl)
                    return;

                _job_Lvl = value;
                MarkDirty("job_lvl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("pub_id", DbType.String, ColumnProperties.ForeignKey, Ordinal = 7, MaxLength = 4, DefaultValue = "('9952')")]
        [ForeignColumn("pub_id", typeof(Publishers), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4, DbType = DbType.String)]
        public string Pub_id
        {
            get => _pub_Id;
            set
            {
                if (value == _pub_Id)
                    return;

                _pub_Id = value;
                MarkDirty("pub_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("hire_date", DbType.DateTime, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime Hire_date
        {
            get => _hire_Date;
            set
            {
                if (value == _hire_Date)
                    return;

                _hire_Date = value;
                MarkDirty("hire_date");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Emp_id => FetchColumn("emp_id");

            public static QueryColumn Fname => FetchColumn("fname");

            public static QueryColumn Minit => FetchColumn("minit");

            public static QueryColumn Lname => FetchColumn("lname");

            public static QueryColumn Job_id => FetchColumn("job_id");

            public static QueryColumn Job_lvl => FetchColumn("job_lvl");

            public static QueryColumn Pub_id => FetchColumn("pub_id");

            public static QueryColumn Hire_date => FetchColumn("hire_date");
        }

        #endregion
    }
}