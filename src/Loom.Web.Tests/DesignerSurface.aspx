<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignerSurface.aspx.cs" Inherits="Loom.Web.Tests.DesignerSurface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div id="dropTarget" class="droppable" style="background-color: blue; height: 150px; width: 150px;">
            <p>Drop here</p>
        </div>
        <div id="dragTarget" class="draggable" style="background-color: Red; height: 100px; width: 100px;">
            <p>Drag me to my target</p>
        </div>
    </div>
</form>
</body>

<script type="text/javascript">
    $(document).ready(function() {
        $('.draggable').draggable();
        $('.droppable').draggable();
        $('.droppable').droppable({
            drop: function(event, ui) {
                $('.draggable').appendTo($('.droppable'));
            }
        });
    });
</script>
</html>