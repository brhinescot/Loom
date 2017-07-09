<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagementConsoleMiddle.aspx.cs" Inherits="Loom.Web.Tests.ManagementConsoleMiddle" %>

<%@ Register Assembly="Loom.Web" Namespace="Loom.Web.UI.WebControls"
TagPrefix="Colossus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <Colossus:BulletedListEx ID="content" runat="server" Sortable="true">
        </Colossus:BulletedListEx>
    </div>
</form>
</body>
</html>