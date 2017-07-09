#region Using Directives

using System.Transactions;
using AdventureWorks;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Delete
{
    [TestFixture]
    public class MultiKeyTableDelete
    {
        [Test]
        [Ignore("Need new code since DB upgrade and different tables.")]
        public void DeleteByKeys()
        {
            using (AdventureWorksDataSession session = new AdventureWorksDataSession("adventureWorks"))
            using (TransactionScope scope = new TransactionScope()) { }
        }
    }
}