namespace Loom.Data
{
    public interface ISchema
    {
        string Datasource { get; }
        string Owner { get; }
        string Name { get; }
        bool IsReadOnly { get; }
    }
}