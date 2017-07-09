<%@ Page Language="C#" Inherits="Loom.Web.Portal.UI.PortalView" %>
<%@ Register Assembly="Loom.Web.Portal" Namespace="Loom.Web.Portal.UI.Controls" TagPrefix="loom" %>
<?xml version="1.0" encoding="UTF-8"?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head runat="server">
    <title></title>
</head>
<body>
<div id="wrapper">
    <loom:Box runat="server" id="pageHeader"></loom:Box>
    <loom:Box runat="server" id="menu"></loom:Box>
    <div style="clear: both;"></div>
    <loom:Box runat="server" id="middlePart"></loom:Box>
</div>
</body>
</html>