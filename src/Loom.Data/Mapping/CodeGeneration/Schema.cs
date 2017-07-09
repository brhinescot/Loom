using Loom.Data.Mapping.Schema;

namespace Loom.Data.Mapping.CodeGeneration
{
    public class Schema : ISchema
    {
        public string Owner { get; private set; }
        public string Name { get; private set; }
        public string Datasource { get; private set; }

        public Schema(string owner, string name)
        {
            Owner = owner;
            Name = name;
        }

        public Schema(string owner, string name, string datasource)
        {
            Owner = owner;
            Name = name;
            Datasource = datasource;
        }
    }
}
