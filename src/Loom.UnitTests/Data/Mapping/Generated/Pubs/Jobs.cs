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

namespace Pubs
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.jobs table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "jobs", "job_id")]
    public class Jobs : DataRecord<Jobs>
    {
        private string _job_Desc;

        private short _job_Id;
        private short _max_Lvl;
        private short _min_Lvl;

        public Jobs() { }
        protected Jobs(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("job_id", DbType.Int16, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public short Job_id
        {
            get => _job_Id;
            set
            {
                if (value == _job_Id)
                    return;

                _job_Id = value;
                MarkDirty("job_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("job_desc", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50, DefaultValue = "('New Position - title not formalized yet')")]
        public string Job_desc
        {
            get => _job_Desc;
            set
            {
                if (value == _job_Desc)
                    return;

                _job_Desc = value;
                MarkDirty("job_desc");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("min_lvl", DbType.Int16, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public short Min_lvl
        {
            get => _min_Lvl;
            set
            {
                if (value == _min_Lvl)
                    return;

                _min_Lvl = value;
                MarkDirty("min_lvl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("max_lvl", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short Max_lvl
        {
            get => _max_Lvl;
            set
            {
                if (value == _max_Lvl)
                    return;

                _max_Lvl = value;
                MarkDirty("max_lvl");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Job_id => FetchColumn("job_id");

            public static QueryColumn Job_desc => FetchColumn("job_desc");

            public static QueryColumn Min_lvl => FetchColumn("min_lvl");

            public static QueryColumn Max_lvl => FetchColumn("max_lvl");
        }

        #endregion
    }
}