#region License Information

// ******************************************************************
// Devinterop Framework 
// 
// Copyright © 2004, 2008 by Brian Scott (DevInterop)
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.devinterop.com
// http://blogs.geekdojo.net/brian
//  
// ******************************************************************

#endregion

#region Using Directives



#endregion

namespace Loom.Data.Mapping.Query
{
    public sealed class QueryCondition<T> : CommandCondition<T, EntitySet<T>, QueryCondition<T>> where T : DataRecord<T>, new()
    {
        public QueryCondition(EntitySet<T> host, ColumnPredicateCollection predicates) : base(host, predicates) {}
    }
}
