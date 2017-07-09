#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Loom.Annotations;
using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal sealed class ControllerActionData
    {
        private ActionExecuter actionExecuter;
        private bool allowRouteTokens;

        public bool AllowRouteTokens
        {
            get
            {
                if (allowRouteTokens)
                    return true;
                return !Secure;
            }
            set => allowRouteTokens = value;
        }

        public bool AntiForgery { get; set; }
        public string AntiForgerySalt { get; set; }

        public List<IPortalFilter> Filters { get; } = new List<IPortalFilter>();

        public List<ActionParameter> Parameters { get; } = new List<ActionParameter>();

        public bool Secure { get; set; }

        public void RegisterMethodDelegate([NotNull] ActionExecuter executer)
        {
            Argument.Assert.IsNotNull(executer, nameof(executer));

            actionExecuter = executer;
        }

        public ActionResult CallMethod([NotNull] IController target, [NotNull] JavaScriptSerializer serializer, [NotNull] Stream stream, RouteTokens tokens = null)
        {
            Argument.Assert.IsNotNull(stream, nameof(stream));

            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                return CallMethod(target, serializer, reader.ReadToEnd(), tokens);
            }
        }

        public ActionResult CallMethod([NotNull] IController target, [NotNull] JavaScriptSerializer serializer, [CanBeNull] string json, RouteTokens tokens = null)
        {
            Argument.Assert.IsNotNull(target, nameof(target));
            Argument.Assert.IsNotNull(serializer, nameof(serializer));

            PortalContext context = PortalContext.Current;
            ActionResult result = null;

            foreach (IPortalFilter filter in Filters)
            {
                filter.Execute(context);
                result = context.Request.Result;
                if (result != null)
                    break;
            }

            if (result == null && Parameters.Count == 0 && !AntiForgery)
                result = actionExecuter(target, null);

            if (result == null)
                result = CreateExecuterWithParameters(target, serializer, json, tokens, result);

            if (result != null)
                result.ViewData = target.ViewData;

            return result;
        }

        private ActionResult CreateExecuterWithParameters(IController target, JavaScriptSerializer serializer, string json, RouteTokens tokens, ActionResult result)
        {
            List<object> methodParameters = new List<object>();

            if (!Compare.IsNullOrEmpty(json))
            {
                object o = serializer.DeserializeObject(json);
                Dictionary<string, object> d = o as Dictionary<string, object>;
                if (d == null)
                    AddSingleParameter(o, methodParameters);
                else
                    AddMultipleParameters(serializer, d, methodParameters, tokens);
            }
            else
            {
                AddTokenOnlyParameters(methodParameters, tokens);
            }

            result = actionExecuter(target, methodParameters);
            return result;
        }

        private void AddMultipleParameters(JavaScriptSerializer serializer, IDictionary<string, object> d, ICollection<object> methodParameters, RouteTokens tokens = null)
        {
            if (AntiForgery)
                AntiForgeryData.ValidateJsonRequest(d, AntiForgerySalt);

            if (tokens != null)
            {
                string[] defaultArguments = tokens.GetMultiToken("arguments");
                foreach (string argument in defaultArguments)
                    methodParameters.Add(argument);
            }

            foreach (ActionParameter p in Parameters)
            {
                string parameterName = p.Name;
                object parameterValue = null;

                if (d.ContainsKey(parameterName))
                {
                    parameterValue = d[parameterName];
                    Dictionary<string, object> dictionary = parameterValue as Dictionary<string, object>;
                    if (dictionary != null)
                    {
                        dictionary.Add("__type", p.TypeName);
                        parameterValue = serializer.DeserializeObject(serializer.Serialize(dictionary));
                    }
                }

                if (parameterValue == null && AllowRouteTokens && tokens != null && tokens.Contains(parameterName))
                    if (tokens.IsMultiToken(parameterName))
                        parameterValue = tokens.GetMultiToken(parameterName);
                    else
                        parameterValue = tokens[parameterName];

                methodParameters.Add(parameterValue);
            }
        }

        private void AddSingleParameter(object o, ICollection<object> methodParameters)
        {
            if (Parameters.Count == 1)
                methodParameters.Add(o);
            else
                throw new Exception("No DTO name is specified and the method has more than one parameter. The DTO name should match the parameter name.");
        }

        private void AddTokenOnlyParameters(ICollection<object> methodParameters, RouteTokens tokens)
        {
            if (!AllowRouteTokens || Compare.IsNullOrEmpty(tokens))
                return;

            string[] defaultArguments = tokens.GetMultiToken("arguments");
            foreach (string argument in defaultArguments)
                methodParameters.Add(argument);

            foreach (ActionParameter p in Parameters)
            {
                string parameterName = p.Name;
                object parameterValue = null;

                if (tokens.Contains(parameterName))
                    parameterValue = tokens[parameterName];

                methodParameters.Add(parameterValue);
            }
        }
    }
}