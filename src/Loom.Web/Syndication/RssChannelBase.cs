#region Using Directives

using System.Collections.Generic;
using System.Xml;
using Loom.Collections;

#endregion

namespace Loom.Web.Syndication
{
    // base class for RSS channel (for strongly-typed and late-bound channel types)
    public abstract class RssChannelBase<RssItemType, RssImageType> : RssElementBase
        where RssItemType : RssElementBase, new()
        where RssImageType : RssElementBase, new()
    {
        private RssImageType image;

        public List<RssItemType> Items { get; } = new List<RssItemType>();

        internal string Url { get; private set; }

        protected void LoadFromUrl(string url)
        {
            // download the feed
            RssChannelDom dom = RssDownloadManager.GetChannel(url);

            // create the channel
            LoadFromDom(dom);

            // remember the url
            Url = url;
        }

        protected void LoadFromXml(XmlDocument doc)
        {
            // parse XML
            RssChannelDom dom = RssXmlHelper.ParseChannelXml(doc);

            // create the channel
            LoadFromDom(dom);
        }

        internal void LoadFromDom(RssChannelDom dom)
        {
            // channel attributes
            SetAttributes(dom.Channel);

            // image attributes
            if (dom.Image != null)
            {
                RssImageType image = new RssImageType();
                image.SetAttributes(dom.Image);
                this.image = image;
            }

            // items
            foreach (CaseInsensitiveStringDictionary i in dom.Items)
            {
                RssItemType item = new RssItemType();
                item.SetAttributes(i);
                Items.Add(item);
            }
        }

        internal XmlDocument SaveAsXml()
        {
            XmlDocument doc = RssXmlHelper.CreateEmptyRssXml();
            XmlNode channelNode = RssXmlHelper.SaveRssElementAsXml(doc.DocumentElement, this, "channel");

            if (image != null)
                RssXmlHelper.SaveRssElementAsXml(channelNode, image, "image");

            foreach (RssItemType item in Items)
                RssXmlHelper.SaveRssElementAsXml(channelNode, item, "item");

            return doc;
        }

        protected RssImageType GetImage()
        {
            if (image == null)
                image = new RssImageType();

            return image;
        }
    }
}