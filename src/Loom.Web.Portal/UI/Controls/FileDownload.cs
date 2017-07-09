#region Using Directives

using System;
using System.ComponentModel;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [PersistChildren(false)]
    [ParseChildren(true)]
    [ToolboxData("<{0}:FileDownload runat=\"server\"></{0}:FileDownload>")]
    public class FileDownload : PortalControl
    {
        private object dataSource;

        public string DownloadUrl { get; set; }
        public string DataUrlField { get; set; }
        public bool AutoDownload { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(TextItem))]
        public ITemplate AutoDownloadMessageTemplate { get; set; }

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(TextItem))]
        public ITemplate DownloadMessageTemplate { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (AutoDownload)
                View.PortalScript.RegisterBodyScript(GetType(), DownloadUrl, "top.location='" + ResolveUrl(DownloadUrl) + "';");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!Compare.IsNullOrEmpty(Controls))
            {
                foreach (Control control in Controls)
                    control.RenderControl(writer);
                return;
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Href, ResolveUrl(DownloadUrl));
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write("Click Here");
            writer.RenderEndTag();
            writer.Write(AutoDownload ? " if your download does not begin automatically." : " to download your file.");
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            ITemplate messageTemplate;

            if (AutoDownload)
                messageTemplate = AutoDownloadMessageTemplate ?? DownloadMessageTemplate;
            else
                messageTemplate = DownloadMessageTemplate;

            if (messageTemplate == null)
                return;

            TextItem clientItem = new TextItem(dataSource);
            messageTemplate.InstantiateIn(clientItem);
            Controls.Add(clientItem);
            if (ViewData != null)
                clientItem.DataBind();
        }

        protected override void OnSetViewData(object data)
        {
            if (data == null)
                return;
            dataSource = data;

            string s = data as string;
            if (!Compare.IsNullOrEmpty(s))
            {
                DownloadUrl = s;
                return;
            }

            if (!Compare.IsNullOrEmpty(DataUrlField))
            {
                PropertyDescriptor url = TypeDescriptor.GetProperties(data)[DataUrlField];
                if (url == null)
                    throw new ArgumentException("The ViewData object does not contain a public property named '" + DataUrlField + "'.");

                DownloadUrl = url.GetValue(data) as string;
                return;
            }

            DownloadUrl = data.ToString();
        }
    }

    public class TextItem : Control, IDataItemContainer
    {
        private readonly object dataItem;

        internal TextItem(object entity)
        {
            dataItem = entity;
        }

        #region IDataItemContainer Members

        object IDataItemContainer.DataItem => dataItem;

        int IDataItemContainer.DataItemIndex => 0;

        int IDataItemContainer.DisplayIndex => 0;

        #endregion
    }
}