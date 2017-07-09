#region Using Directives

using System.Web;
using System.Xml;

#endregion

namespace Loom.Web.Syndication
{
    // base class for RssHttpHandler - Generic handler and strongly typed ones are derived from it
    public abstract class RssHttpHandlerBase<RssChannelType, RssItemType, RssImageType> : IHttpHandler
        where RssChannelType : RssChannelBase<RssItemType, RssImageType>, new()
        where RssItemType : RssElementBase, new()
        where RssImageType : RssElementBase, new()
    {
        protected RssChannelType Channel { get; private set; }

        #region IHttpHandler Members

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            // create the channel
            Channel = new RssChannelType();
            Channel.SetDefaults();

            // parse the channel name and the user name from the query string
            string userName;
            string channelName;
            RssHttpHandlerHelper.ParseChannelQueryString(context.Request, out channelName, out userName);

            // populate items (call the derived class)
            PopulateChannel(channelName, userName);

            // save XML into response
            XmlDocument doc = Channel.SaveAsXml();
            context.Response.ContentType = "text/xml";
            doc.Save(context.Response.OutputStream);
        }

        bool IHttpHandler.IsReusable => false;

        #endregion

        // the only method derived classes are supposed to override
        protected virtual void PopulateChannel(string channelName, string userName) { }
    }
}