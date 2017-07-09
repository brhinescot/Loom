#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.Web.Syndication
{
    // late-bound RSS element (used for late bound item and image)
    public sealed class GenericRssElement : RssElementBase
    {
        public new Dictionary<string, string> Attributes => base.Attributes;

        public string this[string attributeName]
        {
            get => GetAttributeValue(attributeName);
            set => Attributes[attributeName] = value;
        }
    }
}