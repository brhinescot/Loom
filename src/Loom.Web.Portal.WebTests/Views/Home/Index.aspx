<%@ Page Language="C#" AutoEventWireup="false" Inherits="Loom.Web.Portal.UI.PortalView" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Portal Client Dev</title>
    <meta name="viewport" content="initial-scale=1.3, maximum-scale=1.3, user-scalable=no">
</head>
<body class="spa">
<nav class="navbar navbar-default navbar-fixed-top" role="navigation">
    <div class="navbar-header">
        <button type="button" class="navbar-toggle needsclick" data-toggle="collapse" data-target=".navbar-ex1-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="#">the Rephinery</a>
    </div>

    <div class="collapse navbar-collapse navbar-ex1-collapse">
        <ul class="nav navbar-nav">
            <li>
                <a href="#routetest" data-toggle="collapse" data-target=".navbar-collapse.in">Route Test</a>
            </li>
            <li>
                <a href="#edit" data-toggle="collapse" data-target=".navbar-collapse.in">Edit</a>
            </li>
            <li>
                <a href="#edit/save" data-toggle="collapse" data-target=".navbar-collapse.in">Save</a>
            </li>
            <li>
                <a href="#edit/archive" data-toggle="collapse" data-target=".navbar-collapse.in">Archive</a>
            </li>
            <li>
                <a href="#modal/add" data-is-modal="true" data-toggle="collapse" data-target=".navbar-collapse.in">Add</a>
            </li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle needsclick" data-toggle="dropdown">Sidebar<b class="caret"></b></a>
                <ul class="dropdown-menu context" data-loom-target="#sidebar">
                    <li>
                        <a href="#routeEditor" data-toggle="collapse" data-target=".navbar-collapse.in">Route Editor</a>
                    </li>
                    <li>
                        <a href="#forms/validation" data-toggle="collapse" data-target=".navbar-collapse.in">Form Validation</a>
                    </li>
                    <li>
                        <a href="#forms/serialize" data-toggle="collapse" data-target=".navbar-collapse.in">Form Serialization</a>
                    </li>
                    <li>
                        <a href="#binding" data-toggle="collapse" data-target=".navbar-collapse.in">Templates</a>
                    </li>
                </ul>
            </li>
        </ul>
        <ul class="nav navbar-nav navbar-right">
            <li class="dropdown">
                <a href="#" class="dropdown-toggle needsclick" data-toggle="dropdown">brian@therephinery.com<b class="caret"></b></a>
                <ul class="dropdown-menu">
                    <li>
                        <a href="#account"><i class="icon-user"></i> Account Details</a>
                    </li>
                    <li>
                        <a href="#account/preferences"><i class="icon-cog"></i> Preferences</a>
                    </li>
                    <li>
                        <a href="#account/inbox"><i class="icon-inbox"></i> Inbox</a>
                    </li>
                    <li class="divider"></li>
                    <li>
                        <a href="#account/signout"><i class="icon-off"></i> Sign Out</a>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
</nav>

<div class="scroller">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <section id="content">
                    <div class="row">
                        <div id="mainContent" class="dynamic col-md-6"></div>
                        <div id="sidebar" class="dynamic col-md-6"></div>
                    </div>
                </section>
            </div>
        </div>
    </div>
</div>

<div id="modal">
    <div class="shade closer"></div>
    <div class="content"></div>
</div>

<div id="debug">
    <span class="screen-width">0</span>
</div>
</body>
</html>

<loom:StyleSheet runat="server" ID="Styles">
    <Externals>
        <loom:ExternalResource Path="~/content/bootstrap/bootstrap.css"/>
        <loom:ExternalResource Path="~/content/font-awesome.css"/>
        <loom:ExternalResource Path="~/content/style.css"/>
        <loom:ExternalResource Path="~/content/bootstrap-theme-square.css"/>
        <loom:ExternalResource Path="~/content/theme.css"/>
    </Externals>
</loom:StyleSheet>

<loom:Script runat="server" ID="Scripts">
    <Externals>
        <loom:ExternalResource Path="~/content/script/fastclick.js"/>
        <loom:ExternalResource Path="~/content/script/jquery.mousewheel.js"/>
        <loom:ExternalResource Path="~/content/script/jquery-duplicate.js"/>
        <loom:ExternalResource Path="~/content/script/bootstrap.js"/>
    </Externals>
    <ClientScript>
        $(window).resize(function () {
        showWindowSize();
        });

        function showWindowSize() {
        var windowWidth = $(window).width();
        $('.screen-width').text(windowWidth + 'px');
        }

        showWindowSize();
    </ClientScript>
    <DocumentReadyScript>
        FastClick.attach(document.body);

        p$.relativeAppPath = '/';
        p$.routes({
        default: {
        url: '{controller}/{action}/{id}'
        }
        });

        p$.controllers({
        edit: {
        save: function (customer) {

        },
        finalize: function () {

        },
        index: function () {

        }
        }
        });

        p$.views({});
    </DocumentReadyScript>
</loom:Script>