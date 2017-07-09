namespace Loom.Dynamic
{
    public delegate void PropertySetter<in TObjectType, in TPropertyType>(TObjectType obj, TPropertyType value);
}