#region Using Directives

using System.Web.Script.Serialization;
using NUnit.Framework;

#endregion

namespace Loom.Web
{
    [TestFixture]
    public class JavaScriptSerializerTests
    {
        [Test]
        public void Test()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer(new SimpleTypeResolver());
            UserTest user = new UserTest {Age = 22, Name = "Tim", Email = "asdas@adsfsd.com"};
            string json = serializer.Serialize(user);
            string json2 = "{\"action\":\"add\",\"userTest\":{\"__type\":\"" + typeof(UserTest).AssemblyQualifiedName + "\",\"Name\":\"Tim\",\"Age\":26,\"Email\":\"tim@yahoo.com\"}}";

            object o = serializer.DeserializeObject(json);
            object o2 = serializer.DeserializeObject(json2);
        }
    }

    public class UserTest
    {
        public int Age { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}