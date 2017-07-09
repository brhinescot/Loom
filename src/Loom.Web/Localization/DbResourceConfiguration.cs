#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.Design;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     The configuration class that is used to configure the Resource Provider.
    ///     This class contains various configuration settings that the provider requires
    ///     to operate both at design time and runtime.
    ///     The application uses the static Current property to access the actual
    ///     configuration settings object. By default it reads the configuration settings
    ///     from web.config (at runtime). You can override this behavior by creating your
    ///     own configuration object and assigning it to the DbResourceConfiguration.Current property.
    /// </summary>
    public sealed class DbResourceConfiguration
    {
        /// <summary>
        ///     A global instance of the current configuration. By default this instance reads its
        ///     configuration values from web.config at runtime, but it can be overridden to
        ///     assign specific values or completely replace this object.
        ///     NOTE: Any assignment made to this property should be made at Application_Start
        ///     or other 'application initialization' event so that these settings are applied
        ///     BEFORE the resource provider is used for the first time.
        /// </summary>
        public static DbResourceConfiguration Current;

        /// <summary>
        ///     Keep track of loaded providers so we can unload them
        /// </summary>
        internal static List<IResourceProvider> LoadedProviders = new List<IResourceProvider>();

        private string connectionString = "";

        /// <summary>
        ///     Static constructor for the Current property - guarantees this
        ///     code fires exactly once giving us a singleton instance
        ///     of the configuration object.
        /// </summary>
        static DbResourceConfiguration()
        {
            Current = new DbResourceConfiguration(true);
        }

        /// <summary>
        ///     Base constructor that doesn't do anything to the default values.
        /// </summary>
        public DbResourceConfiguration() { }

        /// <summary>
        ///     Default constructor used to read the configuration section to retrieve its values
        ///     on startup.
        /// </summary>
        /// <param name="readConfigurationSection"></param>
        public DbResourceConfiguration(bool readConfigurationSection)
        {
            if (readConfigurationSection)
                ReadConfigurationSection();
        }

        /// <summary>
        ///     Determines whether any resources that are not found are automatically
        ///     added to the resource file.
        ///     Note only applies to the Invariant culture.
        /// </summary>
        public bool AddMissingResources { get; set; } = true;

        /// <summary>
        ///     Database connection string to the resource data.
        ///     The string can either be a full connection string or an entry in the
        ///     ConnectionStrings section of web.config.
        ///     <seealso>
        ///         Class DbResource
        ///         Compiling Your Applications with the Provider
        ///     </seealso>
        /// </summary>
        public string ConnectionString
        {
            get
            {
                // If no = in the string assume it's a ConnectionStrings entry instead
                if (!connectionString.Contains("="))
                    try
                    {
                        return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
                    }
                    catch
                    {
                        return connectionString;
                    }
                return connectionString;
            }
            set => connectionString = value;
        }

        /// <summary>
        ///     The virtual path for the Web application. This value is used at design time.
        /// </summary>
        public string DesignTimeVirtualPath { get; set; } = "";

        /// <summary>
        ///     Determines the location of the Localization form in a Web relative path.
        ///     This form is popped up when clicking on Edit Resources in the
        ///     DbResourceControl
        /// </summary>
        public string LocalizationFormWebPath { get; set; } = "~/LocalizationAdmin/LocalizationAdmin.aspx";

        /// <summary>
        ///     Database table name used in the database
        /// </summary>
        public string ResourceTableName { get; set; } = "Localizations";

        /// <summary>
        ///     Determines whether page controls show icons when a
        ///     DbResourceControl is active. Note requires that ShowLocalizationControlOptions
        ///     is true as well.
        /// </summary>
        public bool ShowControlIcons { get; set; }

        /// <summary>
        ///     Determines whether the DbResourceControl shows its localization options on the
        ///     page.
        /// </summary>
        public bool ShowLocalizationControlOptions { get; set; }

        /// <summary>
        ///     Path and Name space of an optionally generated strongly typed resource
        ///     which is created when exporting to ResX resources.
        ///     Leave this value blank if you don't want a strongly typed resource class
        ///     generated for you.
        ///     Otherwise format is: (File Path,Namespace)
        ///     ~/App_Code/Resources.cs,AppResources
        /// </summary>
        public string StronglyTypedGlobalResource { get; set; } = "~/App_Code/Resources.cs,AppResources";

        /// <summary>
        ///     Determines whether generated Resource names use the same syntax
        ///     as VS.Net uses. Defaults to false, which uses simple control
        ///     name syntax (no ResourceX value) if possible. The dfeault value
        ///     is shown without a number and numbers are only used on duplication.
        /// </summary>
        public bool UseVsNetResourceNaming { get; set; }

        /// <summary>
        ///     Reads the DbResourceProvider Configuration Section and assigns the values
        ///     to the properties of this class
        /// </summary>
        /// <returns></returns>
        public bool ReadConfigurationSection()
        {
            object section = WebConfigurationManager.GetWebApplicationSection("DbResourceProvider");
            if (section == null)
                return false;

            DbResourceProviderSection resourceProviderSection = section as DbResourceProviderSection;
            ReadSectionValues(resourceProviderSection);

            return true;
        }

        /// <summary>
        ///     Handle design time access to the configuration settings - used for the
        ///     DbDesignTimeResourceProvider - when loaded we re-read the settings
        /// </summary>
        /// <param name="serviceProvider"></param>
        public bool ReadDesignTimeConfiguration(IServiceProvider serviceProvider)
        {
            IWebApplication webApp = serviceProvider.GetService(typeof(IWebApplication)) as IWebApplication;

            // Can't get an application instance - can only exit
            if (webApp == null)
                return false;

            object section = webApp.OpenWebConfiguration(true).GetSection("DbResourceProvider");
            if (section == null)
                return false;

            DbResourceProviderSection resourceProviderSection = section as DbResourceProviderSection;
            ReadSectionValues(resourceProviderSection);

            // If the connection string doesn't contain = then it's
            // a ConnectionString key from .config. This is handled in
            // in the propertyGet of the resource configration, but it uses
            // ConfigurationManager which is not available at design time
            //  So we have to duplicate the code here using the WebConfiguration.
            if (!ConnectionString.Contains("="))
                try
                {
                    string conn = webApp.OpenWebConfiguration(true).ConnectionStrings.ConnectionStrings[ConnectionString].ConnectionString;
                    ConnectionString = conn;
                }
                catch { }

            return true;
        }

        /// <summary>
        ///     Reads the actual section values
        /// </summary>
        /// <param name="section"></param>
        private void ReadSectionValues(DbResourceProviderSection section)
        {
            ConnectionString = section.ConnectionString;
            ResourceTableName = section.ResourceTableName;
            DesignTimeVirtualPath = section.DesignTimeVirtualPath;
            LocalizationFormWebPath = section.LocalizationFormWebPath;
            ShowLocalizationControlOptions = section.ShowLocalizationControlOptions;
            ShowControlIcons = section.ShowControlIcons;
            AddMissingResources = section.AddMissingResources;
            StronglyTypedGlobalResource = section.StronglyTypedGlobalResource;
        }

        /// <summary>
        ///     This static method clears all resources from the loaded Resource Providers
        ///     and forces them to be reloaded the next time they are requested.
        ///     Use this method after you've edited resources in the database and you want
        ///     to refresh the UI to show the newly changed values.
        ///     This method works by internally tracking all the loaded ResourceProvider
        ///     instances and calling the IwwResourceProvider.ClearResourceCache() method
        ///     on each of the provider instances. This method is called by the Resource
        ///     Administration form when you explicitly click the Reload Resources button.
        ///     <seealso>Class DbResourceConfiguration</seealso>
        /// </summary>
        public static void ClearResourceCache()
        {
            foreach (IResourceProvider provider in LoadedProviders)
                provider.ClearResourceCache();
        }
    }
}