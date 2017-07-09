﻿<%@ Control Language="C#" AutoEventWireup="false" Inherits="Loom.Web.Portal.UI.PortalMasterView" %>
<!doctype html>
<!--[if lt IE 7 ]> <html class="no-js ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]>    <html class="no-js ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]>    <html class="no-js ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--> <html class="no-js" lang="en"> <!--<![endif]-->
    <head id="Head1" runat="server">
        <meta charset="utf-8" />

        <title></title>
        <meta name="description" content="" />
        <meta name="author" content="" />
        <meta name="keywords" content="" />

        <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    </head>
    <body>
        <div id="container">
            <header>

            </header>

            <loom:Box runat="server"></loom:Box>

            <footer>

            </footer>
        </div>
        <loom:Script runat="server">
            <Externals>
                <loom:ExternalResource Path="~/scriptresource/modernizr.js" HeadScript="true" />
            </Externals>
        </loom:Script>
    </body>
</html>