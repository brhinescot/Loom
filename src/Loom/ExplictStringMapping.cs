#region Using Directives

using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom
{
    public class ExplictStringMapping : StringMapping, IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> mappings = new Dictionary<string, string>();

        public ExplictStringMapping(bool suppressNull = false) : base(suppressNull) { }

        #region IEnumerable<KeyValuePair<string,string>> Members

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return mappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mappings.GetEnumerator();
        }

        #endregion

        public void Add(string from, string to)
        {
            mappings.Add(from, to);
        }

        protected override string Get(string from)
        {
            return mappings.ContainsKey(from) ? mappings[from] : SuppressNull ? from : null;
        }
    }
}