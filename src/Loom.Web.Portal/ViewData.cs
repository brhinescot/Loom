#region Using Directives

using System;
using System.Collections.Generic;
using System.Dynamic;

#endregion

namespace Loom.Web.Portal
{
    internal sealed class ViewData : DynamicObject
    {
        private readonly Dictionary<string, object> members = new Dictionary<string, object>();

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return members.Keys;
        }

        public override int GetHashCode()
        {
            return members.GetHashCode();
        }

        /// <summary>
        ///     Provides the implementation of setting a member.
        /// </summary>
        /// <returns>
        ///     Returns true if the operation is complete, false if the call site should determine behavior.
        /// </returns>
        /// <param name="binder">The binder provided by the call site.</param>
        /// <param name="value">
        ///     The
        ///     value to set.
        /// </param>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return TrySetMember(binder.Name, value);
        }

        /// <summary>
        ///     Provides the implementation of setting a member.
        /// </summary>
        /// <returns>
        ///     Returns true if the operation is complete, false if the call site should determine behavior.
        /// </returns>
        /// <param name="name"></param>
        /// <param name="value">
        ///     The
        ///     value to set.
        /// </param>
        internal bool TrySetMember(string name, object value)
        {
            if (members.ContainsKey(name))
                members[name] = value;
            else
                members.Add(name, value);
            return true;
        }

        /// <summary>
        ///     Provides the implementation of getting a member.
        /// </summary>
        /// <returns>
        ///     Returns true if the operation is complete, false if the call site should determine behavior.
        /// </returns>
        /// <param name="binder">The binder provided by the call site.</param>
        /// <param name="result">
        ///     The result
        ///     of the get operation.
        /// </param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return TryGetMember(binder.Name, out result);
        }

        /// <summary>
        ///     Provides the implementation of getting a member.
        /// </summary>
        /// <returns>
        ///     Returns true if the operation is complete, false if the call site should determine behavior.
        /// </returns>
        /// <param name="name"></param>
        /// <param name="result">
        ///     The result
        ///     of the get operation.
        /// </param>
        internal bool TryGetMember(string name, out object result)
        {
            result = members.ContainsKey(name) ? members[name] ?? string.Empty : string.Empty;
            return true;
        }

        /// <summary>
        ///     Provides the implementation of calling a member.
        /// </summary>
        /// <returns>
        ///     Returns true if the operation is complete, false if the call site should determine behavior.
        /// </returns>
        /// <param name="binder">The binder provided by the call site.</param>
        /// <param name="args">
        ///     The arguments
        ///     that will be used for the invocation.
        /// </param>
        /// <param name="result">The result of the invocation.</param>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string key;
            if (string.Equals(binder.Name, "GetViewData", StringComparison.Ordinal) && args != null && args.Length == 1 && args[0] != null)
                key = args[0] as string;
            else
                return base.TryInvokeMember(binder, args, out result);

            if (!Compare.IsNullOrEmpty(key) && members.ContainsKey(key))
                result = members[key];
            else
                result = null;

            return true;
        }
    }
}