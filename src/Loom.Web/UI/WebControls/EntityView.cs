#region Using Directives

using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:EntityView runat=server></{0}:EntityView>")]
    public class EntityView : WebControl
    {
        private object dataSource;

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
        public object DataSource
        {
            get => dataSource;
            set
            {
                if (value is IEnumerable)
                    throw new ArgumentException("The DataSource property does not support enumerable objects.", "value");

                dataSource = value;
            }
        }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(EntityViewItem))]
        public ITemplate ItemTemplate { get; set; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void CreateChildControls()
        {
            if (DataSource == null || ItemTemplate == null)
                return;

            EntityViewItem view = new EntityViewItem(DataSource);
            ItemTemplate.InstantiateIn(view);
            Controls.Add(view);
            view.DataBind();
        }
    }
}