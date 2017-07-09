#region Using Directives

using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

#endregion

namespace Loom.Web
{
    public static class JavaScriptSerializerExtensions
    {
        public static T ConvertToType<T>(this JavaScriptSerializer serializer, object obj, string dtoName = null)
        {
            Argument.Assert.IsNotNull(serializer, nameof(serializer));
            Argument.Assert.IsNotNull(obj, nameof(obj));

            return ConvertToTypePrivate<T>(serializer, obj, dtoName);
        }

        public static T Deserialize<T>(this JavaScriptSerializer serializer, string input, string dtoName = null)
        {
            Argument.Assert.IsNotNull(serializer, nameof(serializer));
            Argument.Assert.IsNotNullOrEmpty(input, nameof(input));

            return ConvertToTypePrivate<T>(serializer, serializer.DeserializeObject(input), dtoName);
        }

        public static T Deserialize<T>(this JavaScriptSerializer serializer, Stream stream, string dtoName = null)
        {
            Argument.Assert.IsNotNull(serializer, nameof(serializer));
            Argument.Assert.IsNotNull(stream, nameof(stream));

            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                return ConvertToTypePrivate<T>(serializer, serializer.DeserializeObject(reader.ReadToEnd()), dtoName);
            }
        }

        public static dynamic DeserializeDynamic(this JavaScriptSerializer serializer, string input, string dtoName = null)
        {
            Argument.Assert.IsNotNull(serializer, nameof(serializer));
            Argument.Assert.IsNotNullOrEmpty(input, nameof(input));

            return serializer.DeserializeObject(input).ToDynamic(dtoName);
        }

        public static dynamic DeserializeDynamic(this JavaScriptSerializer serializer, Stream stream, string dtoName = null)
        {
            Argument.Assert.IsNotNull(serializer, nameof(serializer));
            Argument.Assert.IsNotNull(stream, nameof(stream));

            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                return serializer.DeserializeObject(reader.ReadToEnd()).ToDynamic(dtoName);
            }
        }

        private static T ConvertToTypePrivate<T>(this JavaScriptSerializer serializer, object obj, string dtoName = null)
        {
            Dictionary<string, object> dict = obj as Dictionary<string, object>;
            if (dict == null)
                return default(T);

            return Compare.IsNullOrEmpty(dtoName)
                ? serializer.ConvertToType<T>(dict)
                : serializer.ConvertToType<T>(dict[dtoName]);
        }
    }
}