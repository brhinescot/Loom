#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Web.UI;
using Loom.Collections;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [PersistChildren(false)]
    [ParseChildren(true)]
    [ToolboxData("<{0}:PropertyGrid runat=\"server\"></{0}:PropertyGrid>")]
    public class PropertyGrid : PortalControl
    {
        private PropertyInfo[] properties;

        public PropertyGrid()
        {
            PropertyValues = new Dictionary<string, string>();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type DataSource { get; set; }

        public string PropertyColumnCssClass { get; set; }
        public string ValueColumnCssClass { get; set; }
        public string DescriptionColumnCssClass { get; set; }

        private Dictionary<string, string> PropertyValues { get; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate ItemTemplate { get; set; }

        public void SetProperty(string name, object value)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));
            string v = value == null ? null : value.ToString();

            if (PropertyValues.ContainsKey(name))
                PropertyValues[name] = v;
            else
                PropertyValues.Add(name, v);
        }

        public override void DataBind()
        {
            if (DataSource == null)
                return;

            properties = DataSource.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                ModulePropertyAttribute attribute = (ModulePropertyAttribute) Attribute.GetCustomAttribute(property, typeof(ModulePropertyAttribute));
                if (attribute == null)
                    continue;

                string value = Context.Request.Form[property.Name];
                if (value == null)
                    continue;

                if (PropertyValues.ContainsKey(property.Name))
                    PropertyValues[property.Name] = value;
                else
                    PropertyValues.Add(property.Name, value);
            }
        }

        public string ToJsonString()
        {
            AutoStringDictionary propertyValues = new AutoStringDictionary();

            foreach (KeyValuePair<string, string> pair in PropertyValues)
                propertyValues.Add(pair.Key, pair.Value);

            return propertyValues.ToJson();
        }

        protected override void CreateChildControls()
        {
            if (ItemTemplate == null)
            {
                base.CreateChildControls();
                return;
            }

            for (int index = 0; index < properties.Length; index++)
            {
                PropertyInfo property = properties[index];
                ModulePropertyAttribute attribute = (ModulePropertyAttribute) Attribute.GetCustomAttribute(property, typeof(ModulePropertyAttribute));
                if (attribute == null)
                    continue;

                Control edit = GetEditControl(property, attribute);
                Control label = GetLabelControl(property, attribute);

                StringWriter sw = new StringWriter();
                HtmlTextWriter writer = new HtmlTextWriter(sw);

                label.RenderControl(writer);
                string labelHtml = sw.ToString();

                sw = new StringWriter();
                writer = new HtmlTextWriter(sw);

                edit.RenderControl(writer);
                string editHtml = sw.ToString();

                PropertyGridItem item = new PropertyGridItem(labelHtml, editHtml, attribute.Description);

                RenderTemplate(ItemTemplate, item, index);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ItemTemplate != null)
            {
                base.Render(writer);
                return;
            }

            if (DataSource == null)
                return;

            if (CssClass != null)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Thead);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            if (PropertyColumnCssClass != null)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, PropertyColumnCssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Property");
            writer.RenderEndTag();

            if (ValueColumnCssClass != null)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, ValueColumnCssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Value");
            writer.RenderEndTag();

            if (DescriptionColumnCssClass != null)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, DescriptionColumnCssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Description");
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            foreach (PropertyInfo property in properties)
            {
                ModulePropertyAttribute attribute = (ModulePropertyAttribute) Attribute.GetCustomAttribute(property, typeof(ModulePropertyAttribute));
                if (attribute == null)
                    continue;

                Control edit = GetEditControl(property, attribute);
                Control label = GetLabelControl(property, attribute);

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                if (PropertyColumnCssClass != null)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, PropertyColumnCssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                label.RenderControl(writer);
                writer.RenderEndTag();

                if (ValueColumnCssClass != null)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, ValueColumnCssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                edit.RenderControl(writer);
                writer.RenderEndTag();

                if (DescriptionColumnCssClass != null)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, DescriptionColumnCssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(attribute.Description);
                writer.RenderEndTag();

                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            writer.RenderEndTag();
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

        private Control GetEditControl(PropertyInfo property, ModulePropertyAttribute attribute)
        {
            string name = property.Name;
            if (PropertyValues.ContainsKey(name))
                attribute.DefaultValue = PropertyValues[name];

            PortalControl editControl = attribute.GetEditControl(property.PropertyType);
            editControl.ID = name;
            string value = Context.Request.Form[name];
            if (value != null)
            {
                ITextControl textControl = editControl as ITextControl;
                if (textControl != null)
                {
                    textControl.Text = value;
                    return editControl;
                }

                IListControl listControl = editControl as IListControl;
                if (listControl != null)
                {
                    ListItem item = listControl.Items.FindByValue(value);
                    if (item != null)
                        item.Selected = true;
                    return editControl;
                }
            }

            return editControl;
        }

        private static Label GetLabelControl(PropertyInfo property, ModulePropertyAttribute attribute)
        {
            Label label = new Label();
            label.Text = attribute.Name ?? property.Name.ToProperCase();
            label.ID = property.Name + "Label";
            label.AssociatedControlId = property.Name;
            return label;
        }
    }
}