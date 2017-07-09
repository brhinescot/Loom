#region Using Directives

using Loom.Data.Mapping;
using Loom.Web.Portal.Data.Person;
using Loom.Web.Portal.Data.Portal;
using Loom.Web.Portal.Data.Security;

#endregion

namespace Loom.Web.Portal.Data
{
    public interface IPortalDataSession
    {
        EntitySet<Contact> Contacts { get; }

        EntitySet<Application> Applications { get; }

        EntitySet<Domain> Domains { get; }

        EntitySet<EntityBase> EntityBases { get; }

        EntitySet<MetaBag> MetaBags { get; }

        EntitySet<MetaBagTranslation> MetaBagTranslations { get; }

        EntitySet<MetaField> MetaFields { get; }

        EntitySet<Module> Modules { get; }

        EntitySet<Network> Networks { get; }

        EntitySet<NetworkTenant> NetworkTenants { get; }

        EntitySet<Redirect> Redirects { get; }

        EntitySet<Route> Routes { get; }

        EntitySet<RouteModule> RouteModules { get; }

        EntitySet<Setting> Settings { get; }

        EntitySet<SettingField> SettingFields { get; }

        EntitySet<Tenant> Tenants { get; }

        EntitySet<Login> Logins { get; }

        EntitySet<Role> Roles { get; }

        EntitySet<User> Users { get; }
    }
}