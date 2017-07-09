namespace Loom.Data.Mapping.Query
{
    public interface IPredicate<T> where T : IPredicate<T>
    {
        T Or(T predicate);
        T And(T predicate);
        bool OrNextPredicate { get; set; }
        bool OrToPreviousGroup { get; set; }
    }
}
