namespace Loom.Web.Portal.UI.Controls
{
    public class PropertyGridItem
    {
        public PropertyGridItem(string label, string input, string description = null)
        {
            Label = label;
            Input = input;
            Description = description;
        }

        public string Label { get; set; }
        public string Input { get; set; }
        public string Description { get; set; }
    }
}