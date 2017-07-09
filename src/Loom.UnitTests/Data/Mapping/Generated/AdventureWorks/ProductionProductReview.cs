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
    ///     This is an DataRecord class which wraps the Production.ProductReview table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductReview", "ProductReviewID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductReview : DataRecord<ProductReview>
    {
        private string _comments;
        private string _emailAddress;
        private DateTime _modifiedDate;
        private int _productId;

        private int _productReviewId;
        private int _rating;
        private DateTime _reviewDate;
        private string _reviewerName;

        public ProductReview() { }
        protected ProductReview(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for ProductReview records.
        /// </summary>
        [ActiveColumn("ProductReviewID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ProductReviewId
        {
            get => _productReviewId;
            set
            {
                if (value == _productReviewId && IsPropertyDirty("ProductReviewID"))
                    return;

                _productReviewId = value;
                MarkDirty("ProductReviewID");
            }
        }

        /// <summary>
        ///     Product identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId && IsPropertyDirty("ProductID"))
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        ///     Name of the reviewer.
        /// </summary>
        [ActiveColumn("ReviewerName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string ReviewerName
        {
            get => _reviewerName;
            set
            {
                if (value == _reviewerName && IsPropertyDirty("ReviewerName"))
                    return;

                _reviewerName = value;
                MarkDirty("ReviewerName");
            }
        }

        /// <summary>
        ///     Date review was submitted.
        /// </summary>
        [ActiveColumn("ReviewDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ReviewDate
        {
            get => _reviewDate;
            set
            {
                if (value == _reviewDate && IsPropertyDirty("ReviewDate"))
                    return;

                _reviewDate = value;
                MarkDirty("ReviewDate");
            }
        }

        /// <summary>
        ///     Reviewer's e-mail address.
        /// </summary>
        [ActiveColumn("EmailAddress", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                if (value == _emailAddress && IsPropertyDirty("EmailAddress"))
                    return;

                _emailAddress = value;
                MarkDirty("EmailAddress");
            }
        }

        /// <summary>
        ///     Product rating given by the reviewer. Scale is 1 to 5 with 5 as the highest rating.
        /// </summary>
        [ActiveColumn("Rating", DbType.Int32, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
        public int Rating
        {
            get => _rating;
            set
            {
                if (value == _rating && IsPropertyDirty("Rating"))
                    return;

                _rating = value;
                MarkDirty("Rating");
            }
        }

        /// <summary>
        ///     Reviewer's comments
        /// </summary>
        [ActiveColumn("Comments", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 3850)]
        public string Comments
        {
            get => _comments;
            set
            {
                if (value == _comments && IsPropertyDirty("Comments"))
                    return;

                _comments = value;
                MarkDirty("Comments");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(getdate())")]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductReviewId => FetchColumn("ProductReviewID");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ReviewerName => FetchColumn("ReviewerName");

            public static QueryColumn ReviewDate => FetchColumn("ReviewDate");

            public static QueryColumn EmailAddress => FetchColumn("EmailAddress");

            public static QueryColumn Rating => FetchColumn("Rating");

            public static QueryColumn Comments => FetchColumn("Comments");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}