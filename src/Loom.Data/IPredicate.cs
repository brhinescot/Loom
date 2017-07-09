namespace Loom.Data
{
    public interface IPredicate<T> where T : IPredicate<T>
    {
        bool OrNextPredicate { get; set; }
        bool OrToPreviousGroup { get; set; }
        T Or(T predicate);
        T And(T predicate);
    }
}