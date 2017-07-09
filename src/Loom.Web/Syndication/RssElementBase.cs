#region Using Directives

using Loom.Collections;

#endregion

namespace Loom.Web.Syndication
{
    // the base class for all RSS elements (item, image, channel)
    // has collection of attributes
    public abstract class RssElementBase
    {
        protected internal CaseInsensitiveStringDictionary Attributes { get; private set; } = new CaseInsensitiveStringDictionary();

        public virtual void SetDefaults() { }

        internal void SetAttributes(CaseInsensitiveStringDictionary attributes)
        {
            Attributes = attributes;
        }

        protected string GetAttributeValue(string attributeName)
        {
            string attributeValue;

            if (!Attributes.TryGetValue(attributeName, out attributeValue))
                attributeValue = string.Empty;

            return attributeValue;
        }

        protected void SetAttributeValue(string attributeName, string attributeValue)
        {
            Attributes[attributeName] = attributeValue;
        }
    }
}