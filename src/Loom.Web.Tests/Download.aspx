<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="Loom.Web.Tests.Download" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Button ID="Download1" runat="server" OnClick="Download1_Click" Text="Download"
                    Width="100px"/>&nbsp; 3 Zipped Files<br/>
        <br/>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Download"
                    Width="100px"/>&nbsp; 1 Zipped File<br/>
        <br/>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Download"
                    Width="100px"/>&nbsp; 1 Non-Zipped File<br/>
        <br/>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Download" Width="100px"/>&nbsp;
        1 Non-Zipped MP3<br/>
        <br/>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
</form>
</body>
</html>