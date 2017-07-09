#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Web.Portal.UI
{
    [DebuggerStepThrough]
    internal sealed class ClientIncludeKey
    {
        private readonly bool isInclude;
        private readonly string key;
        private readonly Type type;

        [DebuggerStepThrough]
        internal ClientIncludeKey(Type type, string key, bool isInclude = false)
        {
            this.type = type;
            if (string.IsNullOrEmpty(key))
                key = null;

            this.key = key;
            this.isInclude = isInclude;
        }

        [DebuggerStepThrough]
        public override bool Equals(object o)
        {
            ClientIncludeKey clientIncludeKey = (ClientIncludeKey) o;
            return clientIncludeKey.type == type && clientIncludeKey.key == key && clientIncludeKey.isInclude == isInclude;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return HashCode.Combine(type.GetHashCode(), key.GetHashCode(), isInclude.GetHashCode());
        }
    }
}