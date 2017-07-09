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
    ///     This is an DataRecord class which wraps the dbo.Categories table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Categories", "CategoryID")]
    public class Categories : DataRecord<Categories>
    {
        private int _categoryId;
        private string _categoryName;
        private string _description;
        private byte[] _picture;

        public Categories() { }
        protected Categories(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (value == _categoryId)
                    return;

                _categoryId = value;
                MarkDirty("CategoryID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 15)]
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                if (value == _categoryName)
                    return;

                _categoryName = value;
                MarkDirty("CategoryName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 1073741823)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description)
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Picture", DbType.Binary, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 2147483647)]
        public byte[] Picture
        {
            get => _picture;
            set
            {
                if (value == _picture)
                    return;

                _picture = value;
                MarkDirty("Picture");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CategoryId => FetchColumn("CategoryID");

            public static QueryColumn CategoryName => FetchColumn("CategoryName");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn Picture => FetchColumn("Picture");
        }

        #endregion
    }
}