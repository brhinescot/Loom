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

namespace AdventureWorks.HumanResources
{
    /// <summary>
    ///     This is an DataRecord class which wraps the HumanResources.EmployeePayHistory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "EmployeePayHistory", "RateChangeDate", ModifiedOnColumn = "ModifiedDate")]
    public class EmployeePayHistory : DataRecord<EmployeePayHistory>
    {
        private int _businessEntityId;
        private DateTime _modifiedDate;
        private short _payFrequency;
        private decimal _rate;
        private DateTime _rateChangeDate;

        public EmployeePayHistory() { }
        protected EmployeePayHistory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Employee identification number. Foreign key to Employee.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Employee), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        ///     Date the change in pay is effective
        /// </summary>
        [ActiveColumn("RateChangeDate", DbType.DateTime, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        public DateTime RateChangeDate
        {
            get => _rateChangeDate;
            set
            {
                if (value == _rateChangeDate && IsPropertyDirty("RateChangeDate"))
                    return;

                _rateChangeDate = value;
                MarkDirty("RateChangeDate");
            }
        }

        /// <summary>
        ///     Salary hourly rate.
        /// </summary>
        [ActiveColumn("Rate", DbType.Currency, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public decimal Rate
        {
            get => _rate;
            set
            {
                if (value == _rate && IsPropertyDirty("Rate"))
                    return;

                _rate = value;
                MarkDirty("Rate");
            }
        }

        /// <summary>
        ///     1 = Salary received monthly, 2 = Salary received biweekly
        /// </summary>
        [ActiveColumn("PayFrequency", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short PayFrequency
        {
            get => _payFrequency;
            set
            {
                if (value == _payFrequency && IsPropertyDirty("PayFrequency"))
                    return;

                _payFrequency = value;
                MarkDirty("PayFrequency");
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
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn RateChangeDate => FetchColumn("RateChangeDate");

            public static QueryColumn Rate => FetchColumn("Rate");

            public static QueryColumn PayFrequency => FetchColumn("PayFrequency");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}