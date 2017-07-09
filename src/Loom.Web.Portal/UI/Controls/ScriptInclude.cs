#region Using Directives

using System.Web.UI;
using System.Web.UI.HtmlControls;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    internal sealed class ScriptInclude : HtmlGenericControl
    {
        public ScriptInclude(string src, string type = Strings.TextJavascript) : base(Strings.Script)
        {
            Type = type;
            Src = src;
        }

        public string Src
        {
            get => GetAttribute(Strings.Src);
            private set => Attributes.Add(Strings.Src, ResolveUrl(value));
        }

        public string Type
        {
            get => GetAttribute(Strings.Type);
            private set => Attributes.Add(Strings.Type, value);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.WriteLine();
        }
    }
}