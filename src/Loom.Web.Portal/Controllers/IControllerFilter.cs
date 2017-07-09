namespace Loom.Web.Portal.Controllers
{
    public interface IControllerFilter
    {
        void OnProcessAction(IPortalContext context);
    }
}