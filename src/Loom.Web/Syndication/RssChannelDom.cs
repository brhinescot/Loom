#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Collections;

#endregion

namespace Loom.Web.Syndication
{
    // internal representation of parsed RSS channel
    internal class RssChannelDom
    {
        internal RssChannelDom(CaseInsensitiveStringDictionary channel,
            CaseInsensitiveStringDictionary image,
            List<Dictionary<string, string>> items)
        {
            Channel = channel;
            Image = image;
            Items = items;
            UtcExpiry = DateTime.MaxValue;
        }

        internal CaseInsensitiveStringDictionary Channel { get; }

        internal CaseInsensitiveStringDictionary Image { get; }

        internal List<Dictionary<string, string>> Items { get; }

        internal DateTime UtcExpiry { get; private set; }

        internal void SetExpiry(DateTime utcExpiry)
        {
            UtcExpiry = utcExpiry;
        }
    }
}