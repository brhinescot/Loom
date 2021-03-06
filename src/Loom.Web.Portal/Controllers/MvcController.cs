#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public abstract class MvcController : IController
    {
        private ViewData viewData;

        protected MvcController()
        {
            Context = PortalContext.Current;
            Response = Context.Response;
            Request = Context.Request;
        }

        protected IPortalContext Context { get; set; }
        protected IPortalResponse Response { get; set; }
        protected IPortalRequest Request { get; set; }

        #region IController Members

        public dynamic ViewData => viewData ?? (viewData = new ViewData());

        #endregion

        protected static ActionResult View(string name = null)
        {
            return new FileViewResult(name);
        }

        protected static ActionResult ResourceView(string path, Assembly assembly = null)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));

            return new ResourceViewResult(path, assembly);
        }

        protected static ActionResult Message(MessageResultType type, string title, string message)
        {
            return new MessageResult(type, title, message);
        }

        protected static ActionResult AjaxRedirect(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));

            return new AjaxRedirectResult(path);
        }

        protected static ActionResult AjaxRedirect<T>(Expression<Func<T, ActionResult>> expression) where T : MvcController
        {
            Argument.Assert.IsNotNull(expression, nameof(expression));

            return AjaxRedirect(ParseRedirectExpression(expression));
        }

        protected static ActionResult Redirect(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));

            return new RedirectResult(path);
        }

        protected static ActionResult Redirect(Action path)
        {
            Argument.Assert.IsNotNull(path, nameof(path));

            string controller = GetControllerName(path.Method);
            string method = path.Method.Name;

            return new RedirectResult("~/" + controller + "/" + method);
        }

        protected static ActionResult Redirect<T>(Expression<Func<T, ActionResult>> expression) where T : MvcController
        {
            Argument.Assert.IsNotNull(expression, nameof(expression));

            return new RedirectResult(ParseRedirectExpression(expression));
        }

        protected ActionResult Json(object obj)
        {
            return new JsonResult(obj);
        }

        private static string ParseRedirectExpression<T>(Expression<Func<T, ActionResult>> expression) where T : MvcController
        {
            MethodCallExpression callExpression = expression.Body as MethodCallExpression;
            if (callExpression == null)
                throw new ArgumentException("The expression must be a method call.", "expression");

            string controllerName = GetControllerName(callExpression.Method);
            string actionName = GetActionName(callExpression.Method);

            StringBuilder queryString = new StringBuilder();
            ParameterInfo[] parameters = callExpression.Method.GetParameters();
            ReadOnlyCollection<Expression> arguments = callExpression.Arguments;

            for (int i = 0; i < arguments.Count; i++)
            {
                Expression argument = arguments[i];

                MemberExpression me = argument as MemberExpression;
                if (me != null)
                {
                    object targetController = ((ConstantExpression) me.Expression).Value;
                    Type type = targetController.GetType();
                    FieldInfo[] fieldInfo = type.GetFields();
                    for (int k = 0; k < fieldInfo.Length; k++)
                        queryString.Append(parameters[k].Name + "=" + fieldInfo[k].GetValue(targetController));
                }
                else
                {
                    ConstantExpression ce = argument as ConstantExpression;
                    if (ce != null && ce.Value != null)
                        queryString.Append(parameters[i].Name + "=" + ce.Value);
                }
            }

            return "~/" + controllerName + "/" + actionName + (queryString.Length > 0 ? "?" + queryString : null);
        }

        private static string GetControllerName(MethodInfo method)
        {
            if (method.DeclaringType == null)
                throw new ControllerResultException("Unable to determine the controller name.");

            return method.DeclaringType.Name.ToLower().Replace("controller", string.Empty);
        }

        private static string GetActionName(MethodInfo method)
        {
            string actionName = null;
            object[] attributes = method.GetCustomAttributes(typeof(ActionNameAttribute), true);
            if (attributes.Length > 0)
            {
                ActionNameAttribute attribute = attributes[0] as ActionNameAttribute;
                if (attribute != null)
                    actionName = attribute.Name.ToLower();
            }
            return actionName ?? method.Name;
        }
    }
}