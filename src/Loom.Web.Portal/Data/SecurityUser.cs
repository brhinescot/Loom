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
using Loom.Web.Portal.Data.Person;

#endregion

namespace Loom.Web.Portal.Data.Security
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Security.User table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Security", "User", "UserId", DeletedColumn = "Deleted")]
    public class User : DataRecord<User>
    {
        private int _contactId;
        private bool _deleted;
        private int? _deletedBy;
        private DateTime? _deletedOn;
        private int _loginId;

        private int _userId;
        private string _userName;

        public User() { }
        protected User(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("UserId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int UserId
        {
            get => _userId;
            set
            {
                if (value == _userId && IsPropertyDirty("UserId"))
                    return;

                _userId = value;
                MarkDirty("UserId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UserName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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
        /// </summary>
        [ActiveColumn("ContactId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ContactId", typeof(Contact), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ContactId
        {
            get => _contactId;
            set
            {
                if (value == _contactId && IsPropertyDirty("ContactId"))
                    return;

                _contactId = value;
                MarkDirty("ContactId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("LoginId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("LoginId", typeof(Login), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int LoginId
        {
            get => _loginId;
            set
            {
                if (value == _loginId && IsPropertyDirty("LoginId"))
                    return;

                _loginId = value;
                MarkDirty("LoginId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public DateTime? DeletedOn
        {
            get => _deletedOn;
            set
            {
                if (value == _deletedOn && IsPropertyDirty("DeletedOn"))
                    return;

                _deletedOn = value;
                MarkDirty("DeletedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DeletedBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? DeletedBy
        {
            get => _deletedBy;
            set
            {
                if (value == _deletedBy && IsPropertyDirty("DeletedBy"))
                    return;

                _deletedBy = value;
                MarkDirty("DeletedBy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0))")]
        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value == _deleted && IsPropertyDirty("Deleted"))
                    return;

                _deleted = value;
                MarkDirty("Deleted");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn UserId => FetchColumn("UserId");

            public static QueryColumn UserName => FetchColumn("UserName");

            public static QueryColumn ContactId => FetchColumn("ContactId");

            public static QueryColumn LoginId => FetchColumn("LoginId");

            public static QueryColumn DeletedOn => FetchColumn("DeletedOn");

            public static QueryColumn DeletedBy => FetchColumn("DeletedBy");

            public static QueryColumn Deleted => FetchColumn("Deleted");
        }

        #endregion
    }
}