#region Using Directives

using System.Data.Common;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Providers;
using Loom.Web.Portal.Data.Person;
using Loom.Web.Portal.Data.Portal;
using Loom.Web.Portal.Data.Security;

#endregion

namespace Loom.Web.Portal.Data
{
    public class PortalDataSession : DataSession
    {
        public PortalDataSession(string sessionProviderName = null) : base(sessionProviderName) { }
        public PortalDataSession(IActiveDataProvider provider, string connectionString) : base(provider, connectionString) { }
        public PortalDataSession(IActiveDataProvider provider, DbConnection connection) : base(provider, connection) { }

        public EntitySet<Contact> Contacts => EntitySet<Contact>();

        public EntitySet<Application> Applications => EntitySet<Application>();

        public EntitySet<Domain> Domains => EntitySet<Domain>();

        public EntitySet<EntityBase> EntityBases => EntitySet<EntityBase>();

        public EntitySet<MetaBag> MetaBags => EntitySet<MetaBag>();

        public EntitySet<MetaBagTranslation> MetaBagTranslations => EntitySet<MetaBagTranslation>();

        public EntitySet<MetaField> MetaFields => EntitySet<MetaField>();

        public EntitySet<Module> Modules => EntitySet<Module>();

        public EntitySet<Network> Networks => EntitySet<Network>();

        public EntitySet<NetworkTenant> NetworkTenants => EntitySet<NetworkTenant>();

        public EntitySet<Redirect> Redirects => EntitySet<Redirect>();

        public EntitySet<Route> Routes => EntitySet<Route>();

        public EntitySet<RouteModule> RouteModules => EntitySet<RouteModule>();

        public EntitySet<Setting> Settings => EntitySet<Setting>();

        public EntitySet<SettingField> SettingFields => EntitySet<SettingField>();

        public EntitySet<Tenant> Tenants => EntitySet<Tenant>();

        public EntitySet<Login> Logins => EntitySet<Login>();

        public EntitySet<Role> Roles => EntitySet<Role>();

        public EntitySet<User> Users => EntitySet<User>();
    }
}