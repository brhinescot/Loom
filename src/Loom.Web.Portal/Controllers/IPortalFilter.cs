namespace Loom.Web.Portal.Controllers
{
    public interface IPortalFilter
    {
        int Order { get; set; }
        void Execute(IPortalContext context);
    }
}