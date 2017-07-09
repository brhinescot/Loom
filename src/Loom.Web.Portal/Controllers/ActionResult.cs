#region Using Directives

#endregion

namespace Loom.Web.Portal.Controllers
{
    public abstract class ActionResult
    {
        private ViewData viewData;

        public string[] ViewPaths { get; internal set; }

        public dynamic ViewData
        {
            get => viewData ?? (viewData = new ViewData());
            internal set => viewData = value;
        }

        protected abstract void OnExecute(ControllerContext context);

        internal void Execute(ControllerContext context)
        {
            OnExecute(context);
        }
    }
}