<%@ Control Language="C#" AutoEventWireup="false" Inherits="Loom.Web.Portal.UI.PortalMasterView" %>
<!doctype html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8"/>
    <title></title>
    <meta name="description" content=""/>
    <meta name="keywords" content=""/>
    <meta name="author" content=""/>
    <meta name="viewport" content="width=device-width; initial-scale=1.0"/>
    <link href="http://localhost/PortalTest/style.css" rel="stylesheet" type="text/css"/>
    <!-- !CSS
    <link href="css/html5reset.css" rel="stylesheet"/>
    <link href="css/style.css" rel="stylesheet"/>
    -->

    <!-- !Modernizr - All other JS at bottom
    <script src="js/modernizr-1.5.min.js"></script>
    -->

    <!-- Grab Microsoft's or Google's CDN'd jQuery. fall back to local if necessary -->
    <!-- <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js" type="text/javascript"></script> -->
    <!-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script> -->
    <!--
    <script>
            !window.jQuery && document.write('<script src="js/jquery-1.4.2.min.js"><\/script>')
        </script>
    -->
</head>
<body>
<div id="container">
    <loom:Box runat="server" ID="headerBox"/>
    <loom:Box runat="server" ID="mainBox"/>
    <loom:Box runat="server" ID="footerBox"/>
</div>
</body>
</html>