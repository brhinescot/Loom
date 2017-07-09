#region Using Directives

using System;
using System.Collections.Generic;
using System.Dynamic;

#endregion

namespace Loom.Collections
{
    public class DictionaryDynamic : DynamicObject
    {
        private readonly Dictionary<string, object> lookup = new Dictionary<string, object>();

        public DictionaryDynamic() { }

        public DictionaryDynamic(Dictionary<string, object> initialMembers)
        {
            lookup = initialMembers;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            lookup[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!lookup.ContainsKey(binder.Name))
                throw new ArgumentException("Error getting member value. The dynamic object does not contain a member named \"" + binder.Name + "\".");

            result = lookup[binder.Name];
            return true;
        }
    }
}