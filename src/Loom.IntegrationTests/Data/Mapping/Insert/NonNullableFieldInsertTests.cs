#region Using Directives

using System.Threading.Tasks;
using NUnit.Framework;
using OmniMount;
using OmniMount.Sales;

#endregion

namespace Loom.Data.Mapping.Crud
{
    [TestFixture]
    public class NonNullableFieldInsertTests : ThreadedTestBase
    {
        private const int Iterations = 8000;

        [TearDown]
        public void TearDown()
        {
            using (ProductDataSession session = new ProductDataSession("sales"))
            {
                session.Delete<AgeRange>(a => a.AgeRangeId > 4);
            }
        }

        [Test]
        public void Insert1_Warmup()
        {
            using (ProductDataSession session = new ProductDataSession("sales"))
            {
                AgeRange ageRange = new AgeRange {Max = 20, Min = 0};
                session.Insert(ageRange);
            }
        }

        [Test]
        public void Insert3_Parallel()
        {
            Parallel.For(0, Iterations, index =>
            {
                using (ProductDataSession session = new ProductDataSession("sales"))
                {
                    AgeRange ageRange = new AgeRange {Max = 20 + index, Min = 0 + index};
                    session.Insert(ageRange);
                }
            });
        }

        [Test]
        public void Insert2_ThreadRepeat()
        {
            ThreadedRepeat(Iterations, (index, asserter) =>
            {
                using (ProductDataSession session = new ProductDataSession("sales"))
                {
                    AgeRange ageRange = new AgeRange {Max = 20 + index, Min = 0 + index};
                    session.Insert(ageRange);
                }
            });
        }
    }
}