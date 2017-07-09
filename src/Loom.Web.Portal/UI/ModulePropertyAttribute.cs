#region Using Directives

using System;
using System.IO;
using System.Web.Script.Serialization;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.UI
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ModulePropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object DefaultValue { get; set; }
        public string PropertyEditorTypeName { get; set; }
        public Type EditControlDataType { get; set; }
        public string EditControlData { get; set; }

        public PortalControl GetEditControl(Type propertyType)
        {
            ModulePropertyEditor propertyEditor = !Compare.IsNullOrEmpty(PropertyEditorTypeName)
                ? ModulePropertyEditor.CreateTypeEditor(PropertyEditorTypeName, EditControlData)
                : ModulePropertyEditor.CreateTypeEditor(propertyType);

            return propertyEditor.GetEditControl(DefaultValue);
        }
    }

    public abstract class ModulePropertyEditor
    {
        protected ModulePropertyEditor(string data, Type dataType)
        {
            Data = data;
            Context = PortalContext.Current;

            if (Compare.IsNullOrEmpty(data) || dataType == null)
                return;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            SettingsObject = serializer.Deserialize(data, dataType);
        }

        public string Data { get; }
        protected object SettingsObject { get; }
        public IPortalContext Context { get; set; }

        public abstract PortalControl GetEditControl(object defaultValue);

        public static ModulePropertyEditor CreateTypeEditor(string typeName, string data = null)
        {
            Type type = Type.GetType(typeName);
            return Activator.CreateInstance(type, data) as ModulePropertyEditor;
        }

        public static ModulePropertyEditor CreateTypeEditor(Type propertyType)
        {
            if (propertyType == typeof(string))
                return new StringEditor();
            if (propertyType == typeof(bool))
                return new BooleanEditor(null);
            return new StringEditor();
        }
    }

    public class StringEditor : ModulePropertyEditor
    {
        public StringEditor() : base(null, null) { }

        public override PortalControl GetEditControl(object defaultValue)
        {
            return new TextBox {Text = defaultValue == null ? null : defaultValue.ToString()};
        }
    }

    public class BooleanEditor : ModulePropertyEditor
    {
        public BooleanEditor(string data) : base(data, null) { }

        public override PortalControl GetEditControl(object defaultValue)
        {
            DropDownList list = new DropDownList();
            list.Items.Add(new ListItem {Text = "No", Value = "False"});
            list.Items.Add(new ListItem {Text = "Yes", Value = "True"});

            if (defaultValue != null)
            {
                ListItem item = list.Items.FindByValue(defaultValue.ToString());
                if (item != null)
                    item.Selected = true;
            }

            return list;
        }
    }

    public class FileList : ModulePropertyEditor
    {
        public FileList(string data) : base(data, typeof(Setting)) { }

        public override PortalControl GetEditControl(object defaultValue)
        {
            if (SettingsObject == null)
                return null;

            Setting settings = (Setting) SettingsObject;

            DropDownList list = new DropDownList();
            SearchOption searchOption = settings.IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            list.AddItem("Select", string.Empty);
            foreach (string file in Directory.GetFiles(Context.HttpContext.Server.MapPath(settings.Directory), settings.SearchPattern ?? "*.*", searchOption))
                list.AddItem(Path.GetFileName(file), file);

            if (defaultValue != null)
            {
                ListItem item = list.Items.FindByValue(defaultValue.ToString());
                if (item != null)
                    item.Selected = true;
            }

            return list;
        }

        #region Nested type: Setting

        internal class Setting
        {
            public string Directory { get; set; }
            public string SearchPattern { get; set; }
            public bool IncludeSubDirectories { get; set; }
        }

        #endregion
    }
}