#region Using Directives

using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Loom.Annotations;

#endregion

namespace Loom.Web.UI
{
    public static class AjaxUtility
    {
        private const string BeginRenderControlBlock = "<!-- BEGIN AJAXUTILITY BLOCK -->";
        private const string EndRenderControlBlock = "<!-- END AJAXUTILITY BLOCK -->";

        /// <summary>
        ///     Writes the rendered html of the specified <paramref name="control" /> to the response buffer.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isSimpleControl"></param>
        public static void WriteControl(Control control, bool isSimpleControl = false)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Write(RenderControl(control, isSimpleControl));
            response.Complete();
        }

        /// <summary>
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isSimpleControl"></param>
        /// <returns></returns>
        [PublicAPI]
        public static string RenderControl(Control control, bool isSimpleControl = false)
        {
            using (StringWriter sw = new StringWriter())
            {
                return isSimpleControl ? RenderSimpleControl(control, sw) : RenderComplexControl(control, sw);
            }
        }

        public static Control CreateControl(Control control)
        {
            using (StringWriter sw = new StringWriter())
            {
                Page page = new Page {EnableViewState = false};
                HtmlForm form = new HtmlForm {ID = "__t"};

                page.Controls.Add(form);

                form.Controls.Add(control);

                HttpContext.Current.Server.Execute(page, sw, true);

                return control;
            }
        }

        /// <summary>
        ///     Writes the rendered html of the specified <paramref name="control" /> to the response buffer.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isSimpleControl"></param>
        public static void WriteChildControls(Control control, bool isSimpleControl = false)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Write(RenderChildControls(control, isSimpleControl));
            response.Complete();
        }

        /// <summary>
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isSimpleControl"></param>
        /// <returns></returns>
        [PublicAPI]
        public static string RenderChildControls(Control control, bool isSimpleControl = false)
        {
            using (StringWriter sw = new StringWriter())
            {
                return isSimpleControl ? RenderSimpleChildControls(control, sw) : RenderComplexChildControls(control, sw);
            }
        }

        public static ControlCollection CreateChildControls(Control control)
        {
            using (StringWriter sw = new StringWriter())
            {
                Page page = new Page {EnableViewState = false};
                HtmlForm form = new HtmlForm {ID = "__t"};

                page.Controls.Add(form);
                foreach (Control child in control.Controls)
                    form.Controls.Add(child);

                HttpContext.Current.Server.Execute(page, sw, true);

                return form.Controls;
            }
        }

        private static string RenderComplexControl(Control control, TextWriter writer)
        {
            Page page = new Page {EnableViewState = false};
            HtmlForm form = new HtmlForm {ID = "__t"};

            page.Controls.Add(form);

            form.Controls.Add(new LiteralControl(BeginRenderControlBlock));
            form.Controls.Add(control);
            form.Controls.Add(new LiteralControl(EndRenderControlBlock));

            HttpContext.Current.Server.Execute(page, writer, true);

            return writer.ToString().Extract(BeginRenderControlBlock, EndRenderControlBlock);
        }

        private static string RenderSimpleControl(Control control, TextWriter writer)
        {
            using (Html32TextWriter htmlWriter = new Html32TextWriter(writer))
            {
                control.RenderControl(htmlWriter);
                return writer.ToString();
            }
        }

        private static string RenderComplexChildControls(Control control, TextWriter writer)
        {
            Page page = new Page {EnableViewState = false};
            HtmlForm form = new HtmlForm {ID = "__t"};

            page.Controls.Add(form);

            form.Controls.Add(new LiteralControl(BeginRenderControlBlock));
            foreach (Control child in control.Controls)
                form.Controls.Add(child);
            form.Controls.Add(new LiteralControl(EndRenderControlBlock));

            HttpContext.Current.Server.Execute(page, writer, true);

            return writer.ToString().Extract(BeginRenderControlBlock, EndRenderControlBlock);
        }

        private static string RenderSimpleChildControls(Control control, TextWriter writer)
        {
            using (Html32TextWriter htmlWriter = new Html32TextWriter(writer))
            {
                foreach (Control child in control.Controls)
                    child.RenderControl(htmlWriter);

                return writer.ToString();
            }
        }
    }
}