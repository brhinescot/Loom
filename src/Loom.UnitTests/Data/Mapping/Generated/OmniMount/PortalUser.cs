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

namespace OmniMount.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.User table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "User", "UserId", DeletedColumn = "Deleted")]
    public class User : DataRecord<User>
    {
        private string _activationToken;
        private int? _contactId;
        private bool? _deleted;
        private string _password;
        private int _roleId;

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
        [ActiveColumn("UserName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 30)]
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
        [ActiveColumn("Password", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 192)]
        public string Password
        {
            get => _password;
            set
            {
                if (value == _password && IsPropertyDirty("Password"))
                    return;

                _password = value;
                MarkDirty("Password");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public bool? Deleted
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

        /// <summary>
        /// </summary>
        [ActiveColumn("ActivationToken", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 20)]
        public string ActivationToken
        {
            get => _activationToken;
            set
            {
                if (value == _activationToken && IsPropertyDirty("ActivationToken"))
                    return;

                _activationToken = value;
                MarkDirty("ActivationToken");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("ContactId", typeof(Contact), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ContactId
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
        [ActiveColumn("RoleId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 7, MaxLength = 0, DefaultValue = "((1))")]
        [ForeignColumn("RoleId", typeof(Role), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int RoleId
        {
            get => _roleId;
            set
            {
                if (value == _roleId && IsPropertyDirty("RoleId"))
                    return;

                _roleId = value;
                MarkDirty("RoleId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn UserId => FetchColumn("UserId");

            public static QueryColumn UserName => FetchColumn("UserName");

            public static QueryColumn Password => FetchColumn("Password");

            public static QueryColumn Deleted => FetchColumn("Deleted");

            public static QueryColumn ActivationToken => FetchColumn("ActivationToken");

            public static QueryColumn ContactId => FetchColumn("ContactId");

            public static QueryColumn RoleId => FetchColumn("RoleId");
        }

        #endregion
    }
}