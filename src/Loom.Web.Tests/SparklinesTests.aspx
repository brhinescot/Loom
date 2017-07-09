<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SparklinesTests.aspx.cs" Inherits="Loom.Web.Tests.SparklinesTests" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <image src="sparkline.ashx?type=smooth&d=86,66,82,44,64,66,88,96,26,14,0,0,26,8,6,24,52,36,6,10,30&height=50&min-color=red&max-color=blue&last-color=green&step=2&last-m=true&max-m=true&min-m=true"></image>
        <image src="sparkline.ashx?type=bars&d=1,10,8,3,5&width=100&scale=true"></image>
    </div>
</form>
</body>
</html>