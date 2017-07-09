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
    ///     This is an DataRecord class which wraps the Sales.ManufacturerProduct table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "ManufacturerProduct", "ManufacturerProductId")]
    public class ManufacturerProduct : DataRecord<ManufacturerProduct>
    {
        private string _fastener;
        private decimal _holeHeight;
        private decimal _holeWidth;
        private int _manufacturerId;

        private int _manufacturerProductId;
        private string _model;
        private decimal _panelDepth;
        private decimal _panelHeight;
        private decimal _panelWidth;
        private decimal _screenSize;
        private string _screwSize;
        private bool _spacer;
        private string _type;
        private decimal _weight;

        public ManufacturerProduct() { }
        protected ManufacturerProduct(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ManufacturerProductId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ManufacturerProductId
        {
            get => _manufacturerProductId;
            set
            {
                if (value == _manufacturerProductId && IsPropertyDirty("ManufacturerProductId"))
                    return;

                _manufacturerProductId = value;
                MarkDirty("ManufacturerProductId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ManufacturerId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ManufacturerId", typeof(Manufacturer), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ManufacturerId
        {
            get => _manufacturerId;
            set
            {
                if (value == _manufacturerId && IsPropertyDirty("ManufacturerId"))
                    return;

                _manufacturerId = value;
                MarkDirty("ManufacturerId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Model", DbType.String, ColumnProperties.Unique, Ordinal = 3, MaxLength = 50)]
        public string Model
        {
            get => _model;
            set
            {
                if (value == _model && IsPropertyDirty("Model"))
                    return;

                _model = value;
                MarkDirty("Model");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Type", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 6)]
        public string Type
        {
            get => _type;
            set
            {
                if (value == _type && IsPropertyDirty("Type"))
                    return;

                _type = value;
                MarkDirty("Type");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Fastener", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 2)]
        public string Fastener
        {
            get => _fastener;
            set
            {
                if (value == _fastener && IsPropertyDirty("Fastener"))
                    return;

                _fastener = value;
                MarkDirty("Fastener");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ScrewSize", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 15)]
        public string ScrewSize
        {
            get => _screwSize;
            set
            {
                if (value == _screwSize && IsPropertyDirty("ScrewSize"))
                    return;

                _screwSize = value;
                MarkDirty("ScrewSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Spacer", DbType.Boolean, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0))")]
        public bool Spacer
        {
            get => _spacer;
            set
            {
                if (value == _spacer && IsPropertyDirty("Spacer"))
                    return;

                _spacer = value;
                MarkDirty("Spacer");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HoleHeight", DbType.Decimal, ColumnProperties.None, Ordinal = 8, MaxLength = 0)]
        public decimal HoleHeight
        {
            get => _holeHeight;
            set
            {
                if (value == _holeHeight && IsPropertyDirty("HoleHeight"))
                    return;

                _holeHeight = value;
                MarkDirty("HoleHeight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HoleWidth", DbType.Decimal, ColumnProperties.None, Ordinal = 9, MaxLength = 0)]
        public decimal HoleWidth
        {
            get => _holeWidth;
            set
            {
                if (value == _holeWidth && IsPropertyDirty("HoleWidth"))
                    return;

                _holeWidth = value;
                MarkDirty("HoleWidth");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Weight", DbType.Decimal, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
        public decimal Weight
        {
            get => _weight;
            set
            {
                if (value == _weight && IsPropertyDirty("Weight"))
                    return;

                _weight = value;
                MarkDirty("Weight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ScreenSize", DbType.Decimal, ColumnProperties.None, Ordinal = 11, MaxLength = 0)]
        public decimal ScreenSize
        {
            get => _screenSize;
            set
            {
                if (value == _screenSize && IsPropertyDirty("ScreenSize"))
                    return;

                _screenSize = value;
                MarkDirty("ScreenSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PanelWidth", DbType.Decimal, ColumnProperties.None, Ordinal = 12, MaxLength = 0)]
        public decimal PanelWidth
        {
            get => _panelWidth;
            set
            {
                if (value == _panelWidth && IsPropertyDirty("PanelWidth"))
                    return;

                _panelWidth = value;
                MarkDirty("PanelWidth");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PanelHeight", DbType.Decimal, ColumnProperties.None, Ordinal = 13, MaxLength = 0)]
        public decimal PanelHeight
        {
            get => _panelHeight;
            set
            {
                if (value == _panelHeight && IsPropertyDirty("PanelHeight"))
                    return;

                _panelHeight = value;
                MarkDirty("PanelHeight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PanelDepth", DbType.Decimal, ColumnProperties.None, Ordinal = 14, MaxLength = 0)]
        public decimal PanelDepth
        {
            get => _panelDepth;
            set
            {
                if (value == _panelDepth && IsPropertyDirty("PanelDepth"))
                    return;

                _panelDepth = value;
                MarkDirty("PanelDepth");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ManufacturerProductId => FetchColumn("ManufacturerProductId");

            public static QueryColumn ManufacturerId => FetchColumn("ManufacturerId");

            public static QueryColumn Model => FetchColumn("Model");

            public static QueryColumn Type => FetchColumn("Type");

            public static QueryColumn Fastener => FetchColumn("Fastener");

            public static QueryColumn ScrewSize => FetchColumn("ScrewSize");

            public static QueryColumn Spacer => FetchColumn("Spacer");

            public static QueryColumn HoleHeight => FetchColumn("HoleHeight");

            public static QueryColumn HoleWidth => FetchColumn("HoleWidth");

            public static QueryColumn Weight => FetchColumn("Weight");

            public static QueryColumn ScreenSize => FetchColumn("ScreenSize");

            public static QueryColumn PanelWidth => FetchColumn("PanelWidth");

            public static QueryColumn PanelHeight => FetchColumn("PanelHeight");

            public static QueryColumn PanelDepth => FetchColumn("PanelDepth");
        }

        #endregion
    }
}