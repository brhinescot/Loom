#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom
{
    internal interface IEntityConverter<in TSource, TDestination> where TDestination : new()
    {
        IList<TDestination> Convert(IEnumerable<TSource> source);
        TDestination Convert(TSource source);
    }
}