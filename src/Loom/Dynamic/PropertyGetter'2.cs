namespace Loom.Dynamic
{
    public delegate TPropertyType PropertyGetter<in TObjectType, out TPropertyType>(TObjectType obj);

    public delegate TPropertyType PropertyGetter<out TPropertyType>();
}