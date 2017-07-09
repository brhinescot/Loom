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
    ///     This is an DataRecord class which wraps the dbo.Invoices table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Invoices", ReadOnly = true)]
    public class Invoices : DataRecord<Invoices>
    {
        private string _address;
        private string _city;
        private string _country;
        private string _customerId;
        private string _customerName;
        private decimal _discount;
        private decimal? _extendedPrice;
        private decimal? _freight;
        private DateTime? _orderDate;
        private int _orderId;
        private string _postalCode;
        private int _productId;
        private string _productName;
        private short _quantity;
        private string _region;
        private DateTime? _requiredDate;
        private string _salesperson;
        private string _shipAddress;
        private string _shipCity;
        private string _shipCountry;

        private string _shipName;
        private DateTime? _shippedDate;
        private string _shipperName;
        private string _shipPostalCode;
        private string _shipRegion;
        private decimal _unitPrice;

        public Invoices() { }
        protected Invoices(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipName", DbType.String, ColumnProperties.Nullable, Ordinal = 1, MaxLength = 40)]
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
        [ActiveColumn("ShipAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 60)]
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
        [ActiveColumn("ShipCity", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 15)]
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
        [ActiveColumn("ShipRegion", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 15)]
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
        [ActiveColumn("ShipPostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 10)]
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
        [ActiveColumn("ShipCountry", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 15)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerID", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 5)]
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
        [ActiveColumn("CustomerName", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 40)]
        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (value == _customerName)
                    return;

                _customerName = value;
                MarkDirty("CustomerName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Address", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 60)]
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
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 15)]
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
        [ActiveColumn("Region", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 15)]
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
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 10)]
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
        [ActiveColumn("Country", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 15)]
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
        [ActiveColumn("Salesperson", DbType.String, ColumnProperties.None, Ordinal = 14, MaxLength = 31)]
        public string Salesperson
        {
            get => _salesperson;
            set
            {
                if (value == _salesperson)
                    return;

                _salesperson = value;
                MarkDirty("Salesperson");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("OrderID", DbType.Int32, ColumnProperties.None, Ordinal = 15, MaxLength = 0)]
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
        [ActiveColumn("OrderDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 16, MaxLength = 0)]
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
        [ActiveColumn("RequiredDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 17, MaxLength = 0)]
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
        [ActiveColumn("ShippedDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 0)]
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
        [ActiveColumn("ShipperName", DbType.String, ColumnProperties.None, Ordinal = 19, MaxLength = 40)]
        public string ShipperName
        {
            get => _shipperName;
            set
            {
                if (value == _shipperName)
                    return;

                _shipperName = value;
                MarkDirty("ShipperName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.None, Ordinal = 20, MaxLength = 0)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId)
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductName", DbType.String, ColumnProperties.None, Ordinal = 21, MaxLength = 40)]
        public string ProductName
        {
            get => _productName;
            set
            {
                if (value == _productName)
                    return;

                _productName = value;
                MarkDirty("ProductName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UnitPrice", DbType.Currency, ColumnProperties.None, Ordinal = 22, MaxLength = 0)]
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value == _unitPrice)
                    return;

                _unitPrice = value;
                MarkDirty("UnitPrice");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Quantity", DbType.Int16, ColumnProperties.None, Ordinal = 23, MaxLength = 0)]
        public short Quantity
        {
            get => _quantity;
            set
            {
                if (value == _quantity)
                    return;

                _quantity = value;
                MarkDirty("Quantity");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Discount", DbType.Decimal, ColumnProperties.None, Ordinal = 24, MaxLength = 0)]
        public decimal Discount
        {
            get => _discount;
            set
            {
                if (value == _discount)
                    return;

                _discount = value;
                MarkDirty("Discount");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ExtendedPrice", DbType.Currency, ColumnProperties.Nullable, Ordinal = 25, MaxLength = 0)]
        public decimal? ExtendedPrice
        {
            get => _extendedPrice;
            set
            {
                if (value == _extendedPrice)
                    return;

                _extendedPrice = value;
                MarkDirty("ExtendedPrice");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Freight", DbType.Currency, ColumnProperties.Nullable, Ordinal = 26, MaxLength = 0)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ShipName => FetchColumn("ShipName");

            public static QueryColumn ShipAddress => FetchColumn("ShipAddress");

            public static QueryColumn ShipCity => FetchColumn("ShipCity");

            public static QueryColumn ShipRegion => FetchColumn("ShipRegion");

            public static QueryColumn ShipPostalCode => FetchColumn("ShipPostalCode");

            public static QueryColumn ShipCountry => FetchColumn("ShipCountry");

            public static QueryColumn CustomerId => FetchColumn("CustomerID");

            public static QueryColumn CustomerName => FetchColumn("CustomerName");

            public static QueryColumn Address => FetchColumn("Address");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn Region => FetchColumn("Region");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn Country => FetchColumn("Country");

            public static QueryColumn Salesperson => FetchColumn("Salesperson");

            public static QueryColumn OrderId => FetchColumn("OrderID");

            public static QueryColumn OrderDate => FetchColumn("OrderDate");

            public static QueryColumn RequiredDate => FetchColumn("RequiredDate");

            public static QueryColumn ShippedDate => FetchColumn("ShippedDate");

            public static QueryColumn ShipperName => FetchColumn("ShipperName");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ProductName => FetchColumn("ProductName");

            public static QueryColumn UnitPrice => FetchColumn("UnitPrice");

            public static QueryColumn Quantity => FetchColumn("Quantity");

            public static QueryColumn Discount => FetchColumn("Discount");

            public static QueryColumn ExtendedPrice => FetchColumn("ExtendedPrice");

            public static QueryColumn Freight => FetchColumn("Freight");
        }

        #endregion
    }
}