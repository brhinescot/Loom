<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JQueryControls.aspx.cs"
Inherits="Loom.Web.Tests.JQueryControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/contentslider_style.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
        #sortList {
            list-style-type: none;
            margin-bottom: 0pt;
            margin-left: 0pt;
            margin-right: 0pt;
            margin-top: 0pt;
            padding-bottom: 0pt;
            padding-left: 0pt;
            padding-right: 0pt;
            padding-top: 0pt;
            width: 60%;
        }

        #sortList li span {
            margin-left: -1.3em;
            position: absolute;
        }

        #sortList li {
            background-attachment: scroll;
            background-color: #e6e6e6;
            background-image: url(images/e6e6e6_40x100_textures_02_glass_75.png);
            background-position: 0pt 50%;
            background-repeat: repeat-x;
            border-bottom-color: #d3d3d3;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #d3d3d3;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #d3d3d3;
            border-right-style: solid;
            border-right-width: 1px;
            border-top-color: #d3d3d3;
            border-top-style: solid;
            border-top-width: 1px;
            color: #555555;
            cursor: default;
            font-family: Tahoma, Verdana, Arial, Sans-Serif;
            font-size: 1.4em;
            font-size: small;
            font-weight: normal;
            height: 18px;
            margin: 3px;
            outline-color: -moz-use-text-color;
            outline-style: none;
            outline-width: medium;
            padding: 0.4em;
            padding-left: .5em;
            width: 240px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div id="ajaxLoad">
    </div>
    <div>
        <loom:BulletedListEx ID="sortList" runat="server" AllowHtml="true" Sortable="true">
            <asp:ListItem Text="<b>Motion52</b> Cool, and < $1200." Value="1"/>
            <asp:ListItem Text="ULPTL" Value="2"/>
            <asp:ListItem Text="Motion25" Value="3"/>
            <asp:ListItem Text="Vent65" Value="4"/>
        </loom:BulletedListEx>
        <asp:Button ID="Button1" runat="server" Text="Button"/>
        <loom:Slider ID="Slider1" runat="server" SlideInterval="0">
            <loom:SliderItem runat="server" ID="SliderItem1" Title="Panel 1" ThumbnailUrl="~/images/tempphoto-1.jpg"
                             ResizeThumbnail="true" MaximumThumbnailSize="60,40" AltText="temp-thumb">
                <img src="images/tempphoto-1.jpg" alt="temp"/>
            </loom:SliderItem>
            <loom:SliderItem runat="server" ID="SliderItem2" Title="Panel 2" ThumbnailUrl="~/images/tempphoto-2thumb.jpg"
                             AltText="temp-thumb">
                <img src="images/tempphoto-2.jpg" alt="temp"/>
            </loom:SliderItem>
            <loom:SliderItem runat="server" ID="SliderItem3" Title="Panel 4" ThumbnailUrl="~/images/tempphoto-4thumb.jpg"
                             AltText="temp-thumb">
                <img src="images/tempphoto-4.jpg" alt="temp"/>
            </loom:SliderItem>
            <loom:SliderItem runat="server" ID="SliderItem4" Title="Panel 5" ThumbnailUrl="~/images/tempphoto-5thumb.jpg"
                             AltText="temp-thumb">
                <loom:ImageEx runat="server" ImageUrl="images/tempphoto-5.jpg" Resize="true" MaximumSize="100,100"/>
            </loom:SliderItem>
        </loom:Slider>
        <div style="clear: both;">
        </div>
        <loom:Slider ID="Slider2" runat="server" SlideInterval="0">
            <loom:SliderItem runat="server" ID="SliderItem5" Title="Panel 1" ThumbnailUrl="~/images/tempphoto-1thumb.jpg"
                             AltText="temp-thumb">
                <img src="images/tempphoto-1.jpg" alt="temp"/>
            </loom:SliderItem>
            <loom:SliderItem runat="server" ID="SliderItem6" Title="Panel 2" ThumbnailUrl="~/images/tempphoto-2thumb.jpg"
                             AltText="temp-thumb">
                <img src="images/tempphoto-2.jpg" alt="temp"/>
            </loom:SliderItem>
            <loom:SliderItem runat="server" ID="SliderItem7" Title="Panel 4" ThumbnailUrl="~/images/tempphoto-4thumb.jpg"
                             AltText="temp-thumb">
                <img src="images/tempphoto-4.jpg" alt="temp"/>
            </loom:SliderItem>
            <loom:SliderItem runat="server" ID="SliderItem8" Title="Panel 5" ThumbnailUrl="~/images/tempphoto-5thumb.jpg"
                             AltText="temp-thumb">
                <img src="images/tempphoto-5.jpg" alt="temp"/>
            </loom:SliderItem>
        </loom:Slider>
        <!-------------- ENTITY EXPLORER ------------------>
        <loom:CmsEntityExplorer ID="CmsEntityExplorer1" runat="server" Layout="Tree">
            <DetailForm>

            </DetailForm>
        </loom:CmsEntityExplorer>
    </div>

    <script src="js/ajaxSubmit.js" type="text/javascript"></script>

</form>
</body>
</html>