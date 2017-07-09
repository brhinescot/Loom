namespace Loom.Data.Mapping.Schema
{
    public interface ICallable : ISchema
    {
        ICallableParameterCollection Parameters { get; }
    }
}