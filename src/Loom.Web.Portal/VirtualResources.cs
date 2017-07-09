#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.Web.Portal
{
    internal sealed class VirtualResources : Dictionary<string, VirtualResourceData>
    {
        public void SetData(string name, VirtualResourceData data)
        {
            if (ContainsKey(name))
                this[name] = data;
            else
                Add(name, data);
        }

        public VirtualResourceData GetData(string name)
        {
            return !ContainsKey(name) ? null : this[name];
        }
    }
}