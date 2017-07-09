#region Using Directives

using System.Data.Common;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Providers;
using OmniMount.Cms;
using OmniMount.Portal;
using OmniMount.Production;
using OmniMount.Sales;

#endregion

namespace OmniMount
{
    public class ProductDataSession : DataSession
    {
        public ProductDataSession(string sessionProviderName = null) : base(sessionProviderName) { }
        public ProductDataSession(IActiveDataProvider provider, string connectionString) : base(provider, connectionString) { }
        public ProductDataSession(IActiveDataProvider provider, DbConnection connection) : base(provider, connection) { }

        public EntitySet<Article> Articles => EntitySet<Article>();

        public EntitySet<ArticleAttachment> ArticleAttachments => EntitySet<ArticleAttachment>();

        public EntitySet<ArticleTranslation> ArticleTranslations => EntitySet<ArticleTranslation>();

        public EntitySet<ArticleType> ArticleTypes => EntitySet<ArticleType>();

        public EntitySet<CategoryImage> CategoryImages => EntitySet<CategoryImage>();

        public EntitySet<Faq> Faqs => EntitySet<Faq>();

        public EntitySet<FaqQuestion> FaqQuestions => EntitySet<FaqQuestion>();

        public EntitySet<FaqQuestionTranslation> FaqQuestionTranslations => EntitySet<FaqQuestionTranslation>();

        public EntitySet<Image> Images => EntitySet<Image>();

        public EntitySet<ImageType> ImageTypes => EntitySet<ImageType>();

        public EntitySet<ItemType> ItemTypes => EntitySet<ItemType>();

        public EntitySet<MetaBag> MetaBags => EntitySet<MetaBag>();

        public EntitySet<MetaField> MetaFields => EntitySet<MetaField>();

        public EntitySet<MetaFieldOption> MetaFieldOptions => EntitySet<MetaFieldOption>();

        public EntitySet<MetaFieldValue> MetaFieldValues => EntitySet<MetaFieldValue>();

        public EntitySet<MetaFieldValueTranslation> MetaFieldValueTranslations => EntitySet<MetaFieldValueTranslation>();

        public EntitySet<ProductImage> ProductImages => EntitySet<ProductImage>();

        public EntitySet<DatabaseVersion> DatabaseVersions => EntitySet<DatabaseVersion>();

        public EntitySet<Application> Applications => EntitySet<Application>();

        public EntitySet<Contact> Contacts => EntitySet<Contact>();

        public EntitySet<Domain> Domains => EntitySet<Domain>();

        public EntitySet<Embed> Embeds => EntitySet<Embed>();

        public EntitySet<Localization> Localizations => EntitySet<Localization>();

        public EntitySet<Menu> Menus => EntitySet<Menu>();

        public EntitySet<ModuleDefinition> ModuleDefinitions => EntitySet<ModuleDefinition>();

        public EntitySet<PageColumn> PageColumns => EntitySet<PageColumn>();

        public EntitySet<PageDefinition> PageDefinitions => EntitySet<PageDefinition>();

        public EntitySet<Redirect> Redirects => EntitySet<Redirect>();

        public EntitySet<Role> Roles => EntitySet<Role>();

        public EntitySet<Route> Routes => EntitySet<Route>();

        public EntitySet<RouteType> RouteTypes => EntitySet<RouteType>();

        public EntitySet<Setting> Settings => EntitySet<Setting>();

        public EntitySet<User> Users => EntitySet<User>();

        public EntitySet<Attachment> Attachments => EntitySet<Attachment>();

        public EntitySet<Award> Awards => EntitySet<Award>();

        public EntitySet<AwardTranslation> AwardTranslations => EntitySet<AwardTranslation>();

        public EntitySet<Category> Categorys => EntitySet<Category>();

        public EntitySet<CategoryTranslation> CategoryTranslations => EntitySet<CategoryTranslation>();

        public EntitySet<MountSize> MountSizes => EntitySet<MountSize>();

        public EntitySet<ProductAccessory> ProductAccessorys => EntitySet<ProductAccessory>();

        public EntitySet<ProductAdapter> ProductAdapters => EntitySet<ProductAdapter>();

        public EntitySet<ProductAttachment> ProductAttachments => EntitySet<ProductAttachment>();

        public EntitySet<ProductAward> ProductAwards => EntitySet<ProductAward>();

        public EntitySet<ProductCategory> ProductCategorys => EntitySet<ProductCategory>();

        public EntitySet<ProductCompatibility> ProductCompatibilitys => EntitySet<ProductCompatibility>();

        public EntitySet<ProductEmbed> ProductEmbeds => EntitySet<ProductEmbed>();

        public EntitySet<ProductMountSize> ProductMountSizes => EntitySet<ProductMountSize>();

        public EntitySet<ProductTool> ProductTools => EntitySet<ProductTool>();

        public EntitySet<ProductTour> ProductTours => EntitySet<ProductTour>();

        public EntitySet<ProductTranslation> ProductTranslations => EntitySet<ProductTranslation>();

        public EntitySet<ProductType> ProductTypes => EntitySet<ProductType>();

        public EntitySet<ProductUpsale> ProductUpsales => EntitySet<ProductUpsale>();

        public EntitySet<ProductVariant> ProductVariants => EntitySet<ProductVariant>();

        public EntitySet<ProductVesaPattern> ProductVesaPatterns => EntitySet<ProductVesaPattern>();

        public EntitySet<Tool> Tools => EntitySet<Tool>();

        public EntitySet<ToolTranslation> ToolTranslations => EntitySet<ToolTranslation>();

        public EntitySet<Tour> Tours => EntitySet<Tour>();

        public EntitySet<VesaPattern> VesaPatterns => EntitySet<VesaPattern>();

        public EntitySet<Address> Addresss => EntitySet<Address>();

        public EntitySet<AgeRange> AgeRanges => EntitySet<AgeRange>();

        public EntitySet<Company> Companys => EntitySet<Company>();

        public EntitySet<CompanyAddress> CompanyAddresss => EntitySet<CompanyAddress>();

        public EntitySet<CompanyProduct> CompanyProducts => EntitySet<CompanyProduct>();

        public EntitySet<CompanyType> CompanyTypes => EntitySet<CompanyType>();

        public EntitySet<Country> Countrys => EntitySet<Country>();

        public EntitySet<IncomeRange> IncomeRanges => EntitySet<IncomeRange>();

        public EntitySet<Manufacturer> Manufacturers => EntitySet<Manufacturer>();

        public EntitySet<ManufacturerProduct> ManufacturerProducts => EntitySet<ManufacturerProduct>();

        public EntitySet<ManufacturerProductVesaPattern> ManufacturerProductVesaPatterns => EntitySet<ManufacturerProductVesaPattern>();

        public EntitySet<Newsletter> Newsletters => EntitySet<Newsletter>();

        public EntitySet<NewsletterContact> NewsletterContacts => EntitySet<NewsletterContact>();

        public EntitySet<ProductRegistration> ProductRegistrations => EntitySet<ProductRegistration>();

        public EntitySet<State> States => EntitySet<State>();

        public EntitySet<ThirdPartyMount> ThirdPartyMounts => EntitySet<ThirdPartyMount>();
    }
}