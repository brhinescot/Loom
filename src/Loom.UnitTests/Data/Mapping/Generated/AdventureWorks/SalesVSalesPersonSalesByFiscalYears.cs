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

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.vSalesPersonSalesByFiscalYears table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "vSalesPersonSalesByFiscalYears", ReadOnly = true)]
    public class VSalesPersonSalesByFiscalYears : DataRecord<VSalesPersonSalesByFiscalYears>
    {
        private string _fullName;
        private string _jobTitle;
        private decimal? _n2002;
        private decimal? _n2003;
        private decimal? _n2004;
        private int? _salesPersonId;

        private string _salesTerritory;

        public VSalesPersonSalesByFiscalYears() { }
        protected VSalesPersonSalesByFiscalYears(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("SalesTerritory", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
        public string SalesTerritory
        {
            get => _salesTerritory;
            set
            {
                if (value == _salesTerritory && IsPropertyDirty("SalesTerritory"))
                    return;

                _salesTerritory = value;
                MarkDirty("SalesTerritory");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("SalesPersonID", DbType.Int32, ColumnProperties.Nullable, Ordinal = 1, MaxLength = 0)]
        public int? SalesPersonId
        {
            get => _salesPersonId;
            set
            {
                if (value == _salesPersonId && IsPropertyDirty("SalesPersonID"))
                    return;

                _salesPersonId = value;
                MarkDirty("SalesPersonID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FullName", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 152)]
        public string FullName
        {
            get => _fullName;
            set
            {
                if (value == _fullName && IsPropertyDirty("FullName"))
                    return;

                _fullName = value;
                MarkDirty("FullName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("2002", DbType.Currency, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public decimal? N2002
        {
            get => _n2002;
            set
            {
                if (value == _n2002 && IsPropertyDirty("2002"))
                    return;

                _n2002 = value;
                MarkDirty("2002");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("2003", DbType.Currency, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public decimal? N2003
        {
            get => _n2003;
            set
            {
                if (value == _n2003 && IsPropertyDirty("2003"))
                    return;

                _n2003 = value;
                MarkDirty("2003");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("JobTitle", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
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
        /// </summary>
        [ActiveColumn("2004", DbType.Currency, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public decimal? N2004
        {
            get => _n2004;
            set
            {
                if (value == _n2004 && IsPropertyDirty("2004"))
                    return;

                _n2004 = value;
                MarkDirty("2004");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn SalesTerritory => FetchColumn("SalesTerritory");

            public static QueryColumn SalesPersonId => FetchColumn("SalesPersonID");

            public static QueryColumn FullName => FetchColumn("FullName");

            public static QueryColumn N2002 => FetchColumn("2002");

            public static QueryColumn N2003 => FetchColumn("2003");

            public static QueryColumn JobTitle => FetchColumn("JobTitle");

            public static QueryColumn N2004 => FetchColumn("2004");
        }

        #endregion
    }
}