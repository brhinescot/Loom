namespace Loom
{
    /// <summary>
    ///     A delegate to retrieve the value for a dynamic column.
    /// </summary>
    public delegate object Getter<in T>(T item);
}