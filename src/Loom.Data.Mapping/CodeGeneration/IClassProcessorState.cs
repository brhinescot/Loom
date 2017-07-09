namespace Loom.Data.Mapping.CodeGeneration
{
    internal interface IClassProcessorState
    {
        string ClassTemplate { get; }
        string PropertyTemplate { get; }
        string EnumPropertyTemplate { get; }
    }
}