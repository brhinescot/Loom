#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using Loom.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [DefaultProperty("Items")]
    [ParseChildren(true, "Items")]
    [ToolboxData("<{0}:DropDownList runat=\"server\"></{0}:DropDownList>")]
    public class DropDownList : PortalControl, IListControl, IFormInput
    {
        private ListItemCollection items;
        public object DataSource { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public string DataTextFormatString { get; set; }
        public string GroupByField { get; set; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Select;

        #region IFormInput Members

        public bool Disabled { get; set; }
        public char AccessKey { get; set; }
        public int TabIndex { get; set; }

        public string Name { get; set; }

        #endregion

        #region IListControl Members

        [DefaultValue(null)]
        [MergableProperty(false)]
        public ListItemCollection Items => items ?? (items = new ListItemCollection());

        #endregion

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataSource == null)
                return;

            base.OnDataBinding(e);

            Type t = DataSource as Type;
            if (t != null)
                BindEnum(t);
            else
                BindIEnumerable();
        }

        public ListItem AddItem(string text, string value = null, string group = null)
        {
            ListItem item = new ListItem(text, value) {Group = group};
            Items.Add(item);
            return item;
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!Compare.IsNullOrEmpty(Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, Name);
            else if (!Compare.IsNullOrEmpty(ID))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, ID);

            if (AccessKey != '\0')
                writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey.ToString());
            if (TabIndex > 0)
                writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString(CultureInfo.InvariantCulture));
            if (Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");

            base.AddAttributesToRender(writer);
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (items == null)
                return;

            ListItemCollection listItems = items;
            if (listItems.Count == 0)
                return;

            writer.Indent++;

            string currentGroupValue = null;

            for (int i = 0; i < listItems.Count; i++)
            {
                ListItem listItem = listItems[i];

                if (!Compare.IsNullOrEmpty(GroupByField) && listItem.Group != currentGroupValue)
                {
                    if (i > 0)
                    {
                        writer.Indent--;
                        writer.WriteEndTag("optgroup");
                        writer.WriteLine();
                    }

                    writer.WriteBeginTag("optgroup");
                    writer.WriteAttribute("label", listItem.Group);
                    writer.Write('>');
                    writer.WriteLine();
                    writer.Indent++;

                    currentGroupValue = listItem.Group;
                }

                writer.WriteBeginTag("option");

                if (!Compare.IsNullOrEmpty(listItem.Value))
                    writer.WriteAttribute("value", listItem.Value);
                if (!Compare.IsNullOrEmpty(listItem.Label))
                    writer.WriteAttribute("label", listItem.Label);
                if (listItem.Selected)
                    writer.WriteAttribute("selected", "selected");
                if (listItem.Disabled)
                    writer.WriteAttribute("disabled", "disabled");

                if (listItem.HasAttributes)
                    listItem.Attributes.Render(writer);

                writer.Write('>');

                if (!Compare.IsNullOrEmpty(listItem.Text))
                    writer.Write(listItem.Text);

                writer.WriteEndTag("option");

                if (i < listItems.Count - 1)
                    writer.WriteLine();
            }

            if (currentGroupValue != null)
                writer.WriteEndTag("optgroup");

            writer.Indent--;
        }

        protected override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Indent++;
            base.RenderEndTag(writer);
            writer.Indent--;
        }

        protected override void OnSetViewData(object data)
        {
            DataSource = data;
            DataBind(true);
        }

        private void BindIEnumerable()
        {
            IEnumerable dataSource = DataSourceHelper.GetResolvedDataSource(DataSource);
            foreach (object obj in dataSource)
                if (Compare.IsAnyNullOrEmpty(DataTextField, DataValueField))
                    AddItem(obj.ToString(), obj.ToString());
                else
                    AddItem(DataBinder.GetPropertyValue(obj, DataTextField, DataTextFormatString), DataBinder.GetPropertyValue(obj, DataValueField, null), GroupByField == null ? null : DataBinder.GetPropertyValue(obj, GroupByField, null));
        }

        private void BindEnum(Type t)
        {
            if (!t.IsEnum)
                throw new InvalidOperationException("A DataSource of type 'Type' must be an Enum.");

            foreach (KeyValuePair<string, object> pair in EnumDescriptionAttribute.GetEnumData(t))
                AddItem(pair.Key, pair.Value.ToString());
        }
    }
}