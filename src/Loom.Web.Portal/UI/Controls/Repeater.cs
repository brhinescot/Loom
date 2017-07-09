#region Using Directives

using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using Loom.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [PersistChildren(false)]
    [ParseChildren(true)]
    [ToolboxData("<{0}:Repeater runat=\"server\"></{0}:Repeater>")]
    public class Repeater : PortalControl
    {
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate AlternatingGroupFooterTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate AlternatingGroupHeaderTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate AlternatingItemTemplate { get; set; }

        [Browsable(false)]
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        [Browsable(false)]
        public object DataSource { get; set; }

        public string DataMember { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate EmptyTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate FooterTemplate { get; set; }

        [Browsable(false)]
        public IComparer GroupComparer { get; set; }

        public string GroupBy { get; set; }

        public bool SkipSort { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate GroupFooterTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate GroupHeaderTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate HeaderTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate ItemTemplate { get; set; }

        protected override void CreateChildControls()
        {
            if (DataSource == null || ItemTemplate == null)
                return;

            IEnumerable e = DataSourceHelper.GetResolvedDataSource(DataSource, DataMember);
            if (e == null)
                throw new Exception("The DataSource property must be of type IEnumerable or IListSource.");

            if (!SkipSort && GroupComparer != null)
            {
                ArrayList items = new ArrayList();
                foreach (object o in e)
                    items.Add(o);

                items.Sort(GroupComparer);
                e = items;
            }

            int itemIndex = 0;
            int groupIndex = 0;
            object previous = null;
            foreach (object item in e)
            {
                if (itemIndex == 0)
                    RenderTemplate(HeaderTemplate);

                if (GroupComparer != null)
                    groupIndex = RenderGroupIfNeeded(item, previous, groupIndex);

                if (itemIndex % 2 != 0 && AlternatingItemTemplate != null)
                    RenderTemplate(AlternatingItemTemplate, item, itemIndex++);
                else
                    RenderTemplate(ItemTemplate, item, itemIndex++);

                previous = item;
            }

            if (itemIndex > 0)
            {
                if (GroupComparer != null)
                    RenderTemplate(GroupFooterTemplate, previous, groupIndex);
                RenderTemplate(FooterTemplate);
                return;
            }

            RenderTemplate(EmptyTemplate);
        }

        protected override void RenderBeginTag(HtmlTextWriter writer) { }

        protected override void RenderEndTag(HtmlTextWriter writer) { }

        private int RenderGroupIfNeeded(object item, object previous, int index)
        {
            bool isAlternateRow = index % 2 != 0 && (AlternatingGroupFooterTemplate != null || AlternatingGroupHeaderTemplate != null);

            if (GroupComparer.Compare(item, previous) == 1)
                RenderTemplate(isAlternateRow ? AlternatingGroupFooterTemplate : GroupFooterTemplate, previous, index);
            if (GroupComparer.Compare(previous, item) == -1)
                RenderTemplate(isAlternateRow ? AlternatingGroupHeaderTemplate : GroupHeaderTemplate, item, index++);

            return index;
        }

        private void RenderTemplate(ITemplate template, object item = null, int index = 0)
        {
            if (template == null)
                return;

            RepeaterItem view = new RepeaterItem(item, index);
            template.InstantiateIn(view);
            Controls.Add(view);
            if (item != null)
                view.DataBind();
        }

        protected override void OnSetViewData(object data)
        {
            if (GroupComparer == null && !Compare.IsNullOrEmpty(GroupBy))
                GroupComparer = new PropertyComparer {PropertyName = GroupBy};
            DataSource = data;
        }
    }

    public class PropertyComparer : IComparer
    {
        public string PropertyName { get; set; }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            PropertyDescriptor propertyX = TypeDescriptor.GetProperties(x)[PropertyName];
            PropertyDescriptor propertyY = TypeDescriptor.GetProperties(y)[PropertyName];

            if (propertyX == null || propertyY == null)
                throw new ArgumentException("Object does not contain a property named '" + PropertyName + "'.");

            IComparable compareX = propertyX.GetValue(x) as IComparable;
            IComparable compareY = propertyY.GetValue(y) as IComparable;

            if (compareX == null || compareY == null)
                throw new ArgumentException("The property specified by PropertyName is not comparable. It must implement IComparable.");

            return compareX.CompareTo(compareY);
        }

        #endregion
    }
}