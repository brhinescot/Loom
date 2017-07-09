#region Using Directives

using System.Collections.ObjectModel;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public sealed class CaseCollection : Collection<Case>
    {
        public Case FindCase(string caseId)
        {
            foreach (Case c in this)
                if (c.ID == caseId)
                    return c;

            return null;
        }
    }
}