#region Using Directives

using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

#endregion

namespace Loom.Web.UI
{
    public class ControlConverter<T> : StringConverter where T : Control
    {
        /// <summary>
        ///     Returns a collection of standard values for the data type this type converter
        ///     is designed for when provided with a format context.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="System.ComponentModel.ITypeDescriptorContext" /> that provides a format context
        ///     that can be used to extract additional information about the environment from
        ///     which this converter is invoked. This parameter or properties of this parameter
        ///     can be a <null />.
        /// </param>
        /// <returns>
        ///     A <see cref="System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid
        ///     values, or a <null /> if the data type does
        ///     not support a standard set of values.
        /// </returns>
        /// <remarks>Use this method to get the list of compatible list controls.</remarks>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context == null || context.Container == null)
                return null;

            string[] controls = GetControlIds(context);
            return controls != null ? new StandardValuesCollection(controls) : null;
        }

        private static string[] GetControlIds(ITypeDescriptorContext context)
        {
            ComponentCollection collection = context.Container.Components;
            List<string> internalList = new List<string>();
            string contextId = null;

            T control = context.Instance as T;
            if (null != control)
                if (!Compare.IsNullOrEmpty(control.ID))
                    contextId = control.ID;

            foreach (IComponent component in collection)
            {
                control = component as T;
                if (control == null)
                    continue;

                if (!Compare.IsNullOrEmpty(control.ID) && contextId != control.ID)
                    internalList.Add(string.Copy(control.ID));
            }
            internalList.Sort(Comparer<string>.Default);
            return internalList.ToArray();
        }

        /// <summary>
        ///     Returns whether the collection of standard values returned from GetStandardValues
        ///     is an exclusive list of possible values, using the specified context.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.
        /// </param>
        /// <returns>
        ///     <true /> if the <see cref="TypeConverter.StandardValuesCollection" /> returned
        ///     from <see cref="GetStandardValues" /> is an exhaustive list of possible values;
        ///     <false /> if other values are possible.
        /// </returns>
        /// <remarks>
        ///     <para>As implemented in this class, this method always returns <false />.</para>
        ///     <para>
        ///         If the list is exclusive, such as in an enumeration data type, then
        ///         no other values are valid. If the list is not exclusive, then other valid
        ///         values might exist in addition to the list of standard values that GetStandardValues
        ///         provides.
        ///     </para>
        ///     <note type="inheritinfo">
        ///         Override this method if the type you want to convert
        ///         supports standard values.
        ///     </note>
        ///     <para>
        ///         Use the <i>context</i> parameter to extract additional information about the environment
        ///         from which this converter is invoked. This parameter can be a <null />, so always
        ///         check it. Also, properties on the context object can return a <null />.
        ///     </para>
        /// </remarks>
        /// <platform>
        ///     <os predefined="all" />
        /// </platform>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        /// <summary>
        ///     Returns whether this object supports a standard set of values that can be picked
        ///     from a list, using the specified context.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="System.ComponentModel.ITypeDescriptorContext" /> that provides a
        ///     format context.
        /// </param>
        /// <returns>
        ///     <true />
        /// </returns>
        /// <remarks>
        ///     As implemented in this class, this method always returns <true />.  This ensures that the
        ///     list controls on the currnent Web Forms page are always listed.
        ///     <note type="inheritinfo">
        ///         Override this method if the type you want to convert
        ///         supports standard values.
        ///     </note>
        /// </remarks>
        /// <platform>
        ///     <os predefined="all" />
        /// </platform>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}