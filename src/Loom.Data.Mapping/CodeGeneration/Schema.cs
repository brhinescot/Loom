namespace Loom.Data.Mapping.CodeGeneration
{
    public class Schema : ISchema
    {
        public Schema(string owner, string name)
        {
            Owner = owner;
            Name = name;
            IsReadOnly = false;
        }

        public Schema(string owner, string name, string datasource)
        {
            Owner = owner;
            Name = name;
            Datasource = datasource;
            IsReadOnly = false;
        }

        #region ISchema Members

        public string Owner { get; }
        public string Name { get; }
        public bool IsReadOnly { get; }
        public string Datasource { get; }

        #endregion
    }
}