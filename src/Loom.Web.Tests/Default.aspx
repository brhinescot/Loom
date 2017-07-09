<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Loom.Web.Tests.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body runat="server" id="body">
<loom:Body ID="Body1" runat="server" BodyID="body" NewBodyID="newbody" BodyCssClass="test"/>
<form id="form1" runat="server">
    <div style="font-family: Tahoma, Sans-Serif; font-size: 12px; line-height: 16px;">
        <loom:WaitScreen ID="WaitScreen1" runat="server" ForeColor="SteelBlue" Height="107px"
                         ImageUrl="~/Files/steelBlueIndicatorBig.gif" Text="Processing, please wait..."
                         Font-Names="Tahoma"/>
        <a href="ExFormatter.aspx?id=2&action=delete">Exception Formatter</a><br/>
        <a href="Download.aspx">Download</a><br/>
        <a href="PageParts.aspx">Page Parts</a><br/>
        <a href="Validators.aspx">Validators</a><br/>
        <a href="Controls.aspx">Controls</a><br/>
        <br/>
        <br/>
        <loom:ButtonEx ID="button21" AntiSpam="true" OnClick="Button1_Click" Text="Colossus Button" runat="server"/>
        <loom:TextBoxEx ID="TextBox21" AntiSpam="true" Text="Colossus Button" runat="server" ResourceKey="TextBox21"/>
        <br/>
        <br/>
        <loom:HyperLinkEx ID="hyperlink21" runat="server" NavigateUrl="mailto:brian.scott@colossusinteractive.com?subject=Company Info Request"
                          Obfuscate="true" Text="Colossus Hyperlink">
        </loom:HyperLinkEx>
    </div>
</form>
</body>
</html>