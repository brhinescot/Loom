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

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.State table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "State", "StateId")]
    public class State : DataRecord<State>
    {
        private string _description;
        private string _state;

        private int _stateId;

        public State() { }
        protected State(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("StateId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int StateId
        {
            get => _stateId;
            set
            {
                if (value == _stateId && IsPropertyDirty("StateId"))
                    return;

                _stateId = value;
                MarkDirty("StateId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("State", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 2)]
        public string State_
        {
            get => _state;
            set
            {
                if (value == _state && IsPropertyDirty("State"))
                    return;

                _state = value;
                MarkDirty("State");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 20)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description && IsPropertyDirty("Description"))
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn StateId => FetchColumn("StateId");

            public static QueryColumn State => FetchColumn("State");

            public static QueryColumn Description => FetchColumn("Description");
        }

        #endregion
    }
}