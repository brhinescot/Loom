#region Using Directives

using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Media.Meta.Wmf
{
    public class WmfMetaReader : WmfMetaBase, IEnumerable<MetaAttribute>
    {
        public WmfMetaReader(string fileName) : base(fileName) { }

        #region IEnumerable<MetaAttribute> Members

        public IEnumerator<MetaAttribute> GetEnumerator()
        {
            int count = GetAttributeCount();
            for (ushort i = 0; i < count; i++)
                yield return GetMetaAttributeByIndex(i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public MetaAttributeCollection GetAllAttributes()
        {
            MetaAttributeCollection attributes = new MetaAttributeCollection();
            foreach (MetaAttribute pair in this)
                attributes.Add(pair);
            return attributes;
        }
    }
}