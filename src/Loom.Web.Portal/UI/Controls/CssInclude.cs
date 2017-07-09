#region Using Directives

using System.Web.UI;
using System.Web.UI.HtmlControls;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    internal sealed class CssInclude : HtmlGenericControl
    {
        public CssInclude(string src, string type = Strings.TextCss) : base(Strings.Link)
        {
            Type = type;
            Src = src;
            Attributes.Add(Strings.Rel, Strings.Stylesheet);
        }

        public string Src
        {
            get => GetAttribute(Strings.Href);
            private set => Attributes.Add(Strings.Href, ResolveUrl(value));
        }

        public string Type
        {
            get => GetAttribute(Strings.Type);
            private set => Attributes.Add(Strings.Type, value);
        }

        public string Condition { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            bool isConditional = !Compare.IsNullOrEmpty(Condition);

            writer.Indent++;
            writer.WriteLine();
            if (isConditional)
            {
                writer.WriteLine("<!--[if " + Condition + "]>");
                writer.Indent++;
            }

            base.Render(writer);

            if (isConditional)
            {
                writer.Indent--;
                writer.WriteLine();
                writer.Write("<![endif]--> ");
            }
            writer.Indent--;
        }
    }
}