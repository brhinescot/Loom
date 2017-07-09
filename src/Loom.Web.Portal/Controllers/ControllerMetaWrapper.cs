#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal sealed class ControllerMetaWrapper
    {
        private const string ParamContext = "context";
        private const string RequiredContentType = "application/json; charset=utf-8";
        private const string RequiredRequestType = "POST";

        private readonly Lazy<Dictionary<string, ControllerActionData>> actionDataListInitializer;

        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer(new SimpleTypeResolver());
        private readonly Type type;

        internal ControllerMetaWrapper([NotNull] Type type, string name)
        {
            Argument.Assert.IsNotNull(type, nameof(type));
            Argument.Assert.IsNotNull(name, nameof(name));

            this.type = type;
            BaseViewVirtualPaths = new List<string>();
            Name = name;
            actionDataListInitializer = new Lazy<Dictionary<string, ControllerActionData>>(InitializeActionDataList);
        }

        public List<string> BaseViewVirtualPaths { get; set; }

        public string Name { get; }

        [CanBeNull]
        public ActionResult Execute([NotNull] IPortalContext context)
        {
            Argument.Assert.IsNotNull(context, ParamContext);
            if (context.Request == null)
                throw new ArgumentException("The request has not been initialized.");

            PortalTrace.Write("ControllerMetaWrapper", "Execute", "Begin Controller Execution '{0}'.", type.FullName);
            IController controller = (IController) Activator.CreateInstance(type);
            PortalTrace.Write("ControllerMetaWrapper", "Execute", "End Controller Execution '{0}'.", type.FullName);

            try
            {
                return ExecuteControllerAction(context, controller);
            }
            catch (ControllerResultException ex)
            {
                return ex.GetResult();
            }
        }

        private static void WriteJsonServerException(string message, IHttpContext context)
        {
            IHttpResponse response = context.Response;
            response.ClearHeaders();
            response.ClearContent();
            response.Clear();
            response.StatusCode = 500;
            response.Write("{\"Message\":\"" + message.Replace("\r\n", " ") + "\"}");
            response.Complete();
        }

        private Dictionary<string, ControllerActionData> InitializeActionDataList()
        {
            return ControllerInitializer.CreateActionDataList(type);
        }

        private ActionResult ExecuteControllerAction(IPortalContext context, IController controller)
        {
            string actionName = context.Request.ActionName.ToUpper(CultureInfo.InvariantCulture);
            Dictionary<string, ControllerActionData> actionDataList = actionDataListInitializer.Value;
            if (actionDataList.ContainsKey(actionName))
            {
                ControllerActionData actionData = actionDataList[actionName];
                IHttpRequest httpRequest = context.HttpContext.Request;
                if (actionData.Secure && (!RequiredRequestType.Equals(httpRequest.RequestType, StringComparison.OrdinalIgnoreCase) || !RequiredContentType.Equals(httpRequest.ContentType, StringComparison.OrdinalIgnoreCase)))
                    WriteJsonServerException("Invalid Request", context.HttpContext);

                try
                {
                    ActionResult action = actionData.CallMethod(controller, serializer, context.HttpContext.Request.InputStream, context.Request.Tokens);
                    if (action == null)
                        throw new ControllerResultException("Controller did not return an ActionResult.");

                    action.ViewPaths = BaseViewVirtualPaths.ToArray();
                    return action;
                }
                catch (Exception ex)
                {
                    if (actionData.Secure)
#if DEBUG
                    {
                        string message = ex.Message;
                        if (ex.InnerException != null)
                            message += " " + ex.InnerException.Message;
                        WriteJsonServerException(message, context.HttpContext);
                    }
#endif
#if !DEBUG
                        WriteJsonServerException(ex.Message, context.HttpContext);
#endif

                    else
                    {
                        throw;
                    }
                }
            }

            return null;
        }
    }
}