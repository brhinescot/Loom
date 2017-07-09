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
using OmniMount.Portal;

#endregion

namespace OmniMount.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.Category table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Category", "CategoryId")]
    public class Category : DataRecord<Category>
    {
        private int _applicationId;

        private int _categoryId;
        private string _description;
        private string _name;
        private int _ordinal;
        private int? _parentId;

        public Category() { }
        protected Category(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (value == _categoryId && IsPropertyDirty("CategoryId"))
                    return;

                _categoryId = value;
                MarkDirty("CategoryId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ParentId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("CategoryId", typeof(Category), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ParentId
        {
            get => _parentId;
            set
            {
                if (value == _parentId && IsPropertyDirty("ParentId"))
                    return;

                _parentId = value;
                MarkDirty("ParentId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
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
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 400)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public int Ordinal
        {
            get => _ordinal;
            set
            {
                if (value == _ordinal && IsPropertyDirty("Ordinal"))
                    return;

                _ordinal = value;
                MarkDirty("Ordinal");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0, DefaultValue = "((1))")]
        [ForeignColumn("ApplicationId", typeof(Application), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                if (value == _applicationId && IsPropertyDirty("ApplicationId"))
                    return;

                _applicationId = value;
                MarkDirty("ApplicationId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CategoryId => FetchColumn("CategoryId");

            public static QueryColumn ParentId => FetchColumn("ParentId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}