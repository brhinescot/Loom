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
    ///     This is an DataRecord class which wraps the Production.vProductAndDescription table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "vProductAndDescription", ReadOnly = true)]
    public class VProductAndDescription : DataRecord<VProductAndDescription>
    {
        private string _cultureId;
        private string _description;
        private string _name;

        private string _productModel;

        public VProductAndDescription() { }
        protected VProductAndDescription(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductModel", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string ProductModel
        {
            get => _productModel;
            set
            {
                if (value == _productModel && IsPropertyDirty("ProductModel"))
                    return;

                _productModel = value;
                MarkDirty("ProductModel");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CultureID", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 6)]
        public string CultureId
        {
            get => _cultureId;
            set
            {
                if (value == _cultureId && IsPropertyDirty("CultureID"))
                    return;

                _cultureId = value;
                MarkDirty("CultureID");
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
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 400)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description && IsPropertyDirty("Description"))
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductModel => FetchColumn("ProductModel");

            public static QueryColumn CultureId => FetchColumn("CultureID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");
        }

        #endregion
    }
}