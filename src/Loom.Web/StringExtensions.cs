#region Using Directives

using System.Text.RegularExpressions;

#endregion

namespace Loom.Web
{
    public static class StringExtensions
    {
        public static string FixHtmlForDisplay(this string html)
        {
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            html = html.Replace("\"", "&quot;");
            return html;
        }

        public static string StripHtml(this string html)
        {
            html = Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
            html = html.Replace("\t", " ");
            html = html.Replace("\r\n", "");
            html = html.Replace("   ", " ");
            return html.Replace("  ", " ");
        }
    }
}