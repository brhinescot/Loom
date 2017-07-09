#region Using Directives

using System.Data.Common;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Providers;

#endregion

namespace Pubs
{
    public class PubsDataSession : DataSession
    {
        public PubsDataSession(string sessionProviderName) : base(sessionProviderName) { }
        public PubsDataSession(IActiveDataProvider provider, string connectionString) : base(provider, connectionString) { }
        public PubsDataSession(IActiveDataProvider provider, DbConnection connection) : base(provider, connection) { }

        public EntitySet<Authors> Authorss => EntitySet<Authors>();

        public EntitySet<Discounts> Discountss => EntitySet<Discounts>();

        public EntitySet<Employee> Employees => EntitySet<Employee>();

        public EntitySet<Jobs> Jobss => EntitySet<Jobs>();

        public EntitySet<Pub_info> Pub_infos => EntitySet<Pub_info>();

        public EntitySet<Publishers> Publisherss => EntitySet<Publishers>();

        public EntitySet<Roysched> Royscheds => EntitySet<Roysched>();

        public EntitySet<Sales> Saless => EntitySet<Sales>();

        public EntitySet<Stores> Storess => EntitySet<Stores>();

        public EntitySet<Titleauthor> Titleauthors => EntitySet<Titleauthor>();

        public EntitySet<Titles> Titless => EntitySet<Titles>();

        public EntitySet<Titleview> Titleviews => EntitySet<Titleview>();
    }
}