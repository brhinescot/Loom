namespace Loom.Data.Mapping.Schema
{
    internal sealed class ProcedureData : ICallable
    {
        private const string DataSource = "Default";
        private ICallableParameterCollection parameters;

        public ProcedureData(string name, string owner)
        {
            Name = name;
            Owner = owner;
        }

        #region ICallable Members

        public ICallableParameterCollection Parameters => parameters ?? (parameters = new ICallableParameterCollection());

        public string Owner { get; }

        public string Name { get; }

        public string Datasource => DataSource;

        public bool IsReadOnly => false;

        #endregion
    }
}