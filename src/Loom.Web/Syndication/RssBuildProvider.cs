#region Using Directives

using System.CodeDom;
using System.IO;
using System.Web;
using System.Web.Compilation;
using System.Xml;

#endregion

namespace Loom.Web.Syndication
{
    // build provided for .rss file - channel definition in app_code
    // generates strognly-typed channel type
    [BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
    public sealed class RssBuildProvider : BuildProvider
    {
        public override void GenerateCode(AssemblyBuilder assemblyBuilder)
        {
            // get name and namespace from the filename
            string fname = VirtualPathUtility.GetFileName(VirtualPath);
            fname = fname.Substring(0, fname.LastIndexOf('.')); // no extension
            int i = fname.LastIndexOf('.');

            string name, ns;

            if (i < 0)
            {
                name = fname;
                ns = string.Empty;
            }
            else
            {
                name = fname.Substring(i + 1);
                ns = fname.Substring(0, i);
            }

            // load as XML
            XmlDocument doc = new XmlDocument();
            using (Stream s = OpenStream(VirtualPath))
            {
                doc.Load(s);
            }

            // create the channel
            GenericRssChannel channel = GenericRssChannel.LoadChannel(doc);

            // compile the channel
            CodeCompileUnit ccu = new CodeCompileUnit();
            RssCodeGenerator.GenerateCodeDomTree(channel, ns, name, ccu);
            assemblyBuilder.AddCodeCompileUnit(this, ccu);
        }
    }
}