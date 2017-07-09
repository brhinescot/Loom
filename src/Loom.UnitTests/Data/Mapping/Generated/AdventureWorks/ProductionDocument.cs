#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.HumanResources;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.Document table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Document", "DocumentNode", ModifiedOnColumn = "ModifiedDate")]
    public class Document : DataRecord<Document>
    {
        private int _changeNumber;
        private byte[] _document;
        private short? _documentLevel;

        private string _documentNode;
        private string _documentSummary;
        private string _fileExtension;
        private string _fileName;
        private bool _folderFlag;
        private DateTime _modifiedDate;
        private int _owner;
        private string _revision;
        private Guid _rowguid;
        private short _status;
        private string _title;

        public Document() { }
        protected Document(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Document records.
        /// </summary>
        [ActiveColumn("DocumentNode", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 892)]
        public string DocumentNode
        {
            get => _documentNode;
            set
            {
                if (value == _documentNode && IsPropertyDirty("DocumentNode"))
                    return;

                _documentNode = value;
                MarkDirty("DocumentNode");
            }
        }

        /// <summary>
        ///     Depth in the document hierarchy.
        /// </summary>
        [ActiveColumn("DocumentLevel", DbType.Int16, ColumnProperties.Computed | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public short? DocumentLevel
        {
            get => _documentLevel;
            set
            {
                if (value == _documentLevel && IsPropertyDirty("DocumentLevel"))
                    return;

                _documentLevel = value;
                MarkDirty("DocumentLevel");
            }
        }

        /// <summary>
        ///     Title of the document.
        /// </summary>
        [ActiveColumn("Title", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title && IsPropertyDirty("Title"))
                    return;

                _title = value;
                MarkDirty("Title");
            }
        }

        /// <summary>
        ///     Employee who controls the document.  Foreign key to Employee.BusinessEntityID
        /// </summary>
        [ActiveColumn("Owner", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Employee), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int Owner
        {
            get => _owner;
            set
            {
                if (value == _owner && IsPropertyDirty("Owner"))
                    return;

                _owner = value;
                MarkDirty("Owner");
            }
        }

        /// <summary>
        ///     0 = This is a folder, 1 = This is a document.
        /// </summary>
        [ActiveColumn("FolderFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public bool FolderFlag
        {
            get => _folderFlag;
            set
            {
                if (value == _folderFlag && IsPropertyDirty("FolderFlag"))
                    return;

                _folderFlag = value;
                MarkDirty("FolderFlag");
            }
        }

        /// <summary>
        ///     File name of the document
        /// </summary>
        [ActiveColumn("FileName", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 400)]
        public string FileName
        {
            get => _fileName;
            set
            {
                if (value == _fileName && IsPropertyDirty("FileName"))
                    return;

                _fileName = value;
                MarkDirty("FileName");
            }
        }

        /// <summary>
        ///     File extension indicating the document type. For example, .doc or .txt.
        /// </summary>
        [ActiveColumn("FileExtension", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 8)]
        public string FileExtension
        {
            get => _fileExtension;
            set
            {
                if (value == _fileExtension && IsPropertyDirty("FileExtension"))
                    return;

                _fileExtension = value;
                MarkDirty("FileExtension");
            }
        }

        /// <summary>
        ///     Revision number of the document.
        /// </summary>
        [ActiveColumn("Revision", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 5)]
        public string Revision
        {
            get => _revision;
            set
            {
                if (value == _revision && IsPropertyDirty("Revision"))
                    return;

                _revision = value;
                MarkDirty("Revision");
            }
        }

        /// <summary>
        ///     Engineering change approval number.
        /// </summary>
        [ActiveColumn("ChangeNumber", DbType.Int32, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        public int ChangeNumber
        {
            get => _changeNumber;
            set
            {
                if (value == _changeNumber && IsPropertyDirty("ChangeNumber"))
                    return;

                _changeNumber = value;
                MarkDirty("ChangeNumber");
            }
        }

        /// <summary>
        ///     1 = Pending approval, 2 = Approved, 3 = Obsolete
        /// </summary>
        [ActiveColumn("Status", DbType.Int16, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
        public short Status
        {
            get => _status;
            set
            {
                if (value == _status && IsPropertyDirty("Status"))
                    return;

                _status = value;
                MarkDirty("Status");
            }
        }

        /// <summary>
        ///     Document abstract.
        /// </summary>
        [ActiveColumn("DocumentSummary", DbType.String, ColumnProperties.Nullable, Ordinal = 11)]
        public string DocumentSummary
        {
            get => _documentSummary;
            set
            {
                if (value == _documentSummary && IsPropertyDirty("DocumentSummary"))
                    return;

                _documentSummary = value;
                MarkDirty("DocumentSummary");
            }
        }

        /// <summary>
        ///     Complete document.
        /// </summary>
        [ActiveColumn("Document", DbType.Binary, ColumnProperties.Nullable, Ordinal = 12)]
        public byte[] Document_
        {
            get => _document;
            set
            {
                if (value == _document && IsPropertyDirty("Document"))
                    return;

                _document = value;
                MarkDirty("Document");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Required for FileStream.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.Unique, Ordinal = 13, MaxLength = 0, DefaultValue = "(newid())")]
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
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 14, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn DocumentNode => FetchColumn("DocumentNode");

            public static QueryColumn DocumentLevel => FetchColumn("DocumentLevel");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn Owner => FetchColumn("Owner");

            public static QueryColumn FolderFlag => FetchColumn("FolderFlag");

            public static QueryColumn FileName => FetchColumn("FileName");

            public static QueryColumn FileExtension => FetchColumn("FileExtension");

            public static QueryColumn Revision => FetchColumn("Revision");

            public static QueryColumn ChangeNumber => FetchColumn("ChangeNumber");

            public static QueryColumn Status => FetchColumn("Status");

            public static QueryColumn DocumentSummary => FetchColumn("DocumentSummary");

            public static QueryColumn Document => FetchColumn("Document");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}