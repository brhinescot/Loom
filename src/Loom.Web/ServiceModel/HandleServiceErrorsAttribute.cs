#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

#endregion

namespace Loom.Web.ServiceModel
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class HandleServiceErrorsAttribute : Attribute, IServiceBehavior, IErrorHandler
    {
        #region IErrorHandler Members

        bool IErrorHandler.HandleError(Exception error)
        {
            Trace.TraceError(error.ToString());
            return true;
        }

        void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultException<ApiFault> faultException = new FaultException<ApiFault>(ApiFault.FromException(error));

            object detail = faultException.GetType().GetProperty("Detail").GetGetMethod().Invoke(faultException, null);

            fault = Message.CreateMessage(version, "", detail, new DataContractJsonSerializer(detail.GetType()));
            WebBodyFormatMessageProperty webBodyFormatMessageProp = new WebBodyFormatMessageProperty(WebContentFormat.Json);

            fault.Properties.Add(WebBodyFormatMessageProperty.Name, webBodyFormatMessageProp);

            HttpResponseMessageProperty httpResponseMessageProp = new HttpResponseMessageProperty();
            httpResponseMessageProp.Headers[HttpResponseHeader.ContentType] = "application/json";
            httpResponseMessageProp.StatusCode = HttpStatusCode.InternalServerError;
            httpResponseMessageProp.StatusDescription = error.Message;

            fault.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProp);
        }

        #endregion

        #region IServiceBehavior Members

        void IServiceBehavior.AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters) { }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = new HandleServiceErrorsAttribute();

            for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
            {
                ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;

                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        #endregion
    }
}