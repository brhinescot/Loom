#region Using Directives

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BulletedListEx runat=server></{0}:BulletedListEx>")]
    public class BulletedListEx : BulletedList
    {
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        [Browsable(true)]
        public bool Sortable
        {
            get
            {
                object obj = ViewState["Sortable"];
                if (obj != null)
                    return (bool) obj;
                return false;
            }
            set => ViewState["Sortable"] = value;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        [Browsable(true)]
        public bool AllowHtml
        {
            get
            {
                object obj = ViewState["AllowHtml"];
                if (obj != null)
                    return (bool) obj;
                return false;
            }
            set => ViewState["AllowHtml"] = value;
        }

        private string HiddenFieldName => "__" + ClientID + "_Order";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Page.IsPostBack && Sortable)
                LoadSortable();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Sortable)
                PreRenderSortable();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (AllowHtml)
                RenderWithHtml(writer);
            else
                base.Render(writer);
        }

        private void LoadSortable()
        {
            string[] order = Page.Request.Form[HiddenFieldName].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            if (order.Length <= 1)
                return;

            foreach (string itemValue in order)
            {
                ListItem item = Items.FindByValue(itemValue);
                Items.Remove(item);
                Items.Add(item);
            }
        }

        private void PreRenderSortable()
        {
            foreach (ListItem item in Items)
                item.Attributes.Add("id", item.Value);

            string makeSortableScript = @"$(function() {$('#" + ClientID + @"').sortable();});";
            Type localType = GetType();

            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.Core);
            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.UI);

            Page.ClientScript.RegisterHiddenField(HiddenFieldName, null);
            Page.ClientScript.RegisterOnSubmitStatement(localType, ClientID + "submitStatement", "$('#" + HiddenFieldName + "').val($('#" + ClientID + "').sortable('toArray'))");

            Page.ClientScript.RegisterStartupScript(localType, ClientID + "sortable", makeSortableScript, true);
        }

        private void RenderWithHtml(TextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(sw))
            {
                base.Render(htmlWriter);
                writer.Write(HttpUtility.HtmlDecode(sb.ToString()));
            }
        }
    }
}