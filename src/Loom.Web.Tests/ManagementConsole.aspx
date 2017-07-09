<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagementConsole.aspx.cs" Inherits="Loom.Web.Tests.ManagementConsole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/jquery.treeview.css" rel="stylesheet" type="text/css"/>
    <link href="css/screen.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
        #leftPane, #moviesSub { float: left; }

        #leftPane li { cursor: default; }

        #middlePane li {
            background-color: White;
            border: solid 1px #cecece;
            cursor: default;
            font-family: Verdana;
            font-size: 12px;
            height: 20px;
            list-style-type: none;
            padding: 4px 0 0 4px;
            width: 80px;
        }

        #middlePane { float: left; }

        #contentPane {
            float: left;
            font-family: Verdana;
            font-size: 12px;
            margin-left: 35px;
            margin-top: 16px;
            width: 350px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div>

        <loom:BulletedListEx ID="BulletedListEx1" runat="server" Sortable="true">
        </loom:BulletedListEx>
        <loom:TreeView ID="leftPane" runat="server" CssClass="filetree">
            <Nodes>
                <loom:TreeNode Text="Mounts" Value="1" CssClass="folder">
                    <loom:TreeNode Text="Cantilever" Value="1.1" CssClass="folder"/>
                    <loom:TreeNode Text="Motorized" Value="1.2" CssClass="folder"/>
                    <loom:TreeNode Text="Swivel" Value="1.3" CssClass="folder"/>
                </loom:TreeNode>
                <loom:TreeNode Text="Furniture" Value="2" CssClass="folder">
                    <loom:TreeNode Text="Stands" Value="2.1" CssClass="folder"/>
                    <loom:TreeNode Text="Floor" Value="2.2" CssClass="folder"/>
                    <loom:TreeNode Text="Wall" Value="2.3" CssClass="folder"/>
                </loom:TreeNode>
            </Nodes>
        </loom:TreeView>
        <loom:DropDownListEx runat="server">

        </loom:DropDownListEx>
        <div id="middlePane"></div>

        <div id="contentPane"></div>

    </div>
</form>
</body>
<script type="text/javascript">
    $(document).ready(function() {
        $("#leftPane").treeview({ dblclickexpand: true });
        $('#leftPane li span').click(function() {
            $('#middlePane').load('ManagementConsoleMiddle.aspx?id=' + $(this).attr('id') + ' #content',
                null,
                function() {
                    $('#middlePane #content').sortable();
                    $('#middlePane li').click(function() {
                        $('#contentPane')
                            .load('ManagementConsoleContent.aspx?content=' + $(this).attr('id') + ' #content');
                    });
                });
        });
    });
</script>
</html>