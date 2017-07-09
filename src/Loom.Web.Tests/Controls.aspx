<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Loom.Web.Tests.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon"/>
    <link href="css/styles.css" rel="stylesheet" type="text/css" media="screen"/>
    <!--[if lte IE 6]>
        <link href="css/styles_ie6.css" rel="stylesheet" type="text/css" media="screen"/>
    <![endif]-->
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div>
            <loom:ButtonEx ID="ButtonEx2" runat="server" Text="2" OnClick="ButtonEx2_Click"/>
            <loom:ButtonEx ID="ButtonEx1" runat="server" Text="1" OnClick="ButtonEx1_Click"/>
            <loom:TextBoxEx ID="TextBoxEx1" runat="server" EnterClickButtonId="ButtonEx1"></loom:TextBoxEx>
        </div>
        <br/>
        <br/>
        <div>
            ImageEx
            <loom:ImageEx ID="ImageEx1" runat="server" AlternateText="Snake Eyes Thumbnailed"
                          ImageUrl="~/Files/snakeeyes.jpg" Resize="true" MaximumSize="300,300" Quality="75"
                          CacheDuration="1"/>
            HyperLinkEx
            <loom:HyperLinkEx ID="ImageEx2" runat="server" Text="Snake Eyes Thumbnailed"
                              ImageUrl="~/Files/snakeeyes.jpg" ResizeImage="true" MaximumThumbnailSize="300,300"
                              ThumbnailQuality="75" ThumbnailCacheDuration="1"/>
        </div>
        <br/>
        <br/>
        <div>
            <loom:RepeaterEx runat="server" ID="RepeaterEx1" RepeatColumns="3">
                <ItemTemplate>
                    <loom:RadioButtonEx ID="RadioButtonEx1" runat="server" GroupName="test" ForceGroup="true"/>
                    Name = <%# Eval("Name") %>
                </ItemTemplate>
                <SeparatorTemplate>
                    &nbsp;&nbsp;|&nbsp;&nbsp;
                </SeparatorTemplate>
            </loom:RepeaterEx>
        </div>
        <br/>
        <br/>

        <div id="content">
            <loom:CmsContainer ID="cmsContainer" runat="server" Title="cmsContainer Test" SelectedTabIndex="1">
                <loom:TabItem runat="server" Text="Tab 0">
                    <p>
                        Tab0 <br/><br/><br/><br/>
                    </p>
                </loom:TabItem>
                <loom:TabItem runat="server" Text="Tab 1">
                    <p>
                        Tab1 <br/><br/><br/><br/>
                    </p>
                </loom:TabItem>
                <loom:TabItem runat="server" Text="Tab 2">
                    <p>
                        Tab2 <br/><br/><br/><br/>
                    </p>
                </loom:TabItem>
            </loom:CmsContainer>
            <loom:CmsEntityView ID="cmsEntityView1" runat="server" Title="CmsEntityView Test">
                <ItemTemplate>
                    <label>
                        Small Input:
                    </label>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Description") %>' CssClass="small"></asp:TextBox>
                    <p>
                        This is the ItemTemplate. The data bound name is
                        <%# Eval("Name") %>
                    </p>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSource='<%# Bind("Things") %>'
                                      CssClass="small">
                    </asp:DropDownList>
                </ItemTemplate>
            </loom:CmsEntityView>
            <loom:EntityView ID="entityView1" runat="server" style="border: solid 1px #cecece; padding: 10px;">
                <ItemTemplate>
                    <p style="clear: both; padding-bottom: 10px; padding-top: 10px;">
                        EntityView Test
                    </p>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
                    <p style="clear: both; padding-bottom: 10px; padding-top: 10px;">
                        This is the ItemTemplate. The data bound name is
                        <%# Eval("Name") %>
                    </p>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSource='<%# Eval("Things") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
            </loom:EntityView>
        </div>
        <loom:TreeView ID="TreeView1" runat="server">
        </loom:TreeView>
        <loom:BulletedListEx ID="BulletedListEx1" runat="server" Sortable="true">
        </loom:BulletedListEx>
    </div>
</form>
</body>
</html>