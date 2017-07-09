#region Using Directives

using System.Web.Script.Serialization;

#endregion

namespace Loom.Web
{
    public static class ObjectExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}