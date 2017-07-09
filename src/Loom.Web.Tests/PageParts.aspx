<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageParts.aspx.cs" Inherits="Loom.Web.Tests.PageParts" %>

<%@ Register Assembly="Loom.Web" Namespace="Loom.Web.UI.PageParts"
TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Files/PageParts.css" rel="stylesheet" type="text/css"/>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <cc1:PagePartContainer id="PageContainer1" runat="server" ColumnLayout="TwoColumns"></cc1:PagePartContainer>
    </div>
</form>
</body>
</html>