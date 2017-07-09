#region Using Directives

using System.CodeDom;
using System.IO;
using System.Web;
using System.Web.Compilation;
using System.Xml;

#endregion

namespace Loom.Web.Syndication
{
    // build provider for .rssdl type - to automatically generate strongly typed
    // classes for channels from URLs defined in .rssdl file
    [BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
    public sealed class RssdlBuildProvider : BuildProvider
    {
        public override void GenerateCode(AssemblyBuilder assemblyBuilder)
        {
            // load as XML
            XmlDocument doc = new XmlDocument();

            using (Stream s = OpenStream(VirtualPath))
            {
                doc.Load(s);
            }

            // valide root rssdl node
            XmlNode root = doc.DocumentElement;

            if (root.Name != "rssdl")
                throw new InvalidDataException(
                    string.Format("Unexpected root node '{0}' -- expected root 'rssdl' node", root.Name));

            // iterate through rss nodes
            for (XmlNode n = root.FirstChild; n != null; n = n.NextSibling)
            {
                if (n.NodeType != XmlNodeType.Element)
                    continue;

                if (n.Name != "rss")
                    throw new InvalidDataException(
                        string.Format("Unexpected node '{0}' -- expected root 'rss' node", root.Name));

                string name = string.Empty;
                string file = string.Empty;
                string url = string.Empty;
                string ns = string.Empty;

                foreach (XmlAttribute attr in n.Attributes)
                    switch (attr.Name)
                    {
                        case "name":
                            name = attr.Value;
                            break;
                        case "url":
                            if (!Compare.IsNullOrEmpty(file))
                                throw new InvalidDataException("Only one of 'file' and 'url' can be specified");

                            url = attr.Value;
                            break;
                        case "file":
                            if (!Compare.IsNullOrEmpty(url))
                                throw new InvalidDataException("Only one of 'file' and 'url' can be specified");

                            file = VirtualPathUtility.Combine(VirtualPathUtility.GetDirectory(VirtualPath), attr.Value);
                            break;
                        case "namespace":
                            ns = attr.Value;
                            break;
                        default:
                            throw new InvalidDataException(
                                string.Format("Unexpected attribute '{0}'", attr.Name));
                    }

                if (Compare.IsNullOrEmpty(name))
                    throw new InvalidDataException("Missing 'name' attribute");

                if (Compare.IsNullOrEmpty(url) && Compare.IsNullOrEmpty(file))
                    throw new InvalidDataException("Missing 'url' or 'file' attribute - one must be specified");

                // load channel
                GenericRssChannel channel;

                if (!Compare.IsNullOrEmpty(url))
                {
                    channel = GenericRssChannel.LoadChannel(url);
                }
                else
                {
                    XmlDocument rssDoc = new XmlDocument();

                    using (Stream s = OpenStream(file))
                    {
                        rssDoc.Load(s);
                    }

                    channel = GenericRssChannel.LoadChannel(rssDoc);
                }

                // compile channel
                CodeCompileUnit ccu = new CodeCompileUnit();
                RssCodeGenerator.GenerateCodeDomTree(channel, ns, name, ccu);
                assemblyBuilder.AddCodeCompileUnit(this, ccu);
            }
        }
    }
}