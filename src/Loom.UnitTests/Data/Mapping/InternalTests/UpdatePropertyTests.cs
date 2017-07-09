#region Using Directives

using System.Collections.Generic;
using AdventureWorks.HumanResources;
using Loom.Dynamic;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.InternalTests
{
    [TestFixture]
    public class UpdatePropertyTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(null)]
        public void Test(int? value)
        {
            JobCandidate candidate = new JobCandidate();
            candidate.BusinessEntityId = value;

            List<DynamicProperty<JobCandidate>> list = new List<DynamicProperty<JobCandidate>>(candidate.GetUpdatedProperties());

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(value, list[0].InvokeGetterOn(candidate));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(null, -1)]
        public void Test2(int? value1, int value2)
        {
            JobCandidate candidate = new JobCandidate();
            candidate.BusinessEntityId = value1;
            candidate.JobCandidateId = value2;

            List<DynamicProperty<JobCandidate>> list = new List<DynamicProperty<JobCandidate>>(candidate.GetUpdatedProperties());

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(value1, list[0].InvokeGetterOn(candidate));
            Assert.AreEqual(value2, list[1].InvokeGetterOn(candidate));
        }

        [TestCase("123456")]
        [TestCase("Executive Summary")]
        [TestCase("")]
        [TestCase(null)]
        public void Test3(string value)
        {
            JobCandidate candidate = new JobCandidate();
            candidate.Resume = value;

            List<DynamicProperty<JobCandidate>> list = new List<DynamicProperty<JobCandidate>>(candidate.GetUpdatedProperties());

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(value, list[0].InvokeGetterOn(candidate));
        }

        [TestCase(null, null)]
        [TestCase(null, 0)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, null)]
        [TestCase(0, null)]
        public void Test4(int? value1, int? value2)
        {
            List<DynamicProperty<JobCandidate>> list;
            JobCandidate candidate = new JobCandidate();

            candidate.BusinessEntityId = value1;

            list = new List<DynamicProperty<JobCandidate>>(candidate.GetUpdatedProperties());
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(value1, list[0].InvokeGetterOn(candidate));

            candidate.BusinessEntityId = value2;

            list = new List<DynamicProperty<JobCandidate>>(candidate.GetUpdatedProperties());
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(value2, list[0].InvokeGetterOn(candidate));
        }
    }
}