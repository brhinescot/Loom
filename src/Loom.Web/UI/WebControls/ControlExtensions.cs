#region Using Directives

using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web.UI.WebControls
{
    public static class ControlExtensions
    {
        /// <summary>
        ///     Similar to <see cref="Control.FindControl" />, but also searches through child controls.
        /// </summary>
        public static T FindChild<T>(this Control control, string id) where T : Control
        {
            return FindChild(control, id) as T;
        }

        /// <summary>
        ///     Similar to <see cref="Control.FindControl" />, but also searches through child controls.
        /// </summary>
        public static Control FindChild(this Control control, string id)
        {
            Argument.Assert.IsNotNull(control, nameof(control));
            Argument.Assert.IsNotNullOrEmpty(id, nameof(id));

            return control.Controls.Count == 0 ? null : ControlRecursiveSearch.Find(control, id);
        }

        /// <summary>
        ///     Gets the control's value from the current <see cref="HttpRequest" />
        /// </summary>
        /// <param name="control"></param>
        /// <returns>A <see cref="string" /> representing the control's value.</returns>
        public static string RequestValue(this Control control)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            return RequestValue(control, false);
        }

        /// <summary>
        ///     Gets the control's value from the current <see cref="HttpRequest" />
        /// </summary>
        /// <param name="control"></param>
        /// <param name="unencoded"></param>
        /// <returns>A <see cref="string" /> representing the control's value.</returns>
        public static string RequestValue(this Control control, bool unencoded)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            HttpRequest request = HttpContext.Current.Request;
            string value = request[control.UniqueID];
            if (value == null)
                return null;

            value = value.Trim();
            return unencoded ? value : AntiXss.HtmlEncode(value);
        }

        /// <summary>
        ///     Gets the control's value represented by the <paramref name="id" /> from the current <see cref="HttpRequest" />
        /// </summary>
        /// <param name="control"></param>
        /// <param name="id"></param>
        /// <returns>A <see cref="string" /> representing the control's value.</returns>
        public static string RequestChildValue(this Control control, string id)
        {
            Argument.Assert.IsNotNull(control, nameof(control));
            Argument.Assert.IsNotNullOrEmpty(id, nameof(id));

            Control found = FindChild(control, id);

            return found == null ? null : found.RequestValue();
        }

        /// <summary>
        ///     Gets the control's value from the current <see cref="HttpRequest" />
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="control"></param>
        /// <returns>A <see cref="string" /> representing the control's value.</returns>
        public static string AjaxRequestValue(this Control control)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            string value = HttpContext.Current.Request[control.UniqueID];
            return value ?? HttpContext.Current.Request[control.ID];
        }

        /// <summary>
        ///     Gets the control's value represented by the <paramref name="id" /> from the current <see cref="HttpRequest" />
        /// </summary>
        /// <param name="control"></param>
        /// <param name="id"></param>
        /// <returns>A <see cref="string" /> representing the control's value.</returns>
        public static string AjaxRequestChildValue(this Control control, string id)
        {
            Argument.Assert.IsNotNull(control, nameof(control));
            Argument.Assert.IsNotNullOrEmpty(id, nameof(id));

            Control found = FindChild(control, id);

            return found == null ? null : found.AjaxRequestValue();
        }

        public static ITextControl FindTextControl(this Control item, string id)
        {
            return item.FindControl(id) as ITextControl;
        }

        public static ListControl FindListControl(this Control item, string id)
        {
            return item.FindControl(id) as ListControl;
        }

        public static ICheckBoxControl FindCheckControl(this Control item, string id)
        {
            return item.FindControl(id) as ICheckBoxControl;
        }

        public static WebControl FindWebControl(this Control item, string id)
        {
            return item.FindControl(id) as WebControl;
        }

        public static ILocalizable FindLocalizableControl(this Control item, string id)
        {
            return item.FindControl(id) as ILocalizable;
        }

        public static string RenderControl(this Control control)
        {
            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(sw))
            {
                control.RenderControl(htmlWriter);
                return sb.Length == 0 ? null : sb.ToString();
            }
        }
    }
}