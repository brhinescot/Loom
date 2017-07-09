<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAdapterTest.aspx.cs" Inherits="Loom.Web.Tests.FormAdapterTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        UserId<br/><asp:TextBox ID="UserId" runat="server"></asp:TextBox> = int<br/><br/>
        Name<br/><asp:TextBox ID="Name" runat="server"></asp:TextBox> = string<br/><br/>
        Email<br/><asp:TextBox ID="Email" runat="server"></asp:TextBox> = string<br/><br/>
        Percent<br/><asp:TextBox ID="Percent" runat="server"></asp:TextBox> = float<br/><br/>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1Click"/>
    </div>
</form>
</body>
</html>