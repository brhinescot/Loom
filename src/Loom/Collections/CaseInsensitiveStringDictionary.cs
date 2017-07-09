#region Using Directives

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Loom.Collections
{
    [Serializable]
    public class CaseInsensitiveStringDictionary : Dictionary<string, string>
    {
        public CaseInsensitiveStringDictionary() : base(StringComparer.OrdinalIgnoreCase) { }

        public CaseInsensitiveStringDictionary(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase) { }

        public CaseInsensitiveStringDictionary(IDictionary<string, string> dictionary) : base(dictionary, StringComparer.OrdinalIgnoreCase) { }

        protected CaseInsensitiveStringDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}