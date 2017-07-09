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
    ///     This is an DataRecord class which wraps the dbo.authors table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "authors", "au_id")]
    public class Authors : DataRecord<Authors>
    {
        private string _address;
        private string _au_Fname;

        private string _au_Id;
        private string _au_Lname;
        private string _city;
        private bool _contract;
        private string _phone;
        private string _state;
        private string _zip;

        public Authors() { }
        protected Authors(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("au_id", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 11)]
        public string Au_id
        {
            get => _au_Id;
            set
            {
                if (value == _au_Id)
                    return;

                _au_Id = value;
                MarkDirty("au_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("au_lname", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 40)]
        public string Au_lname
        {
            get => _au_Lname;
            set
            {
                if (value == _au_Lname)
                    return;

                _au_Lname = value;
                MarkDirty("au_lname");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("au_fname", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 20)]
        public string Au_fname
        {
            get => _au_Fname;
            set
            {
                if (value == _au_Fname)
                    return;

                _au_Fname = value;
                MarkDirty("au_fname");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("phone", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 12, DefaultValue = "('UNKNOWN')")]
        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone)
                    return;

                _phone = value;
                MarkDirty("phone");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("address", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 40)]
        public string Address
        {
            get => _address;
            set
            {
                if (value == _address)
                    return;

                _address = value;
                MarkDirty("address");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("city", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 20)]
        public string City
        {
            get => _city;
            set
            {
                if (value == _city)
                    return;

                _city = value;
                MarkDirty("city");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("state", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 2)]
        public string State
        {
            get => _state;
            set
            {
                if (value == _state)
                    return;

                _state = value;
                MarkDirty("state");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("zip", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 5)]
        public string Zip
        {
            get => _zip;
            set
            {
                if (value == _zip)
                    return;

                _zip = value;
                MarkDirty("zip");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("contract", DbType.Boolean, ColumnProperties.None, Ordinal = 9, MaxLength = 0)]
        public bool Contract
        {
            get => _contract;
            set
            {
                if (value == _contract)
                    return;

                _contract = value;
                MarkDirty("contract");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Au_id => FetchColumn("au_id");

            public static QueryColumn Au_lname => FetchColumn("au_lname");

            public static QueryColumn Au_fname => FetchColumn("au_fname");

            public static QueryColumn Phone => FetchColumn("phone");

            public static QueryColumn Address => FetchColumn("address");

            public static QueryColumn City => FetchColumn("city");

            public static QueryColumn State => FetchColumn("state");

            public static QueryColumn Zip => FetchColumn("zip");

            public static QueryColumn Contract => FetchColumn("contract");
        }

        #endregion
    }
}