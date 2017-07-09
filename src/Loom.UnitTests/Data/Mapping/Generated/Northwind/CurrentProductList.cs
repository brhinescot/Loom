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
    ///     This is an DataRecord class which wraps the dbo.Current Product List table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Current Product List", ReadOnly = true)]
    public class CurrentProductList : DataRecord<CurrentProductList>
    {
        private int _productId;
        private string _productName;

        public CurrentProductList() { }
        protected CurrentProductList(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.Identity, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("ProductName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 40)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ProductName => FetchColumn("ProductName");
        }

        #endregion
    }
}