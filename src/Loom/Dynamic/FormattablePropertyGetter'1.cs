namespace Loom.Dynamic
{
    public delegate string FormattablePropertyGetter<in TObjectType>(TObjectType obj, string format);
}