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
    ///     This is an DataRecord class which wraps the dbo.stores table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "stores", "stor_id")]
    public class Stores : DataRecord<Stores>
    {
        private string _city;
        private string _state;
        private string _stor_Address;

        private string _stor_Id;
        private string _stor_Name;
        private string _zip;

        public Stores() { }
        protected Stores(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("stor_id", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4)]
        public string Stor_id
        {
            get => _stor_Id;
            set
            {
                if (value == _stor_Id)
                    return;

                _stor_Id = value;
                MarkDirty("stor_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("stor_name", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 40)]
        public string Stor_name
        {
            get => _stor_Name;
            set
            {
                if (value == _stor_Name)
                    return;

                _stor_Name = value;
                MarkDirty("stor_name");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("stor_address", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 40)]
        public string Stor_address
        {
            get => _stor_Address;
            set
            {
                if (value == _stor_Address)
                    return;

                _stor_Address = value;
                MarkDirty("stor_address");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("city", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 20)]
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
        [ActiveColumn("state", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 2)]
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
        [ActiveColumn("zip", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 5)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Stor_id => FetchColumn("stor_id");

            public static QueryColumn Stor_name => FetchColumn("stor_name");

            public static QueryColumn Stor_address => FetchColumn("stor_address");

            public static QueryColumn City => FetchColumn("city");

            public static QueryColumn State => FetchColumn("state");

            public static QueryColumn Zip => FetchColumn("zip");
        }

        #endregion
    }
}