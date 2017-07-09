#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:Form runat=\"server\"></{0}:Form>")]
    public class Form : PortalControl, IDataItemContainer
    {
        private object viewData;

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(typeof(ITemplate), "Template")]
        [Description("Form template")]
        public virtual ITemplate Template { get; set; }

        #region IDataItemContainer Members

        /// <summary>
        ///     When implemented, gets an object that is used in simplified data-binding operations.
        /// </summary>
        /// <returns>
        ///     An object that represents the value to use when data-binding operations are performed.
        /// </returns>
        object IDataItemContainer.DataItem => viewData;

        /// <summary>
        ///     When implemented, gets the index of the data item bound to a control.
        /// </summary>
        /// <returns>
        ///     An Integer representing the index of the data item in the data source.
        /// </returns>
        int IDataItemContainer.DataItemIndex => 0;

        /// <summary>
        ///     When implemented, gets the position of the data item as displayed in a control.
        /// </summary>
        /// <returns>
        ///     An Integer representing the position of the data item as displayed in a control.
        /// </returns>
        int IDataItemContainer.DisplayIndex => 0;

        #endregion

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (Template == null)
                return;

            Template.InstantiateIn(this);
        }

        protected override void OnSetViewData(object data)
        {
            viewData = data;
        }
    }
}