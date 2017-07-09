#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls.CMS
{
    [DefaultProperty("Layout")]
    [ToolboxData("<{0}:CmsEntityExplorer runat=server></{0}:CmsEntityExplorer>")]
    [ParseChildren(true)]
    public class CmsEntityExplorer : Panel
    {
        private const string CallbackDataParamName = "CallbackData";
        private const string CallbackMethodParamName = "Callback";
        private const string CallbackMethodShowDetail = "showdetail";
        private const string CallbackMethodShowList = "showlist";
        private const string CallbackMethodShowTree = "showtree";
        private const string CallbackMethodSubmitDetail = "submitdetail";
        private const string CallbackMethodSaveOrder = "saveOrder";

        private const string AllJavascript = @"
function onDetailLoaded(selectedItem){{}}
$(document).ready(function() {{
    $('.detailForm, .paneHeader, .paneSubHeader', '#detailPane').empty();
    $('ul', '#treePane').treeview({{ dblclickexpand: true }});
    $('.filetree li span').click(function() {{
        var callbackData = $(this).attr('id');
        var selectedItem = this;
        $('.filetree li span').removeClass('selected');
        $(this).addClass('selected');
        $('.detailForm, .paneHeader, .paneSubHeader', '#detailPane').empty();
        $('#listPane').load('{0}', {{ Callback: 'showlist', CallbackData: callbackData }}, function() {{
            $('#listPane .list li').click(function() {{
                $('#listPane .list li').removeClass('selected');
                $(this).addClass('selected');
                callbackData = $(this).attr('id');
                selectedItem = this;
                $('#detailPane').load('{0}', {{ Callback: 'showdetail', CallbackData: callbackData }}, function() {{
                    {1}
                }});
            }});
        }});
    }});
}});
";

        private const string TreeAndListJavascript = @"
function onDetailLoaded(selectedItem){{}}
$(document).ready(function() {{
    $('.detailForm, .paneHeader, .paneSubHeader', '#detailPane').empty();
    $('ul', '#treePane').treeview({{ dblclickexpand: true }});
    $('.filetree li span').click(function() {{
        var callbackData = $(this).attr('id');
        var selectedItem = this;
        $('.filetree li span').removeClass('selected');
        $(this).addClass('selected');
        $('#listPane').load('{0}', {{ Callback: 'showlist', CallbackData: callbackData }}, function(){{
            $('#listPane .list').sortable({{
                update: function(event, ui){{
                    var callbackData = '';
                    $('#listPane .list li').each(function(i, item){{
                        callbackData += $(item).attr('id') + ',';
                    }});
                    var data = {{}};
                    data['Callback'] = 'saveOrder';
                    data['CallbackData'] = callbackData;
                    $.post('{0}', data);
                }}
            }});
        }});
    }});
}});
";

        private const string ListJavascript = @"
function onDetailLoaded(selectedItem){{}}
$(document).ready(function() {{
    $('.detailForm, .paneHeader, .paneSubHeader', '#detailPane').empty();
    $('#listPane .list li').click(function() {{
        $('#listPane .list li').removeClass('selected');
        $(this).addClass('selected');
        var callbackData = $(this).attr('id');
        var selectedItem = this;
        $('#detailPane').load('{0}', {{ Callback: 'showdetail', CallbackData: callbackData }}, function() {{
            {1}
        }});
    }});
}});
";

        private const string TreeJavascript = @"
function onDetailLoaded(selectedItem){{}}
$(document).ready(function() {{
    $('.detailForm, .paneHeader, .paneSubHeader', '#detailPane').empty();
    $('ul', '#treePane').treeview({{ dblclickexpand: true }});
    $('.filetree li span').click(function() {{   
        var callbackData = $(this).attr('id');
        var selectedItem = this;
        $('.filetree li span').removeClass('selected');
        $(this).addClass('selected');
        $('#detailPane').load('{0}', {{ Callback: 'showdetail', CallbackData: callbackData }}, function() {{
            {1}
        }});
    }});
}});
";

        private const string DetailLoadedDefaultScript = @"
            onDetailLoaded(selectedItem);
            $('a.button span', '#detailPane .paneSubHeader').click(function() {{
                var data = {{}};
                $('*[name]', '#aspnetForm').each(function() {{
                    var t = $(this);
                    var val = (t.attr('type') == 'checkbox') ? (t.attr('checked') == true) ? 1 : 0 : t.val();
                    data[t.attr('name')] = val;
                }});
                data['Callback'] = 'submitdetail';
                data['CallbackData'] = callbackData;
                $.post('{0}', data, function(data) {{
                    var search = '.paneSubHeader span.positive, .paneSubHeader span.negative, .paneSubHeader span.feedback';
                    $(search).replaceWith($(search, $(data)));
                    $('.detailForm', '#detailPane').load('{0} .formContent', {{ Callback: 'showdetail', CallbackData: callbackData }}, function(){{
                        onDetailLoaded(selectedItem);
                    }});
                }});
            }});
";

        private const string DetailLoadedCustomScript = @"onDetailLoaded(selectedItem);";

        public static readonly object CallbackKey = new object();

        private EntityExplorerHeader detailSubHeader;
        private BulletedListEx list;
        private string submitText;
        private TreeView tree;

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public EntityExplorerItem DetailForm { get; } = new EntityExplorerItem();

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public EntityExplorerHeader DetailSubHeader
        {
            get
            {
                if (detailSubHeader == null)
                    detailSubHeader = new EntityExplorerHeader();
                return detailSubHeader;
            }
        }

        [DefaultValue(EntityExplorerLayout.All)]
        public EntityExplorerLayout Layout { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BulletedListEx List
        {
            get
            {
                if (list == null)
                    list = new BulletedListEx();
                return list;
            }
        }

        [DefaultValue("Save")]
        public string SubmitText
        {
            get
            {
                if (submitText == null)
                    return "Save";
                return submitText;
            }
            set => submitText = value;
        }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeView Tree
        {
            get
            {
                if (tree == null)
                    tree = new TreeView();
                return tree;
            }
        }

        private HttpRequest Request => Page.Request;

        private HttpResponse Response => Page.Response;

        public event EventHandler<CallbackEventArgs> Callback
        {
            add => Events.AddHandler(CallbackKey, value);
            remove => Events.RemoveHandler(CallbackKey, value);
        }

        protected void OnCallback(CallbackEventArgs e)
        {
            EventHandler<CallbackEventArgs> handler = (EventHandler<CallbackEventArgs>) Events[CallbackKey];
            if (handler != null)
                handler(this, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            HandleCallback();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (DesignMode)
                return;

            string pageName = Page.GetFileName();

            string detailLoadScript = detailSubHeader == null ? string.Format(DetailLoadedDefaultScript, pageName) : DetailLoadedCustomScript;

            switch (Layout)
            {
                case EntityExplorerLayout.All:
                    Page.ClientScript.RegisterStartupScript(typeof(CmsEntityView), "AllJavascript", string.Format(AllJavascript, pageName, detailLoadScript), true);
                    break;
                case EntityExplorerLayout.Tree:
                    Page.ClientScript.RegisterStartupScript(typeof(CmsEntityView), "TreeJavascript", string.Format(TreeJavascript, pageName, detailLoadScript), true);
                    break;
                case EntityExplorerLayout.List:
                    Page.ClientScript.RegisterStartupScript(typeof(CmsEntityView), "ListJavascript", string.Format(ListJavascript, pageName, detailLoadScript), true);
                    break;
                case EntityExplorerLayout.TreeAndList:
                    Page.ClientScript.RegisterStartupScript(typeof(CmsEntityView), "TreeListJavascript", string.Format(TreeAndListJavascript, pageName), true);
                    break;
                default:
                    Page.ClientScript.RegisterStartupScript(typeof(CmsEntityView), "AllJavascript", string.Format(AllJavascript, pageName, detailLoadScript), true);
                    break;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "explorer");
        }

        protected override void CreateChildControls()
        {
            if (Layout != EntityExplorerLayout.List)
                CreateTreePane();

            if (Layout != EntityExplorerLayout.Tree)
            {
                Control listControl = CreateListPane();
                string listId = Page.Request.QueryString["listId"];
                if (!Compare.IsNullOrEmpty(listId))
                    listControl.Controls.Add(CreateListPaneContent(new CallbackEventArgs("showdetail", listId)));
            }

            if (Layout != EntityExplorerLayout.TreeAndList)
            {
                Control detailControl = CreateDetailPane();
                string detailId = Page.Request.QueryString["detailId"];
                if (!Compare.IsNullOrEmpty(detailId))
                    detailControl.Controls.Add(CreateListPaneContent(new CallbackEventArgs("showdetail", detailId)));
            }
        }

        private Control CreateDetailPane()
        {
            HtmlGenericControl detailPane = new HtmlGenericControl("div");
            detailPane.Attributes.Add("id", "detailPane");
            detailPane.Controls.Add(CreateDetailPaneContent(null));

            Controls.Add(detailPane);
            Controls.Add(new LiteralControl("\r\n"));

            return detailPane;
        }

        private Control CreateDetailPaneContent(CallbackEventArgs args)
        {
            HtmlGenericControl detailPaneContent = new HtmlGenericControl("div");
            detailPaneContent.Attributes.Add("class", "content");

            HtmlGenericControl paneHeader = new HtmlGenericControl("div");
            paneHeader.Attributes.Add("class", "paneHeader");
            if (args != null && args.HeaderText != null)
                paneHeader.InnerText = args.HeaderText;
            detailPaneContent.Controls.Add(paneHeader);

            if (detailSubHeader == null)
                AddDefaultSubHeader(detailPaneContent, args);
            else
                detailPaneContent.Controls.Add(detailSubHeader);

            DetailForm.ID = "detailForm";
            detailPaneContent.Controls.Add(DetailForm);

            return detailPaneContent;
        }

        private void AddDefaultSubHeader(Control detailPaneContent, CallbackEventArgs args)
        {
            HtmlGenericControl paneSubHeader = new HtmlGenericControl("div");
            paneSubHeader.Attributes.Add("class", "paneSubHeader");
            detailPaneContent.Controls.Add(paneSubHeader);

            HtmlGenericControl feedback = new HtmlGenericControl("span");

            feedback.Attributes.Add("class", args != null && !Compare.IsNullOrEmpty(args.FeedbackText) ? (args.CallbackFailed ? "negative" : "positive") : "feedback");
            feedback.InnerText = args != null ? args.FeedbackText : null;

            paneSubHeader.Controls.Add(feedback);

            HtmlGenericControl formSubmit = new HtmlGenericControl("a");
            formSubmit.Attributes.Add("class", "button");
            HtmlGenericControl formSubmitText = new HtmlGenericControl("span");
            formSubmitText.InnerText = SubmitText;
            formSubmit.Controls.Add(formSubmitText);
            paneSubHeader.Controls.Add(formSubmit);
        }

        private Control CreateListPane()
        {
            HtmlGenericControl listPane = new HtmlGenericControl("div");
            listPane.Attributes.Add("id", "listPane");
            listPane.Controls.Add(CreateListPaneContent(null));

            Controls.Add(listPane);
            Controls.Add(new LiteralControl("\r\n"));

            return listPane;
        }

        private Control CreateListPaneContent(CallbackEventArgs args)
        {
            HtmlGenericControl listPaneContent = new HtmlGenericControl("div");
            listPaneContent.Attributes.Add("class", "content");

            HtmlGenericControl paneHeader = new HtmlGenericControl("div");
            paneHeader.Attributes.Add("class", "paneHeader");
            if (args != null && args.HeaderText != null)
                paneHeader.InnerText = args.HeaderText.Truncate(20, "...");
            listPaneContent.Controls.Add(paneHeader);

            HtmlGenericControl scroller = new HtmlGenericControl("div");
            scroller.Attributes.Add("class", "scroll");
            listPaneContent.Controls.Add(scroller);

            List.CssClass = "list";
            scroller.Controls.Add(List);

            return listPaneContent;
        }

        private void CreateTreePane()
        {
            HtmlGenericControl treePane = new HtmlGenericControl("div");
            treePane.Attributes.Add("id", "treePane");
            treePane.Controls.Add(CreateTreePaneContent(null));

            Controls.Add(treePane);
            Controls.Add(new LiteralControl("\r\n"));
        }

        private Control CreateTreePaneContent(CallbackEventArgs args)
        {
            HtmlGenericControl treePaneContent = new HtmlGenericControl("div");
            treePaneContent.Attributes.Add("class", "content");

            HtmlGenericControl paneHeader = new HtmlGenericControl("div");
            paneHeader.Attributes.Add("class", "paneHeader");
            if (args != null && args.HeaderText != null)
                paneHeader.InnerText = args.HeaderText;
            treePaneContent.Controls.Add(paneHeader);

            HtmlGenericControl scroller = new HtmlGenericControl("div");
            scroller.Attributes.Add("class", "scroll");
            treePaneContent.Controls.Add(scroller);

            Tree.CssClass = "filetree";
            scroller.Controls.Add(Tree);

            return treePaneContent;
        }

        private void HandleCallback()
        {
            string callbackMethod = Request.GetSafeParameter(CallbackMethodParamName);
            if (Compare.IsNullOrEmpty(callbackMethod))
                return;

            string callbackData = Request.GetSafeParameter(CallbackDataParamName);

            CallbackEventArgs args = new CallbackEventArgs(callbackMethod, callbackData);
            OnCallback(args);

            switch (callbackMethod)
            {
                case CallbackMethodShowTree:
                    if (Layout == EntityExplorerLayout.List)
                        throw new InvalidOperationException("Invalid callback method. Cannot show tree in list layout.");
                    AjaxUtility.WriteControl(CreateTreePaneContent(args));
                    break;
                case CallbackMethodShowList:
                    if (Layout == EntityExplorerLayout.Tree)
                        throw new InvalidOperationException("Invalid callback method. Cannot show list in tree layout.");
                    AjaxUtility.WriteControl(CreateListPaneContent(args));
                    break;
                case CallbackMethodShowDetail:
                    if (Layout == EntityExplorerLayout.TreeAndList)
                        throw new InvalidOperationException("Invalid callback method. Cannot show detail in tree and list layout.");
                    AjaxUtility.WriteControl(CreateDetailPaneContent(args));
                    break;
                case CallbackMethodSubmitDetail:
                    AjaxUtility.WriteControl(CreateDetailPaneContent(args));
                    break;
                case CallbackMethodSaveOrder:
                    return;
            }

            Response.StatusCode = 500;
            Response.Write("Invalid Callback Method");
            Response.Complete();
        }
    }

    [ToolboxItem(false)]
    [ParseChildren(false)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class EntityExplorerItem : Panel
    {
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "detailForm");
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formContent");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.RenderContents(writer);
            writer.RenderEndTag();
        }
    }

    [ToolboxItem(false)]
    [ParseChildren(false)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class EntityExplorerHeader : Panel
    {
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "paneSubHeader");
        }
    }
}