#region Using Directives

using System.Collections.Generic;
using System.Text;

#endregion

namespace Loom.Collections
{
    public class AutoStringDictionary : Dictionary<string, string>
    {
        public AutoStringDictionary() { }
        public AutoStringDictionary(int capacity) : base(capacity) { }
        public AutoStringDictionary(char pairSeperator, char keyValueSeperator) : this(16, pairSeperator, keyValueSeperator) { }

        public AutoStringDictionary(int capacity, char pairSeperator, char keyValueSeperator) : base(capacity)
        {
            PairSeperator = pairSeperator;
            KeyValueSeperator = keyValueSeperator;
        }

        public char KeyValueSeperator { get; set; }
        public char PairSeperator { get; set; }

        public static AutoStringDictionary Parse(string s, char pairSeperator, char keyValueSeperator)
        {
            if (Compare.IsNullOrEmpty(s))
                return new AutoStringDictionary(pairSeperator, keyValueSeperator);

            int length = s.Length / 11;

            AutoStringDictionary dictionary = new AutoStringDictionary(length < 16 ? 16 : length);
            dictionary.KeyValueSeperator = keyValueSeperator;
            dictionary.PairSeperator = pairSeperator;

            if (Compare.IsNullOrEmpty(s))
                return dictionary;

            string[] keyValues = s.Split(pairSeperator);
            if (keyValues.Length == 0)
                return dictionary;

            foreach (string keyValue in keyValues)
            {
                string[] pair = keyValue.Split(keyValueSeperator);
                dictionary.Add(pair[0], pair[1]);
            }

            return dictionary;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(50);

            if (KeyValueSeperator == '0')
                KeyValueSeperator = ' ';

            if (PairSeperator == '0')
                PairSeperator = ' ';

            int i = 0;
            foreach (string key in Keys)
            {
                builder.Append(key + KeyValueSeperator + this[key]);
                if (++i < Count)
                    builder.Append(PairSeperator);
            }

            return builder.ToString();
        }

        public string ToJson()
        {
            StringBuilder builder = new StringBuilder(50);

            if (KeyValueSeperator == '0')
                KeyValueSeperator = ' ';

            if (PairSeperator == '0')
                PairSeperator = ' ';

            int i = 0;
            builder.Append("{");
            foreach (string key in Keys)
            {
                builder.Append("'" + key + "':'" + this[key] + "'");
                if (++i < Count)
                    builder.Append(",");
            }
            builder.Append("}");

            return builder.ToString();
        }
    }
}