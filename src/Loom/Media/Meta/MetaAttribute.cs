namespace Loom.Media.Meta
{
    public class MetaAttribute
    {
        public MetaAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}