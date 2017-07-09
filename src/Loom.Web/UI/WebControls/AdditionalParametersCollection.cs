#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Loom.Web.UI.WebControls
{
    public class AdditionalParametersCollection : Collection<KeyValuePair<string, string>>
    {
        public string this[string key]
        {
            get
            {
                foreach (KeyValuePair<string, string> pair in this)
                    if (key == pair.Key)
                        return pair.Value;
                return null;
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                foreach (KeyValuePair<string, string> pair in this)
                    yield return pair.Key;
            }
        }

        public IEnumerable<string> Values
        {
            get
            {
                foreach (KeyValuePair<string, string> pair in this)
                    yield return pair.Value;
            }
        }

        public void Add(string key, string value)
        {
            Add(new KeyValuePair<string, string>(key, value));
        }
    }
}