<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Demo.Default" %>

<%@ Register Assembly="Loom.Web" Namespace="Loom.Web.UI.WebControls"
TagPrefix="Colossus" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html>
<head>
    <title>Relational ListControls Demo</title>
</head>
<body>
<form id="Form1" method="post" runat="server">
<asp:Panel ID="Panel1" runat="server" Width="744px" Font-Names="Tahoma,Arial,sans-serif"
           BackColor="OldLace">
<asp:Panel ID="DivLabel2" runat="server" ForeColor="White" Width="744px" Font-Size="Larger"
           Font-Names="Tahoma,Arial,sans-serif" BackColor="SteelBlue">
    &nbsp;Relational ListControls Demo
</asp:Panel>
<table id="table1" height="724" cellspacing="0" cellpadding="0" width="744" border="0">
<tr>
    <td width="19">
    </td>
    <td colspan="3">
    </td>
    <td>
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="226">
        &nbsp;
    </td>
    <td width="236">
        &nbsp;
    </td>
    <td>
        &nbsp;
    </td>
    <td>
        &nbsp;&nbsp;
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="226">
        <asp:Label ID="Label2" runat="server" Width="160px" Font-Size="Smaller" Font-Names="Tahoma">Parent List</asp:Label>
    </td>
    <td width="236">
        <asp:Label ID="Label3" runat="server" Font-Size="Smaller" Font-Names="Tahoma">Child List (KeepFirstItem = False)</asp:Label>
    </td>
    <td>
        <asp:Label ID="Label4" runat="server" Font-Size="Smaller" Font-Names="Tahoma">GrandChild1List (KeepFirstItem = true)</asp:Label>
    </td>
    <td>
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19" height="16">
        &nbsp;
    </td>
    <td width="226" height="16">
        <Colossus:RelationalDropDownList ID="ParentList" runat="server" Width="200px" DataValueField="ID"
                                         DataTextField="Name" DataMember="Parent" DataSource="<%# relationalDataSet1 %>">
        </Colossus:RelationalDropDownList>
    </td>
    <td width="236" height="16">
        <Colossus:RelationalDropDownList ID="ChildList" runat="server" Width="200px" DataValueField="ID"
                                         DataTextField="Name" DataMember="Child" DataSource="<%# relationalDataSet1 %>"
                                         ParentListId="ParentList">
        </Colossus:RelationalDropDownList>
    </td>
    <td height="16">
        <Colossus:RelationalDropDownList ID="GrandChildList1" runat="server" Width="280px"
                                         DataValueField="ID" DataTextField="Name" DataMember="GrandChild" DataSource="<%# relationalDataSet1 %>"
                                         ParentListId="ChildList" KeepFirstItem="true">
        </Colossus:RelationalDropDownList>
    </td>
    <td height="16">
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="226">
        &nbsp;
    </td>
    <td width="236">
        &nbsp;
    </td>
    <td>
        &nbsp;
    </td>
    <td>
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="226">
        &nbsp;
    </td>
    <td width="236">
        &nbsp;
    </td>
    <td>
        <asp:Label ID="Label5" runat="server" Font-Size="Smaller" Font-Names="Tahoma">GrandChild2List (KeepFirstItem = true)</asp:Label>
    </td>
    <td>
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19" height="13">
        &nbsp;
    </td>
    <td width="226" height="13">
        &nbsp;
    </td>
    <td width="236" height="13">
        &nbsp;
    </td>
    <td height="13">
        <Colossus:RelationalDropDownList ID="GrandChildList2" runat="server" Width="280px"
                                         DataValueField="ID" DataTextField="Name" DataMember="GrandChild2" DataSource="<%# relationalDataSet1 %>"
                                         ParentListId="ChildList" KeepFirstItem="true">
        </Colossus:RelationalDropDownList>
    </td>
    <td height="13">
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="226">
        &nbsp;
    </td>
    <td width="236">
        &nbsp;
    </td>
    <td>
        &nbsp;
    </td>
    <td>
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="465" colspan="2">
        <Colossus:LabelEx ResourceKey="Label1" ID="Label1" runat="server" Width="408px" Font-Size="Smaller" Font-Names="Tahoma"></Colossus:LabelEx>
    </td>
    <td valign="top" align="right">
        <asp:Button ID="Button1" runat="server" Font-Names="Tahoma" Text="Post Form" OnClick="Button1_Click">
        </asp:Button>
    </td>
    <td valign="top" align="right">
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td width="465" colspan="2">
        &nbsp;
    </td>
    <td valign="top" align="right">
        &nbsp;
    </td>
    <td valign="top" align="right">
        &nbsp;
    </td>
</tr>
<tr>
    <td width="19">
    </td>
    <td width="465" colspan="2">
        &nbsp;
    </td>
    <td valign="top" align="right">
        &nbsp;
    </td>
    <td valign="top" align="right">
    </td>
</tr>
<tr>
    <td width="19" height="70">
    </td>
    <td valign="top" width="465" colspan="2" height="70">
        <p>
            <asp:Label ID="Label7" runat="server" Width="384px" Font-Size="Smaller" Font-Names="Tahoma"></asp:Label><br>
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Smaller" Font-Names="Tahoma"
                           NavigateUrl="/files/RelationalListControls.zip">
                Download Control
            </asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink2" runat="server" Font-Size="Smaller" Font-Names="Tahoma"
                           NavigateUrl="">
                Online Documentation
            </asp:HyperLink>
        </p>
    </td>
    <td valign="top" align="right" height="70">
        <asp:Image ID="Image1" runat="server" ImageUrl="RlcProperties.png"></asp:Image>
    </td>
    <td valign="top" align="right" height="70">
    </td>
</tr>
<tr>
    <td width="19">
    </td>
    <td valign="top" width="465" colspan="2">
        &nbsp;
    </td>
    <td valign="top" align="center">
        <asp:HyperLink ID="HyperLink4" runat="server" Font-Size="Smaller" Font-Names="Tahoma"
                       NavigateUrl="http://blogs.geekdojo.net/brian/category/220.aspx">
            Screenshot taken with Cropper
        </asp:HyperLink>&nbsp;
    </td>
    <td valign="top" align="right">
    </td>
</tr>
<tr>
    <td width="19">
    </td>
    <td colspan="3">
        <asp:HyperLink ID="HyperLink3" runat="server" Font-Size="Smaller" Font-Names="Tahoma"
                       NavigateUrl="/">
            < Main
        </asp:HyperLink>
    </td>
    <td valign="top" align="right">
    </td>
</tr>
<tr>
    <td width="19">
        &nbsp;
    </td>
    <td colspan="3">
        &nbsp;
    </td>
    <td valign="top" align="right">
        &nbsp;
    </td>
</tr>
</table>
</asp:Panel>
</form>
</body>
</html>