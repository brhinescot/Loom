<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccordianMenuTest.aspx.cs" Inherits="Loom.Web.Tests.AccordianMenuTest" %>
<%@ Register Assembly="Loom.Web" Namespace="Loom.Web.UI.WebControls" TagPrefix="Colossus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" media="screen"/>
    <!--[if lte IE 6]>
        <link href="css/styles_ie6.css" rel="stylesheet" type="text/css" media="screen"/>
    <![endif]-->
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div id="sidebar">
            <Colossus:AccordionMenu ID="sidebarMenu" runat="server"/>
            <span class="clear"></span>
        </div>
    </div>
</form>
</body>
</html>