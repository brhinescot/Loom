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

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.Country table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "Country", "CountryId")]
    public class Country : DataRecord<Country>
    {
        private int _countryId;
        private string _iso2;
        private string _iso3;
        private string _name;

        public Country() { }
        protected Country(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CountryId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int CountryId
        {
            get => _countryId;
            set
            {
                if (value == _countryId && IsPropertyDirty("CountryId"))
                    return;

                _countryId = value;
                MarkDirty("CountryId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Iso2", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 2)]
        public string Iso2
        {
            get => _iso2;
            set
            {
                if (value == _iso2 && IsPropertyDirty("Iso2"))
                    return;

                _iso2 = value;
                MarkDirty("Iso2");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Iso3", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 3)]
        public string Iso3
        {
            get => _iso3;
            set
            {
                if (value == _iso3 && IsPropertyDirty("Iso3"))
                    return;

                _iso3 = value;
                MarkDirty("Iso3");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 100)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CountryId => FetchColumn("CountryId");

            public static QueryColumn Iso2 => FetchColumn("Iso2");

            public static QueryColumn Iso3 => FetchColumn("Iso3");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}