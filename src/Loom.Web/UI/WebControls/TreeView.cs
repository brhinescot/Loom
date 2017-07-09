#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TreeView runat=server></{0}:TreeView>")]
    public class TreeView : WebControl
    {
        private TreeNode rootNode;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [MergableProperty(false)]
        [Editor("System.Web.UI.Design.WebControls.TreeNodeCollectionEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Description("TreeView_Nodes")]
        public TreeNodeCollection Nodes => RootNode.ChildNodes;

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Ul;

        internal TreeNode RootNode => rootNode ?? (rootNode = new TreeNode());

        protected override void RenderContents(HtmlTextWriter writer)
        {
            foreach (TreeNode node in Nodes)
                node.Render(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (DesignMode)
                return;

            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.Core);
            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.Treeview);
        }
    }

    public class TreeNodeCollection : Collection<TreeNode> { }
}