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

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.vProductModelCatalogDescription table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "vProductModelCatalogDescription", ReadOnly = true, ModifiedOnColumn = "ModifiedDate")]
    public class VProductModelCatalogDescription : DataRecord<VProductModelCatalogDescription>
    {
        private string _bikeFrame;
        private string _color;
        private string _copyright;

        private string _crankset;
        private string _maintenanceDescription;
        private string _manufacturer;
        private string _material;
        private DateTime _modifiedDate;
        private string _name;
        private string _noOfYears;
        private string _pedal;
        private string _pictureAngle;
        private string _pictureSize;
        private string _productLine;
        private int _productModelId;
        private string _productPhotoId;
        private string _productURL;
        private string _riderExperience;
        private Guid _rowguid;
        private string _saddle;
        private string _style;
        private string _summary;
        private string _warrantyDescription;
        private string _warrantyPeriod;
        private string _wheel;

        public VProductModelCatalogDescription() { }
        protected VProductModelCatalogDescription(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Crankset", DbType.String, ColumnProperties.Nullable, Ordinal = 15, MaxLength = 256)]
        public string Crankset
        {
            get => _crankset;
            set
            {
                if (value == _crankset && IsPropertyDirty("Crankset"))
                    return;

                _crankset = value;
                MarkDirty("Crankset");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductLine", DbType.String, ColumnProperties.Nullable, Ordinal = 21, MaxLength = 256)]
        public string ProductLine
        {
            get => _productLine;
            set
            {
                if (value == _productLine && IsPropertyDirty("ProductLine"))
                    return;

                _productLine = value;
                MarkDirty("ProductLine");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductPhotoID", DbType.String, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 256)]
        public string ProductPhotoId
        {
            get => _productPhotoId;
            set
            {
                if (value == _productPhotoId && IsPropertyDirty("ProductPhotoID"))
                    return;

                _productPhotoId = value;
                MarkDirty("ProductPhotoID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NoOfYears", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 256)]
        public string NoOfYears
        {
            get => _noOfYears;
            set
            {
                if (value == _noOfYears && IsPropertyDirty("NoOfYears"))
                    return;

                _noOfYears = value;
                MarkDirty("NoOfYears");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Saddle", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 256)]
        public string Saddle
        {
            get => _saddle;
            set
            {
                if (value == _saddle && IsPropertyDirty("Saddle"))
                    return;

                _saddle = value;
                MarkDirty("Saddle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductURL", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 256)]
        public string ProductURL
        {
            get => _productURL;
            set
            {
                if (value == _productURL && IsPropertyDirty("ProductURL"))
                    return;

                _productURL = value;
                MarkDirty("ProductURL");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BikeFrame", DbType.String, ColumnProperties.Nullable, Ordinal = 14)]
        public string BikeFrame
        {
            get => _bikeFrame;
            set
            {
                if (value == _bikeFrame && IsPropertyDirty("BikeFrame"))
                    return;

                _bikeFrame = value;
                MarkDirty("BikeFrame");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Wheel", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 256)]
        public string Wheel
        {
            get => _wheel;
            set
            {
                if (value == _wheel && IsPropertyDirty("Wheel"))
                    return;

                _wheel = value;
                MarkDirty("Wheel");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PictureSize", DbType.String, ColumnProperties.Nullable, Ordinal = 17, MaxLength = 256)]
        public string PictureSize
        {
            get => _pictureSize;
            set
            {
                if (value == _pictureSize && IsPropertyDirty("PictureSize"))
                    return;

                _pictureSize = value;
                MarkDirty("PictureSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 25, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Summary", DbType.String, ColumnProperties.Nullable, Ordinal = 3)]
        public string Summary
        {
            get => _summary;
            set
            {
                if (value == _summary && IsPropertyDirty("Summary"))
                    return;

                _summary = value;
                MarkDirty("Summary");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Color", DbType.String, ColumnProperties.Nullable, Ordinal = 20, MaxLength = 256)]
        public string Color
        {
            get => _color;
            set
            {
                if (value == _color && IsPropertyDirty("Color"))
                    return;

                _color = value;
                MarkDirty("Color");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RiderExperience", DbType.String, ColumnProperties.Nullable, Ordinal = 23, MaxLength = 1024)]
        public string RiderExperience
        {
            get => _riderExperience;
            set
            {
                if (value == _riderExperience && IsPropertyDirty("RiderExperience"))
                    return;

                _riderExperience = value;
                MarkDirty("RiderExperience");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PictureAngle", DbType.String, ColumnProperties.Nullable, Ordinal = 16, MaxLength = 256)]
        public string PictureAngle
        {
            get => _pictureAngle;
            set
            {
                if (value == _pictureAngle && IsPropertyDirty("PictureAngle"))
                    return;

                _pictureAngle = value;
                MarkDirty("PictureAngle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MaintenanceDescription", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 256)]
        public string MaintenanceDescription
        {
            get => _maintenanceDescription;
            set
            {
                if (value == _maintenanceDescription && IsPropertyDirty("MaintenanceDescription"))
                    return;

                _maintenanceDescription = value;
                MarkDirty("MaintenanceDescription");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Material", DbType.String, ColumnProperties.Nullable, Ordinal = 19, MaxLength = 256)]
        public string Material
        {
            get => _material;
            set
            {
                if (value == _material && IsPropertyDirty("Material"))
                    return;

                _material = value;
                MarkDirty("Material");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Style", DbType.String, ColumnProperties.Nullable, Ordinal = 22, MaxLength = 256)]
        public string Style
        {
            get => _style;
            set
            {
                if (value == _style && IsPropertyDirty("Style"))
                    return;

                _style = value;
                MarkDirty("Style");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Pedal", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 256)]
        public string Pedal
        {
            get => _pedal;
            set
            {
                if (value == _pedal && IsPropertyDirty("Pedal"))
                    return;

                _pedal = value;
                MarkDirty("Pedal");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Copyright", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 30)]
        public string Copyright
        {
            get => _copyright;
            set
            {
                if (value == _copyright && IsPropertyDirty("Copyright"))
                    return;

                _copyright = value;
                MarkDirty("Copyright");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("WarrantyDescription", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 256)]
        public string WarrantyDescription
        {
            get => _warrantyDescription;
            set
            {
                if (value == _warrantyDescription && IsPropertyDirty("WarrantyDescription"))
                    return;

                _warrantyDescription = value;
                MarkDirty("WarrantyDescription");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 24, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("WarrantyPeriod", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 256)]
        public string WarrantyPeriod
        {
            get => _warrantyPeriod;
            set
            {
                if (value == _warrantyPeriod && IsPropertyDirty("WarrantyPeriod"))
                    return;

                _warrantyPeriod = value;
                MarkDirty("WarrantyPeriod");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Manufacturer", DbType.String, ColumnProperties.Nullable, Ordinal = 4)]
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (value == _manufacturer && IsPropertyDirty("Manufacturer"))
                    return;

                _manufacturer = value;
                MarkDirty("Manufacturer");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductModelID", DbType.Int32, ColumnProperties.Identity, Ordinal = 1, MaxLength = 0)]
        public int ProductModelId
        {
            get => _productModelId;
            set
            {
                if (value == _productModelId && IsPropertyDirty("ProductModelID"))
                    return;

                _productModelId = value;
                MarkDirty("ProductModelID");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Crankset => FetchColumn("Crankset");

            public static QueryColumn ProductLine => FetchColumn("ProductLine");

            public static QueryColumn ProductPhotoId => FetchColumn("ProductPhotoID");

            public static QueryColumn NoOfYears => FetchColumn("NoOfYears");

            public static QueryColumn Saddle => FetchColumn("Saddle");

            public static QueryColumn ProductURL => FetchColumn("ProductURL");

            public static QueryColumn BikeFrame => FetchColumn("BikeFrame");

            public static QueryColumn Wheel => FetchColumn("Wheel");

            public static QueryColumn PictureSize => FetchColumn("PictureSize");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");

            public static QueryColumn Summary => FetchColumn("Summary");

            public static QueryColumn Color => FetchColumn("Color");

            public static QueryColumn RiderExperience => FetchColumn("RiderExperience");

            public static QueryColumn PictureAngle => FetchColumn("PictureAngle");

            public static QueryColumn MaintenanceDescription => FetchColumn("MaintenanceDescription");

            public static QueryColumn Material => FetchColumn("Material");

            public static QueryColumn Style => FetchColumn("Style");

            public static QueryColumn Pedal => FetchColumn("Pedal");

            public static QueryColumn Copyright => FetchColumn("Copyright");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn WarrantyDescription => FetchColumn("WarrantyDescription");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn WarrantyPeriod => FetchColumn("WarrantyPeriod");

            public static QueryColumn Manufacturer => FetchColumn("Manufacturer");

            public static QueryColumn ProductModelId => FetchColumn("ProductModelID");
        }

        #endregion
    }
}