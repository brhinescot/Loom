namespace Loom.Web.Portal.Controllers
{
    public abstract class ResourceViewController : MvcController
    {
        protected static ActionResult GetResource(string path)
        {
            if (Compare.IsNullOrEmpty(path))
                throw new PortalFatalException("Unable to determine the requested view resource. The 'Path' property has not been initialized.");

            return new ResourceStreamResult(path);
        }

        protected static ActionResult GetTemplateResource(string path)
        {
            if (Compare.IsNullOrEmpty(path))
                throw new PortalFatalException("Unable to determine the requested view resource. The 'Path' property has not been initialized.");

            return new ResourceViewResult(path);
        }
    }
}