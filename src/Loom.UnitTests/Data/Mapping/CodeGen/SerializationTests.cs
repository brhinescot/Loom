#region Using Directives

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Loom.Data.Mapping.CodeGeneration;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.CodeGen
{
    public class SerializationTests
    {
        [Test]
        public void AuditFieldSerialization()
        {
            AuditField field1 = new AuditField(false, false, false, false, true);
            AuditField field2;
            using (Stream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, field1);
                stream.Position = 0;
                field2 = (AuditField) formatter.Deserialize(stream);
            }

            Assert.IsTrue(field1.IsDeleted);
            Assert.AreEqual(field2.IsDeleted, field1.IsDeleted);
        }
    }
}