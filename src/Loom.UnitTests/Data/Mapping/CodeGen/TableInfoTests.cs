#region Using Directives

using System.Collections.Generic;
using Loom.Data.Mapping.CodeGeneration;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.CodeGen
{
    [TestFixture]
    public class TableInfoTests
    {
        private const string Name = "Applications";
        private const string Owner = "Developer";
        private const string FullName = "Developer.Applications";
        private const string FullNameBracketed = "[Developer].[Applications]";
        private const string FullNameDefaultOwner = "dbo.Applications";
        private const string FullNameDefaultOwnerBracketed = "[dbo].[Applications]";
        private readonly CodeGenSession session = new CodeGenSession(@"Data\Mapping\Generated\AdventureWorks\AdventureWorks.map");

        [Test]
        public void FindTable()
        {
            TableDefinitionCollection list = new TableDefinitionCollection(session.Configuration);
            TableDefinition info1 = new TableDefinition(null, "TestTable1", "dbo", session.Configuration);
            TableDefinition info2 = new TableDefinition(null, "TestTable2", "dbo", session.Configuration);
            TableDefinition info3 = new TableDefinition(null, "TestTable3", "dbo", session.Configuration);
            TableDefinition info4 = new TableDefinition(null, "TestTable4", "dbo", session.Configuration);
            TableDefinition info5 = new TableDefinition(null, "TestTable5", "dbo", session.Configuration);

            list.Add(info1);
            list.Add(info2);
            list.Add(info3);
            list.Add(info4);
            list.Add(info5);

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(info3, list.FindTable("dbo", "TestTable3"));
            Assert.AreEqual(info2, list.FindTable("dbo", "TestTable2"));
            Assert.AreEqual(info1, list.FindTable("dbo", "TestTable1"));
            Assert.AreEqual(info4, list.FindTable("dbo", "TestTable4"));
            Assert.AreEqual(info5, list.FindTable("dbo", "TestTable5"));
        }

        [Test]
        public void FindInvalidTable()
        {
            TableDefinitionCollection list = new TableDefinitionCollection(session.Configuration);
            TableDefinition info1 = new TableDefinition(null, "TestTable1", "dbo", session.Configuration);
            TableDefinition info2 = new TableDefinition(null, "TestTable2", "dbo", session.Configuration);
            TableDefinition info3 = new TableDefinition(null, "TestTable3", "dbo", session.Configuration);
            TableDefinition info4 = new TableDefinition(null, "TestTable4", "dbo", session.Configuration);
            TableDefinition info5 = new TableDefinition(null, "TestTable5", "dbo", session.Configuration);

            list.Add(info1);
            list.Add(info2);
            list.Add(info3);
            list.Add(info4);
            list.Add(info5);

            Assert.AreEqual(5, list.Count);
            Assert.IsNull(list.FindTable("XSX", "TestTable3"));
            Assert.IsNull(list.FindTable("dbo", "XXXXTable2"));
            Assert.IsNull(list.FindTable("YyY", "TestTable1"));
            Assert.IsNull(list.FindTable("dbo", "TestTableBingo"));
            Assert.IsNull(list.FindTable("dba", "TestTableLooser"));
        }

        [Test]
        public void EqualityTests()
        {
            TableDefinition info1 = new TableDefinition(null, Name, Owner, session.Configuration);
            TableDefinition info2 = new TableDefinition(null, Name, Owner, session.Configuration);

            Assert.AreEqual(info1, info2);
        }

        [Test]
        public void InEqualityTests()
        {
            TableDefinition info1 = new TableDefinition(null, Name, Owner, session.Configuration);
            TableDefinition info2 = new TableDefinition(null, TableDefinition.DefaultOwner, Owner, session.Configuration);

            Assert.AreNotEqual(info1, info2);
        }

        [Test]
        public void ToStringIsValid()
        {
            TableDefinition info = new TableDefinition(null, Name, Owner, session.Configuration);
            Assert.AreEqual(FullNameBracketed, info.ToString());
        }

        [Test]
        public void PropertiesValidFullConstructor()
        {
            TableDefinition info = new TableDefinition(null, Name, Owner, session.Configuration);

            Assert.AreEqual(Owner, info.Owner);
            Assert.AreEqual(Name, info.Name);
            Assert.AreEqual(FullName, info.FullName);
            Assert.AreEqual(FullNameBracketed, info.FullNameBracketed);
        }

        [Test]
        public void PropertiesValidNameConstructor()
        {
            TableDefinition info = new TableDefinition(null, Name, session.Configuration);

            Assert.AreEqual(TableDefinition.DefaultOwner, info.Owner);
            Assert.AreEqual(Name, info.Name);
            Assert.AreEqual(FullNameDefaultOwner, info.FullName);
            Assert.AreEqual(FullNameDefaultOwnerBracketed, info.FullNameBracketed);
        }

        [Test]
        public void AddSameTable()
        {
            TableDefinitionCollection list = new TableDefinitionCollection(session.Configuration);
            TableDefinition info1 = new TableDefinition(null, "TestTable", "dbo", session.Configuration);

            list.Add(info1);
            list.Add(info1);

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void EnumerateTableList()
        {
            List<TableDefinition> list = new List<TableDefinition>();

            TableDefinition info1 = new TableDefinition(null, Name, "AOwner", session.Configuration);
            TableDefinition info2 = new TableDefinition(null, TableDefinition.DefaultOwner, "FOwner", session.Configuration);
            TableDefinition info3 = new TableDefinition(null, Name, "SOwner", session.Configuration);
            TableDefinition info4 = new TableDefinition(null, TableDefinition.DefaultOwner, "ZOwner", session.Configuration);

            list.Add(info1);
            list.Add(info2);
            list.Add(info3);
            list.Add(info4);

            int i = 0;
            foreach (TableDefinition info in list)
            {
                Assert.AreEqual(list[i], info);
                i++;
            }
        }

        [Test]
        public void ExcludedTable()
        {
            TableDefinitionCollection list = new TableDefinitionCollection(session.Configuration);
            TableDefinition info1 = new TableDefinition(null, "TestExcludeTable", "dbo", session.Configuration);
            TableDefinition info2 = new TableDefinition(null, "NonExcludeTable", "dbo", session.Configuration);

            list.Add(info1);
            list.Add(info2);

            Assert.AreEqual(1, list.Count);
            Assert.IsNull(list.FindTable("dbo", "TestExcludeTable"));
            Assert.IsNotNull(list.FindTable("dbo", "NonExcludeTable"));
        }

        [Test]
        public void OperatorTests()
        {
            TableDefinition info1 = new TableDefinition(null, Name, "AOwner", session.Configuration);
            TableDefinition info2 = new TableDefinition(null, TableDefinition.DefaultOwner, "ZOwner", session.Configuration);

            Assert.IsTrue(info2 != info1);
            Assert.IsFalse(info2 == info1);
        }

        [Test]
        public void SortTests()
        {
            List<TableDefinition> list = new List<TableDefinition>();

            TableDefinition info1 = new TableDefinition(null, Name, "AOwner", session.Configuration);
            TableDefinition info2 = new TableDefinition(null, TableDefinition.DefaultOwner, "FOwner", session.Configuration);
            TableDefinition info3 = new TableDefinition(null, Name, "SOwner", session.Configuration);
            TableDefinition info4 = new TableDefinition(null, TableDefinition.DefaultOwner, "ZOwner", session.Configuration);

            list.Add(info3);
            list.Add(info2);
            list.Add(info1);
            list.Add(info4);
            list.Sort();

            Assert.AreEqual(list[0], info1);
            Assert.AreEqual(list[1], info2);
            Assert.AreEqual(list[2], info3);
            Assert.AreEqual(list[3], info4);
        }
    }
}