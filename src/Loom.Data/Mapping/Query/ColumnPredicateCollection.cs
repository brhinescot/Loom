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

using System.Collections.ObjectModel;

namespace Loom.Data.Mapping.Query
{
    public class ColumnPredicateCollection : Collection<ColumnPredicate>
    {
        private readonly ParameterNameGeneratorHandler nameHandler;

        public ColumnPredicateCollection(ParameterNameGeneratorHandler nameHandler)
        {
            this.nameHandler = nameHandler;
        }

        public string GetUniqueName(ColumnPredicate predicate)
        {
            return nameHandler(predicate.Column.Name);
        }
    }
}
