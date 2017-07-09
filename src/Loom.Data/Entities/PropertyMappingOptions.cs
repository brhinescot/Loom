namespace Loom.Data.Entities
{
    public class PropertyMappingOptions
    {
        public PropertyMappingOptions() : this(new PropertyMappings()) { }

        public PropertyMappingOptions(PropertyMappings mappings, MissingPropertyMappingAction missingPropertyMappingAction = MissingPropertyMappingAction.Error)
        {
            Mappings = mappings;
            MissingPropertyMappingAction = missingPropertyMappingAction;
        }

        public bool IgnoreCase { get; set; }
        public PropertyMappings Mappings { get; }
        public MissingPropertyMappingAction MissingPropertyMappingAction { get; set; }
    }
}