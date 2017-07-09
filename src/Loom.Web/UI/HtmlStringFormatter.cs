#region Using Directives

using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Loom.Web.UI.PageParts;

#endregion

namespace Loom.Web.UI
{
    public static class HtmlStringFormatter
    {
        public static string ToList(string input, string seperator)
        {
            if (Compare.IsNullOrEmpty(input))
                return "<ul></ul>";

            string[] lines = input.Split(new[] {seperator}, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(sw))
            {
                ContentRenderer renderer = new ContentRenderer(htmlWriter);
                renderer.RenderRepeater(lines, new HtmlListRenderer<string>());
            }
            return sb.ToString();
        }
    }
}