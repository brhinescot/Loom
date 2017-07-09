#region Using Directives

using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using Loom.Collections;

#endregion

namespace Loom.Web.Syndication
{
    internal class RssXmlHelper
    {
        // internal helper class for XML to RSS conversion (and for generating XML from RSS)
        internal static RssChannelDom ParseChannelXml(XmlDocument doc)
        {
            CaseInsensitiveStringDictionary channelAttributes = null;
            CaseInsensitiveStringDictionary imageAttributes = null;
            List<Dictionary<string, string>> itemsAttributesList = new List<Dictionary<string, string>>();

            try
            {
                XmlNode root = doc.DocumentElement;
                if (root.Name == "rss")
                    for (XmlNode c = root.FirstChild; c != null; c = c.NextSibling)
                        if (c.NodeType == XmlNodeType.Element && c.Name == "channel")
                        {
                            for (XmlNode n = c.FirstChild; n != null; n = n.NextSibling)
                                if (n.NodeType == XmlNodeType.Element)
                                    if (n.Name == "item")
                                        itemsAttributesList.Add(ParseAttributesFromXml(n));
                                    else if (n.Name == "image")
                                        imageAttributes = ParseAttributesFromXml(n);

                            channelAttributes = ParseAttributesFromXml(c);
                            break;
                        }
                else if (root.Name == "rdf:RDF")
                    for (XmlNode n = root.FirstChild; n != null; n = n.NextSibling)
                        if (n.NodeType == XmlNodeType.Element)
                        {
                            if (n.Name == "channel")
                                channelAttributes = ParseAttributesFromXml(n);
                            if (n.Name == "image")
                                imageAttributes = ParseAttributesFromXml(n);
                            if (n.Name == "item")
                                itemsAttributesList.Add(ParseAttributesFromXml(n));
                        }
                else
                    throw new InvalidOperationException("Unexpected root node");

                if (channelAttributes == null)
                    throw new InvalidOperationException("Cannot find channel node");
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to parse RSS channel", ex);
            }

            return new RssChannelDom(channelAttributes, imageAttributes, itemsAttributesList);
        }

        internal static XmlDocument CreateEmptyRssXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                <rss version=""2.0"">
                </rss>");
            return doc;
        }

        internal static XmlNode SaveRssElementAsXml(XmlNode parentNode, RssElementBase element, string elementName)
        {
            XmlDocument doc = parentNode.OwnerDocument;
            XmlNode node = doc.CreateElement(elementName);
            parentNode.AppendChild(node);

            foreach (KeyValuePair<string, string> attr in element.Attributes)
            {
                XmlNode attrNode = doc.CreateElement(attr.Key);
                attrNode.InnerText = ResolveAppRelativeLinkToUrl(attr.Value);
                node.AppendChild(attrNode);
            }

            return node;
        }

        private static CaseInsensitiveStringDictionary ParseAttributesFromXml(XmlNode node)
        {
            CaseInsensitiveStringDictionary attributes = new CaseInsensitiveStringDictionary();

            for (XmlNode n = node.FirstChild; n != null; n = n.NextSibling)
                if (n.NodeType == XmlNodeType.Element && !NodeHasSubElements(n))
                    if (attributes.ContainsKey(n.Name))
                        attributes[n.Name] = attributes[n.Name] + ", " + n.InnerText.Trim();
                    else
                        attributes.Add(n.Name, n.InnerText.Trim());

            return attributes;
        }

        private static bool NodeHasSubElements(XmlNode node)
        {
            for (XmlNode n = node.FirstChild; n != null; n = n.NextSibling)
                if (n.NodeType == XmlNodeType.Element)
                    return true;

            return false;
        }

        private static string ResolveAppRelativeLinkToUrl(string link)
        {
            if (!Compare.IsNullOrEmpty(link) && link.StartsWith("~/"))
            {
                HttpContext context = HttpContext.Current;

                if (context != null)
                {
                    link = VirtualPathUtility.ToAbsolute(link);
                    link = new Uri(context.Request.Url, link).ToString();
                }
            }

            return link;
        }
    }
}