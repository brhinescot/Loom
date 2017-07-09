using System.Collections.Generic;

namespace Colossus.Framework.Data.Entities
{
    interface IEntityConverter<in TSource, TDestination> where TDestination : new()
    {                                                     
        IList<TDestination> ConvertFrom(IEnumerable<TSource> source);
        TDestination ConvertFrom(TSource source);
    }
}