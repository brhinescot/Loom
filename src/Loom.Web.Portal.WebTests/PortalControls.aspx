<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="Loom.Web.Portal.WebTest.PortalControls" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
<form action="#" method="post">
    <div>
        <loom:FileDownload runat="server" ID="downloader" ViewDataKey="DownloadData" DataUrlField="Url" AutoDownload="true">
            <DownloadMessageTemplate>
                <div style="background-color: #eee; border: 1px solid black; height: 100px; margin: 10px; padding: 0 10px; width: 800px;">
                    <h4>Thank you for downloading</h4>
                    Please <a href="<%# Eval("Url") %>">click here to download</a> the file.
                </div>
            </DownloadMessageTemplate>
            <AutoDownloadMessageTemplate>
                <div style="background-color: #eee; border: 1px solid black; height: 100px; margin: 10px; padding: 0 10px; width: 800px;">
                    <h4>Thank you for downloading</h4>
                    Your download should begin automatically. If it does not you may
                    <a href="<%# Eval("Url") %>"><%# Eval("Message") %></a>.
                </div>
            </AutoDownloadMessageTemplate>
        </loom:FileDownload>

        <loom:ListBox runat="server" ID="bindTest" GroupByField="Name" ViewDataKey="RepeaterData" DataTextField="Name" DataValueField="Id" DataTextFormatString="{0}'s Item"/>

        <loom:ListBox runat="server" ID="select" CssClass="css" Multiple="true" Name="selectName" Rows="2">
            <loom:ListItem Selected="true" Text="Hi!"></loom:ListItem>
            <loom:ListItem Selected="true" Text="How are you?"></loom:ListItem>
            <loom:ListItem>I am fine, thanks.</loom:ListItem>
        </loom:ListBox>

        <loom:DropDownList runat="server" CssClass="css" Style="display: block;" Name="selectName">
            <loom:ListItem Selected="true" Text="Hi!"></loom:ListItem>
            <loom:ListItem Style="color: red; font-weight: bold;" Label="HEY?">How are you?</loom:ListItem>
        </loom:DropDownList>


        <loom:DropDownList ID="enumList" ViewDataKey="EnumList" runat="server" CssClass="css" Style="display: block;">
        </loom:DropDownList>
        <%--        <loom:TextBox runat="server" ID="textbox" Name="textbox"><script type="text/javascript">TextBox</script></loom:TextBox> <br />--%>

        <loom:Literal runat="server">
            <script type="text/javascript">
                alert('Hello, I am encoded!');
            </script>
        </loom:Literal>
        <br/>

        <%--        <loom:Literal runat="server" DisableEncoding="true"><script type="text/javascript">alert('Hello, I am not encoded!');</script></loom:Literal> <br />--%>
        <loom:Hyperlink runat="server" NavigateUrl="http://www.google.com">Google</loom:Hyperlink>
        <br/>

        <loom:CheckBox runat="server" ID="checkbox">CheckBox</loom:CheckBox>
        <br/>

        <loom:RadioButton runat="server" ID="radiobutton">RadioButton</loom:RadioButton>
        <br/>

        <loom:TextBox runat="server" ID="dfjgvfdsfsddjfd" AutoCompleteType="FirstName"></loom:TextBox>

        <p>
            <label>Duplicator</label>
            <input id="duplicator" type="text"/>
        </p>

        <br/>
        <br/>
        <loom:Repeater runat="server" ID="Repeater" ViewDataKey="RepeaterData" GroupBy="Name">
            <HeaderTemplate>
                <div style="font-weight: bold;">
                    This is the header.
                    <hr/>
                </div>
            </HeaderTemplate>
            <GroupHeaderTemplate>
                <div style="background-color: #8cf0b6; margin-bottom: 8px;">
                <b style="background-color: black; color: red; display: block;">This is <%# Eval("Name") %>'s group!</b>
            </GroupHeaderTemplate>
            <AlternatingGroupHeaderTemplate>
                <div style="background-color: #ADD8E6; margin-bottom: 8px;">
                <b style="background-color: black; color: white; display: block;">This is <%# Eval("Name") %>'s group!</b>
            </AlternatingGroupHeaderTemplate>
            <ItemTemplate>
                <div>
                    This is <%# Eval("Name") %>'s item!
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div style="color: red;">
                    This is <%# Eval("Name") %>'s alternating item!
                </div>
            </AlternatingItemTemplate>
            <GroupFooterTemplate>
                </div>
            </GroupFooterTemplate>
            <AlternatingGroupFooterTemplate>
                </div>
            </AlternatingGroupFooterTemplate>
            <FooterTemplate>
                <div style="font-weight: bold;">
                    <hr/>
                    This is the footer.
                </div>
            </FooterTemplate>
            <EmptyTemplate>
                <div>
                    There are no items.
                </div>
            </EmptyTemplate>
        </loom:Repeater>

        <br/>
        File Gallery
        <br/>
        <loom:FileGallery runat="server" SourceDirectory="~/content/images" IncludeSubDirectories="true" SearchPattern="*.png">
            <ItemTemplate>
                <img src='<%# Eval("RelativePath") %>' alt="">
                </img>
            </ItemTemplate>
        </loom:FileGallery>

        <loom:StyleSheet runat="server" ID="adas">
            <Embedded>
                body{background:<%= Request.QueryString["color"] %>;}
            </Embedded>
            <Externals>
                <loom:ExternalResource Path="~/asdfasd/dfdd.css"/>
            </Externals>
        </loom:StyleSheet>

        <loom:Script runat="server" ID="javaScriptTest">
            <Externals>
                <loom:ExternalResource Path="~/assets/js/dragdrop.js"/>
                <loom:ExternalResource Path="~/assets/js/dragdrop.js"/>
            </Externals>
            <ClientScript>
                alert('Hi!<%# ViewData.Name %>');
            </ClientScript>
            <DocumentReadyScript>
                $('#duplicator').autoDuplicate('blur', {template: $('#duplicator').closest('p')});
            </DocumentReadyScript>
        </loom:Script>
        <br/>
        <br/>

        <loom:Switch runat="server" DefaultCaseId="Case0" Expression='<%# Request.QueryString["uid"] %>'>
            <loom:Case ID="Case0" runat="server" Value="0">
                Value = 0 : <%# ViewData.Name %>
            </loom:Case>
            <loom:Case ID="Case1" runat="server" Value="1">
                Value = 1 : <%# ViewData.Name %>
            </loom:Case>
            <loom:Case ID="Case2" runat="server" Value="2">
                Value = 2 : <%# ViewData.Name %>
            </loom:Case>
        </loom:Switch>
    </div>
    <br/>
    <br/>
    <loom:PropertyGrid runat="server" ID="propertyGrid" CssClass="table" PropertyColumnCssClass="property">
        <ItemTemplate>
            <div>
                <%# Eval("Label") %><br/>
                <%# Eval("Input") %><br/>
                <%# Eval("Description") %><br/><br/>
            </div>
        </ItemTemplate>
    </loom:PropertyGrid>

    <loom:Label runat="server" ID="propertyValues"></loom:Label>
    <br/>
    <input type="submit" id="submit" name="submit" value="Submit"/>
</form>

<loom:Form runat="server" ViewDataKey="Form">
    <Template>
        hELLO wORLD!<%# Eval("Name") %>
    </Template>
</loom:Form>
</body>
</html>