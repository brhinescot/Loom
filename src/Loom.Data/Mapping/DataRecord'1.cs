#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

// ReSharper disable StaticFieldInGenericType
#endregion

#region Using Directives

using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Loom.Data.Diff;
using Loom.Data.Mapping.Schema;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping
{
    ///<summary>
    ///</summary>
    ///<typeparam name="TDataRecord"></typeparam>
    [Serializable, DebuggerDisplay("Table=[{DataRecord<TDataRecord>.Table.Owner,nq}.{DataRecord<TDataRecord>.Table.Name,nq}], IsDirty={IsDirty}")]
    public abstract class DataRecord<TDataRecord> : DataEntity<TDataRecord>, ISerializable, IXmlSerializable
        where TDataRecord : DataRecord<TDataRecord>, new()
    {
        #region Type Fields

        private static readonly int IsDirtyRecord = BitVector32.CreateMask();
        private static readonly int LoadedFromDatasource = BitVector32.CreateMask(IsDirtyRecord);

        #endregion

        #region Member Fields

        [NonSerialized]
        private BitVector32 flags = new BitVector32(0);
        private readonly DataRecordVersions<TDataRecord> versions;

        #endregion

        #region Property Accessors

        /// <summary>
        /// Gets a <see cref="TableData"/> object representing a table in the data source.
        /// </summary>
        public static TableData Table
        {
            get { return TableSingleton.Singleton; }
        }

        [DiffVisible(false), XmlIgnore]
        public bool IsLoadedFromDatasource
        {
            get { return flags[LoadedFromDatasource]; }
            set { flags[LoadedFromDatasource] = value; }
        }

        #endregion

        #region .ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRecord{TDataRecord}"/> class.
        /// </summary>
        protected DataRecord()
        {
            versions = new DataRecordVersions<TDataRecord>(Properties, this);
        }

        #endregion
        
        #region Versioning

        /// <summary>
        /// Resets all property values to the state of the previous <see cref="Insert"/>, <see cref="Update"/> or <see cref="SaveVersion"/> specified by
        /// the <paramref name="steps"/> parameter.
        /// </summary>
        /// <param name="steps">The number of steps in history to undo.</param>
        /// <returns><see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.</returns>
        public bool Undo(int steps)
        {
            return DataRecordVersions.Undo(steps);
        }

        /// <summary>
        /// Saves the current property values to history.
        /// </summary>
        /// <returns>An <see cref="int"/> representing the current number of steps in history.</returns>
        public int SaveVersion()
        {
            return DataRecordVersions.Version();
        }

        /// <summary>
        /// Resets all property values to the state of the previous <see cref="Insert"/>, <see cref="Update"/> or <see cref="SaveVersion"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.</returns>
        internal bool Undo()
        {
            return DataRecordVersions.Undo(1);
        }

        /// <summary>
        /// Gets the number of history steps currently stored for this instance.
        /// </summary>
        [DiffVisible(false), XmlIgnore]
        internal int DataRecordHistorySteps
        {
            get { return DataRecordVersions.HistorySteps; }
        }

        [DiffVisible(false), XmlIgnore]
        private DataRecordVersions<TDataRecord> DataRecordVersions
        {
            get { return versions; }
        }

        #endregion

        #region Runtime Serialization

        #region .ctors

        protected DataRecord(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                if (Compare.AreSameOrdinal("isDirtyRecord", entry.Name))
                    continue;

                Properties[entry.Name].InvokeSetter((TDataRecord) this, entry.Value);
            }

            flags[IsDirtyRecord] = info.GetBoolean("isDirtyRecord");
        }

        #endregion

        ///<summary>
        ///Populates a <see cref="SerializationInfo"></see> with the data needed to serialize the target object.
        ///</summary>
        ///<param name="context">The destination (see <see cref="System.Runtime.Serialization.StreamingContext"></see>) 
        /// for this serialization. </param>
        ///<param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"></see> to populate with data. </param>
        ///<exception cref="System.Security.SecurityException">The caller does not have the required permission. </exception>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (DynamicProperty<TDataRecord> property in Properties)
                info.AddValue(property.Name, property.InvokeGetter((TDataRecord) this));

            info.AddValue("isDirtyRecord", flags[IsDirtyRecord]);
        }

        #endregion

        #region Custom XML Serialization

        /// <exception cref="InvalidOperationException"><c>InvalidOperationException</c>.</exception>
        XmlSchema IXmlSerializable.GetSchema()
        {
            // Should never be called by XmlSerializer
            throw new NotImplementedException("The method IXmlSerializable.GetSchema() should never be called on this type.");
        }

        ///<summary>
        ///Generates an object from its XML representation.
        ///</summary>
        ///
        ///<param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from 
        /// which the object is deserialized. </param> 
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            ReadXml(reader);
        }

        ///<summary>
        ///Converts an object into its XML representation.
        ///</summary>
        ///
        ///<param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to 
        /// which the object is serialized. </param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            WriteXml(writer);
        }

        /// <summary>
        /// Creates an xml schema for the derived classes. This method is specified by the <see cref="XmlSchemaProviderAttribute"/>
        /// applied to derived classes.
        /// </summary>
        /// <param name="xs">An <see cref="XmlSchemaSet"/> that is to be populated with schema information.</param>
        /// <returns>An <see cref="XmlQualifiedName"/> instance.</returns>
        /// <param name="targetNamespace"></param>
        /// <param name="allowNillable"></param>
        protected static XmlQualifiedName CreateXmlSchema(XmlSchemaSet xs, string targetNamespace, bool allowNillable)
        {
            XmlTypeSchemas additionalSchemas = new XmlTypeSchemas();
            xs.Add(GenerateClassSchema(additionalSchemas, targetNamespace, allowNillable));

            for (int i = 0; i < additionalSchemas.RequiredSchemas.Count; i++)
                xs.Add(additionalSchemas.RequiredSchemas[i]);

            return new XmlQualifiedName(Table.Name, targetNamespace);
        }

        /// <summary>
        /// When implemented in a derived class, customizes the way the instance is deserialized from xml.
        /// </summary>
        /// <param name="reader">An <see cref="XmlReader"/> supplied by the xml serialization process.</param>
        protected virtual void ReadXml(XmlReader reader)
        {
            Argument.Assert.IsNotNull(reader, Argument.Names.reader);

            reader.ReadStartElement();
            while (reader.IsStartElement())
                AddPropertyValue(reader.Name, reader);
        }

        /// <summary>
        /// When implemented in a derived class, customizes the way the instance is serialized to xml.
        /// </summary>
        /// <param name="writer">An <see cref="XmlWriter"/> supplied by the xml serialization process.</param>
        protected virtual void WriteXml(XmlWriter writer)
        {
            Argument.Assert.IsNotNull(writer, Argument.Names.writer);

            foreach (var property in Properties)
            {
                object value = property.InvokeGetter((TDataRecord) this);
                if (value == null)
                    continue;

                if (value.GetType() == typeof (byte[]))
                {
                    writer.WriteStartElement(property.Name);
                    byte[] buffer = (byte[]) value;
                    writer.WriteBase64(buffer, 0, buffer.Length);
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteElementString(property.Name, value.ToString());
                }
            }
        }

        #region Private Methods

        private static XmlSchema GenerateClassSchema(XmlTypeSchemas additionalSchemas, string targetNamespace, bool allowNillable)
        {
            XmlSchema schema = new XmlSchema {TargetNamespace = targetNamespace, ElementFormDefault = XmlSchemaForm.Qualified};

            // complexType
            XmlSchemaComplexType complexType = new XmlSchemaComplexType {Name = Table.Name};
            schema.Items.Add(complexType);

            // sequence
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            complexType.Particle = sequence;

            // element
            foreach (QueryColumn info in Table.Columns)
                sequence.Items.Add(additionalSchemas.RetrieveXmlSchemaObject(info.Name, info.ColumnProperties, info.DbType, allowNillable));

            return schema;
        }
           
        // TODO: Add remaining types for xml serialization.
        private void AddPropertyValue(string columnName, XmlReader reader)
        {
            IQueryableColumn column = Table.Columns.FindColumn(columnName);
            if (column == null)
                return;

            switch (column.DbType)
            {
                case DbType.Int32:
                    this[columnName] = reader.ReadElementContentAsInt();
                    break;
                case DbType.Int64:
                    this[columnName] = reader.ReadElementContentAsLong();
                    break;
                case DbType.Int16:
                    this[columnName] = (short) reader.ReadElementContentAsInt();
                    break;
                case DbType.Single:
                    this[columnName] = reader.ReadElementContentAsFloat();
                    break;
                case DbType.Boolean:
                    this[columnName] = reader.ReadElementContentAsBoolean();
                    break;
                case DbType.Date:
                case DbType.Time:
                case DbType.DateTime:
                    this[columnName] = DateTime.Parse(reader.ReadElementContentAsString());
                    break;
                case DbType.Decimal:
                case DbType.Currency:
                case DbType.VarNumeric:
                    this[columnName] = reader.ReadElementContentAsDecimal();
                    break;
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.Guid:
                    this[columnName] = reader.ReadElementContentAsString();
                    break;
                case DbType.Binary:
                    byte[] buffer = new byte[50];
                    using (MemoryStream stream = new MemoryStream())
                    {
                        int readBytes;
                        while ((readBytes = reader.ReadElementContentAsBase64(buffer, 0, 50)) > 0)
                            stream.Write(buffer, 0, readBytes);
                        this[columnName] = stream.ToArray();
                    }
                    break;
                case DbType.Byte:
                    this[columnName] = Convert.ToByte(reader.ReadElementContentAsObject());
                    break;
                case DbType.Double:
                    this[columnName] = reader.ReadElementContentAsDouble();
                    break;
                case DbType.Object:
                    this[columnName] = reader.ReadElementContentAsObject();
                    break;
                case DbType.SByte:
                    this[columnName] = Convert.ToSByte(reader.ReadElementContentAsObject());
                    break;
                case DbType.UInt16:
                    this[columnName] = Convert.ToUInt16(reader.ReadElementContentAsObject());
                    break;
                case DbType.UInt32:
                    this[columnName] = Convert.ToUInt32(reader.ReadElementContentAsObject());
                    break;
                case DbType.UInt64:
                    this[columnName] = Convert.ToUInt64(reader.ReadElementContentAsObject());
                    break;
                case DbType.Xml:
                    throw new NotSupportedException("DbType.Xml is not supported");
                default:
                    this[columnName] = reader.ReadElementContentAsObject();
                    break;
            }
        }

        #endregion

        #endregion

        #region Diff

        /// <summary>
        /// Creates a <see cref="Loom.Data.Diff"/> between this instance and the supplied <see cref="TDataRecord"/>.
        /// </summary>
        /// <param name="other">The <see cref="TDataRecord"/> to compare to this instance.</param>
        /// <returns>
        /// A <see cref="Loom.Data.Diff"/> instance representing the differences between the 
        /// property values of this instance and the supplied instance of a 
        /// <see cref="DataRecord{TDataRecord}"/>
        /// </returns>
        public Diff<TDataRecord> CreateDiff(TDataRecord other)
        {
            DiffGenerator<TDataRecord> diff = new DiffGenerator<TDataRecord>();
            return diff.Generate((TDataRecord) this, other);
        }

        /// <summary>
        /// Creates a <see cref="Loom.Data.Diff"/> between this instance and the supplied <see cref="DiffBaseline"/>.
        /// </summary>
        /// <param name="baseline">The <see cref="Diff.DiffBaseline"/> to compare to this instance.</param>
        /// <returns>A <see cref="Loom.Data.Diff"/> instance representing the differences between the property values 
        /// of this instance and the cached values of the <see cref="Diff.DiffBaseline"/>. </returns>
        public Diff<TDataRecord> CreateDiff(DiffBaseline baseline)
        {
            DiffGenerator<TDataRecord> diffGenerator = new DiffGenerator<TDataRecord>();
            return diffGenerator.Generate(baseline, (TDataRecord) this);
        }

        /// <summary>
        /// Creates a <see cref="DiffBaseline"/> from this instance.
        /// </summary>
        /// <returns>A <see cref="DiffBaseline"/> representing the current values of all the properties 
        /// of this instance.</returns>
        public DiffBaseline CreateDiffBaseline()
        {
            DiffGenerator<TDataRecord> diffGenerator = new DiffGenerator<TDataRecord>();
            return diffGenerator.GenerateBaseline((TDataRecord) this);
        }

        #endregion

        internal object this[IQueryableColumn column]
        {
            get
            {
                if (column == null)
                    return null;

                DynamicProperty<TDataRecord> property = Properties[column.Name];
                return property == null ? null : property.InvokeGetter((TDataRecord)this);
            }
            set
            {
                DynamicProperty<TDataRecord> property = Properties[column.Name];
                if (property != null)
                    property.InvokeSetter((TDataRecord)this, value);
            }
        }

        protected static QueryColumn FetchColumn(string columnName)
        {
            return Table.FindColumn(columnName);
        }

        private static class TableSingleton
        {
            internal static readonly TableData Singleton;

            static TableSingleton()
            {
                Singleton = TableData.CreateUnitialized(typeof(TDataRecord));
                Singleton.Initialize();
            }
        }
    }
}