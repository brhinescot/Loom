#region Using Directives

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Html")]
    [ToolboxData("<{0}:HtmlRotator runat=server></{0}:HtmlRotator>")]
    public class HtmlRotator : WebControl
    {
        private Collection<string> htmlItems;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public Collection<string> HtmlItems
        {
            get
            {
                if (htmlItems == null)
                    htmlItems = new Collection<string>();
                return htmlItems;
            }
        }

        /// <summary>
        ///     Restores view-state information from a previous request that was saved with the
        ///     <see cref="SaveViewState"></see> method.
        /// </summary>
        /// <param name="savedState">An object that represents the control state to restore. </param>
        protected override void LoadViewState(object savedState)
        {
            object[] objArray = (object[]) savedState;
            base.LoadViewState(objArray[0]);
            if (objArray[1] != null)
                htmlItems = objArray[1] as Collection<string>;
        }

        /// <summary>
        ///     Saves any state that was modified after the <see cref="Style.TrackViewState"></see> method
        ///     was invoked.
        /// </summary>
        /// <returns>
        ///     An object that contains the current view state of the control; otherwise, if there is no view
        ///     state associated with the control, null.
        /// </returns>
        protected override object SaveViewState()
        {
            return new[] {base.SaveViewState(), htmlItems};
        }
    }
}