#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Loom.Data.Diff;
using Loom.Dynamic;

#endregion

// ReSharper disable StaticFieldInGenericType
namespace Loom.Data
{
    public abstract class DataEntity<T> where T : DataEntity<T>, new()
    {
        private static readonly Func<object> Constructor = DynamicType<T>.CreateTypeConstructor();

        private readonly Dictionary<string, DynamicProperty<T>> updatedProperties = new Dictionary<string, DynamicProperty<T>>();

        public object this[string propertyName]
        {
            get => Properties.InvokeGetter((T) this, propertyName);
            set => Properties.InvokeSetter((T) this, propertyName, value);
        }

        public static DynamicProperties<T> Properties { get; } = new DynamicProperties<T>();

        /// <summary>
        ///     Gets a value indicating if the data in this <see cref="DataEntity{T}" /> object has changed.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The <see cref="DataEntity{T}" /> object is considered dirty if the data has changed
        ///         since it was loaded from the data source.
        ///     </para>
        /// </remarks>
        [DiffVisible(false)]
        [XmlIgnore]
        public bool IsDirty => updatedProperties.Count > 0;

        public IEnumerable<DynamicProperty<T>> GetUpdatedProperties()
        {
            foreach (DynamicProperty<T> property in updatedProperties.Values)
                yield return property;
        }

        /// <summary>
        ///     Returns a new object deserialized from the supplied <paramref name="xml" />.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>A new object representing the supplied <paramref name="xml" />.</returns>
        public static T FromXml(string xml)
        {
            Argument.Assert.IsNotNull(xml, nameof(xml));

            object output = null;
            if (xml.Length > 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    output = serializer.Deserialize(reader);
                }
            }
            return (T) output;
        }

        /// <summary>
        ///     Returns a new object deserialized from the supplied <paramref name="stream" />.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>A new object representing the xml from the supplied stream.</returns>
        public static T FromXml(Stream stream)
        {
            Argument.Assert.IsNotNull(stream, nameof(stream));

            object output;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(stream))
            {
                output = serializer.Deserialize(reader);
            }

            return (T) output;
        }

        /// <summary>
        ///     Returns a new object deserialized from the supplied <paramref name="reader" />.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>A new object representing the supplied xml.</returns>
        public static T FromXml(TextReader reader)
        {
            Argument.Assert.IsNotNull(reader, nameof(reader));

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(reader);
        }

        /// <summary>
        ///     Serializes this instance to an xml representation.
        /// </summary>
        /// <returns>A <see langword="string" /> containing an xml representation of this instance.</returns>
        public string ToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (Stream stream = new MemoryStream())
            {
                serializer.Serialize(stream, this);
                stream.Position = 0;
                using (StreamReader stmReader = new StreamReader(stream))
                {
                    return stmReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     Serializes this instance to an xml representation.
        /// </summary>
        public void ToXml(Stream stream)
        {
            Argument.Assert.IsNotNull(stream, nameof(stream));

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, this);
        }

        /// <summary>
        ///     Serializes this instance to an xml representation.
        /// </summary>
        public void ToXml(TextWriter writer)
        {
            Argument.Assert.IsNotNull(writer, nameof(writer));

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, this);
        }

        /// <summary>
        ///     Creates a deep copy of the constructed <see cref="DataEntity{T}" /> class.
        /// </summary>
        /// <returns>A new <see cref="DataEntity{T}" /> instance.</returns>
        public T DeepCopy()
        {
            using (Stream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return (T) formatter.Deserialize(stream);
            }
        }

        /// <summary>
        ///     Creates a shallow copy of the constructed <see cref="DataEntity{T}" /> class.
        /// </summary>
        /// <returns>A new <see cref="DataEntity{T}" /> instance.</returns>
        public T ShallowCopy()
        {
            T record = Instantiate();
            foreach (DynamicProperty<T> property in Properties)
            {
                object value = property.InvokeGetterOn((T) this);
                property.InvokeSetterOn(record, value);
            }
            return record;
        }

        public static T Instantiate()
        {
            return (T) Constructor();
        }

        protected void MarkDirty(string propertyName)
        {
            if (!updatedProperties.ContainsKey(propertyName))
                updatedProperties.Add(propertyName, Properties[propertyName]);
        }

        protected bool IsPropertyDirty(string propertyName)
        {
            return updatedProperties.ContainsKey(propertyName);
        }

        public void Clean()
        {
            updatedProperties.Clear();
        }
    }
}