namespace Loom.Data.Mapping
{
    public abstract class ActiveRecord<TActiveRecord> : DataRecord<TActiveRecord>
        where TActiveRecord : DataRecord<TActiveRecord>, new() { }
}