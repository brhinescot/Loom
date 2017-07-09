#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Loom.Web.Portal.Routing
{
    internal class TenantData
    {
        internal Dictionary<string, SectionData> Sections { get; } = new Dictionary<string, SectionData>(StringComparer.OrdinalIgnoreCase);

        internal SectionData DefaultSection { get; set; }

        internal string Namespace { get; set; }
        internal string Name { get; set; }
    }
}