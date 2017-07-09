namespace Loom.Data.Mapping.CodeGeneration
{
    internal sealed class ReadWriteProcessorState : IClassProcessorState
    {
        #region IClassProcessorState Members

        public string ClassTemplate => Templates.ActiveRecordClass;

        public string PropertyTemplate => Templates.Property;

        public string EnumPropertyTemplate => Templates.EnumProperty;

        #endregion
    }
}