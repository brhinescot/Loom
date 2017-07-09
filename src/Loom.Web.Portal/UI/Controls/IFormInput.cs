namespace Loom.Web.Portal.UI.Controls
{
    public interface IFormInput
    {
        char AccessKey { get; set; }
        string Name { get; set; }
        int TabIndex { get; set; }
        bool Disabled { get; set; }
    }
}