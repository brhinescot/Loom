#region Using Directives

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Loom.Data.Diff
{
    [TestFixture]
    public class DiffTests
    {
        private const int NumberOfProperties = 7;

        [Test]
        public void DiffEngineOneChange()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
            Assert.AreEqual("DiffObject", diff.Name);
        }

        [Test]
        public void DiffEngineOneChangeWithPropertyFormat()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2223");

            const string phoneFormat = "{{AreaCode}}||{{Exchange}}||{{Number}}";
            FormattedProperty formattedProperty = new FormattedProperty("PhoneNumber", phoneFormat);
            List<FormattedProperty> formats = new List<FormattedProperty>();
            formats.Add(formattedProperty);

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>(formats);
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Phone Number", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.PhoneNumber.ToString(phoneFormat), diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.PhoneNumber.ToString(phoneFormat), diff.Entries[0].NewValue);
            Assert.AreEqual("DiffObject", diff.Name);
        }

        [Test]
        public void DiffEngineTwoChanges()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Name = "Brian Scott";
            diffObject2.Address = "150 Main Street";

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Name", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Name, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Name, diff.Entries[0].NewValue);
            Assert.AreEqual("Address", diff.Entries[1].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[1].NewValue);
        }

        [Test]
        public void DiffEngineChangeWithFriendlyName()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-4444");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Phone Number", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.PhoneNumber.ToString("{{AreaCode}}.{{Exchange}}.{{Number}}"), diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.PhoneNumber.ToString("{{AreaCode}}.{{Exchange}}.{{Number}}"), diff.Entries[0].NewValue);
        }

        [Test]
        public void DiffEngineChangeDateWithFriendlyName()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            baseDiffObject.HireDate = DateTime.Parse("06/22/2006 06:22");
            diffObject2.HireDate = DateTime.Parse("12/12/2006 12:22");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Hire Date", diff.Entries[0].Name);
            Assert.AreEqual("06-22-06", diff.Entries[0].BaselineValue);
            Assert.AreEqual("12-12-06", diff.Entries[0].NewValue);
        }

        [Test]
        public void DiffableInterfaceOneChange()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";

            Diff<DiffObject> diff = baseDiffObject.CreateDiff(diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
        }

        [Test]
        public void DiffableInterfaceTwoChanges()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Name = "Brian Scott";
            diffObject2.Address = "150 Main Street";

            Diff<DiffObject> diff = baseDiffObject.CreateDiff(diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Name", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Name, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Name, diff.Entries[0].NewValue);
            Assert.AreEqual("Address", diff.Entries[1].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[1].NewValue);
        }

        [Test]
        public void DiffableInterfaceOnStruct()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";
            diffObject2.BirthDate = DateTime.Parse("1/1/1976");

            Diff<DiffObject> diff = baseDiffObject.CreateDiff(diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
            Assert.AreEqual("BirthDate", diff.Entries[1].Name);
            Assert.AreEqual(baseDiffObject.BirthDate.ToString(), diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.BirthDate.ToString(), diff.Entries[1].NewValue);
        }

        [Test]
        public void DiffWithVisableAndNonVisablePropertyChange()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";
            diffObject2.Office = "South";

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);
            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
        }

        [Test]
        public void DiffWithNonVisablePropertyChange()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Office = "South";

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(0, diff.Entries.Count);
        }

        [Test]
        public void DiffWithNonAtrributedPropertyChange()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Name = "Brian Scott";
            diffObject2.Position = "Lead Developer";

            Diff<DiffObject> diff = baseDiffObject.CreateDiff(diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Name", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Name, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Name, diff.Entries[0].NewValue);
            Assert.AreEqual("Position", diff.Entries[1].Name);
            Assert.AreEqual(baseDiffObject.Position, diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.Position, diff.Entries[1].NewValue);
        }

        [Test]
        public void OneChangeInExplicitPropertyList()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>("Address");
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(1, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
        }

        [Test]
        public void OneChangeNotInExplicitPropertyList()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>("Name");
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(0, diff.Entries.Count);
        }

        [Test]
        public void NullObjectToNotNull()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            diffObject2.Address = "150 Main Street";
            diffObject2.NullObject = new object();

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiffObject, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
            Assert.AreEqual("NullObject", diff.Entries[1].Name);
            Assert.AreEqual(null, diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.NullObject.ToString(), diff.Entries[1].NewValue);
        }

        [Test]
        public void PhoneNumberDiff()
        {
            PhoneNumber baseNumber = PhoneNumber.Parse("222-222-2222");
            PhoneNumber newNumber = PhoneNumber.Parse("222-111-2222");

            DiffGenerator<PhoneNumber> diffGenerator = new DiffGenerator<PhoneNumber>();
            Diff<PhoneNumber> diff = diffGenerator.Generate(baseNumber, newNumber);

            Assert.AreEqual(1, diff.Entries.Count);
        }

        [Test]
        public void CreateBaseDiff()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            DiffBaseline diff = diffGenerator.GenerateBaseline(baseDiffObject);

            Assert.AreEqual(NumberOfProperties, diff.Entries.Count);
        }

        [Test]
        public void DiffWithBaselineFromPropertDiffClass()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            DiffBaseline baseDiff = diffGenerator.GenerateBaseline(diffObject2);
            Assert.AreEqual(NumberOfProperties, baseDiff.Entries.Count);

            diffObject2.Address = "222 Main Street";
            diffObject2.PhoneNumber = PhoneNumber.Parse("444-444-4444");

            Diff<DiffObject> diff = diffGenerator.Generate(baseDiff, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
            Assert.AreEqual("Phone Number", diff.Entries[1].Name);
            Assert.AreEqual(baseDiffObject.PhoneNumber.ToString(), diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.PhoneNumber.ToString(), diff.Entries[1].NewValue);
        }

        [Test]
        public void DiffWithBaselineFromPropertDiffBaselineClass()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffBaseline baseDiff = DiffBaseline.Create(diffObject2);
            Assert.AreEqual(NumberOfProperties, baseDiff.Entries.Count);

            diffObject2.Address = "222 Main Street";
            diffObject2.PhoneNumber = PhoneNumber.Parse("444-444-4444");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            Diff<DiffObject> diff = diffGenerator.Generate(baseDiff, diffObject2);

            Assert.IsNotNull(diff);
            Assert.AreEqual(2, diff.Entries.Count);
            Assert.AreEqual("Address", diff.Entries[0].Name);
            Assert.AreEqual(baseDiffObject.Address, diff.Entries[0].BaselineValue);
            Assert.AreEqual(diffObject2.Address, diff.Entries[0].NewValue);
            Assert.AreEqual("Phone Number", diff.Entries[1].Name);
            Assert.AreEqual(baseDiffObject.PhoneNumber.ToString(), diff.Entries[1].BaselineValue);
            Assert.AreEqual(diffObject2.PhoneNumber.ToString(), diff.Entries[1].NewValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AttemptDiffWithSubclass()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            diffGenerator.Generate(baseDiffObject, diffObject3);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AttemptDiffWithNullBaselineObject()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            diffGenerator.Generate(baseDiffObject, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AttemptDiffWithNullWorkingObject()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();
            DiffObject diffObject1 = null;
            diffGenerator.Generate(diffObject1, diffObject2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidItemInPropertyList()
        {
            DiffObject baseDiffObject = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            baseDiffObject.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffObject diffObject2 = new DiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject2.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            SubDiffObject diffObject3 = new SubDiffObject("Brian", "Developer", "111 Main Street", DateTime.Parse("1/1/1973"), "North");
            diffObject3.PhoneNumber = PhoneNumber.Parse("222-222-2222");

            DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>("XAddressX");
            diffGenerator.Generate(baseDiffObject, diffObject2);
        }
    }

    public class SubDiffObject : DiffObject
    {
        public SubDiffObject(string name, string position, string address, DateTime birthDate, string orientation) : base(name, position, address, birthDate, orientation) { }
    }

    public class DiffObject
    {
        private readonly DiffGenerator<DiffObject> diffGenerator = new DiffGenerator<DiffObject>();

        public DiffObject() { }

        public DiffObject(string name, string position, string address)
        {
            Name = name;
            Position = position;
            Address = address;
        }

        public DiffObject(string name, string position, string address, DateTime birthDate)
        {
            Name = name;
            Position = position;
            Address = address;
            BirthDate = birthDate;
        }

        public DiffObject(string name, string position, string address, DateTime birthDate, string office)
        {
            Name = name;
            Position = position;
            Address = address;
            BirthDate = birthDate;
            Office = office;
        }

        [DiffVisible]
        public string Name { get; set; }

        public string Position { get; set; }

        [DiffVisible]
        public string Address { get; set; }

        [DiffVisible]
        public DateTime BirthDate { get; set; }

        [DiffVisible(FriendlyName = "Hire Date", Format = "MM-dd-yy")]
        public DateTime HireDate { get; set; }

        [DiffVisible(false)]
        public string Office { get; set; }

        public object NullObject { get; set; }

        [DiffVisible(FriendlyName = "Phone Number", Format = "{{AreaCode}}.{{Exchange}}.{{Number}}")]
        public PhoneNumber PhoneNumber { get; set; }

        public Diff<DiffObject> CreateDiff(DiffObject other)
        {
            return diffGenerator.Generate(this, other);
        }

        public Diff<DiffObject> CreateDiff(DiffBaseline baseline)
        {
            return diffGenerator.Generate(baseline, this);
        }

        public DiffBaseline CreateDiffBaseline()
        {
            return diffGenerator.GenerateBaseline(this);
        }
    }
}