namespace Loom.Web.Portal
{
    public interface ITenantContext
    {
        int ApplicationId { get; }
        bool Localize { get; }
        string ApplicationName { get; set; }
    }
}