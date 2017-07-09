#region Using Directives

using System.Runtime.InteropServices;

#endregion

namespace Loom.Web.UI.WebControls
{
    [StructLayout(LayoutKind.Auto)]
    internal struct EditorType
    {
        /// <summary>
        ///     The System.Web.UI.Design.ImageUrlEditor
        /// </summary>
        internal const string ImageUrl = "System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

        /// <summary>
        ///     The System.ComponentModel.Design.MultilineStringEditor
        /// </summary>
        internal const string MultiLineString = "System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
    }
}