#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Loom
{
    public interface IMetaContainer
    {
        IEnumerable<MetaData> GetMetaData();
        object GetMetaValue(string field);
        void SetMetaValue(string field, object value);
    }

    public class MetaData
    {
        public string Name { get; set; }

        public Type Type => typeof(string);

        public object Value { get; set; }
    }
}