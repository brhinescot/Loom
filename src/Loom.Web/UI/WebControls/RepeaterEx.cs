#region Using Directives

using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     A data-bound list control that allows custom layout by repeating a specified template for each item displayed in
    ///     the list.
    /// </summary>
    [Designer("System.Web.UI.Design.WebControls.RepeaterDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [DefaultEvent("ItemCommand")]
    [DefaultProperty("DataSource")]
    [PersistChildren(false)]
    [ParseChildren(true)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:RepeaterEx runat=server></{0}:RepeaterEx>")]
    public class RepeaterEx : Repeater
    {
        private bool boundToDataSource;

        private int currentColumn;

        private IDataItemContainer currentHeaderDataItem;
        private bool groupStarted;
        private bool hasData;
        private object lastvalue;
        private int repeatColumns;

        /// <summary>
        ///     Gets or sets the number of columns to display in the <see cref="RepeaterEx" /> control.
        /// </summary>
        /// <returns>
        ///     The number of columns to display in the <see cref="RepeaterEx" /> control. The default value is 0,
        ///     which indicates that the items in the <see cref="RepeaterEx" /> control are displayed in a single
        ///     column.
        /// </returns>
        [DefaultValue(0)]
        [Category("Layout")]
        [Description("The number of columns to display in the RepeaterEx control.")]
        public virtual int RepeatColumns
        {
            get => repeatColumns;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                repeatColumns = value;
            }
        }

        [Browsable(false)]
        public IComparer Comparer { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate GroupTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate GroupFooterTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate EmptyTemplate { get; set; }

        public override void DataBind()
        {
            lastvalue = null;
            base.DataBind();
        }

        protected override void OnItemCreated(RepeaterItemEventArgs e)
        {
            hasData = true;
            base.OnItemCreated(e);

            RepeaterItem item = e.Item;

            bool isBindable = item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;

            if (!boundToDataSource && isBindable && GroupTemplate != null)
                RenderGroupTemplate(item);
            else if (isBindable && GroupTemplate != null && Comparer != null && Comparer.Compare(lastvalue, item.DataItem) != 0)
                RenderGroupTemplate(item);

            if (currentColumn == RepeatColumns)
                currentColumn = 0;

            if (item.ItemType != ListItemType.Separator)
                currentColumn++;

            if (currentColumn != 0)
                return;

            StartNewRow(item);
        }

        protected override void CreateControlHierarchy(bool useDataSource)
        {
            boundToDataSource = useDataSource;
            base.CreateControlHierarchy(useDataSource);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!hasData && EmptyTemplate != null)
            {
                EmptyTemplate.InstantiateIn(this);
                return;
            }

            if (groupStarted && GroupFooterTemplate != null)
                RenderGroupFooterTemplate(currentHeaderDataItem);
        }

        private void RenderGroupTemplate(IDataItemContainer item)
        {
            currentHeaderDataItem = item;
            if (groupStarted && GroupFooterTemplate != null)
                RenderGroupFooterTemplate(item);

            RepeaterItem groupHeader = new RepeaterItem(item.DataItemIndex, ListItemType.Item);

            GroupTemplate.InstantiateIn(groupHeader);
            groupHeader.DataItem = item.DataItem;

            Controls.Add(groupHeader);

            groupHeader.DataBind();

            currentColumn = 0;
            lastvalue = item.DataItem;
            groupStarted = true;
        }

        private void RenderGroupFooterTemplate(IDataItemContainer item)
        {
            if (!groupStarted)
                return;

            RepeaterItem groupHeader = new RepeaterItem(item.DataItemIndex, ListItemType.Item);

            GroupFooterTemplate.InstantiateIn(groupHeader);
            groupHeader.DataItem = item.DataItem;

            Controls.Add(groupHeader);

            groupHeader.DataBind();

            currentColumn = 0;
            lastvalue = item.DataItem;

            groupStarted = false;
        }

        private static void StartNewRow(RepeaterItem item)
        {
            if (item.ItemType == ListItemType.Separator)
                item.Controls.Clear();
        }
    }
}