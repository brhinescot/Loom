#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.UI;
using Loom.Dynamic;
using Loom.Web.Caching;

#endregion

namespace Loom.Web.UI.WebControls
{
    public static class ControlLocalizer
    {
        private const string Dot = ".";

        public static void Localize<T>(T control, string resourceKey) where T : Control
        {
            Localize(control, resourceKey, null);
        }

        public static void Localize<T>(T control, string resourceKey, Collection<string> additionalLocalizables) where T : Control
        {
            Argument.Assert.IsNotNull(control, nameof(control));
            Argument.Assert.IsNotNullOrEmpty(resourceKey, nameof(resourceKey));

            LocalizePrivate(control, resourceKey, additionalLocalizables);
        }

        private static IEnumerable<PropertyInfo> EnumerateLocalizableProperties<T>(T control, Collection<string> additionalLocalizables) where T : Control
        {
            bool hasAdditional = !Compare.IsNullOrEmpty(additionalLocalizables);

            foreach (PropertyInfo property in control.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                LocalizableAttribute attr = (LocalizableAttribute) Attribute.GetCustomAttribute(property, typeof(LocalizableAttribute));
                if (attr != null)
                {
                    yield return property;
                }
                else
                {
                    if (hasAdditional && additionalLocalizables.Contains(property.Name))
                        yield return property;
                }
            }
        }

        private static void LocalizePrivate<T>(T control, string resourceKey, Collection<string> additionalLocalizables) where T : Control
        {
            string virtualPath = control.Page.Request.AppRelativeCurrentExecutionFilePath;
            string cacheKey = CacheExtensions.CachePrefix + virtualPath + resourceKey + CultureInfo.CurrentUICulture.Name;

            List<CachedResource<T>> cached = control.Page.Cache[cacheKey] as List<CachedResource<T>>;

            if (cached != null)
                SetCachedResources(control, virtualPath, cached);
            else
                SetNonCachedResources(control, resourceKey, virtualPath, cacheKey, additionalLocalizables);
        }

        private static void SetCachedResources<T>(T control, string virtualPath, ICollection<CachedResource<T>> cached) where T : Control
        {
            if (cached.Count == 0)
                return;

            foreach (CachedResource<T> cachedResource in cached)
            {
                object resource = HttpContext.GetLocalResourceObject(virtualPath, cachedResource.ResourceKey, null);
                if (resource == null)
                    continue;

                cachedResource.Property.InvokeSetterOn(control, resource);
            }
        }

        private static void SetNonCachedResources<T>(T control, string resourceKey, string virtualPath, string cacheKey, Collection<string> additionalLocalizables) where T : Control
        {
            List<CachedResource<T>> cached = new List<CachedResource<T>>();

            foreach (PropertyInfo property in EnumerateLocalizableProperties(control, additionalLocalizables))
            {
                string dbResourceId = resourceKey + Dot + property.Name;
                object resource = HttpContext.GetLocalResourceObject(virtualPath, dbResourceId, null);
                if ((string) resource == string.Empty)
                    continue;

                DynamicProperty<T> dynamicProperty = DynamicType<T>.CreateDynamicProperty(property.Name);
                cached.Add(new CachedResource<T>(dbResourceId, dynamicProperty));
                if (dynamicProperty.HasSetter)
                    dynamicProperty.InvokeSetterOn(control, resource);
            }

            control.Page.Cache[cacheKey] = cached;
        }

        #region Nested type: CachedResource

        private sealed class CachedResource<T>
        {
            public CachedResource(string resourceKey, DynamicProperty<T> property)
            {
                Property = property;
                ResourceKey = resourceKey;
            }

            public DynamicProperty<T> Property { get; }
            public string ResourceKey { get; }
        }

        #endregion
    }
}