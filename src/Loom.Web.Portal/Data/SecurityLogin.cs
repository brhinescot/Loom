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

namespace Loom.Web.Portal.Data.Security
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Security.Login table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Security", "Login", "LoginId", DeletedColumn = "Deleted")]
    public class Login : DataRecord<Login>
    {
        private bool _deleted;
        private int? _deletedBy;
        private DateTime? _deletedOn;
        private short _groupMask;
        private DateTime _lastActivityDate;
        private DateTime _lastLoginDate;

        private int _loginId;
        private string _password;
        private string _resetAnswer;
        private string _resetQuestion;

        public Login() { }
        protected Login(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("LoginId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Password", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 192)]
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
        [ActiveColumn("LastLoginDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime LastLoginDate
        {
            get => _lastLoginDate;
            set
            {
                if (value == _lastLoginDate && IsPropertyDirty("LastLoginDate"))
                    return;

                _lastLoginDate = value;
                MarkDirty("LastLoginDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("LastActivityDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime LastActivityDate
        {
            get => _lastActivityDate;
            set
            {
                if (value == _lastActivityDate && IsPropertyDirty("LastActivityDate"))
                    return;

                _lastActivityDate = value;
                MarkDirty("LastActivityDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("GroupMask", DbType.Int16, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public short GroupMask
        {
            get => _groupMask;
            set
            {
                if (value == _groupMask && IsPropertyDirty("GroupMask"))
                    return;

                _groupMask = value;
                MarkDirty("GroupMask");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ResetQuestion", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 150)]
        public string ResetQuestion
        {
            get => _resetQuestion;
            set
            {
                if (value == _resetQuestion && IsPropertyDirty("ResetQuestion"))
                    return;

                _resetQuestion = value;
                MarkDirty("ResetQuestion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ResetAnswer", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 192)]
        public string ResetAnswer
        {
            get => _resetAnswer;
            set
            {
                if (value == _resetAnswer && IsPropertyDirty("ResetAnswer"))
                    return;

                _resetAnswer = value;
                MarkDirty("ResetAnswer");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
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
        [ActiveColumn("DeletedBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 10, MaxLength = 0)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn LoginId => FetchColumn("LoginId");

            public static QueryColumn Password => FetchColumn("Password");

            public static QueryColumn LastLoginDate => FetchColumn("LastLoginDate");

            public static QueryColumn LastActivityDate => FetchColumn("LastActivityDate");

            public static QueryColumn GroupMask => FetchColumn("GroupMask");

            public static QueryColumn ResetQuestion => FetchColumn("ResetQuestion");

            public static QueryColumn ResetAnswer => FetchColumn("ResetAnswer");

            public static QueryColumn Deleted => FetchColumn("Deleted");

            public static QueryColumn DeletedOn => FetchColumn("DeletedOn");

            public static QueryColumn DeletedBy => FetchColumn("DeletedBy");
        }

        #endregion
    }
}