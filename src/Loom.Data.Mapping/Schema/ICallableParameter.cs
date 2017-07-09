namespace Loom.Data.Mapping.Schema
{
    public interface ICallableParameter : IDbColumn
    {
        ParameterType ParameterType { get; }

        bool IsResult { get; }
    }
}