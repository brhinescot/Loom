#region Using Directives

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Loom.Collections;

#endregion

namespace Loom.Web.Syndication
{
    // Class to consume (or create) a channel in a late-bound way
    public sealed class GenericRssChannel : RssChannelBase<GenericRssElement, GenericRssElement>
    {
        public new CaseInsensitiveStringDictionary Attributes => base.Attributes;

        public string this[string attributeName]
        {
            get => GetAttributeValue(attributeName);
            set => Attributes[attributeName] = value;
        }

        public GenericRssElement Image => GetImage();

        public static GenericRssChannel LoadChannel(string url)
        {
            GenericRssChannel channel = new GenericRssChannel();
            channel.LoadFromUrl(url);
            return channel;
        }

        public static GenericRssChannel LoadChannel(XmlDocument doc)
        {
            GenericRssChannel channel = new GenericRssChannel();
            channel.LoadFromXml(doc);
            return channel;
        }

        // Select method for programmatic databinding
        public IEnumerable SelectItems()
        {
            List<RssElementCustomTypeDescriptor> data = new List<RssElementCustomTypeDescriptor>();

            foreach (GenericRssElement element in Items)
                data.Add(new RssElementCustomTypeDescriptor(element.Attributes));

            return data;
        }
    }
}