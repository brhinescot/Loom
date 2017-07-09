#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

#endregion

namespace Loom
{
    public sealed class EnumDescriptionAttribute : DescriptionAttribute
    {
        private static readonly Regex CapitalLetterMatch = new Regex("\\B[A-Z]", RegexOptions.Compiled);

        private static readonly int ReplacedFlag = BitVector32.CreateMask();

        private BitVector32 flags;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnumDescriptionAttribute" />.
        /// </summary>
        /// <param name="description">
        ///     The description of the <see cref="Enum" /> value.
        /// </param>
        public EnumDescriptionAttribute(string description) : base(description)
        {
            flags[ReplacedFlag] = false;
        }

        public EnumDescriptionAttribute()
        {
            flags[ReplacedFlag] = false;
        }

        /// <summary>
        ///     Gets the description of the <see cref="Enum" /> value.
        /// </summary>
        public override string Description => GetDescription();

        public string ResourceAssemblyName { get; set; }
        public string ResourceBaseName { get; set; }
        public string ResourceName { get; set; }

        /// <summary>
        ///     Returns the description property of the <see cref="EnumDescriptionAttribute" />
        ///     applied to the specified <see cref="Enum" /> value.
        /// </summary>
        /// <param name="decoratedEnum">
        ///     The <see cref="Enum" /> value decorated with an
        ///     <see cref="EnumDescriptionAttribute" />
        /// </param>
        /// <returns>
        ///     The <see cref="string" /> description of the <see cref="Enum" /> value.
        /// </returns>
        public static string ToString(Enum decoratedEnum)
        {
            Argument.Assert.IsNotNull(decoratedEnum, "decoratedEnum");

            return ToStringPrivate(decoratedEnum);
        }

        public static Dictionary<string, object> GetEnumData(Type enumType)
        {
            return GetEnumDataPrivate(enumType);
        }

        private static Dictionary<string, object> GetEnumDataPrivate(IReflect enumType)
        {
            FieldInfo[] fields = enumType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            Dictionary<string, object> dictionary = new Dictionary<string, object>(fields.Length);

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo enumField = fields[i];
                object[] attributes = enumField.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                string name = null;

                if (attributes.Length > 0)
                {
                    EnumDescriptionAttribute descriptionAttribute = (EnumDescriptionAttribute) attributes[0];
                    name = descriptionAttribute.Description;
                }

                if (Compare.IsNullOrEmpty(name))
                    name = CapitalLetterMatch.Replace(enumField.Name, " $&");

                dictionary.Add(name, enumField.GetRawConstantValue());
            }
            return dictionary;
        }

        private static string ToDefaultString(Enum decoratedEnum)
        {
            return CapitalLetterMatch.Replace(decoratedEnum.ToString(), " $&");
        }

        private static string ToStringPrivate(Enum decoratedEnum)
        {
            MemberInfo[] memberInfo = decoratedEnum.GetType().GetMember(decoratedEnum.ToString());
            if (memberInfo.Length == 1)
            {
                object[] customAttributes = memberInfo[0].GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (customAttributes.Length == 1)
                {
                    EnumDescriptionAttribute attribute = (EnumDescriptionAttribute) customAttributes[0];
                    string description = attribute.Description;
                    if (!Compare.IsNullOrEmpty(description))
                        return description;
                }
            }

            return ToDefaultString(decoratedEnum);
        }

        private string GetDescription()
        {
            if (flags[ReplacedFlag])
                return base.Description;

            if (!Compare.IsAnyNullOrEmpty(ResourceBaseName, ResourceAssemblyName, ResourceBaseName))
                DescriptionValue = GetResourceStringPrivate();

            flags[ReplacedFlag] = true;
            return base.Description;
        }

        private string GetResourceStringPrivate()
        {
            Assembly resourceAssembly = Assembly.Load(ResourceAssemblyName);
            if (resourceAssembly == null)
                return base.Description;

            ResourceManager resourceManager = new ResourceManager(ResourceBaseName, resourceAssembly);
            string description = resourceManager.GetString(ResourceName, CultureInfo.CurrentCulture);
            return description ?? base.Description;
        }
    }
}