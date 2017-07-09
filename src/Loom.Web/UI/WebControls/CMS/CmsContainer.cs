#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls.CMS
{
    [ToolboxData("<{0}:CmsContainer runat=server></{0}:CmsContainer>")]
    [ParseChildren(true, "Tabs")]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class CmsContainer : WebControl, INamingContainer
    {
        [Bindable(true)]
        [DefaultValue("")]
        public string Title
        {
            get
            {
                object o = ViewState["Title"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["Title"] = value;
        }

        public int SelectedTabIndex
        {
            get
            {
                object o = ViewState["SelectedTabIndex"];
                return o == null ? 0 : Convert.ToInt32(o);
            }
            set => ViewState["SelectedTabIndex"] = value;
        }

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [MergableProperty(false)]
        public TabItemCollection Tabs => (TabItemCollection) Controls;

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override ControlCollection CreateControlCollection()
        {
            return new TabItemCollection(this);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.H2);
            writer.Write(Title);
            writer.RenderEndTag();

            int nonVisibleCount = 0;
            for (int i = 0; i < Tabs.Count; i++)
                if (!Tabs[i].Visible)
                    nonVisibleCount++;

            if (Tabs.Count - nonVisibleCount > 1)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "tabs");
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                for (int i = 0; i < Tabs.Count; i++)
                {
                    TabItem tabItem = Tabs[i];
                    bool renderTab = tabItem.Visible;
                    if (i == SelectedTabIndex)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "active");
                        tabItem.Visible = true;
                    }
                    else
                    {
                        tabItem.Visible = false;
                    }

                    if (!renderTab)
                        continue;

                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, tabItem.Title);
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, tabItem.NavigateUrl);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.WriteLine(tabItem.Text);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }

            base.RenderContents(writer);
        }
    }
}