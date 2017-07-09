#region Using Directives

using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    public static class HtmlHeadExtensions
    {
        public static void AddScript(this HtmlHead head, string script, bool addScriptTags)
        {
            Literal literal = new Literal();
            if (addScriptTags)
                literal.Text = "\r\n\t<script type=\"text/javascript\">" +
                               "\r\n\t\t//<![CDATA[\"\r\n\t\t\t" +
                               script +
                               "\r\n\t\t\"//]]>" +
                               "\r\n\t</script>";
            else
                literal.Text = script;
            head.Controls.Add(literal);
        }

        public static void AddKeywords(this HtmlHead head, params string[] keywords)
        {
            AddKeywordsPrivate(head, keywords);
        }

        public static void AddKeywords(this HtmlHead head, IEnumerable<string> keywords)
        {
            AddKeywordsPrivate(head, keywords);
        }

        public static void AddDescription(this HtmlHead head, string description)
        {
            Literal literal = new Literal();

            literal.Text = "\r\n\t<meta name=\"description\" content=\"" + description + "\" />";
            head.Controls.Add(literal);
        }

        public static void AddMetaData(this HtmlHead head, string name, string content)
        {
            Literal literal = new Literal();

            literal.Text = "\r\n\t<meta name=\"" + name + "\" content=\"" + content + "\" />";
            head.Controls.Add(literal);
        }

        private static void AddKeywordsPrivate(this Control head, IEnumerable<string> keywords)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("\r\n\t<meta name=\"keywords\" content=\"");
            foreach (string s in keywords)
                builder.Append(s + ", ");
            builder.Append("\" />");

            head.Controls.Add(new LiteralControl(builder.ToString()));
        }
    }
}