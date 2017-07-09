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
    ///     This is an DataRecord class which wraps the dbo.Orders table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Orders", "OrderID")]
    public class Orders : DataRecord<Orders>
    {
        private string _customerId;
        private int? _employeeId;
        private decimal? _freight;
        private DateTime? _orderDate;

        private int _orderId;
        private DateTime? _requiredDate;
        private string _shipAddress;
        private string _shipCity;
        private string _shipCountry;
        private string _shipName;
        private DateTime? _shippedDate;
        private string _shipPostalCode;
        private string _shipRegion;
        private int? _shipVia;

        public Orders() { }
        protected Orders(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("OrderID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value == _orderId)
                    return;

                _orderId = value;
                MarkDirty("OrderID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerID", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 5)]
        [ForeignColumn("CustomerID", typeof(Customers), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 5, DbType = DbType.String)]
        public string CustomerId
        {
            get => _customerId;
            set
            {
                if (value == _customerId)
                    return;

                _customerId = value;
                MarkDirty("CustomerID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmployeeID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("EmployeeID", typeof(Employees), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? EmployeeId
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
        [ActiveColumn("OrderDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public DateTime? OrderDate
        {
            get => _orderDate;
            set
            {
                if (value == _orderDate)
                    return;

                _orderDate = value;
                MarkDirty("OrderDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RequiredDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public DateTime? RequiredDate
        {
            get => _requiredDate;
            set
            {
                if (value == _requiredDate)
                    return;

                _requiredDate = value;
                MarkDirty("RequiredDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShippedDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public DateTime? ShippedDate
        {
            get => _shippedDate;
            set
            {
                if (value == _shippedDate)
                    return;

                _shippedDate = value;
                MarkDirty("ShippedDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipVia", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        [ForeignColumn("ShipperID", typeof(Shippers), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ShipVia
        {
            get => _shipVia;
            set
            {
                if (value == _shipVia)
                    return;

                _shipVia = value;
                MarkDirty("ShipVia");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Freight", DbType.Currency, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
        public decimal? Freight
        {
            get => _freight;
            set
            {
                if (value == _freight)
                    return;

                _freight = value;
                MarkDirty("Freight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipName", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 40)]
        public string ShipName
        {
            get => _shipName;
            set
            {
                if (value == _shipName)
                    return;

                _shipName = value;
                MarkDirty("ShipName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 60)]
        public string ShipAddress
        {
            get => _shipAddress;
            set
            {
                if (value == _shipAddress)
                    return;

                _shipAddress = value;
                MarkDirty("ShipAddress");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipCity", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 15)]
        public string ShipCity
        {
            get => _shipCity;
            set
            {
                if (value == _shipCity)
                    return;

                _shipCity = value;
                MarkDirty("ShipCity");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipRegion", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 15)]
        public string ShipRegion
        {
            get => _shipRegion;
            set
            {
                if (value == _shipRegion)
                    return;

                _shipRegion = value;
                MarkDirty("ShipRegion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipPostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 10)]
        public string ShipPostalCode
        {
            get => _shipPostalCode;
            set
            {
                if (value == _shipPostalCode)
                    return;

                _shipPostalCode = value;
                MarkDirty("ShipPostalCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipCountry", DbType.String, ColumnProperties.Nullable, Ordinal = 14, MaxLength = 15)]
        public string ShipCountry
        {
            get => _shipCountry;
            set
            {
                if (value == _shipCountry)
                    return;

                _shipCountry = value;
                MarkDirty("ShipCountry");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn OrderId => FetchColumn("OrderID");

            public static QueryColumn CustomerId => FetchColumn("CustomerID");

            public static QueryColumn EmployeeId => FetchColumn("EmployeeID");

            public static QueryColumn OrderDate => FetchColumn("OrderDate");

            public static QueryColumn RequiredDate => FetchColumn("RequiredDate");

            public static QueryColumn ShippedDate => FetchColumn("ShippedDate");

            public static QueryColumn ShipVia => FetchColumn("ShipVia");

            public static QueryColumn Freight => FetchColumn("Freight");

            public static QueryColumn ShipName => FetchColumn("ShipName");

            public static QueryColumn ShipAddress => FetchColumn("ShipAddress");

            public static QueryColumn ShipCity => FetchColumn("ShipCity");

            public static QueryColumn ShipRegion => FetchColumn("ShipRegion");

            public static QueryColumn ShipPostalCode => FetchColumn("ShipPostalCode");

            public static QueryColumn ShipCountry => FetchColumn("ShipCountry");
        }

        #endregion
    }
}