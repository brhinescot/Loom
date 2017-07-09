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

namespace AdventureWorks.Person
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Person.Password table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "Password", ModifiedOnColumn = "ModifiedDate")]
    public class Password : DataRecord<Password>
    {
        private DateTime _modifiedDate;

        private string _passwordHash;
        private string _passwordSalt;
        private Guid _rowguid;

        public Password() { }
        protected Password(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Password for the e-mail account.
        /// </summary>
        [ActiveColumn("PasswordHash", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 128)]
        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (value == _passwordHash && IsPropertyDirty("PasswordHash"))
                    return;

                _passwordHash = value;
                MarkDirty("PasswordHash");
            }
        }

        /// <summary>
        ///     Random value concatenated with the password string before the password is hashed.
        /// </summary>
        [ActiveColumn("PasswordSalt", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 10)]
        public string PasswordSalt
        {
            get => _passwordSalt;
            set
            {
                if (value == _passwordSalt && IsPropertyDirty("PasswordSalt"))
                    return;

                _passwordSalt = value;
                MarkDirty("PasswordSalt");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn PasswordHash => FetchColumn("PasswordHash");

            public static QueryColumn PasswordSalt => FetchColumn("PasswordSalt");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}