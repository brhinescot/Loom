#region Using Directives

using System.Collections.ObjectModel;
using System.Collections.Specialized;

#endregion

namespace Loom.Web.Reporting
{
    internal class UniqueEntryCollectionBuilder
    {
        private readonly NameValueCollection collection = new NameValueCollection();

        internal void Append(string fieldValue)
        {
            Argument.Assert.IsNotNull(fieldValue, "fieldValue");

            collection.Add(fieldValue.Replace("+", " "), string.Empty);
        }

        internal Collection<UniqueEntry> ToCollection()
        {
            Collection<UniqueEntry> uniqueEntryCollection = new Collection<UniqueEntry>();
            foreach (string s in collection)
                uniqueEntryCollection.Add(new UniqueEntry(collection.GetValues(s).Length, s));

            return uniqueEntryCollection;
        }
    }
}