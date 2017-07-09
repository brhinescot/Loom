#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Diff
{
    public class DiffGenerator<T>
    {
        private readonly List<Introspector> introspectors = new List<Introspector>();
        private readonly string name;

        public DiffGenerator(params string[] properties) : this(false, properties) { }

        public DiffGenerator(bool ignoreProperties, params string[] properties)
        {
            Type diffItemType = typeof(T);
            name = diffItemType.Name;

            if (properties == null || properties.Length == 0)
            {
                InitializeAllIntrospection(diffItemType);
                return;
            }

            if (ignoreProperties)
                InitializeIgnoreIntrospection(diffItemType, properties);
            else
                InitializeIntrospection(properties);
        }

        public DiffGenerator(ICollection<FormattedProperty> propertyFormats)
        {
            Type diffItemType = typeof(T);
            name = diffItemType.Name;

            if (propertyFormats != null && propertyFormats.Count != 0)
            {
                InitializeIntrospection(propertyFormats);
                return;
            }

            PropertyInfo[] propertyInfos = diffItemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> propertyNames = new List<string>();
            foreach (PropertyInfo info in propertyInfos)
                if (!info.PropertyType.IsSubclassOf(typeof(IEnumerable)))
                    propertyNames.Add(info.Name);
            InitializeIntrospection(propertyNames);
        }

        /// <summary>
        ///     Generates a <see cref="Diff{T}" /> between the specified <paramref name="baseline" /> and
        ///     <paramref name="working" /> objects.
        /// </summary>
        /// <param name="baseline">The base object.</param>
        /// <param name="working">The working object.</param>
        /// <returns>
        ///     A <see cref="Diff{T}" /> object containing a list of properties that have
        ///     changed.
        /// </returns>
        public Diff<T> Generate(T baseline, T working)
        {
            Argument.Assert.IsNotNull(baseline, nameof(baseline));
            Argument.Assert.IsNotNull(working, nameof(working));

            if (baseline.GetType() != working.GetType())
                throw new ArgumentException();

            return GeneratePrivate(baseline, working);
        }

        private Diff<T> GeneratePrivate(T baseline, T working)
        {
            Diff<T> diff = new Diff<T>();
            diff.BaselineItem = baseline;
            diff.WorkingItem = working;
            diff.Name = name;

            foreach (Introspector introspector in introspectors)
            {
                DiffVisibleAttribute attribute = introspector.DiffVisibleAttribute;
                if (attribute != null && !attribute.Visible)
                    continue;

                string baselineValue = introspector.Getter(baseline, introspector.Format);
                string workingValue = introspector.Getter(working, introspector.Format);

                if (string.Compare(baselineValue, workingValue, StringComparison.Ordinal) != 0)
                    diff.AddEntry(attribute != null && attribute.FriendlyName != null ? attribute.FriendlyName : introspector.Name, baselineValue, workingValue);
            }

            return diff;
        }

        internal static Diff<T> CreateEmpty()
        {
            return new Diff<T>();
        }

        /// <summary>
        ///     Creates the base diff.
        /// </summary>
        /// <param name="baseLine">The base object.</param>
        /// <returns></returns>
        public DiffBaseline GenerateBaseline(T baseLine)
        {
            return GenerateBaseline(baseLine, null);
        }

        /// <summary>
        ///     Creates the base diff.
        /// </summary>
        /// <param name="baseline">The base object.</param>
        /// <param name="properties">The included property names.</param>
        /// <returns></returns>
        public DiffBaseline GenerateBaseline(T baseline, params string[] properties)
        {
            return DiffBaseline.Create(baseline, properties);
        }

        /// <summary>
        ///     Executes the specified base property diff.
        /// </summary>
        /// <param name="baseline">The base property diff.</param>
        /// <param name="working">The new object.</param>
        /// <returns></returns>
        public Diff<T> Generate(DiffBaseline baseline, T working)
        {
            Argument.Assert.IsNotNull(baseline, nameof(baseline));
            Argument.Assert.IsNotNull(working, nameof(working));

            if (baseline.ReflectedType != working.GetType())
                throw new ArgumentException("The type of 'baseline.ReflectedType' and 'working' must match.");

            Diff<T> diff = new Diff<T>();
            diff.Name = baseline.ReflectedType.Name;

            for (int i = 0; i < baseline.Entries.Count; i++)
            {
                DiffBaselineEntry entry = baseline.Entries[i];
                PropertyInfo propertyInfo = baseline.ReflectedType.GetProperty(entry.Name);
                DiffVisibleAttribute attr = (DiffVisibleAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof(DiffVisibleAttribute));
                if (attr == null || attr.Visible)
                {
                    string newValueString = null;
                    object newValue = baseline.ReflectedType.GetProperty(entry.Name).GetValue(working, null);
                    if (newValue != null)
                        newValueString = newValue.ToString();

                    if (string.Compare(entry.BaselineValue, newValueString, StringComparison.Ordinal) != 0)
                        diff.AddEntry(attr != null && attr.FriendlyName != null ? attr.FriendlyName : entry.Name, entry.BaselineValue, newValueString);
                }
            }
            return diff;
        }

        private void InitializeAllIntrospection(Type diffItemType)
        {
            PropertyInfo[] propertyInfos = diffItemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> propertyNames = new List<string>();
            foreach (PropertyInfo info in propertyInfos)
                if (!info.PropertyType.IsSubclassOf(typeof(IEnumerable)))
                    propertyNames.Add(info.Name);
            InitializeIntrospection(propertyNames);
        }

        private void InitializeIgnoreIntrospection(Type diffItemType, ICollection<string> propertiesToIgnore)
        {
            PropertyInfo[] propertyInfos = diffItemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> propertyNames = new List<string>();
            foreach (PropertyInfo info in propertyInfos)
            {
                if (propertiesToIgnore.Contains(info.Name))
                    continue;

                if (!info.PropertyType.IsSubclassOf(typeof(IEnumerable)))
                    propertyNames.Add(info.Name);
            }
            InitializeIntrospection(propertyNames);
        }

        private void InitializeIntrospection(IList<string> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                string property = properties[i];
                FormattablePropertyGetter<T> getter = DynamicType<T>.CreateFormattablePropertyGetter(property);
                PropertyInfo propertyInfo = typeof(T).GetProperty(property);
                Introspector introspector = new Introspector(getter, propertyInfo);
                introspectors.Add(introspector);
            }
        }

        private void InitializeIntrospection(IEnumerable<FormattedProperty> formattedProperties)
        {
            foreach (FormattedProperty formattedProperty in formattedProperties)
            {
                FormattablePropertyGetter<T> getter = DynamicType<T>.CreateFormattablePropertyGetter(formattedProperty.PropertyName);
                PropertyInfo propertyInfo = typeof(T).GetProperty(formattedProperty.PropertyName);
                Introspector introspector = new Introspector(getter, propertyInfo, formattedProperty.Format);
                introspectors.Add(introspector);
            }
        }

        #region Nested type: Introspector

        [DebuggerDisplay("Name={Name}")]
        private class Introspector
        {
            public Introspector(FormattablePropertyGetter<T> getter, MemberInfo propertyInfo) : this(getter, propertyInfo, null) { }

            public Introspector(FormattablePropertyGetter<T> getter, MemberInfo propertyInfo, string format)
            {
                Getter = getter;
                Name = propertyInfo.Name;
                DiffVisibleAttribute = (DiffVisibleAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof(DiffVisibleAttribute));
                if (format != null)
                    Format = format;
                else if (DiffVisibleAttribute != null)
                    Format = DiffVisibleAttribute.Format;
            }

            public FormattablePropertyGetter<T> Getter { get; }

            public DiffVisibleAttribute DiffVisibleAttribute { get; }

            public string Name { get; }

            public string Format { get; }
        }

        #endregion
    }
}