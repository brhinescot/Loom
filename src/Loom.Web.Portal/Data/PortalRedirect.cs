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

namespace Loom.Web.Portal.Data.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.Redirect table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Redirect", "RedirectId")]
    public class Redirect : DataRecord<Redirect>
    {
        private string _expression;
        private bool _isPermanent;

        private int _redirectId;
        private string _redirectUrl;
        private int _tenantId;

        public Redirect() { }
        protected Redirect(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RedirectId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int RedirectId
        {
            get => _redirectId;
            set
            {
                if (value == _redirectId && IsPropertyDirty("RedirectId"))
                    return;

                _redirectId = value;
                MarkDirty("RedirectId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TenantId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("TenantId", typeof(Tenant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int TenantId
        {
            get => _tenantId;
            set
            {
                if (value == _tenantId && IsPropertyDirty("TenantId"))
                    return;

                _tenantId = value;
                MarkDirty("TenantId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Expression", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 1024)]
        public string Expression
        {
            get => _expression;
            set
            {
                if (value == _expression && IsPropertyDirty("Expression"))
                    return;

                _expression = value;
                MarkDirty("Expression");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RedirectUrl", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 1024)]
        public string RedirectUrl
        {
            get => _redirectUrl;
            set
            {
                if (value == _redirectUrl && IsPropertyDirty("RedirectUrl"))
                    return;

                _redirectUrl = value;
                MarkDirty("RedirectUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("IsPermanent", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((1))")]
        public bool IsPermanent
        {
            get => _isPermanent;
            set
            {
                if (value == _isPermanent && IsPropertyDirty("IsPermanent"))
                    return;

                _isPermanent = value;
                MarkDirty("IsPermanent");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn RedirectId => FetchColumn("RedirectId");

            public static QueryColumn TenantId => FetchColumn("TenantId");

            public static QueryColumn Expression => FetchColumn("Expression");

            public static QueryColumn RedirectUrl => FetchColumn("RedirectUrl");

            public static QueryColumn IsPermanent => FetchColumn("IsPermanent");
        }

        #endregion
    }
}