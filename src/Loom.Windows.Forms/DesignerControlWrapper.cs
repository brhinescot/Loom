#region Using Directives

using System;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class DesignerControlWrapper
    {
        private readonly Type designerControlType;

        private DesignerControl designerControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DesignerControlWrapper" /> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public DesignerControlWrapper(DesignerControl control)
        {
            designerControl = control;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DesignerControlWrapper" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DesignerControlWrapper(Type type)
        {
            designerControlType = type;
        }

        /// <summary>
        ///     Gets the control.
        /// </summary>
        /// <value>The control.</value>
        public DesignerControl Control
        {
            get
            {
                if (designerControl == null && designerControlType != null)
                    designerControl = (DesignerControl) Activator.CreateInstance(designerControlType);
                return designerControl;
            }
        }
    }
}