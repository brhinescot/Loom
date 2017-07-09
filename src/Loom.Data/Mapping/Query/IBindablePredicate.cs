namespace Loom.Data.Mapping.Query
{
    public interface IBindablePredicate
    {
        void BindWhere(ICommandBuilder builder);
        void BindHaving(ICommandBuilder builder);
    }
}
