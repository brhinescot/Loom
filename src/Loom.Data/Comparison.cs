namespace Loom.Data
{
    /// <summary>
    ///     Comparison Operators
    /// </summary>
    public enum Comparison
    {
        Equal = 0,
        NotEqual = 1,
        Greater = 4,
        GreaterOrEqual = 5,
        Less = 6,
        LessOrEqual = 7,
        Between = 10,
        True = 11,
        False = 12,
        StartsWith = 13,
        EndsWith = 14,
        Contains = 15,
        DoesNotStartWith = 16,
        DoesNotEndWith = 17,
        DoesNotContain = 18
    }
}