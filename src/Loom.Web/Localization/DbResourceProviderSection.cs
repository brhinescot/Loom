#region Using Directives

using System.ComponentModel;
using System.Configuration;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     This is the resource provider section that mimics the settings stored in DbResourceConfiguration object.
    /// </summary>
    public sealed class DbResourceProviderSection : ConfigurationSection
    {
        public DbResourceProviderSection(string ConnectionString, string ResourceTableName, string DesignTimeVirtualPath)
        {
            this.ConnectionString = ConnectionString;
            this.DesignTimeVirtualPath = DesignTimeVirtualPath;
            this.ResourceTableName = ResourceTableName;
        }

        public DbResourceProviderSection() { }

        [Description("Determines whether any missing resources are automatically added to the Invariant culture. Defaults to true")]
        [ConfigurationProperty("addMissingResources", DefaultValue = true)]
        public bool AddMissingResources
        {
            get => (bool) this["addMissingResources"];
            set => this["addMissingResources"] = value;
        }

        [ConfigurationProperty("connectionString", DefaultValue = "")]
        [Description("The connection string used to connect to the db Resourcewl provider")]
        public string ConnectionString
        {
            get => this["connectionString"] as string;
            set => this["connectionString"] = value;
        }

        [ConfigurationProperty("designTimeVirtualPath", DefaultValue = "")]
        [Description("The virtual path to the application. This value is used at design time and should be in the format of: /MyVirtual")]
        public string DesignTimeVirtualPath
        {
            get => this["designTimeVirtualPath"] as string;
            set => this["designTimeVirtualPath"] = value;
        }

        [Description("The web path to the administration localization form used to display and edit resources.")]
        [ConfigurationProperty("localizationFormWebPath", DefaultValue = "~/admin/localizeform.aspx")]
        public string LocalizationFormWebPath
        {
            get => this["localizationFormWebPath"] as string;
            set => this["localizationFormWebPath"] = value;
        }

        [ConfigurationProperty("resourceTableName", DefaultValue = "Localizations")]
        [Description("The name of the table used in the Connection String database for localizations.")]
        public string ResourceTableName
        {
            get => this["resourceTableName"] as string;
            set => this["resourceTableName"] = value;
        }

        [Description("Determines whether the DbResourceControl shows icons next to each control of a page to jump to the localization page.")]
        [ConfigurationProperty("showControlIcons", DefaultValue = "false")]
        public bool ShowControlIcons
        {
            get => (bool) this["showControlIcons"];
            set => this["showControlIcons"] = value;
        }

        [Description("Determines whether the DbResourceControl shows its localization options on the page.")]
        [ConfigurationProperty("showLocalizationControlOptions", DefaultValue = "false")]
        public bool ShowLocalizationControlOptions
        {
            get => (bool) this["showLocalizationControlOptions"];
            set => this["showLocalizationControlOptions"] = value;
        }

        [Description("Determines whether a strongly typed resource is created when database resources are exported to a ResX file")]
        [ConfigurationProperty("stronglyTypedGlobalResource", DefaultValue = "~/App_Code/Resources.cs,AppResources")]
        public string StronglyTypedGlobalResource
        {
            get => (string) this["stronglyTypedGlobalResource"];
            set => this["stronglyTypedGlobalResource"] = value;
        }

        [Description("If set to true uses Visual Studio naming for generate resource names that have a ResourceX prefix. The default doesn't generate the Resource text and omits the number if possible")]
        [ConfigurationProperty("useVsNetResourceNaming", DefaultValue = false)]
        public bool UseVsNetResourceNaming
        {
            get => (bool) this["useVsNetResourceNaming"];
            set => this["useVsNetResourceNaming"] = value;
        }
    }
}