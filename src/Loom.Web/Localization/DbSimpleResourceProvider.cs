#region Using Directives

using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     Implementation of a very simple database Resource Provider. This implementation
    ///     is self contained and doesn't use a custom ResourceManager. Instead it
    ///     talks directly to the data resoure business layer (DbResourceDataManager).
    ///     Dependencies:
    ///     DbResourceDataManager
    ///     DbResourceConfiguration
    ///     You can replace those depencies (marked below in code) with your own data access
    ///     management. The two dependcies manage all data access as well as configuration
    ///     management via web.config configuration section. It's easy to remove these
    ///     and instead use custom data access code of your choice.
    /// </summary>
    public sealed class DbSimpleResourceProvider : System.Web.Compilation.IResourceProvider, IResourceProvider
    {
        /// <summary>
        ///     Keep track of the 'className' passed by ASP.NET
        ///     which is the ResourceSetId in the database.
        /// </summary>
        private readonly string resourceSetName;

        /// <summary>
        ///     Cache for each culture of this ResourceSet. Once
        ///     loaded we just cache the resources.
        /// </summary>
        private IDictionary resourceCache;

        public static bool ProviderLoaded;

        /// <summary>
        ///     Critical section for loading Resource Cache safely
        /// </summary>
        private static readonly object SyncLock = new object();

        public DbSimpleResourceProvider(string className)
        {
            if (!ProviderLoaded)
                ProviderLoaded = true;

            resourceSetName = className;
            DbResourceConfiguration.LoadedProviders.Add(this);
        }

        /// <summary>
        ///     Manages caching of the Resource Sets. Once loaded the values are loaded from the
        ///     cache only.
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        private IDictionary GetResourceCache(string cultureName)
        {
            if (cultureName == null)
                cultureName = "";

            if (resourceCache == null)
                resourceCache = new ListDictionary();

            IDictionary resources = resourceCache[cultureName] as IDictionary;
            if (resources == null)
            {
                // DEPENDENCY HERE (#1): Using DbResourceDataManager to retrieve resources

                // Use datamanager to retrieve the resource keys from the database
                DbResourceDataManager data = new DbResourceDataManager();

                lock (SyncLock)
                {
                    if (resourceCache.Contains(cultureName))
                        resources = resourceCache[cultureName] as IDictionary;
                    else
                        resources = data.GetResourceSet(cultureName, resourceSetName);
                    resourceCache[cultureName] = resources;
                }
            }

            return resources;
        }

        /// <summary>
        ///     Clears out the resource cache which forces all resources to be reloaded from
        ///     the database.
        ///     This is never actually called as far as I can tell
        /// </summary>
        public void ClearResourceCache()
        {
            resourceCache.Clear();
        }

        /// <summary>
        ///     The main worker method that retrieves a resource key for a given culture
        ///     from a ResourceSet.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object System.Web.Compilation.IResourceProvider.GetObject(string resourceKey, CultureInfo culture)
        {
            string cultureName = culture != null ? culture.Name : CultureInfo.CurrentUICulture.Name;

            return GetObjectInternal(resourceKey, cultureName);
        }

        /// <summary>
        ///     Internal lookup method that handles retrieving a resource
        ///     by its resource id and culture. Realistically this method
        ///     is always called with the culture being null or empty
        ///     but the routine handles resource fallback in case the
        ///     code is manually called.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        private object GetObjectInternal(string resourceKey, string cultureName)
        {
            IDictionary resources = GetResourceCache(cultureName);

            object value = resources == null ? null : resources[resourceKey];

            // If we're at a specific culture (en-Us) and there's no value fall back
            // to the generic culture (en)
            if (value == null && cultureName.Length > 3)
                return GetObjectInternal(resourceKey, cultureName.Substring(0, 2));

            // If the value is still null get the invariant value
            if (value == null)
            {
                resources = GetResourceCache("");
                value = resources == null ? null : resources[resourceKey];
            }

            // If the value is still null and we're at the invariant culture
            // let's add a marker that the value is missing
            // this also allows the pre-compiler to work and never return null
            if (value == null && string.IsNullOrEmpty(cultureName))
                value = "";

            return value;
        }

        /// <summary>
        ///     The Resource Reader is used parse over the resource collection
        ///     that the ResourceSet contains. It's basically an IEnumarable interface
        ///     implementation and it's what's used to retrieve the actual keys
        /// </summary>
        public IResourceReader ResourceReader
        {
            get
            {
                if (resourceReader != null)
                    return resourceReader;

                resourceReader = new DbSimpleResourceReader(GetResourceCache(null));
                return resourceReader;
            }
        }

        private DbSimpleResourceReader resourceReader;

        // IImplict Resource Provider implementation is purely optional
        //     If not provided ASP.NET uses a default implementation.
#if false
        #region IImplicitResourceProvider Members

        /// <summary>
        /// Called when an ASP.NET Page is compiled asking for a collection
        /// of keys that match a given control name (keyPrefix). This
        /// routine for example returns control.Text,control.ToolTip from the
        /// Resource collection if they exist when a request for "control"
        /// is made as the key prefix.
        /// </summary>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        public ICollection GetImplicitResourceKeys(string keyPrefix)
        {
            List<ImplicitResourceKey> keys = new List<ImplicitResourceKey>();

            IDictionaryEnumerator Enumerator = this.ResourceReader.GetEnumerator();
            if (Enumerator == null)
                return keys; // Cannot return null!

            foreach (DictionaryEntry dictentry in this.ResourceReader)
            {
                string key = (string)dictentry.Key;

                if (key.StartsWith(keyPrefix + ".", StringComparison.InvariantCultureIgnoreCase) == true)
                {
                    string keyproperty = String.Empty;
                    if (key.Length > (keyPrefix.Length + 1))
                    {
                        int pos = key.IndexOf('.');
                        if ((pos > 0) && (pos == keyPrefix.Length))
                        {
                            keyproperty = key.Substring(pos + 1);
                            if (String.IsNullOrEmpty(keyproperty) == false)
                            {
                                //Debug.WriteLine("Adding Implicit Key: " + keyPrefix + " - " + keyproperty);
                                ImplicitResourceKey implicitkey = new ImplicitResourceKey(String.Empty, keyPrefix, keyproperty);
                                keys.Add(implicitkey);
                            }
                        }
                    }
                }
            }
            return keys;
        }


        /// <summary>
        /// Returns an Implicit key value from the ResourceSet. 
        /// Note this method is called only if a ResourceKey was found in the
        /// ResourceSet at load time. If a resource cannot be located this
        /// method is never called to retrieve it. IOW, GetImplicitResourceKeys
        /// determines which keys are actually retrievable.
        /// 
        /// This method simply parses the Implicit key and then retrieves
        /// the value using standard GetObject logic for the ResourceID.
        /// </summary>
        /// <param name="implicitKey"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object GetObject(ImplicitResourceKey implicitKey, CultureInfo culture)
        {
            string ResourceKey = ConstructFullKey(implicitKey);

            string CultureName = null;
            if (culture != null)
                CultureName = culture.Name;
            else
                CultureName = CultureInfo.CurrentUICulture.Name;

            return this.GetObjectInternal(ResourceKey, CultureName);
        }


        /// <summary>
        /// Routine that generates a full resource key string from
        /// an Implicit Resource Key value
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static string ConstructFullKey(ImplicitResourceKey entry)
        {
            string text = entry.KeyPrefix + "." + entry.Property;
            if (entry.Filter.Length > 0)
            {
                text = entry.Filter + ":" + text;
            }
            return text;
        }

        #endregion
#endif
    }
}