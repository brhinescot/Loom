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

namespace AdventureWorks
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.ErrorLog table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "ErrorLog", "ErrorLogID")]
    public class ErrorLog : DataRecord<ErrorLog>
    {
        private int? _errorLine;

        private int _errorLogId;
        private string _errorMessage;
        private int _errorNumber;
        private string _errorProcedure;
        private int? _errorSeverity;
        private int? _errorState;
        private DateTime _errorTime;
        private string _userName;

        public ErrorLog() { }
        protected ErrorLog(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for ErrorLog records.
        /// </summary>
        [ActiveColumn("ErrorLogID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ErrorLogId
        {
            get => _errorLogId;
            set
            {
                if (value == _errorLogId && IsPropertyDirty("ErrorLogID"))
                    return;

                _errorLogId = value;
                MarkDirty("ErrorLogID");
            }
        }

        /// <summary>
        ///     The date and time at which the error occurred.
        /// </summary>
        [ActiveColumn("ErrorTime", DbType.DateTime, ColumnProperties.None, Ordinal = 2, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ErrorTime
        {
            get => _errorTime;
            set
            {
                if (value == _errorTime && IsPropertyDirty("ErrorTime"))
                    return;

                _errorTime = value;
                MarkDirty("ErrorTime");
            }
        }

        /// <summary>
        ///     The user who executed the batch in which the error occurred.
        /// </summary>
        [ActiveColumn("UserName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 128)]
        public string UserName
        {
            get => _userName;
            set
            {
                if (value == _userName && IsPropertyDirty("UserName"))
                    return;

                _userName = value;
                MarkDirty("UserName");
            }
        }

        /// <summary>
        ///     The error number of the error that occurred.
        /// </summary>
        [ActiveColumn("ErrorNumber", DbType.Int32, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public int ErrorNumber
        {
            get => _errorNumber;
            set
            {
                if (value == _errorNumber && IsPropertyDirty("ErrorNumber"))
                    return;

                _errorNumber = value;
                MarkDirty("ErrorNumber");
            }
        }

        /// <summary>
        ///     The severity of the error that occurred.
        /// </summary>
        [ActiveColumn("ErrorSeverity", DbType.Int32, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public int? ErrorSeverity
        {
            get => _errorSeverity;
            set
            {
                if (value == _errorSeverity && IsPropertyDirty("ErrorSeverity"))
                    return;

                _errorSeverity = value;
                MarkDirty("ErrorSeverity");
            }
        }

        /// <summary>
        ///     The state number of the error that occurred.
        /// </summary>
        [ActiveColumn("ErrorState", DbType.Int32, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public int? ErrorState
        {
            get => _errorState;
            set
            {
                if (value == _errorState && IsPropertyDirty("ErrorState"))
                    return;

                _errorState = value;
                MarkDirty("ErrorState");
            }
        }

        /// <summary>
        ///     The name of the stored procedure or trigger where the error occurred.
        /// </summary>
        [ActiveColumn("ErrorProcedure", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 126)]
        public string ErrorProcedure
        {
            get => _errorProcedure;
            set
            {
                if (value == _errorProcedure && IsPropertyDirty("ErrorProcedure"))
                    return;

                _errorProcedure = value;
                MarkDirty("ErrorProcedure");
            }
        }

        /// <summary>
        ///     The line number at which the error occurred.
        /// </summary>
        [ActiveColumn("ErrorLine", DbType.Int32, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public int? ErrorLine
        {
            get => _errorLine;
            set
            {
                if (value == _errorLine && IsPropertyDirty("ErrorLine"))
                    return;

                _errorLine = value;
                MarkDirty("ErrorLine");
            }
        }

        /// <summary>
        ///     The message text of the error that occurred.
        /// </summary>
        [ActiveColumn("ErrorMessage", DbType.String, ColumnProperties.None, Ordinal = 9, MaxLength = 4000)]
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value == _errorMessage && IsPropertyDirty("ErrorMessage"))
                    return;

                _errorMessage = value;
                MarkDirty("ErrorMessage");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ErrorLogId => FetchColumn("ErrorLogID");

            public static QueryColumn ErrorTime => FetchColumn("ErrorTime");

            public static QueryColumn UserName => FetchColumn("UserName");

            public static QueryColumn ErrorNumber => FetchColumn("ErrorNumber");

            public static QueryColumn ErrorSeverity => FetchColumn("ErrorSeverity");

            public static QueryColumn ErrorState => FetchColumn("ErrorState");

            public static QueryColumn ErrorProcedure => FetchColumn("ErrorProcedure");

            public static QueryColumn ErrorLine => FetchColumn("ErrorLine");

            public static QueryColumn ErrorMessage => FetchColumn("ErrorMessage");
        }

        #endregion
    }
}