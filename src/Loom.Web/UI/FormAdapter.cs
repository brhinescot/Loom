#region Using Directives

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loom.Dynamic;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI
{
    public sealed class FormAdapter<T>
    {
        private readonly List<string> metaFields;
        private readonly Control parentControl;
        private DynamicProperty<T>[] dynamicProperties;

        private Dictionary<string, string> mappings;

        public FormAdapter(Control parentControl)
        {
            this.parentControl = parentControl;
        }

        public FormAdapter(Control parentControl, ICollection<string> metaFieldNames)
        {
            this.parentControl = parentControl;
            if (metaFieldNames != null && metaFieldNames.Count > 0)
                metaFields = new List<string>(metaFieldNames);
        }

        private Dictionary<string, string> Mappings => mappings ?? (mappings = new Dictionary<string, string>());

        public void AddMapping(string propertyName, string formFieldName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));
            Argument.Assert.IsNotNull(formFieldName, nameof(formFieldName));

            Mappings.Add(propertyName, formFieldName);
        }

        public void FillForm(T entity)
        {
            Argument.Assert.IsNotNull(entity, nameof(entity));
            Argument.Assert.IsNotNull(parentControl, nameof(parentControl));

            CreateProperties();

            foreach (DynamicProperty<T> property in dynamicProperties)
            {
                Control child = parentControl.FindChild(property.Name);

                ITextControl textControl = child as ITextControl;
                if (textControl != null)
                {
                    textControl.Text = Convert.ToString(property.InvokeGetterOn(entity));
                    continue;
                }

                CheckBox check = child as CheckBox;
                if (check != null)
                {
                    check.Checked = Convert.ToBoolean(property.InvokeGetterOn(entity));
                    continue;
                }

                HiddenField hiddenField = child as HiddenField;
                if (hiddenField != null)
                    hiddenField.Value = Convert.ToString(property.InvokeGetterOn(entity));
            }

            IMetaContainer metaContainer = entity as IMetaContainer;
            if (metaContainer == null)
                return;

            foreach (MetaData metaData in metaContainer.GetMetaData())
            {
                Control child = parentControl.FindChild(metaData.Name);

                ITextControl textControl = child as ITextControl;
                if (textControl != null)
                {
                    textControl.Text = Convert.ToString(metaData.Value);
                    continue;
                }

                CheckBox check = child as CheckBox;
                if (check != null)
                {
                    check.Checked = Convert.ToBoolean(metaData.Value);
                    continue;
                }

                HiddenField hidden = child as HiddenField;
                if (hidden == null)
                    continue;

                hidden.Value = Convert.ToString(metaData.Value);
            }
        }

        public void FillEntity(T entity, bool partialPageRendering = false)
        {
            Argument.Assert.IsNotNull(entity, nameof(entity));
            Argument.Assert.IsNotNull(parentControl, nameof(parentControl));

            CreateProperties();

            for (int i = 0; i < dynamicProperties.Length; i++)
            {
                DynamicProperty<T> property = dynamicProperties[i];

                string controlValue = partialPageRendering ? parentControl.AjaxRequestChildValue(property.Name) : parentControl.RequestChildValue(property.Name);

                if (property.Type != typeof(string) && string.IsNullOrEmpty(controlValue))
                    continue;

                if (property.Type == typeof(bool) || property.Type == typeof(bool?))
                {
                    switch (controlValue.ToUpper())
                    {
                        case "FALSE":
                        case "0":
                            property.InvokeSetterOn(entity, false);
                            break;
                        case "TRUE":
                        case "1":
                            property.InvokeSetterOn(entity, true);
                            break;
                        default:
                            throw new InvalidOperationException("Unable to convert value of control '" + property.Name + "' to Boolean.");
                    }
                }
                else
                {
                    object changeType;
                    try
                    {
                        changeType = Convert.ChangeType(controlValue, property.Type);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Error converting the value '" + controlValue + "' to type " + property.Type.Name + ".");
                    }
                    property.InvokeSetterOn(entity, changeType);
                }
            }

            IMetaContainer metaContainer = entity as IMetaContainer;
            if (metaContainer == null)
                return;

            List<MetaData> metaDataList = new List<MetaData>(metaContainer.GetMetaData());

            if (metaFields != null && metaFields.Count > 0)
                foreach (string field in metaFields)
                {
                    string s = field;
                    if (metaDataList.Find(md => md.Name == s) != null)
                        continue;

                    string controlValue = partialPageRendering ? parentControl.AjaxRequestChildValue(field) : parentControl.RequestChildValue(field);

                    if (string.IsNullOrEmpty(controlValue))
                        continue;

                    metaContainer.SetMetaValue(field, controlValue);
                }

            foreach (MetaData metaData in metaDataList)
            {
                string controlValue = partialPageRendering ? parentControl.AjaxRequestChildValue(metaData.Name) : parentControl.RequestChildValue(metaData.Name);

                if (metaData.Type != typeof(string) && string.IsNullOrEmpty(controlValue))
                    continue;

                metaData.Value = Convert.ChangeType(controlValue, metaData.Type);
            }
        }

        private void CreateProperties()
        {
            if (dynamicProperties != null)
                return;

            dynamicProperties = DynamicType<T>.CreateDynamicProperties();
            foreach (DynamicProperty<T> property in dynamicProperties)
                if (Mappings.ContainsKey(property.Name))
                    property.Name = Mappings[property.Name];
        }
    }
}