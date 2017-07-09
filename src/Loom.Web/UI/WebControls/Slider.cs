#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Slider runat=\"server\"></{0}:Slider>")]
    [ParseChildren(true, "Items")]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class Slider : WebControl
    {
        private const string ManualSlideScript = @"
		var ${0}_navthumb;
		var {0}_curclicked = 0;
		
		{0}_theInterval = function(cur){{
			
			if( typeof cur != 'undefined' )
				{0}_curclicked = cur;
			
			${0}_navthumb.eq({0}_curclicked).parent().addClass('active-thumb');
				$('#{0} .stripNav ul li a').eq({0}_curclicked).trigger('click');
		}};
		
		$(function(){{
			
			$('#{0}-slider').codaSlider();
			
			${0}_navthumb = $('#{0} .nav-thumb');			
			${0}_navthumb.click(function() {{
				var $this = $(this);
				{0}_theInterval($this.parent().attr('href').slice(1) - 1);
				return false;
			}});
			
			{0}_theInterval();
		}});";

        private SliderItemCollection itemCollection;

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [MergableProperty(false)]
        public SliderItemCollection Items
        {
            get
            {
                if (itemCollection == null)
                    itemCollection = new SliderItemCollection();
                return itemCollection;
            }
        }

        [Browsable(true)]
        [Description("The title of the slider panel.")]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue(6000)]
        public int SlideInterval
        {
            get
            {
                object obj = ViewState["Title"];
                return obj == null ? 6000 : (int) obj;
            }
            set => ViewState["Title"] = value;
        }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            foreach (SliderItem item in Items)
                Controls.Add(item);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Items.Count == 0)
            {
                Visible = false;
                return;
            }

            EnsureChildControls();

            base.OnPreRender(e);

            if (DesignMode)
                return;

            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.Core);
            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.Easing);
            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.EasingCompatibility);
            Page.ClientScript.RegisterGlobalClientScriptResource(JQueryResourcePath.CodaSlider);

            Page.ClientScript.RegisterStartupScript(GetType(), ClientID + "_activationScript", string.Format(ManualSlideScript, ClientID), true);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "slider-wrap");
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "-slider");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "csw");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panelContainer");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.RenderContents(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();

            if (Items.Count <= 1)
                return;

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "movers-row");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            for (int i = 0; i < Items.Count; i++)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                RenderItem(writer, Items[i], i);
                writer.RenderEndTag();
            }

            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void RenderItem(HtmlTextWriter writer, SliderItem item, int index)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + (index + 1));

            // <a>
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            string thumbnailUrl = Items[index].ThumbnailUrl;
            if (!item.ResizeThumbnail)
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(thumbnailUrl));
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(ImageCacher.StoreImage(this, thumbnailUrl, item.MaximumThumbnailSize, 80)));
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "nav-thumb");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, Items[index].AltText);
            // <img>
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            // </a>
            writer.RenderEndTag();
        }
    }
}