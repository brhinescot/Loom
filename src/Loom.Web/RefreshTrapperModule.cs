#region Using Directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Summary description for RefreshTrapperModule.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///  	<httpModules>
    /// 			<add name="RefreshTrapperModule" type="Loom.Web.RefreshTrapperModule, Loom.Web" />
    /// 		</httpModules>
    ///  ]]>
    ///  </code>
    /// </example>
    public class RefreshTrapperModule : IHttpModule
    {
        /// <summary>
        /// </summary>
        public string ModuleName => "RefreshTrapperModule";

        #region IHttpModule Members

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void Init(HttpApplication context)
        {
            //Register for pipeline events
            context.AcquireRequestState += HandleContextAcquireRequestState;
        }

        /// <summary>
        /// </summary>
        public void Dispose() { }

        #endregion

        private static void HandleContextAcquireRequestState(object sender, EventArgs e)
        {
            //Get access to the HttpContext
            HttpApplication app = (HttpApplication) sender;
            HttpContext context = app.Context;

            //Initialize the session slots for the page (Ticket)
            //and the module (LastTicketServed)

            if (context.Session["LastTicketServed"] == null)
                context.Session["LastTicketServed"] = 0;

            //Set the default result
            context.Items["IsRefresh"] = false;

            //Read the last ticket served and the 
            //ticket of the current request
            int lastTicket = Convert.ToInt32(context.Session["LastTicketServed"]);
            int thisTicket = Convert.ToInt32(context.Request.Params["__TICKET"]);

            //Compare the tickets
            if (thisTicket > lastTicket || thisTicket == lastTicket && thisTicket == 0)
            {
                context.Session["LastTicketServed"] = thisTicket;
                context.Items["IsRefresh"] = false;
            }
            else
            {
                context.Items["IsRefresh"] = true;
            }
        }
    }
}