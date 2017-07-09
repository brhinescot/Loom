#region Using Directives

using System;
using System.Reflection;
using System.Web.UI.Design;

#endregion

namespace Loom.Web.UI.Design
{
    public class ControlDesigner2 : ControlDesigner
    {
        private Type componentType;

        private Type ComponentType
        {
            get
            {
                if (componentType == null)
                    componentType = Component.GetType();
                return componentType;
            }
        }

        public T GetPropertyValue<T>(string propertyName)
        {
            PropertyInfo property = ComponentType.GetProperty(propertyName);
            return (T) property.GetValue(Component, null);
        }
    }
}