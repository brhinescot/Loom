#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

#endregion

namespace Loom.Data.Diff
{
    /// <summary>
    /// </summary>
    public sealed class DiffBaseline
    {
        private readonly List<DiffBaselineEntry> innerList = new List<DiffBaselineEntry>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffBaseline" /> class.
        /// </summary>
        /// <param name="reflectedType">Type of the reflected.</param>
        private DiffBaseline(Type reflectedType)
        {
            ReflectedType = reflectedType;
        }

        /// <summary>
        ///     Gets the type of the reflected.
        /// </summary>
        /// <value>The type of the reflected.</value>
        public Type ReflectedType { get; }

        /// <summary>
        ///     Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public Collection<DiffBaselineEntry> Entries => new Collection<DiffBaselineEntry>(innerList);

        /// <summary>
        ///     Adds the specified property type.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="baselineValue">The baseline value.</param>
        public void Add(string propertyName, string baselineValue)
        {
            innerList.Add(new DiffBaselineEntry(propertyName, baselineValue));
        }

        /// <summary>
        ///     Creates the base diff.
        /// </summary>
        /// <param name="baseLine">The base object.</param>
        /// <returns></returns>
        public static DiffBaseline Create(object baseLine)
        {
            return Create(baseLine, null);
        }

        /// <summary>
        ///     Creates the base diff.
        /// </summary>
        /// <param name="baseline">The base object.</param>
        /// <param name="properties">The included property names.</param>
        /// <returns></returns>
        public static DiffBaseline Create(object baseline, params string[] properties)
        {
            Argument.Assert.IsNotNull(baseline, nameof(baseline));

            DiffBaseline diff = new DiffBaseline(baseline.GetType());

            Type baselineType = baseline.GetType();
            if (properties == null)
                for (int i = 0; i < baselineType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Length; i++)
                    AddPropertyToBaseline(baselineType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[i], baseline, baselineType, diff);
            else
                for (int i = 0; i < properties.Length; i++)
                    AddPropertyToBaseline(baselineType.GetProperty(properties[i]), baseline, baselineType, diff);

            return diff;
        }

        private static void AddPropertyToBaseline(MemberInfo propertyInfo, object baseline, Type baselineType, DiffBaseline diff)
        {
            DiffVisibleAttribute attr = (DiffVisibleAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof(DiffVisibleAttribute));
            if (attr == null || attr.Visible)
            {
                object baselineValue = baselineType.GetProperty(propertyInfo.Name).GetValue(baseline, null);
                diff.Add(propertyInfo.Name, baselineValue != null ? baselineValue.ToString() : null);
            }
        }
    }
}