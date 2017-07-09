#region Using Directives

using System.Collections.ObjectModel;

#endregion

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