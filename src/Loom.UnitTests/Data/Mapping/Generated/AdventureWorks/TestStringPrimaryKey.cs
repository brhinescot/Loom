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

namespace AdventureWorks.Test
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Test.StringPrimaryKey table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Test", "StringPrimaryKey", "UserName")]
    public class StringPrimaryKey : DataRecord<StringPrimaryKey>
    {
        private string _blc;
        private int _ident;
        private char _indicator;
        private string _name;
        private string _sign;
        private string _userName;

        public StringPrimaryKey() { }
        protected StringPrimaryKey(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Blc", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 6)]
        public string Blc
        {
            get => _blc;
            set
            {
                if (value == _blc)
                    return;

                _blc = value;
                MarkDirty("Blc");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UserName", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4)]
        public string UserName
        {
            get => _userName;
            set
            {
                if (value == _userName)
                    return;

                _userName = value;
                MarkDirty("UserName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ident", DbType.Int32, ColumnProperties.Identity, Ordinal = 6, MaxLength = 0)]
        public int Ident
        {
            get => _ident;
            set
            {
                if (value == _ident)
                    return;

                _ident = value;
                MarkDirty("Ident");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Sign", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 50)]
        public string Sign
        {
            get => _sign;
            set
            {
                if (value == _sign)
                    return;

                _sign = value;
                MarkDirty("Sign");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 50)]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name)
                    return;

                _name = value;
                MarkDirty("Name");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Indicator", DbType.AnsiStringFixedLength, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 1)]
        public char Indicator
        {
            get => _indicator;
            set
            {
                if (value == _indicator)
                    return;

                _indicator = value;
                MarkDirty("Indicator");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Blc => FetchColumn("Blc");

            public static QueryColumn UserName => FetchColumn("UserName");

            public static QueryColumn Ident => FetchColumn("Ident");

            public static QueryColumn Sign => FetchColumn("Sign");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Indicator => FetchColumn("Indicator");
        }

        #endregion
    }
}